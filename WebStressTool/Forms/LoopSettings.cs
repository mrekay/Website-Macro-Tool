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
    public partial class LoopSettings : Form
    {
        public LoopSettings()
        {
            InitializeComponent();
        }

        private void btn_tm_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.max_loop_limit = comboBox1.Text == "Yes" ? 2 : 0;
            Properties.Settings.Default.Save();

            if (!string.IsNullOrEmpty(Properties.Settings.Default.last_config_directory))
                functions.GetMainForm().SaveSettings(Properties.Settings.Default.last_config_directory);
            functions.GetMainForm().SomethingChanged = true;
        }

        private void LoopSettings_Load(object sender, EventArgs e)
        {
            comboBox1.Text = Properties.Settings.Default.max_loop_limit > 1 ? "Yes" : "No";
        }
    }
}
