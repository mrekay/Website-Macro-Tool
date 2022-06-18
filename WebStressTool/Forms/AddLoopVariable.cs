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
    public partial class AddLoopVariable : Form
    {

        LoopManager _lManager;
        int LoopID;

        public AddLoopVariable(LoopManager loopManager,int _LoopID)
        {
            InitializeComponent();
            _lManager = loopManager;
            LoopID = _LoopID;

            getOtherVariableNames();
        }

        void getOtherVariableNames()
        {
            List<string> variableNames = new List<string>();
            foreach(var item in _lManager.Loops)
            {
                foreach(var variab in item.variables)
                {
                    if (!variableNames.Contains(variab.Name))
                    {
                        variableNames.Add(variab.Name);
                        cb_variable.Items.Add(variab.Name);
                    }
                }
            }

        }


        private void btn_tm_Click(object sender, EventArgs e)
        {
            _lManager.Loops[LoopID].variables.Add(new Variable() { Name = cb_variable.Text, Value = tb_value.Text });
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
