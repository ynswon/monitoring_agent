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
namespace funda_smartcon1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private String temp_mode; // store_code(id) for mode 0, 1
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
             
            String msg = "";
            String temp_result_code = "";
            String temp_company_id = "";
            String temp_company_id_gc = "";
            String temp_branch_name = "";

            if (!Machine.SmartconLoginApi(textBoxID.Text, textBoxPassword.Text, ref msg,
                ref temp_result_code, ref temp_company_id, ref temp_company_id_gc, ref temp_branch_name)
            )
            {
                _is_updated = true;

                temp_store_code = textBoxID.Text;
                temp_password = textBoxPassword.Text;
                temp_apikey = "";
                temp_mode = "1";
            }
            else
            {
                _is_updated = false;
                temp_store_code = "";
                temp_password = "";
                temp_apikey = "";
            }
           textBoxResult.Text = msg; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!_is_updated)
            {
                MessageBox.Show("조회 버튼을 클릭해 주세요.");
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

            UpdateReset(); 
        }
        private void clearTextBox()
        {
            textBoxID.Text = "";
            textBoxPassword.Text = "";
            textBoxResult.Text = "";
            _is_updated = false;
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
