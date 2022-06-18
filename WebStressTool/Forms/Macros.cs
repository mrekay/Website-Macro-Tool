using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebStressTool.Classes;
using WebStressTool.libs;

namespace WebStressTool.Forms
{
    public partial class Macros : Form
    {

        Url ViewURL;
        MacroTypes SelectedMacroType;

        public Macros(RegisteredSites _regSites, string unique)
        {
            InitializeComponent();

            ViewURL = _regSites.Get(unique);
            GetMacros();
            listView1.Columns[3].Width = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem)
            {
                case "Click":
                    variable_textbox.Visible = false;
                    variable_label.Visible = false;
                    SelectedMacroType = MacroTypes.Click;
                    variable_textbox.Clear();
                    break;
                case "Write":
                    variable_textbox.Visible = true;
                    variable_label.Text = "Yazılacak Metin";
                    variable_label.Visible = true;
                    SelectedMacroType = MacroTypes.Write;
                    variable_textbox.Clear();
                    break;
                case "Wait":
                    variable_textbox.Visible = true;
                    variable_label.Text = "Beklenilecek Süre";
                    variable_label.Visible = true;
                    SelectedMacroType = MacroTypes.Wait;
                    variable_textbox.Clear();
                    break;
                case "Javascript":
                    variable_textbox.Visible = true;
                    variable_label.Text = "Çalıştırılacak JS";
                    variable_label.Visible = true;
                    SelectedMacroType = MacroTypes.Javascript;
                    variable_textbox.Clear();
                    break;
                case "Pause":
                    variable_textbox.Visible = false;
                    variable_label.Visible = false;
                    SelectedMacroType = MacroTypes.Pause;
                    variable_textbox.Clear();
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewURL.macros.Add(new Macro(textBox1.Text, variable_textbox.Text, SelectedMacroType));
            textBox1.Clear();
            comboBox1.Text = "";
            variable_textbox.Clear();
            GetMacros();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            if (comboBox1.Text == "Javascript" || comboBox1.Text == "Wait" || comboBox1.Text == "Pause") { textBox1.Enabled = false; }
            else if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text)) { button1.Enabled = false; return; }

            if (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrWhiteSpace(comboBox1.Text)) { button1.Enabled = false; return; }
            if (comboBox1.Text != "Click")
            {
                if (comboBox1.Text != "Pause")
                {
                    if (string.IsNullOrEmpty(variable_textbox.Text) || string.IsNullOrWhiteSpace(variable_textbox.Text)) { button1.Enabled = false; return; }
                }
            }
            button1.Enabled = true;

        }

        public void GetMacros()
        {
            listView1.Items.Clear();
            foreach (var item in ViewURL.macros)
            {
                string[] row = { item.macroType.ToString(), item.macroXPATH, (!string.IsNullOrEmpty(item.macroText) || !string.IsNullOrWhiteSpace(item.macroText) ? item.macroText : item.macroWaitDelay.ToString()), item.macroID.ToString() };
                ListViewItem listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);
            }
        }

        public int MoveElementUp(int index)
        {
            if (index == 0) return -1;
            var up_element = ViewURL.macros[index - 1];
            ViewURL.macros[index - 1] = ViewURL.macros[index];
            ViewURL.macros[index] = up_element;
            return index - 1;
        }
        public int MoveElementDown(int index)
        {
            if (index == ViewURL.macros.Count) return -1;
            var down_element = ViewURL.macros[index + 1];
            ViewURL.macros[index + 1] = ViewURL.macros[index];
            ViewURL.macros[index] = down_element;
            return index + 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0) return;
            var newIndex = MoveElementUp(listView1.SelectedItems[0].Index);
            GetMacros();
            FocusItem(newIndex);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0) return;
            var newIndex = MoveElementDown(listView1.SelectedItems[0].Index);
            GetMacros();
            FocusItem(newIndex);
        }

        private void FocusItem(int newIndex)
        {
            if (newIndex > -1)
            {
                listView1.Focus();
                listView1.Items[newIndex].Focused = true;
                listView1.Items[newIndex].Selected = true;
                listView1.Items[newIndex].EnsureVisible();
                listView1.Select();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = false;
            if (listView1.SelectedItems.Count <= 0) return;
            if (listView1.SelectedItems[0].Index != 0) button3.Enabled = true;
            if (listView1.SelectedItems[0].Index != listView1.Items.Count) button4.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0) return;
            ViewURL.macros.RemoveAt(listView1.SelectedItems[0].Index);
            GetMacros();
        }
    }
}
