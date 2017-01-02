using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

using System.Net;
using System.IO;

namespace funda_smartcon1
{
    public partial class SmartConForm : Form
    {
        int gMode = 0;
        string gCode = "";
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        

        private bool bDrag;
        private Point startPos;

        public String gBranchId = "";
        public String gShopId = "";
        public string gBranchName = "";


        private String configFilePath = Application.StartupPath + "/config.ini";
        public String APIURL_EXCHANGE = "http://b2b.giftsmartcon.com";
        public String APIURL_AMOUNT = "http://smart.gifticard.com:8888";

        Dictionary<string, string> dictionary = new Dictionary<string, string>();


        bool gbConfirmed = false;

        String gMsgContentOld = "";

        public SmartConForm(string urlExchange, string urlAmount)
        {
            if (!urlExchange.Equals(""))
            {
                APIURL_EXCHANGE = urlExchange;
            }
            if (!urlAmount.Equals(""))
            {
                APIURL_AMOUNT = urlAmount;
            }

            InitializeComponent();
            //this.branchId = branchId;
            //this.posCode = posCode;
            this.KeyPreview = true;
            //couponNum.Text = "022608366418";
            //couponNum.Update();


            dictionary.Add("E0000", "이미 사용된 상품입니다.");
            dictionary.Add("E0001", "UBICLE 연동 프로토콜 버전이 일치하지 않습니다.");
            dictionary.Add("E0002", "UBICLE 에서 전송한 쿠폰 번호가 일치하지 않습니다.");
            dictionary.Add("E0003", "이 쿠폰은 본 매장에서 사용할 수 없습니다.");
            dictionary.Add("E0004", "쿠폰번호검증에 실패 했습니다. 다시 확인하시기 바랍니다.");
            dictionary.Add("E0005", "쿠폰 번호가 유효하지 않습니다.\n다시 확인하시기 바랍니다.");
            dictionary.Add("E0006", "이미 0000년 00월 00일 00시 00분에 0000지점에서 사용된 쿠폰입니다.");
            dictionary.Add("E0007", "사용 기간이 경과 되어 사용할 수 없는 쿠폰입니다.");
            dictionary.Add("E0008", "잘못된 데이터를 수신 했습니다. 다시 시도해 주시기 바랍니다.");
            dictionary.Add("E0009", "사용 가능한 상품입니다. \n치킨스노우퀸 1마리");
            dictionary.Add("E0010", "교환 취소된 쿠폰입니다.");
            dictionary.Add("E0011", "교환당일이 아니면 반품이 불가능합니다.");
            dictionary.Add("E0012", "수신번호가 일치하지 않습니다.");
            dictionary.Add("E0013", "헤더의 수신자번호처리 타입이 제휴사관련 동록 내용과  다릅니다.");
            dictionary.Add("E0100", "서버에서 에러가 발생했습니다.잠시 후 다시 시도해 주세요.");
            dictionary.Add("E0101", "해당하는 승인번호가 존재하지 않습니다.");
            dictionary.Add("E0102", "미 교환 상태여서 교환취소를 할 수 없습니다..");
            dictionary.Add("E0103", "해당 제휴사에서 교환되지 않았음으로 교환취소 할 수 없습니다.");
            dictionary.Add("E0104", "반품(교환취소)가능기간이 지나서 반품이 불가능합니다.");
            dictionary.Add("E0105", "망 취소가 정상 처리 되었습니다.");
            dictionary.Add("E0106", "입력 승인번호 오류입니다.");
            dictionary.Add("E0107", "교환처리 지점 코드가  일치하지 않습니다.");

        }

        private void setConfirmed(bool bConfirm)
        {
            gbConfirmed = bConfirm;

            if (gbConfirmed)
            {
                btnAccept.Enabled = true;
                btnCancel.Enabled = true;
            }
            else
            {
                btnAccept.Enabled = false;
                btnCancel.Enabled = false;
            }
        }

        private string postCode(string action, string code)
        {
            // 서버 API를 호출하고 결과를 받는다.
            HttpWebRequest httpWReq =
            (HttpWebRequest)WebRequest.Create("http://wepass.net/smartcon/" + action + ".php");

            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "code=" + code;
            //            postData += "&qpconBranchId="+branchId;
            //            postData += "&posId=" + posCode;
            byte[] data = encoding.GetBytes(postData);

            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.ContentLength = data.Length;

            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();

            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            responseString = responseString.Trim();

            return responseString;
        }


