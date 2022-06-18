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
namespace WebStressTool.Forms
{
    public partial class Loops : Form
    {

        LoopManager loopMan;

        public Loops(LoopManager loopManager)
        {
            InitializeComponent();
            loopMan = loopManager;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var count = loopMan.Loops.Count.ToString();
            var loopDict = new Variable() { Name = "userData", Value = "Loop" + count };
            var loop = new Loop(); loop.variables.Add(loopDict);
            loopMan.Loops.Add(loop);
            GetLoops();
        }

        public void GetLoops()
        {
            CloseAllTabs();
            var loopCount = 0;
            foreach (var loop in loopMan.Loops)
            {
                TabPage tabPage = new TabPage() { Text = "Loop" + loopCount };
                LoopManagerForm loopManagerForm = new LoopManagerForm(loopMan, loopCount) { Dock = DockStyle.Fill, TopLevel = false };
                tabPage.Controls.Add(loopManagerForm);
                loopManagerForm.Show();
                tabControl1.TabPages.Add(tabPage);
                loopCount++;
            }
        }

        private void CloseAllTabs()
        {
            foreach (TabPage tabpage in tabControl1.TabPages)
            {
                var frm = tabpage.Controls[0] as Form;
                frm.Close();
            }
            tabControl1.TabPages.Clear();
        }

        private void Loops_Load(object sender, EventArgs e)
        {
            GetLoops();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var index = tabControl1.SelectedIndex;
            if (index < 0) return;

            var frm = tabControl1.TabPages[index].Controls[0] as Form;
            frm.Close();

            loopMan.Loops.RemoveAt(index);
            tabControl1.TabPages.RemoveAt(index);
            GetLoops();
        }

        private void btn_add_variable_Click(object sender, EventArgs e)
        {

            var index = tabControl1.SelectedIndex;
            if (index < 0) return;

            var frm = tabControl1.TabPages[index].Controls[0] as LoopManagerForm;
            AddLoopVariable alv = new AddLoopVariable(loopMan, index);
            var dResult = alv.ShowDialog();
            if (dResult == DialogResult.OK) frm.GetVariables();
        }

        private void btn_remove_variable_Click(object sender, EventArgs e)
        {
            var index = tabControl1.SelectedIndex;
            if (index < 0) return;

            var frm = tabControl1.TabPages[index].Controls[0] as LoopManagerForm;
            var selIndex = frm.listView1.SelectedItems[0].Index;
            if (selIndex <= 0) return;
            loopMan.Loops[index].variables.RemoveAt(selIndex);
            frm.GetVariables();
        }

        private void btn_settings_Click(object sender, EventArgs e)
        {
            LoopSettings lpSettings = new LoopSettings();
            lpSettings.ShowDialog();
        }
    }
}
