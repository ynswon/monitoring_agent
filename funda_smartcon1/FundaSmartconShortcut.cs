using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using funda_interface;
namespace funda_smartcon1
{
    public partial class SmartconShortcut : Form
    {
        private FundaInterface my_funda;
        public SmartconShortcut()
        {
            my_funda = new FundaInterface();
            my_funda.Initilize(
                System.Environment.CurrentDirectory + @"\funda_interface"
                );
            my_funda.Start();

            InitializeComponent();
            this.Opacity = 1.0;
        }

        public void UpdateOpacity(String pp)
        {
            pp = pp.Replace("%","");
            if (pp == "없음")
            {
                this.Visible = false;
                return;
            }
            this.Visible = true;
            try
            {
                this.Opacity = double.Parse(pp) * 0.01;
            }
            catch(Exception ee)
            {
                this.Opacity = 0.3;
            }
        }
        private void FundaReportShortcut_Load(object sender, EventArgs e)
        { 
            Machine.ini = new IniFile(System.Environment.CurrentDirectory + @"\data_pos_agent.ini");
            while (true)
            {
                if (Machine.ini.IniReadValue("OPERATION", "MODE") == "1")  
                {

                    String temp_result_code = "";
                    String temp_company_id = "";
                    String temp_company_id_gc = "";
                    String temp_branch_name = "";
                    String msg = "";
                    if (!Machine.SmartconLoginApi(
                        Machine.ini.IniReadValue("STORE", "CODE"),
                        Machine.ini.IniReadValue("STORE", "PASSWORD"), ref msg,
                        ref temp_result_code, ref temp_company_id, ref temp_company_id_gc, ref temp_branch_name)
                    )
                    {
                        Machine.ini.IniWriteValue("SMARTCON", "companyCode", Machine.ini.IniReadValue("STORE", "CODE"));
                        Machine.ini.IniWriteValue("SMARTCON", "companyCodePassword", Machine.ini.IniReadValue("STORE", "PASSWORD"));
                        Machine.ini.IniWriteValue("SMARTCON", "amountCode", temp_company_id_gc);
                        Machine.ini.IniWriteValue("SMARTCON", "shopCode", temp_company_id);
                        Machine.ini.IniWriteValue("SMARTCON","shopName", temp_branch_name);

                        my_funda.TryToSmartconLogin(
                            Machine.ini.IniReadValue("STORE", "CODE"),
                            Machine.ini.IniReadValue("STORE", "PASSWORD"));
                        break;
                    }
                }


                LoginForm lf = new LoginForm();
                lf.ShowDialog(this);

                if (lf.DialogResult == DialogResult.OK)
                { 
                    Machine.ini.IniWriteValue("OPERATION", "MODE", "1");
                    Machine.ini.IniWriteValue("STORE", "CODE", lf.GetStoreCode());
                    Machine.ini.IniWriteValue("STORE", "PASSWORD", lf.GetPassword());
                    continue;
                }
                else
                {
                    Application.Exit();
                }
            }
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Size = new Size(68, 68);
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        { 
        }
        private Point startPos;
        private bool bDrag;
        private void FundaReportShortcut_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int diffX = e.X - startPos.X;
                int diffY = e.Y - startPos.Y;
                if (diffX * diffX + diffY * diffY > 5)
                {
                    this.Left += diffX;
                    this.Top += diffY;
                }

                if (diffX * diffX + diffY * diffY > 36)
                {
                    bDrag = true;
                }
            }
        }

        private void FundaReportShortcut_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && bDrag == false)
            {
                startPos.X = e.X;
                startPos.Y = e.Y;
            }
        }

        private void FundaReportShortcut_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && bDrag == true)
            {
                bDrag = false;
                //saveLocation(this.Location.X, this.Location.Y);
            }
        }

        private void FundaReportShortcut_DoubleClick(object sender, EventArgs e)
        {
            SmartConForm scf = new SmartConForm("","");
            scf.ShowDialog();
            /*
            Program.mFundaReport.Show();
            Program.mFundaReport.Nav(); */
        }

        private void FundaReportShortcut_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }
    }
}