        private void btnConfirm_Click(object sender, EventArgs e)
        { 
            gCode = couponNum_wo_hyphen;
            if (gCode.Equals(""))
            {
                msgContent.Text = "쿠폰번호를 입력하세요";

                SetModeWait();
                return;
            }

            iniUtil ini = new iniUtil("");
            String shopCode = Machine.ini.IniReadValue("SMARTCON", "shopCode");
            String shopName = Machine.ini.IniReadValue("SMARTCON", "shopName");
            String exchangeId = Machine.ini.IniReadValue("SMARTCON", "companyCode");
            String amountCode = Machine.ini.IniReadValue("SMARTCON", "amountCode");
            String companyLoginId = Machine.ini.IniReadValue("SMARTCON", "companyCode");

            // 교환권 조회
            if (gCode.Substring(0, 1).Equals("1"))
            {
                SmartconClient client = new SmartconClient(APIURL_EXCHANGE, exchangeId, shopName, shopCode);
                string responseString = client.Lookup(gCode);
                msgContent.Text = responseString;

                if (client.gErrorCode.Equals("") || client.gErrorCode.Equals("E0009") || client.gErrorCode.Equals("E0010"))
                {
                    SetModeAccept();
                    setConfirmed(true);
                }
                else if (client.gErrorCode.Equals("E0000"))
                {
                    SetModeCancel();
                    setConfirmed(true);
                }
                else if (client.gErrorCode.Equals("E0006")) // 사용된 쿠폰
                {
                    btnCancel.Enabled = true;
                }
                else
                {
                    SetModeWait();
                }

                
            }
            // 금액권 조회
            else
            {



                SmartconAmountClient client = new SmartconAmountClient(APIURL_AMOUNT, amountCode, shopName, shopCode, shopName);
                string responseString = client.Lookup(gCode);
                msgContent.Text = responseString;

                if (client.gErrorCode.Equals("ER000"))
                {
                    setConfirmed(true);
                }
                else if (client.gErrorCode.Equals("ER702")) // 사용된 쿠폰
                {
                    btnCancel.Enabled = true;
                }
            }

            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            gCode = couponNum_wo_hyphen;
            if (gCode.Equals(""))
            {
                msgContent.Text = "쿠폰번호를 입력하세요";

                SetModeWait();
                return;
            }
            String shopCode = Machine.ini.IniReadValue("SMARTCON","shopCode");
            String shopName = Machine.ini.IniReadValue("SMARTCON", "shopName");
            String exchangeId = Machine.ini.IniReadValue("SMARTCON","companyCode");
            String amountCode = Machine.ini.IniReadValue("SMARTCON", "amountCode");

            if (gCode.Substring(0, 1).Equals("1"))
            {
                SmartconClient client = new SmartconClient(APIURL_EXCHANGE, exchangeId, shopName, shopCode);
                string responseString = client.Cancel(gCode, null);
                msgContent.Text = responseString;

                SetModeWait();
            }
            else
            {
                // 취소할 승인 목록을 가져와서 다이얼로그를 띄운다.
                SmartconAmountClient client = new SmartconAmountClient(APIURL_AMOUNT, amountCode, shopName, shopCode, shopName);
                string responseString = client.AcceptList(gCode);

                AcceptList l = new AcceptList(client.acceptLIst);
                l.ShowDialog();
                if (!l.selEXCHANGENUM.Equals(""))
                {
                    responseString = client.Cancel(gCode, l.selEXCHANGENUM);
                    msgContent.Text = responseString;
                    SetModeWait();
                }
                else
                {
                    SetModeWait();
                }

            }

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            gCode = couponNum_wo_hyphen;
            if (gCode.Equals(""))
            {
                msgContent.Text = "쿠폰번호를 입력하세요";

                SetModeWait();
                return;
            }

            iniUtil ini = new iniUtil("");
            String shopCode = Machine.ini.IniReadValue("SMARTCON", "shopCode");
            String shopName = Machine.ini.IniReadValue("SMARTCON", "shopName");
            String exchangeId = Machine.ini.IniReadValue("SMARTCON","companyCode");
            String amountCode = Machine.ini.IniReadValue("SMARTCON","amountCode");

            if (gCode.Substring(0, 1).Equals("1"))
            {
                SmartconClient client = new SmartconClient(APIURL_EXCHANGE, exchangeId, shopName, shopCode);
                string responseString = client.Accept(gCode);
                msgContent.Text = responseString;

                SetModeWait();
            }
            else // 금액권
            {
                SmartconAmountClient client = new SmartconAmountClient(APIURL_AMOUNT, amountCode, shopName, shopCode, shopName);
                string responseString = client.Lookup(gCode);
                responseString += "\r\n금액을 입력하세요";

                setMsgContent(responseString);

                SetModeAmount();
            }

        }

        private void setMsgContent(String msg)
        {
            gMsgContentOld = msgContent.Text;
            msgContent.Text = msg;
        }

        private void rollbackMsgContent()
        {
            msgContent.Text = gMsgContentOld;
        }

