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
            listView1.Columns[5].Width = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_txt_title.Text = "XPATH";
            xpath_textbox.Enabled = false;
            variable_textbox.Visible = false;
            variable_label.Visible = false;
            variable_textbox.Clear();
            switch (comboBox1.SelectedItem)
            {
                case "Click":
                    SelectedMacroType = MacroTypes.Click;
                    xpath_textbox.Enabled = true;
                    break;
                case "Write":
                    variable_textbox.Visible = true;
                    variable_label.Visible = true;
                    xpath_textbox.Enabled = true;
                    variable_label.Text = "Yazılacak Metin";
                    SelectedMacroType = MacroTypes.Write;
                    variable_textbox.Clear();
                    break;
                case "Wait":
                    variable_textbox.Visible = true;
                    variable_label.Visible = true;
                    variable_label.Text = "Beklenilecek Süre";
                    SelectedMacroType = MacroTypes.Wait;
                    break;
                case "Javascript":
                    variable_textbox.Visible = true;
                    variable_label.Visible = true;
                    variable_label.Text = "Çalıştırılacak JS";
                    SelectedMacroType = MacroTypes.Javascript;
                    variable_textbox.Clear();
                    break;
                case "Goto":
                    variable_textbox.Visible = true;
                    variable_label.Visible = true;
                    xpath_textbox.Enabled = true;
                    variable_label.Text = "Tekrar sayısı";
                    lbl_txt_title.Text = "Tekrar edecek makro konumu";
                    SelectedMacroType = MacroTypes.Goto;
                    break;
                case "Pause":
                    SelectedMacroType = MacroTypes.Pause;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (xpath_textbox.Enabled && string.IsNullOrEmpty(xpath_textbox.Text)) return;
            if (variable_textbox.Visible && string.IsNullOrEmpty(variable_textbox.Text)) return;

            ViewURL.macros.Add(new Macro(xpath_textbox.Text, variable_textbox.Text, SelectedMacroType));
            comboBox1.Text = "";
            xpath_textbox.Clear();
            variable_textbox.Clear();
            GetMacros();
            functions.GetMainForm().SomethingChanged = true;
        }
        

        public void GetMacros()
        {
            listView1.Items.Clear();
            foreach (var item in ViewURL.macros)
            {
                string[] row = { item.macroType.ToString(), MacroTypes.Goto != item.macroType ?  item.macroXPATH : "", (!string.IsNullOrEmpty(item.macroText) || !string.IsNullOrWhiteSpace(item.macroText) ? (MacroTypes.Goto != item.macroType ? item.macroText :""): item.macroWaitDelay.ToString()),item.macroText,item.macroType == MacroTypes.Goto ? item.macroXPATH.ToString() :"", item.macroID.ToString() };
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
            functions.GetMainForm().SomethingChanged = true;
        }
        public int MoveElementDown(int index)
        {
            if (index == ViewURL.macros.Count) return -1;
            var down_element = ViewURL.macros[index + 1];
            ViewURL.macros[index + 1] = ViewURL.macros[index];
            ViewURL.macros[index] = down_element;
            return index + 1;
            functions.GetMainForm().SomethingChanged = true;
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
            button5.Enabled = false;
            if (listView1.SelectedItems.Count <= 0) return;
            if (listView1.SelectedItems[0].Index != 0) button3.Enabled = true;
            if (listView1.SelectedItems[0].Index != listView1.Items.Count) button4.Enabled = true;
            button5.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int silinen_count = 0;
                foreach(ListViewItem item in listView1.SelectedItems)
                {
                    var item_index = listView1.Items.IndexOf(item)- silinen_count;
                    ViewURL.macros.RemoveAt(item_index);
                    silinen_count++;
                    
                }
                GetMacros();
            }
            functions.GetMainForm().SomethingChanged = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1) { 
            ViewURL.Duplicate(listView1.SelectedItems[0].Index);
            GetMacros();
            }
            else
            {
                List<int> index_list = new List<int>();
                foreach(ListViewItem item in listView1.SelectedItems)
                {
                    var i = listView1.Items.IndexOf(item);
                    index_list.Add(i);
                }
                ViewURL.Duplicate(index_list.ToArray());
                GetMacros();
            }
            functions.GetMainForm().SomethingChanged = true;
        }

        private void Macros_Load(object sender, EventArgs e)
        {
            
        }
    }
}
