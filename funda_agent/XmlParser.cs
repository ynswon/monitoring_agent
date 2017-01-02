using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;

using System.Collections;
namespace funda_agent
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

                        //case "cardCompanyCode":
                        //    string ccCode = item.InnerText;
                        //    int codeVal=0;
                        //    XmlAttribute ccAttr = item.Attributes["num"];
                        //    if (ccAttr != null)
                        //        codeVal = int.Parse(ccAttr.Value);

                        //    xmlData.setCardCompanyCode(ccCode, codeVal);
                        //    break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("parseNodesList error : " + e.Message.ToString());
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
}
