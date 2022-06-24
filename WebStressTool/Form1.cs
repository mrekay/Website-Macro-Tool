using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebStressTool.Classes;
using WebStressTool.Forms;

namespace WebStressTool
{
    public partial class Form1 : Form
    {

        public bool SomethingChanged = false;
        public Form1()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Properties.Settings.Default.last_config_directory))
                LoadSettings(Properties.Settings.Default.last_config_directory);

            GetRegisteredSites();
            listView1.Columns[1].Width = 0;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.StartsWith("https://") || textBox1.Text.StartsWith("http://"))
            {
                RegisterSite(textBox1.Text);
                textBox1.Clear();
                GetRegisteredSites();
            }
            else
            {
                MessageBox.Show("URL https:// ya da http:// ile başlamalıdır.", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        RegisteredSites registeredSites = new RegisteredSites();
        LoopManager LoopManager = new LoopManager();
        public void GetRegisteredSites()
        {
            listView1.Items.Clear();
            foreach (var item in registeredSites.Get())
            {
                string[] lItem = { item.href, item.unique.ToString() };
                var listViewItem = new ListViewItem(lItem);

                listView1.Items.Add(listViewItem);
            }
            RefreshButtons();
        }
        public void RegisterSite(string _url)
        {
            registeredSites.Add(_url);
            SomethingChanged = true;
        }
        public void RemoveSite()
        {
            registeredSites.Remove(listView1.SelectedItems[0].SubItems[1].Text);
            GetRegisteredSites();
            SomethingChanged = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0) return;
            RemoveSite();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0) return;
            var newIndex = registeredSites.MoveElementUp(listView1.SelectedItems[0].Index);
            GetRegisteredSites();
            FocusItem(newIndex);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0) return;
            var newIndex = registeredSites.MoveElementDown(listView1.SelectedItems[0].Index);
            GetRegisteredSites();
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
            RefreshButtons();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenMacroSettings();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Loops loops = new Loops(LoopManager);
            loops.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            var saveDialog = new SaveFileDialog();
            saveDialog.Filter = "XML files | *.xml";
            var result = saveDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SaveSettings(saveDialog.FileName);
                MessageBox.Show("Dışa Aktarma Başarılı", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void SaveSettings(string path)
        {
            var settings = new Settings();
            settings.loopManager = LoopManager;
            settings.registeredSites = SettingsConverter.ConvertToModel(registeredSites);
            settings.max_loop_limit = Properties.Settings.Default.max_loop_limit;
            XMLHelper.XmlSerialize(typeof(Settings), settings, path);
        }

        private void button8_Click(object sender, EventArgs e)
        {

            var opf = new OpenFileDialog();
            opf.Filter = "XML files | *.xml";
            var result = opf.ShowDialog();


            if (result == DialogResult.OK)
            {
                LoadSettings(opf.FileName);
                MessageBox.Show("Ayarları İçe Aktarma Başarılı", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);


                GetRegisteredSites();
            }
        }

        public void LoadSettings(string path)
        {
            var settings = XMLHelper.XmlDeserialize(typeof(Settings), path) as Settings;
            LoopManager = settings.loopManager;
            registeredSites = SettingsConverter.ConvertToClass(settings.registeredSites);
            Properties.Settings.Default.max_loop_limit = settings.max_loop_limit;
            Properties.Settings.Default.last_config_directory = path;
            Properties.Settings.Default.Save();
            Text = string.Format("Website Macro Tool ({0})", path);
        }

        OperationManager operationManager;
        private void btn_operation_start_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem((x) =>
            {
                if (Properties.Settings.Default.max_loop_limit > 1)
                {

                    int loopIndex = 0;

                    foreach (var loopItem in LoopManager.Loops)
                    {
                        var oMan = new OperationManager(registeredSites, LoopManager, loopIndex);
                        new Task(oMan.Start).Start();
                        loopIndex++;
                    }
                }
                else
                {
                    operationManager = new OperationManager(registeredSites, LoopManager, -1);
                    operationManager.Start();
                }
            });

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            OpenMacroSettings();
        }

        void OpenMacroSettings()
        {
            if (listView1.SelectedItems.Count <= 0) return;
            Macros macros = new Macros(registeredSites, listView1.SelectedItems[0].SubItems[1].Text);
            macros.ShowDialog();
        }

        void RefreshButtons()
        {
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            btn_edit.Enabled = false;
            btn_delete.Enabled = false;
            if (listView1.SelectedItems.Count <= 0) return;
            if (listView1.SelectedItems[0].Index != 0) button3.Enabled = true;
            if (listView1.SelectedItems[0].Index != listView1.Items.Count) button4.Enabled = true;
            button5.Enabled = true;
            btn_edit.Enabled = true;
            btn_delete.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            var valueChanger = new ValueChanger();
            valueChanger.textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            var dResult = valueChanger.ShowDialog();
            if (dResult == DialogResult.OK)
            {
                var newVal = valueChanger.textBox1.Text;
                registeredSites.Edit(listView1.SelectedItems[0].SubItems[1].Text, newVal);
            }
            GetRegisteredSites();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SomethingChanged)
            {
                if (MessageBox.Show("Ayarları Kaydetmek İster misiniz?", "Ayarları Kaydet", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.last_config_directory))
                    {
                        SaveSettings(Properties.Settings.Default.last_config_directory);
                    }
                    else
                    {
                        button7_Click(sender, e);
                    }
                }
            }
        }
    }
}
