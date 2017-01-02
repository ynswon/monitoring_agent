using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml; 
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms; 


using System.Security.Cryptography;
namespace funda_smartcon1
{
    public static class Machine
    {
        public static IniFile ini;
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        public static bool SmartconLoginApi(String login_id, String login_password, ref String result_message,
             ref string result_code,
             ref string rcompany_id,
             ref string rcompany_id_gc,
             ref string branch_name)
        {
            try
            {
                MyWebClient WClient = new MyWebClient();
                WClient.Encoding = System.Text.Encoding.UTF8;
                String id = login_id;
                String pwd = login_password;
                String not_encoded_url = "http://183.111.10.91:17080/login/login_wps.sc?login_type=S&member_id=" + id + "&password=" + pwd;

                String encoded_url = "http://183.111.10.91:17080/login/login_wps.sc?login_type=S&member_id=" + HttpUtility.UrlEncode(id) + "&password=" + HttpUtility.UrlEncode(pwd);
                String data = WClient.DownloadString(not_encoded_url);
                WClient.DownloadFile(encoded_url,
                    Application.StartupPath + @"\loginResult.xml");
                //LogWriter.WriteLine("2");

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(data); // suppose that myXmlString contains "<Names>...</Names>"

                XmlNodeList xnList = xml.SelectNodes("/result");

                 

                foreach (XmlNode xn in xnList)
                { 
                    result_code = xn["result_code"].InnerText;
                    rcompany_id = xn["rcompany_id"].InnerText;
                    rcompany_id_gc = xn["rcompany_id_gc"].InnerText;
                    branch_name = xn["branch_name"].InnerText; 

                    if (result_code == "01")
                    {
                        result_message = "로그인 실패";
                        return true;
                    }
                    else
                    {
                        result_message = String.Format("로그인 성공\r\n본사코드(교환권): {0}\r\n본사코드(금액권): {1}\r\n지점명: {2}\r\n",
                            rcompany_id, rcompany_id_gc, branch_name);
                        return false;
                    }
                    break;
                }
            }
            catch (Exception eee)
            {
                Console.WriteLine("스마트콘 로그인 실패");
                return true;
            }
            return false; 
        }
    }
}
