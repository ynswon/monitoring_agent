using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net;
using System.Xml;
using Newtonsoft.Json;

namespace funda_agent
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Program.mPortMonitor.initPortListener();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Config_Load(object sender, EventArgs e)
        {

            this.ShowInTaskbar = false;
            textBoxStoreId.Text = Machine.ini.IniReadValue("STORE", "CODE");
            textBoxApiKey.Text = Machine.ini.IniReadValue("STORE", "APIKEY");
            comboBoxTrans.SelectedText = Machine.ini.IniReadValue("STORE", "LOGO_TRANS", "30%");
            timer1.Enabled = true;
            timer1.Interval = 1; 
        }

        private void comboBoxPosComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            ComboBox t = (ComboBox)sender;
            String str;
            str = t.SelectedItem.ToString();
            if (str.Contains("자동"))
            {
                Program.mPortMonitor.ini.IniWriteValue("SYSTEM", "POSPROGCOM", "0");
            }
            else
            {
                str = str.Substring(3);
                Program.mPortMonitor.ini.IniWriteValue("SYSTEM", "POSPROGCOM", str);
            }
             */
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //textBox1.Text = PortListener.display_sample_data;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("개발 모드로 전환하시겠습니까?", "Funda Agent",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RegistryHandler.SetModeAsDevelopment();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("기본 모드로 전환하시겠습니까?", "Funda Agent",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RegistryHandler.SetModeAsNormal();
            }
        }

        


        private void buttonValidationStoreId_Click(object sender, EventArgs e)
        {
             
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            /*
            Program.mPortMonitor.ini.IniWriteValue("STORE", "CODE", textBoxStoreId.Text);

            Program.mPortMonitor.ini.IniWriteValue("STORE", "LOGO_TRANS", comboBoxTrans.SelectedText);
            this.Visible = false;

            Program.mFundaReportShortcut.UpdateOpacity(comboBoxTrans.Text);
             */
        }

        private void tabPage0_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("설정을 초기화 하고 프로그램을 다시 시작하시겠습니까?", "초기화 여부",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Machine.ini.IniWriteValue("STORE", "CODE", "");
                Machine.ini.IniWriteValue("STORE", "CODE_PASSWORD", "");
                Machine.ini.IniWriteValue("STORE", "APIKEY", "");
                 
                Application.Restart();
            }
        }

        private void comboBoxTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RegistryHandler.SetDBFromWepass(textBoxWepassStoreID.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        { 
            RegistryHandler.SetDBFromFunda();
        }
    }
}
