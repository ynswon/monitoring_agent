using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Web;
using System.Net;
using Newtonsoft.Json;

namespace funda_interface
{
    
    public class FundaInterface
    {
        public enum LoginMode { PureFundaAgent, SmartCon };
        private String url_parser = null;
        private String url_update_for_smartcon = null;

        private String url_login_for_fundaagent = null;
        private String url_apikey_for_fundaagent = null;
        private String url_update_for_fundaagent = null;

        public String GetUrlUpdateForSmartCon()
        {
            return url_update_for_smartcon;
        }
        public String GetUrlLoginForFundaAgent()
        {
            return url_login_for_fundaagent;
        }
        public String GetUrlApikeyForFundaAgent()
        {
            return url_apikey_for_fundaagent;
        }
        public String GetUrlUpdateForFundaAgent()
        {
            return url_update_for_fundaagent;
        }
        private PortMonitor pm;
        public FundaInterface()
        {
        }
        public string GetMd5Hash(MD5 md5Hash, string input)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="base_directory">if null data will be downloaded to current directory</param>
        public void Initilize(String base_directory = null)
        {
            if (base_directory == null)
            {
                base_directory = System.Environment.CurrentDirectory;
            }
            PortListener.fifi = this;
            //FundaParser.up
            MyWebClient mwc = null;
            String data = "";
            try
            {
                mwc = new MyWebClient();
                
                mwc.Headers.Add("Accept-Language", " en-US");
                mwc.Headers.Add("Accept", " text/html, application/xhtml+xml, */*");
                mwc.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");
        
                data = mwc.DownloadString("https://funda.kr/funda_agent/index.php");
            }
            catch(Exception ee)
            {
                Console.WriteLine(ee.ToString());
            }
            if (data != null)   
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(data);
                XmlNode xn;
                xn = xml.SelectSingleNode("/funda_agent/api_url[@name='funda_interface_parser']");
                if (xn != null) url_parser = xn.InnerText.Trim();
                xn = xml.SelectSingleNode("/funda_agent/api_url[@name='funda_interface_update_for_smartcon']");
                if (xn != null) url_update_for_smartcon = xn.InnerText.Trim();


                xn = xml.SelectSingleNode("/funda_agent/api_url[@name='funda_interface_login_for_fundaagent']");
                if (xn != null) url_login_for_fundaagent = xn.InnerText.Trim();
                xn = xml.SelectSingleNode("/funda_agent/api_url[@name='funda_interface_validate_apikey_for_fundaagent']");
                if (xn != null) url_apikey_for_fundaagent = xn.InnerText.Trim();
                xn = xml.SelectSingleNode("/funda_agent/api_url[@name='funda_interface_update_for_fundaagent']");
                if (xn != null) url_update_for_fundaagent = xn.InnerText.Trim();


                PortListener.SetUpdateUrl(url_update_for_smartcon);

                /*try
                {
                    Directory.CreateDirectory(base_directory);

                    if (url_parser != null)
                    {
                        mwc.DownloadFile(url_parser, base_directory + @"\menu_temp.xml");
                        File.Delete(base_directory + @"\menu.xml");
                        File.Move(base_directory + @"\menu_temp.xml",
                            base_directory + @"\menu.xml");

                        XmlParser parser = new XmlParser();
                        if (parser.ReadXML(base_directory + @"\menu.xml"))
                        {
                            MenuXmlData xmlData = parser.getXmlData();
                            FundaParser.setXmlData(xmlData);
                        }

                    }
                }
                catch (Exception ee)
                {

                    ee = ee;
                }*/
            } 
            pm = new PortMonitor();
            pm.initPortListener(true);
             
