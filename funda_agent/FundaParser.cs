using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Xml; 
using System.Net;
using System.Diagnostics; 
using System.Threading; 
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
namespace funda_agent
{
    class FundaParser
    {
        //public const String RealMenuXmlLinkGetUrl = "http://wepass.co.kr/api/getMenuXml.asp?sid=";
        public static bool updateXML()
        {
            MyWebClient WClient = new MyWebClient();
            string downloadUrl = "http://14.63.215.51:880/xml/menu.xml";
            
            String drloc_temp = Environment.CurrentDirectory + @"\menu_temp.xml";
            String drloc = Environment.CurrentDirectory + @"\menu.xml";
            try
            {
                // catch에 걸리면 파일 삭제됨
                WClient.DownloadFile(downloadUrl, drloc_temp);
                File.Delete(drloc);
                File.Move(drloc_temp, drloc);

            }
            catch (Exception ee)
            {
                MessageBox.Show("인터넷 연결을 확인해 주시기 바랍니다.");
                
                return false;
            }

            XmlParser parser = new XmlParser();
            if (!parser.ReadXML(drloc)) return false;

            MenuXmlData xmlData = parser.getXmlData();
            setXmlData(xmlData);
            return true;

        }
        public static String cutterStr;
        public static String initPrinterStr;
        public static MenuXmlData xmlData;

        public static void setXmlData(MenuXmlData xmlData)
        {
            FundaParser.xmlData = xmlData;

            cutterStr = xmlData.PrinterCutterStrs;
            initPrinterStr = xmlData.PrinterInitStrs;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static bool hasEndData(string str)
        {
            bool hasContains = false;
            try
            {
                string[] arr = cutterStr.Split('/');
                foreach (string s in arr)
                {
                    if (!s.Equals(""))
                    {
                        if (str.Contains(s))
                        {
                            //fileWriterLog(DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond + "--" + "Detect the receipt cutting command." + s + "\r\n \r\n", "log_debug.txt");
                            //fileWriterLog(DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond + "--" + s + "\r\n \r\n", "log_buffer_after.txt");
                            hasContains = true;
                            break;
                        }
                    }
                }   
                return hasContains;
            }
            catch
            {
                //fileWriterLog(DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond + "--" + "Error at hadEndData \r\n", "log_debug.txt");
                return hasContains;
            }

        }
    }
}