        private void SetModeAmount()
        {
            // 모드 세팅
            gMode = 300;

            // 입력버튼을 바꾸고
            btnAdmitInput.Show();
            btnAdmitCancel.Show();
            btnConfirm.Hide();
            btnAccept.Hide();
            btnCancel.Hide();

            // 쿠폰번호창을 지우고
            couponNum.Text = "";
            couponNum_wo_hyphen = "";

        }
        private void SetModeConfirm()
        {
            gMode = 0;

            btnAdmitInput.Hide();
            btnAdmitCancel.Hide();
            btnConfirm.Show();
            btnAccept.Show();
            btnCancel.Show();

            msgContent.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void SetModeAccept()
        {
            gMode = 1;
            btnAdmitInput.Hide();
            btnAdmitCancel.Hide();
            btnConfirm.Show();
            btnAccept.Show();
            btnCancel.Show();
        }

        private void SetModeCancel()
        {
            gMode = 2;
            btnAdmitInput.Hide();
            btnAdmitCancel.Hide();
            btnConfirm.Show();
            btnAccept.Show();
            btnCancel.Show();
        }

        private void SetModeWait()
        {
            gMode = 100;

            btnAdmitInput.Hide();
            btnAdmitCancel.Hide();
            btnConfirm.Show();
            btnAccept.Show();
            btnCancel.Show();
            /*
            timer.Interval = 2000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
             */

            msgContent.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void SetModeAdmitNumber()
        {
            // 모드 세팅
            gMode = 200;

            // 입력버튼을 바꾸고
            btnAdmitInput.Show();
            btnAdmitCancel.Show();
            btnConfirm.Hide();
            btnAccept.Hide();
            btnCancel.Hide();
            
            // 쿠폰번호창을 지우고
            couponNum.Text = "";


            // 메시지 출력을 바꾼다.
            msgContent.Text = "승인번호를 입력해주세요";


            msgContent.BackColor = Color.FromArgb(255, 255, 200);

        }

        private void CheckModeAtKey()
        { 
        }
        protected string AddHyphen(String input)
        {
            if (input.Length <= 4) return input;
            if (input.Length <= 8) return input.Substring(0, 4) + "-" + input.Substring(4);
            if (input.Length <= 12) return input.Substring(0, 4) + "-" + input.Substring(4,4) + "-" + input.Substring(8);
            return input.Substring(0, 4) + "-" + input.Substring(4, 4) + "-" + input.Substring(8,4) + "-" + input.Substring(12);
        }
        protected string couponNum_wo_hyphen = "";
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckModeAtKey();
            string s = couponNum_wo_hyphen;
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                if (s.Length >= 16)
                {
                    return;
                }
                couponNum_wo_hyphen = s + e.KeyChar;

                if (gMode == 300 || gMode == 200)
                {
                    couponNum.Text = couponNum_wo_hyphen;
                }
                else
                {
                    couponNum.Text = AddHyphen(couponNum_wo_hyphen);
                }
                couponNum.Update();

                if (gMode != 300)// 금액 입력중이 아니라면, 새로운 번호가 발생했으므로 조회만 가능하게 막는다
                {
                    setConfirmed(false);
                }
            }
            else if (e.KeyChar == (char)Keys.Back)
            {
                //ShowMsg("상품권 번호를 입력하세요.");
                if (s.Length > 0)
                {
                    couponNum_wo_hyphen = s.Substring(0, s.Length - 1);

                    if (gMode == 300 || gMode == 200)
                    {
                        couponNum.Text = couponNum_wo_hyphen;
                    }
                    else
                    {
                        couponNum.Text = AddHyphen(couponNum_wo_hyphen);
                    }
                     
                    couponNum.Update();
                    if (gMode != 300)   // 금액 입력중이 아니라면, 새로운 번호가 발생했으므로 조회만 가능하게 막는다
                    {
                        setConfirmed(false);
                    }
                }
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            CheckModeAtKey();
            Form1_KeyPress(sender, new KeyPressEventArgs('1'));
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            CheckModeAtKey();
            Form1_KeyPress(sender, new KeyPressEventArgs('2'));
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            CheckModeAtKey();
            Form1_KeyPress(sender, new KeyPressEventArgs('3'));
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            CheckModeAtKey();
            Form1_KeyPress(sender, new KeyPressEventArgs('4'));
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            CheckModeAtKey();
            Form1_KeyPress(sender, new KeyPressEventArgs('5'));
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            CheckModeAtKey();
            Form1_KeyPress(sender, new KeyPressEventArgs('6'));
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            CheckModeAtKey();
            Form1_KeyPress(sender, new KeyPressEventArgs('7'));
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            CheckModeAtKey();
            Form1_KeyPress(sender, new KeyPressEventArgs('8'));
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            CheckModeAtKey();
            Form1_KeyPress(sender, new KeyPressEventArgs('9'));
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            CheckModeAtKey();
            Form1_KeyPress(sender, new KeyPressEventArgs('0'));
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            CheckModeAtKey();
            Form1_KeyPress(sender, new KeyPressEventArgs((char)Keys.Back));
        }

        private void ShowMsg(string msg)
        {
            msgContent.Text = msg;
            msgContent.Update();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Program.ini = new IniFile(System.Environment.CurrentDirectory + @"\data_pos_agent.ini");
            SetModeConfirm();

            ShowMsg("상품권 번호를 입력하세요.");

            setConfirmed(false);
 
        }

        

        void timer_Tick(object sender, System.EventArgs e)
        {
            timer.Stop();
            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (gMode != 300)
            {
                ShowMsg("상품권 번호를 입력하세요.");
                setConfirmed(false);
            }
            couponNum.Text = "";
            couponNum_wo_hyphen = "";
            couponNum.Update();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        { 
            LoginForm lf = new LoginForm();
            iniUtil ini = new iniUtil(""); 
            if (lf.ShowDialog() == DialogResult.OK)
            {

            }

//             } 

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && bDrag == false)
            {
                startPos.X = e.X;
                startPos.Y = e.Y;
            }
        }

        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && bDrag == true)
            {
                bDrag = false;
                //saveLocation(this.Location.X, this.Location.Y);
            }
        }