            pm.initMonitoring();
        }
        private LoginMode login_mode;
        private String user_id;
        private String password;
        private String api_key;
        public void PublishRawData(String data)
        {

            MyWebClient WClient = new MyWebClient();
            string raw_data_for_get;
            //raw_data_for_get = System.Web.HttpUtility.UrlEncode(data);
            raw_data_for_get = data;
            String callUrl = "";            
            String postData = "";


            if(login_mode==LoginMode.SmartCon)
            {
                callUrl = url_update_for_smartcon;
                postData = String.Format("raw_data={0}&smartcon_store_code={1}",
                System.Web.HttpUtility.UrlEncode(raw_data_for_get),
                user_id);
            }
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(callUrl);
            // 인코딩 UTF-8
            byte[] sendData = UTF8Encoding.UTF8.GetBytes(postData);
            httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = sendData.Length;
            Stream requestStream = httpWebRequest.GetRequestStream();
            requestStream.Write(sendData, 0, sendData.Length);
            requestStream.Close();
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
            string myreturn = streamReader.ReadToEnd();
            streamReader.Close();
            httpWebResponse.Close();

            System.Console.Write("return: " + myreturn);

            //String dataFromServer = "";
        }
        public bool TryToSmartconLogin(String user_id, String password)
        {
            String api_key = "";
            return TryToLogin(LoginMode.SmartCon, user_id, password, ref api_key);
        }
        public bool IsValidApiKey(
            String store_code,
            String api_key,
            out String display,
            out String display_desc,
            out Boolean stealth_mode)
        {
            display = null;
            display_desc = null;
            stealth_mode = false;
            MyWebClient WClient = new MyWebClient();
            String downloadUrl = String.Format(GetUrlApikeyForFundaAgent() + "?store_code={0}&api_key={1}",
                HttpUtility.UrlEncode(store_code),
                HttpUtility.UrlEncode(api_key)
            );

            WClient.Encoding = Encoding.UTF8;
            String data = WClient.DownloadString(downloadUrl);

            XmlDocument xmlData = JsonConvert.DeserializeXmlNode(data);
            XmlNode xn;
            xn = xmlData.SelectSingleNode("/api_key_validation/cnt");
            if (xn == null) return false;

            String apikey = xn.InnerText.Trim();
            if (apikey == "0") return false;

            xn = xmlData.SelectSingleNode("/api_key_validation/display");
            if (xn != null) display = xn.InnerText.Trim();

            xn = xmlData.SelectSingleNode("/api_key_validation/display_desc");
            if (xn != null) display_desc = xn.InnerText.Trim();

            xn = xmlData.SelectSingleNode("/api_key_validation/stealth_mode");
            if (xn != null)
            {
                if (xn.InnerText.Trim() == "1") stealth_mode = true;
                else stealth_mode = false;
            }
            return true;
        }
        public bool TryToLogin(LoginMode lm, String user_id, String password, ref String api_key)
        {
            api_key = "";
            if (lm == LoginMode.SmartCon)
            {
                this.user_id = user_id;
                this.password = password;
            }
            else if (lm == LoginMode.PureFundaAgent)
            {
                string hash = "";
                api_key = "";
                this.user_id = user_id;
                String display, display_desc;


                MyWebClient WClient = new MyWebClient();
                String downloadUrl;
                /*
                if (api_key != "")
                {
                    downloadUrl = String.Format(url_apikey_for_fundaagent + "?store_code={0}&api_key={1}",
                        HttpUtility.UrlEncode(user_id),
                        HttpUtility.UrlEncode(api_key)
                    );
                }
                else
                {*/

                using (MD5 md5Hash = MD5.Create())
                {
                    hash = GetMd5Hash(md5Hash, password);
                }
                

                downloadUrl = String.Format(url_login_for_fundaagent + "?store_code={0}&password={1}",
                    HttpUtility.UrlEncode(user_id),
                    HttpUtility.UrlEncode(hash)
                );

                WClient.Encoding = Encoding.UTF8;
                String data = WClient.DownloadString(downloadUrl);

                XmlDocument xmlData = JsonConvert.DeserializeXmlNode(data);
                XmlNode xn;
                xn = xmlData.SelectSingleNode("/apikey");
                if (xn == null) return false;

                api_key = xn.InnerText.Trim();
                if (api_key == "") return false;

                /*
                xn = xmlData.SelectSingleNode("/api_key_validation/display");
                if (xn != null) display = xn.InnerText.Trim();

                xn = xmlData.SelectSingleNode("/api_key_validation/display_desc");
                if (xn != null) display_desc = xn.InnerText.Trim();

                xn = xmlData.SelectSingleNode("/api_key_validation/stealth_mode");
                 */
                /*
                if (xn != null)
                {
                    if (xn.InnerText.Trim() == "1") stealth_mode = true;
                    else stealth_mode = false;
                }*/ 

                return true;

            }
            else
            {

                return true;
            }
            this.login_mode = lm;
            return true;
        }
        public void Start()
        {
            if (pm == null) return;
            pm.startMonitoring();
        }
        public void Stop()
        {
            if (pm == null) return;
            pm.stopMonitoring();
        }
    }
}
