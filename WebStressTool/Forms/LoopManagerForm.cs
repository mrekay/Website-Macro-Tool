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
    public partial class LoopManagerForm : Form
    {

        LoopManager loopMan;
        int myIndex;
        public LoopManagerForm(LoopManager _loopMan, int index)
        {
            InitializeComponent();
            loopMan = _loopMan;
            myIndex = index;
            GetVariables();
        }

        public void GetVariables()
        {
            listView1.Items.Clear();
            foreach (var item in loopMan.Loops[myIndex].variables)
            {
                string[] row = { item.Name, item.Value };
                ListViewItem listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);

            }
        }
    }
}
