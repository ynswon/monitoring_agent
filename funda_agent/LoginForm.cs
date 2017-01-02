using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Web;
using System.Net;
using funda_interface;

namespace funda_agent
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private String temp_mode="0"; // store_code(id) for mode 0, 1
        private String temp_store_code; // store_code(id) for mode 0, 1
        private String temp_password; // password for mode 1
        private String temp_apikey; // apikey for mode 0
        private int mode;
        /// <summary>
        /// 최신 정보인지
        /// </summary>
        private bool _is_updated = false;
        public String GetOperationMode()
        {
            return temp_mode;
        }
        public String GetStoreCode()
        {
            return temp_store_code;
        }
        public String GetApiKey()
        {
            return temp_apikey;
        }
        public String GetPassword()
        {
            return temp_password;
        } 
        private void buttonQuery_Click(object sender, EventArgs e)
        {
            String temp_apikey = "";
            bool myresult = Program.my_funda.TryToLogin(
                FundaInterface.LoginMode.PureFundaAgent,
                textBoxID.Text,
                textBoxPassword.Text,
                ref temp_apikey);

            if (! myresult)
            {
                textBoxResult.Text = "암호가 틀렸습니다.";
                _is_updated = false;
                    
                temp_store_code = "";
                temp_password = "";
                temp_apikey = "";
            }
            else
            {
                temp_store_code = textBoxID.Text;
                textBoxResult.Text =
                    String.Format("암호가 일치합니다.\r\nAPI키 중 일부: {0}..\r\n", temp_apikey.Substring(0, 10));
                _is_updated = true;
                temp_mode = "1";
            } 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!_is_updated)
            {
                MessageBox.Show("조회 버튼을 클릭해 주세요.");
                return;
            }
            if (mode == 0)
            {
                if (temp_apikey == "")
                {
                    MessageBox.Show("암호가 불일치하므로 저장할 수 없습니다.");
                    return;
                }
                Machine.ini.IniWriteValue("OPERATION", "MODE", "0");

            }
            else if (mode == 1)
            {                
                Machine.ini.IniWriteValue("OPERATION", "MODE", "1");
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            mode = 1; 
            UpdateReset(); 
        }
        private void clearTextBox()
        {
            textBoxID.Text = "";
            textBoxPassword.Text = "";
            textBoxResult.Text = "";
            _is_updated = false;
        }
        private void radioButtonFundaAgent_CheckedChanged(object sender, EventArgs e)
        { 
                labelMode.Text = "Mode: SmartCon";
                
            clearTextBox();
            groupBoxAuth.Enabled = true;
        }

        private void UpdateReset()
        {
        }
        private void buttonRetry_Click(object sender, EventArgs e)
        {
            UpdateReset();
        }

        private void groupBoxAuth_Enter(object sender, EventArgs e)
        {

        }
         
    }
}
