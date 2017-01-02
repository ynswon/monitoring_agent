using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;
using Newtonsoft.Json;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms; 


using System.Security.Cryptography;
namespace funda_agent
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
        /*
        public static bool FundaLoginApi(String store_code, String password_for_funda_agent, ref String result_message)
        { 
            MyWebClient WClient = new MyWebClient();
            string hash = "";
            string source = password_for_funda_agent;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, source);
            } 

            String downloadUrl = String.Format("http://14.63.215.51:880/api_fundareport/getApiKey.php?store_code={0}&password={1}",
                HttpUtility.UrlEncode(store_code),
                HttpUtility.UrlEncode(hash)
                );

             
            WClient.Encoding = Encoding.UTF8;
            String data = WClient.DownloadString(downloadUrl);


            XmlDocument xmlData = JsonConvert.DeserializeXmlNode(data);
            XmlNodeList nl = xmlData.SelectNodes("/apikey");
            if (nl.Count > 0)
            {
                String apikey = nl[0].InnerText;
                apikey.Trim();
                if (apikey == "")
                {
                    result_message = "";
                    return true;
                }
                else
                {
                    result_message = nl[0].InnerText;
                }
            }
            return false;


        }
        
         */
    }
}
