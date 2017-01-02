using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace funda_agent
{
    public partial class FundaReport : Form
    {
        public FundaReport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void Nav(int mode = 0)
        {/*
            String storeCode = Program.mPortMonitor.ini.IniReadValue("STORE", "CODE");
            String apiKey = Program.mPortMonitor.ini.IniReadValue("STORE", "APIKEY");
            storeCode = System.Web.HttpUtility.UrlEncode(storeCode);
            apiKey = System.Web.HttpUtility.UrlEncode(apiKey);

            if (mode == 0)
            {
                if (RegistryHandler.IsDBFromFunda())
                {
                    webBrowser1.Navigate("http://14.63.215.51:880/funda.php?date=&store_code=" + storeCode + "&api_key=" + apiKey);
                }
                else
                {
                    webBrowser1.Navigate("http://14.63.215.51:880/funda_wepass.php?date=&branch_id=" + RegistryHandler.GetWepassStoreID());
                }
            }
            else if (mode == 1)
            {
                if (RegistryHandler.IsDBFromFunda())
                {
                    webBrowser1.Navigate("http://14.63.215.51:880/view_fundareport/?mode=1&store=" + storeCode + "&api_key=" + apiKey);
                }
                else
                {
                    webBrowser1.Navigate("http://14.63.215.51:880/funda1_wepass?mode=1&branch_id=" + RegistryHandler.GetWepassStoreID());
                }
            }
            else if (mode == 2)
            {
                if (RegistryHandler.IsDBFromFunda())
                {
                    webBrowser1.Navigate("http://14.63.215.51:880/view_fundareport/?mode=2&store=" + storeCode + "&api_key=" + apiKey);
                }
                else
                {
                    webBrowser1.Navigate("http://14.63.215.51:880/view_fundareport/?mode=2&branch_id=" + RegistryHandler.GetWepassStoreID());

                }
            }
          */
        }
        private void FundaReport_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            updateCloseButtonPosition();
            Nav();
        }

        private void updateCloseButtonPosition()
        {
            buttonClose.Location = new Point( this.Size.Width-120,this.Size.Height-150);
        }
        private void FundaReport_SizeChanged(object sender, EventArgs e)
        {
            updateCloseButtonPosition();
        }

        private void buttonMenu1_Click(object sender, EventArgs e)
        {
            Nav(0);
        }

        private void buttonMenu2_Click(object sender, EventArgs e)
        {
            Nav(1);
        }

        private void buttonMenu3_Click(object sender, EventArgs e)
        {
            Nav(2);

        }

        private void buttonMenu4_Click(object sender, EventArgs e)
        {
            Program.mConfig.ShowDialog();
        }

        private void FundaReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

    }
}
