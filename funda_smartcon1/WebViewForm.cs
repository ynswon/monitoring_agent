    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;

namespace funda_smartcon1
{
    public partial class WebViewForm : Form
    {
        //public string url_first = "http://wepass.co.kr/pos/test.html";
        //public string url_first = "http://wepass.co.kr/pos/mobileSalesList.asp";
        //private String iniFilePath = Application.StartupPath + "\\config.ini";
        //public IniFile config = null;

        public String messageFromMobileSalesWebView = String.Empty;
        
        public WebViewForm(Object wp, String url)
        {
            InitializeComponent();
            textBoxSalesMessage.Visible = false;
            this.webBrowser1.Size = new System.Drawing.Size(1024, 612);
            this.ClientSize = new System.Drawing.Size(1020, 608);
            //wPos = wp;

            {
                int scrW = Screen.PrimaryScreen.WorkingArea.Width;
                int scrH = Screen.PrimaryScreen.WorkingArea.Height;
                int thisW = this.Width;
                int thisH = this.Height;
                int centerW = (scrW - thisW) / 2;
                int centerH = (scrH - thisH) / 2;
                this.Top = centerH;
                this.Left = centerW;
            }

            //this.Top = wp.Location.Y;
            //this.Left = wp.Location.X + 72;

            webBrowser1.ObjectForScripting = new ScriptInterface(this);

            //config = new IniFile(iniFilePath);
            webBrowser_LoadInitPage(url);

        }

        public void reload_webview()
        {
            webBrowser1.Refresh();
        }

        public void finish_form()
        {
            this.Close();
        }

        public void webBrowser_LoadInitPage(String url)
        {
            //Get방식 데이터 입력
            StringBuilder url_9f = new StringBuilder();
            url_9f.Append(url);

            webBrowser1.Navigate(url_9f.ToString());
            /*
            webBrowser1.DocumentText =
                      "<html><head><script>" +
                      "function test(message) { alert(message); }" +
                      "</script></head><body><button " +
                      "onclick=\"window.external.CloseForm()\">" +
                      "call client code from script code</button>" +
                      "</body></html>";*/
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //webBrowser1.Document.InvokeScript("test", new String[] { "called from client code" });
            
        }

        private void webBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            Owner.TopMost = true;
        }


        public void changeParentIcon()
        {
            /*
            if (wPos != null)
            {
                wPos.BackgroundImage = POSGui.Properties.Resources.we_icon;
                wPos.numberPositionChangeToRight();
            }
            */
        }

        //public void changeSalesNumber(int num)
        //{
        //    wPos.changeSalesNumber(num);
        //}

        //public void displayMessage(String msg)
        //{
        //    //this.web
        //    this.Width = 200;
        //    this.Height = 100;
        //    this.BackgroundImage = POSGui.Properties.Resources.background01;
        //    // sample
        //    msg = "까페라떼 1";
        //    textBoxSalesMessage.Visible = true;
            
        //    textBoxSalesMessage.Text = msg;
        //    webBrowser1.Visible = false;
            
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            //this.changeSalesNumber(1);
            this.changeParentIcon();
            this.Close();
        }

    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class ScriptInterface
    {
        public WebViewForm wb;

        public ScriptInterface(Form sender)
        {
            wb = (WebViewForm)sender;
        }

        public void CloseForm(String msg)
        {
            // Do something interesting 
            //wb.displayMessage(msg);
            wb.Close();
        }
        
    }

}