        void Form1_MouseMove(object sender, MouseEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            String mobileSalesURL = String.Empty;
            mobileSalesURL = SvrUrl.RealMobileSalesPage + "?branchid=" + gBranchId;

            WebViewForm webVform = new WebViewForm(null, mobileSalesURL);
            webVform.ShowDialog();
             */


            String mobileSalesURL = String.Empty;
            iniUtil ini = new iniUtil("");
            mobileSalesURL = SvrUrl.RealMobileSalesPage + "?branchid=" + gBranchId;
            mobileSalesURL = String.Format("http://b2b.giftsmartcon.com/login/login.sc?member_id={0}&password={1}&login_type=S",
                Machine.ini.IniReadValue("SMARTCON", "companyCode"),
                Machine.ini.IniReadValue("SMARTCON", "companyCodePassword")
            );
            WebViewForm webVform = new WebViewForm(null, mobileSalesURL);
            webVform.ShowDialog();
        }

        private void btnAdmitCancel_Click(object sender, EventArgs e)
        {
            rollbackMsgContent();

            couponNum_wo_hyphen = "";
            SetModeWait();
        }

        private void btnAdmitInput_Click(object sender, EventArgs e)
        {
            if (gMode == 200)
            {
                String admitNum = couponNum_wo_hyphen;

                String shopCode = Machine.ini.IniReadValue("SMARTCON", "shopCode");
                String shopName = Machine.ini.IniReadValue("SMARTCON", "shopName");
                String exchangeId = Machine.ini.IniReadValue("SMARTCON", "companyCode");
                String exchangeIdPwd = Machine.ini.IniReadValue("SMARTCON", "companyCodePassword");
                String amountCode = Machine.ini.IniReadValue("SMARTCON", "amountCode");
                SmartconClient client = new SmartconClient(APIURL_EXCHANGE, exchangeId, shopName, shopCode);
                string responseString = client.Cancel(gCode, admitNum);
                msgContent.Text = responseString;

                couponNum.Text = "";
                couponNum_wo_hyphen = "";
                SetModeWait();
            }
            else if (gMode == 300)
            {
                String amount = couponNum_wo_hyphen; 

                try
                {
                    int nAmount = Convert.ToInt32(amount);
                    if (nAmount > 0)
                    {
                        String shopCode = Machine.ini.IniReadValue("SMARTCON", "shopCode");
                        String shopName = Machine.ini.IniReadValue("SMARTCON", "shopName");
                        String exchangeId = Machine.ini.IniReadValue("SMARTCON", "companyCode");
                        String amountCode = Machine.ini.IniReadValue("SMARTCON", "amountCode");
                        SmartconAmountClient client = new SmartconAmountClient(APIURL_AMOUNT, amountCode, shopName, shopCode, shopName);
                        string responseString = client.Accept(gCode, amount);
                        msgContent.Text = responseString;

                        couponNum_wo_hyphen = gCode;
                        if (gMode == 300 || gMode == 200)
                        {
                            couponNum.Text = couponNum_wo_hyphen;
                        }
                        else
                        {
                            couponNum.Text = AddHyphen(couponNum_wo_hyphen);
                        }
                        couponNum.Update();

                        SetModeWait();
                    }
                    else
                    {
                        MessageBox.Show("0원 이상의 금액을 입력하세요");
                    }
                }
                catch (Exception ee)
                {
                }
            }
        }
        
    }
}
