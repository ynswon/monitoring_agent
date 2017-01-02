using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data; 
using System.Text; 

using System.IO;
using System.Collections;
using System.Collections.Specialized;

using System.Xml;

using System.Net;
using System.Diagnostics;


using System.Threading;

using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Xml;

namespace funda_interface
{
     
    class XmlParser
    {

        private MenuXmlData xmlData;

        public XmlParser()
        {
            xmlData = new MenuXmlData();

        }
        public MenuXmlData getXmlData()
        {
            return xmlData;
        }
        private void parseNodesList(XmlNodeList itemList)
        {
            try
            {
                foreach (XmlNode item in itemList)
                {
                    switch (item.Name)
                    {
                        case "phoneNumber": // 배달 매장 영수증에서 전화번호 얻어내서 적립에 사용
                            xmlData.TagPhoneNumber = item.InnerText;
                            break;
                        case "returnstr":   // 사용 안하는듯.
                            xmlData.ReturnGoodsStr = item.InnerText;
                            break;
                        case "prepare":     // 영수증 파싱 준비
                            xmlData.ParsePrepareStr = item.InnerText;
                            break;
                        case "start":       // 영수증 파싱 시작
                            if (item.Attributes["type"].Value == "dec")
                            {
                                xmlData.ParseStartStr = convertDecToString(item.InnerText);
                            }
                            else
                            {
                                xmlData.ParseStartStr = item.InnerText;
                            }
                            break;
                        case "stop":        // 영수증 파싱 중지
                            if (item.Attributes["type"].Value == "dec")
                            {
                                xmlData.ParseStopStr = convertDecToString(item.InnerText);
                            }
                            else
                            {
                                xmlData.ParseStopStr = item.InnerText;
                            }
                            break;
                        case "parsetype":   // 적립방식 0 -> 금액별 : 1 -> 메뉴별 : 2 -> 특정메뉴제외한금액 : 3 -> 특정금액이상 무조건 하나 : 4 -> DivAmount 이상이면 2개 미만이면 1개 : 5 -> 무조건 하나
                            xmlData.ParseType = int.Parse(item.Attributes["type"].Value);
                            break;
                        case "divamount":
                            xmlData.DivAmount = int.Parse(item.InnerText);
                            break;
                        case "parseidx":
                            xmlData.IsTwoLine = bool.Parse(item.Attributes["twoline"].Value);
                            parseNodesList(item.ChildNodes);
                            break;
                        case "items":
                            parseNodesList(item.ChildNodes);
                            break;
                        case "nameidx":
                            xmlData.NameIdx = int.Parse(item.InnerText);
                            break;
                        case "unitpriceidx":
                            xmlData.UnitpriceIdx = int.Parse(item.InnerText);
                            break;
                        case "countidx":
                            xmlData.CountIdx = int.Parse(item.InnerText);
                            break;
                        case "priceidx":
                            xmlData.PriceIdx = int.Parse(item.InnerText);
                            break;
                        case "idxoption":
                            xmlData.UseIdxOption = bool.Parse(item.Attributes["useoption"].Value);
                            if (xmlData.UseIdxOption) parseNodesList(item.ChildNodes);
                            break;
                        case "optionnameidx":
                            xmlData.OptionNameIdx = int.Parse(item.InnerText);
                            break;
                        case "optionunitpriceidx":
                            xmlData.OptionUnitpriceIdx = int.Parse(item.InnerText);
                            break;
                        case "optioncountidx":
                            xmlData.OptionCountIdx = int.Parse(item.InnerText);
                            break;
                        case "optionpriceidx":
                            xmlData.OptionPriceIdx = int.Parse(item.InnerText);
                            break;
                        case "item":
                            string itemName = item.InnerText;
                            int checkinCnt = 1;

                            //Attribute가 없을 때의 예외 처리
                            XmlAttribute numAttr = item.Attributes["num"];
                            if (numAttr != null)
                                checkinCnt = int.Parse(numAttr.Value);

                            itemName = itemName.Replace(" ", "");

                            xmlData.setMenuItem(itemName, checkinCnt);
                            break;
                        case "discountstr":
                            xmlData.DiscountStr = item.InnerText;
                            break;
                        case "discountnameidx":
                            xmlData.DiscountNameIdx = int.Parse(item.InnerText);
                            break;
                        case "discountpriceidx":
                            xmlData.DiscountPriceIdx = int.Parse(item.InnerText);
                            break;
                        case "discountmin":
                            xmlData.DiscountMinPrice = int.Parse(item.InnerText);
                            break;
                        case "printer":
                            parseNodesList(item.ChildNodes);
                            break;
                        case "init":
                            parseNodesList(item.ChildNodes);
                            break;
                        case "initbytes":
                            xmlData.SetPrinterInitStrings(convertDecToString(item.InnerText));
                            break;
                        case "cutter":
                            parseNodesList(item.ChildNodes);
                            break;
                        case "cutterbytes":
                            xmlData.SetPrinterCutterStrings(convertDecToString(item.InnerText));
                            break;
                        case "printdelay":
                            xmlData.PrintDelayMs = int.Parse(item.InnerText);
                            break;

                        case "receiptname": // 영수증 detect
                            xmlData.ReceiptName = item.InnerText;
                            break;
                        case "tagreceipts": // 영수증 detect
                            parseNodesList(item.ChildNodes);
                            break;
                        case "tagreceipt":  // 영수증 detect
                            xmlData.setTagReceipt(item.InnerText);
                            break;

                        case "wepassname": // 영수증 아님 detect
                            xmlData.WepassName = item.InnerText;
                            break;
                        case "tagnonreceipts":// 영수증 아님 detect
                            parseNodesList(item.ChildNodes);
                            break;
                        case "tagnonreceipt":// 영수증 아님 detect
                            xmlData.setTagNonReceipt(item.InnerText);
                            break;

                        case "amountcompare":
                            xmlData.setTotalAmounts(item.InnerText);
                            break;
                        case "totalamounts":
                            parseNodesList(item.ChildNodes);
                            break;
                        case "totalamount":
                            xmlData.setTotalAmounts(item.InnerText);
                            break;

                        case "skiprowchars":
                            parseNodesList(item.ChildNodes);
                            break;
                        case "skiprowchar":
                            xmlData.setSkiprowchars(item.InnerText);
                            break;

                        case "checkdiscount":
                            xmlData.setCheckdiscount(item.InnerText);
                            break;

                        case "speakervolume":
                            xmlData.SpkVol = int.Parse(item.InnerText);
                            break;
                        case "clips": // multimedia clips 
                            xmlData.ClipsUrl = item.Attributes["url"].Value;
                            parseNodesList(item.ChildNodes);
                            break;
                        case "clip": // audio clip
                            if (item.Attributes["type"].Value == "audio")
                                xmlData.addClipItem(item.InnerText);
                            break;
                        case "removechars":
                            parseNodesList(item.ChildNodes);
                            break;
                        case "char":
                            xmlData.SetRemoveStrings(convertDecToString(item.InnerText));
                            break;
                        case "remainchars":
                            parseNodesList(item.ChildNodes);
                            break;
                        case "remainchar":
                            string remainChar;
                            if (item.Attributes["type"].Value == "dec") remainChar = convertDecToString(item.InnerText);
                            else remainChar = item.InnerText;
                            xmlData.SetRemainChars(remainChar);
                            break;
                        case "removerb":
                            xmlData.SetRemoveRoundBrackets(item.InnerText);
                            break;
                        case "cardParse":
                            parseNodesList(item.ChildNodes);
                            break;
                        case "cardParseStart":
                            xmlData.CardParseStart = item.InnerText;
                            break;
                        case "cardNum":
                            xmlData.CardNum = item.InnerText;
                            break;
                        case "cardName":
                            xmlData.CardName = item.InnerText;
                            break;
                        case "approvedAmount":
                            xmlData.ApprovedAmount = item.InnerText;
                            break;
                        case "approvedTime":
                            xmlData.ApprovedTime = item.InnerText;
                            break;
                             
                    }
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("parseNodesList error : " + e.Message.ToString());
            }

        }
        private string convertDecToString(string byteStr)
        {
            string result = null;
            string[] valuesSplit = byteStr.Split(' ');
            foreach (String dec in valuesSplit)
            {
                // Convert the number expressed in base-16 to an integer.
                int value = Convert.ToInt32(dec, 10);
                // Get the character corresponding to the integral value.
                //string stringValue = Char.ConvertFromUtf32(value);
                char charValue = (char)value;
                result += charValue;
            }
            return result;
        }
        public Boolean ReadXML(string path)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(path);
                XmlElement root = xmldoc.DocumentElement;

                // 노드 요소들
                XmlNodeList nodes = root.ChildNodes;
                // 노드 요소의 값을 읽어 옵니다.
                parseNodesList(nodes);

                return true;

            }
            catch (Exception ex)
            {

                //MessageBox.Show("XMLParser : " + ex.ToString());
                //LogWriter.WriteLine("XMLParser : " + ex.ToString());
                return false;
            }
        }
    }
    public class MyWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = 20 * 1000;
            return w;
        }
    }
    class FundaParser
    {
        public static bool updateXML(String base_directory)
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
                //MessageBox.Show("인터넷 연결을 확인해 주시기 바랍니다.");

                return true;
            }

            XmlParser parser = new XmlParser();
            if (!parser.ReadXML(drloc)) return false;

            MenuXmlData xmlData = parser.getXmlData();
            setXmlData(xmlData);
            return false;

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

    public class MenuXmlData
    {
        private string parsePrepareStr;
        private string parseStartStr;
        private string parseStopStr;
        private string returnGoodsStr;
        private int nameIdx = -1;
        private int unitpriceIdx = -1;
        private int countIdx = -1;
        private int priceIdx = -1;
        private int parseType = -1;
        private string parseAmountStr;
        private bool isTwoLine = false;
        private int divAmount = -1;
        private string discountStr;
        private int discountPriceIdx = -1;
        private int discountNameIdx = -1;
        private int discountMinPrice = -1;
        private int printDelayMs = 500;
        private string receiptName;
        private string wepassName;

        private string printerInitStrs;
        private string printerCutterStrs;
        private string removeStrs = null;
        private string remainChars = null;

        private int optionPriceIdx = -1;
        private int optionNameIdx = -1;
        private int optionUnitpriceIdx = -1;
        private int optionCountIdx = -1;
        private bool useIdxOption = false;

        private bool removeBracket = true;

        //For audio/video play
        // Oct 4, 2012 Soontak Lee
        //
        private int spkVol;
        private string clipsUrl;
        private ArrayList audioClips = new ArrayList();
        //private string[] videoClips;


        public void addClipItem(string item)
        {
            audioClips.Add(item);
        }
        public string getClipItem(int idx)
        {
            return (string)audioClips[idx];
        }
        public int getClipItemNum()
        {
            return audioClips.Count;
        }

        /*private Hashtable menuTable = new Hashtable();
        public Hashtable getMenuTable()
        {
            return menuTable;
        }
        public void setMenuItem(string item)
        {
            if (!menuTable.ContainsKey(item)) menuTable.Add(item, item);
        }
        public bool checkMenuItem(string menuName)
        {
            return menuTable.ContainsKey(menuName);
        }*/

        /*
         * During menu parsing, skip the row which have 'skiprowchar'.
         */
        private ArrayList _skiprowchars = new ArrayList();
        public void setSkiprowchars(String skipchar)
        {
            _skiprowchars.Add(skipchar);
        }
        public ArrayList skiprowchars()
        {
            return _skiprowchars;
        }

        private bool _checkdiscount = false;
        public void setCheckdiscount(String val)
        {
            try
            {
                _checkdiscount = bool.Parse(val);
            }
            catch
            {
                _checkdiscount = false;
            }
        }
        public bool checkdiscount()
        {
            return _checkdiscount;
        }


        private ArrayList _totalAmounts = new ArrayList();
        public void setTotalAmounts(String totalAmount)
        {
            _totalAmounts.Add(totalAmount);
        }
        public ArrayList totalAmounts()
        {
            return _totalAmounts;
        }

        private ArrayList _tagReceipt = new ArrayList();
        public void setTagReceipt(String tagReceipt)
        {
            _tagReceipt.Add(tagReceipt);
        }
        public ArrayList tagReceipt()
        {
            return _tagReceipt;
        }

        private ArrayList _tagNonReceipt = new ArrayList();
        public void setTagNonReceipt(String tagNonReceipt)
        {
            _tagNonReceipt.Add(tagNonReceipt);
        }
        public ArrayList tagNonReceipt()
        {
            return _tagNonReceipt;
        }
        // cardCompanyCode
        //private ArrayList cardCompanyCode = new ArrayList();
        //public struct CardCompanyCodeStruct
        //{
        //    public string CardCompanyCode;
        //    public int CodeVal;

        //    public CardCompanyCodeStruct(String cardCompanyCode, int codeVal)
        //    {
        //        CardCompanyCode = cardCompanyCode;
        //        CodeVal = codeVal;
        //    }
        //}
        //public ArrayList getCardCompanyCode()
        //{
        //    return cardCompanyCode;
        //}
        //public void setCardCompanyCode(String CardCompanyName, int companyCode)
        //{
        //    cardCompanyCode.Add(new CardCompanyCodeStruct(CardCompanyName, companyCode));
        //}
        //public int returnCardCompanyCode(String companyName)
        //{
        //    int CCode = 0;
        //    foreach (CardCompanyCodeStruct item in cardCompanyCode)
        //    {
        //        String nameStr = item.CardCompanyCode;
        //        if (companyName.Contains(nameStr))
        //        {
        //            CCode = item.CodeVal;
        //            break;
        //        }
        //    }
        //    return CCode;
        //}


        private ArrayList menuTable = new ArrayList();
        public struct MenuStruct
        {
            public string menuName;
            public int checkInNum;

            public MenuStruct(string name, int num)
            {
                menuName = name;
                checkInNum = num;
            }
        }

        public ArrayList getMenuTable()
        {
            return menuTable;
        }
        public void setMenuItem(string item)
        {
            menuTable.Add(item);
        }
        //2012/10/30 오버로딩 추가한 메서드 item별 적립수를 다르게 하기 위해
        public void setMenuItem(string item, int checkInNum)
        {
            menuTable.Add(new MenuStruct(item, checkInNum));
        }
        public void SetPrinterInitStrings(string initStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(initStr).Append("/");
            printerInitStrs += sb.ToString();

        }
        public void SetPrinterCutterStrings(string cutterStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(cutterStr).Append("/");
            printerCutterStrs += sb.ToString();

        }
        public void SetRemoveStrings(string removeChar)
        {
            removeStrs += removeChar;
        }
        public void SetRemainChars(string remainChar)
        {
            remainChars += remainChar;
        }
        public void SetRemoveRoundBrackets(string removerb)
        {
            if (removerb != null)
            {
                removeBracket = Boolean.Parse(removerb);
            }
        }
        public bool checkMenuItem(string menuName)
        {
            bool hasItem = false;
            foreach (MenuStruct item in menuTable)
            {
                string menuStr = item.menuName;
                if (menuName.Contains(menuStr))
                {
                    hasItem = true;
                    break;
                }
            }
            return hasItem;
        }
        //각 메뉴의 checkInNum을 return
        public int checkMenuItemReturnNum(string menuName)
        {
            int numCheckIn = 0;
            foreach (MenuStruct item in menuTable)
            {
                string menuStr = item.menuName;
                if (menuName.Contains(menuStr))
                {
                    numCheckIn = item.checkInNum;
                    break;
                }
            }
            return numCheckIn;
        }



        private string tagPhoneNumber;
        public string TagPhoneNumber
        {
            set { this.tagPhoneNumber = value; }
            get { return this.tagPhoneNumber; }
        }

        public string ParsePrepareStr
        {
            set { this.parsePrepareStr = value; }
            get { return this.parsePrepareStr; }
        }
        public string ParseStartStr
        {
            set { this.parseStartStr = value; }
            get { return this.parseStartStr; }
        }
        public string ParseStopStr
        {
            set { this.parseStopStr = value; }
            get { return this.parseStopStr; }
        }


        public int NameIdx
        {
            set { this.nameIdx = value; }
            get { return this.nameIdx; }
        }
        public int UnitpriceIdx
        {
            set { this.unitpriceIdx = value; }
            get { return this.unitpriceIdx; }
        }
        public int CountIdx
        {
            set { this.countIdx = value; }
            get { return this.countIdx; }
        }
        public int PriceIdx
        {
            set { this.priceIdx = value; }
            get { return this.priceIdx; }
        }

        public int ParseType
        {
            set { this.parseType = value; }
            get { return this.parseType; }
        }
        public string ParseAmountStr
        {
            set { this.parseAmountStr = value; }
            get { return this.parseAmountStr; }
        }

        public int DivAmount
        {
            set { this.divAmount = value; }
            get { return this.divAmount; }
        }

        public bool IsTwoLine
        {
            set { this.isTwoLine = value; }
            get { return this.isTwoLine; }
        }

        public string ReturnGoodsStr
        {
            set { this.returnGoodsStr = value; }
            get { return this.returnGoodsStr; }
        }

        public string DiscountStr
        {
            set { this.discountStr = value; }
            get { return this.discountStr; }
        }

        public int DiscountNameIdx
        {
            set { this.discountNameIdx = value; }
            get { return this.discountNameIdx; }
        }

        public int DiscountPriceIdx
        {
            set { this.discountPriceIdx = value; }
            get { return this.discountPriceIdx; }
        }
        public int DiscountMinPrice
        {
            set { this.discountMinPrice = value; }
            get { return this.discountMinPrice; }
        }

        public string PrinterInitStrs
        {
            set { this.printerInitStrs = value; }
            get { return this.printerInitStrs; }
        }

        public string PrinterCutterStrs
        {
            set { this.printerCutterStrs = value; }
            get { return this.printerCutterStrs; }
        }
        public string RemoveStrs
        {
            set { this.removeStrs = value; }
            get { return this.removeStrs; }
        }
        public string RemainChars
        {
            set { this.remainChars = value; }
            get { return this.remainChars; }
        }
        public int PrintDelayMs
        {
            set { this.printDelayMs = value; }
            get { return this.printDelayMs; }
        }
        public string ReceiptName
        {
            set { this.receiptName = value; }
            get { return this.receiptName; }
        }

        public string WepassName
        {
            set { this.wepassName = value; }
            get { return this.wepassName; }
        }

        public int OptionNameIdx
        {
            set { this.optionNameIdx = value; }
            get { return this.optionNameIdx; }
        }
        public int OptionUnitpriceIdx
        {
            set { this.optionUnitpriceIdx = value; }
            get { return this.optionUnitpriceIdx; }
        }
        public int OptionPriceIdx
        {
            set { this.optionPriceIdx = value; }
            get { return this.optionPriceIdx; }
        }
        public int OptionCountIdx
        {
            set { this.optionCountIdx = value; }
            get { return this.optionCountIdx; }
        }
        public bool UseIdxOption
        {
            set { this.useIdxOption = value; }
            get { return this.useIdxOption; }
        }
        public string ClipsUrl
        {
            set { this.clipsUrl = value; }
            get { return this.clipsUrl; }
        }

        public int SpkVol
        {
            set { this.spkVol = value; }
            get { return this.spkVol; }
        }

        public bool RemoveBracket
        {
            set { this.removeBracket = value; }
            get { return this.removeBracket; }
        }

        // 신용카드 승인 정보 파싱을 위한 변수들
        private String cardParseStart;
        public String CardParseStart
        {
            set { this.cardParseStart = value; }
            get { return this.cardParseStart; }
        }

        private String cardNum;
        public String CardNum
        {
            set { this.cardNum = value; }
            get { return this.cardNum; }
        }

        private String cardName;
        public String CardName
        {
            set { this.cardName = value; }
            get { return this.cardName; }
        }

        private String approvedAmount;
        public String ApprovedAmount
        {
            set { this.approvedAmount = value; }
            get { return this.approvedAmount; }
        }

        private String approvedTime;
        public String ApprovedTime
        {
            set { this.approvedTime = value; }
            get { return this.approvedTime; }
        }
    }

}
