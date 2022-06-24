using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebStressTool.libs;

namespace WebStressTool.Forms
{
    public partial class ValueChanger : Form
    {
        public ValueChanger()
        {
            InitializeComponent();
        }

        private void btn_tm_Click(object sender, EventArgs e)
        {
            functions.GetMainForm().SomethingChanged = true;
        }
    }
}
