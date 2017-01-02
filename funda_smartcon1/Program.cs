using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;

namespace funda_smartcon1
{
    static class Program
    {
        public static IniFile ini;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SmartconShortcut());
        }
    }

    public class IniFile
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <param name="INIPath"></param>
        public IniFile(string INIPath)
        {
            path = INIPath;
        }
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <param name="Section"></param>
        /// Section name
        /// <param name="Key"></param>
        /// Key Name
        /// <param name="Value"></param>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value)
        {
            String stacktrace = Environment.StackTrace;

            //fileWriterLog(DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond + "--" +stacktrace+ "Update config:", "log_debug.txt");
            //Log.WriteLine(DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond + "--" + stacktrace + "Update config:", "log_debug.txt");
            //LogWriter.WriteLine("Update config " + Section + " " + Key + " " + Value + ".\r\n");
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key, string default_value = "")
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, default_value, temp, 255, this.path);
            return temp.ToString();

        }
    }
    static class SvrUrl
    {
        public const String RealPutBranchSalesHistory = "http://m.9flava.com/webapp/proc/WepassPOSPutBranchSalesHistoryWithQrIdx.asp";   // DBHelper include 변경해야함!!!

        public const String RealGetShopIdFromBizNo = "http://m.9flava.com/webapp/proc/GetShopIdFromBizNo.asp"; //shop Id, branch Id, 카드타입(적립), 영수증 parsing rule 가져옴.
        public const String RealGetShopIdFromBizNo_V4 = "http://m.9flava.com/webapp/proc/GetShopIdFromBizNo_V4.asp"; //shop Id, branch Id, 카드타입(적립), 영수증 parsing rule 가져옴.
        //public const String RealGetShopIdFromBizNo = "http://m.9flava.com/webapp/proc/GetShopIdFromBizNo_test.asp"; //shop Id, branch Id, 카드타입(적립), 영수증 parsing rule 가져옴.

        public const String RealLogUpload = "http://m.9flava.com/admin/uploadLog.asp";
        public const String RealMenuXmlLinkGetUrl = "http://wepass.co.kr/api/getMenuXml.asp?sid=";  // branch/shop id에 해당하는 menu.xml 다운로드
        //public const String RealMenuXmlLinkGetUrl = "http://wepass.co.kr/api/getMenuXmlTest.asp?sid=";  // branch/shop id에 해당하는 menu.xml 다운로드
        public const String RealGetQRCodeUrl = "http://m.9flava.com/admin/getCodeFromBranch.asp";   // QR 코드값 가져오기

        public const String RealGetShortQRCodeUrl = "http://m.9flava.com/admin/getShortCodeFromBranch.asp";   // QR 코드값 가져오기

        //public const String DevGetShortQRCodeUrl = "http://m.9flava.com/admin/getShortCodeFromBranch_Shop6167Test.asp";   // 짧은 QR 코드값 가져오기
        //public const String DevGetShortQRCodeUrl = "http://m.9flava.com/admin/getShortCodeFromBranchDEV.asp"; // 짧은 QR code값 가져오는 코드. 
        public const String DevGetShortQRCodeUrl = "http://m.9flava.com/admin/getShortCodeFromBranch_smsDEV.asp";   // 짧은 QR code값 가져오는 코드 + 문자 메시지 적립을 위한 정보 저장 추가됨.

        public const String DevGetQRCodeUrl = "http://m.9flava.com/admin/getShortCodeFromBranch_dev.asp";  // 개발 : 짧은 QR코드값 가져오기
        public const String RealPutSalesHistory = "http://m.9flava.com/webapp/proc/WepassPOSPutSalesHistory.asp";   // QR 발행하면서 영수증 전체 업로드

        public const String CheckInByPhoneUrl = "http://wepass.co.kr/api/checkInByPhone.asp";   // 전화번호 적립

        // 모바일 판매
        public const String RealMobileSalesPage = "http://funda.kr/report/smartcon/index.php";    // 모바일 판매현황 보여주는 페이지
        public const String AddData = "https://funda.kr/api/addData.php";    // 모바일 판매현황 보여주는 페이지
        //public const String DevMobileSalesPage = "http://wepass.co.kr/pos/mobileSalesList.asp";    // 모바일 판매현황 보여주는 페이지
        // 소셜마케팅
        public const String RealSocialMarketingPage = "http://wepass.co.kr/pos/socialMarketing.asp";    // 모바일 판매현황 보여주는 페이지

    }
}
