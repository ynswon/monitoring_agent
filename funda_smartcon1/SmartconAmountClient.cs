using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;
using System.Collections;
using System.Xml;
using System.IO;

namespace funda_smartcon1
{

    class SmartconAmountClient
    {
        public string gResultXml = "";
        public string gErrorCode = "";
        public string gStatusCode = "";
        public string gCompanyName = "";

        String m_shopCode = "";
        String m_shopName = "인증테스트";
        String EXCHANGEID = "";

        public string APIBASE; // = "http://smart.gifticard.com:8888/posauthinter/authreq.gc";

        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public ArrayList acceptLIst = new ArrayList();

        public static String SELL_PRICE;

        public SmartconAmountClient(String APIURL, String ExchangeId, String shopName, String shopCode, String companyName)
        {
            APIBASE = APIURL + "/posauthinter/authreq.gc";
            EXCHANGEID = ExchangeId;
            m_shopCode = shopCode;
            m_shopName = shopName;
            gCompanyName = companyName;

            dictionary.Add("ER000", "금액권 번호 불일치");
            dictionary.Add("ER701", "스마트콘 연동 프로토콜 버전이 일치하지 않습니다.");
            dictionary.Add("ER702", "이미 교환");
            dictionary.Add("ER703", "사용 기간이 경과");
            dictionary.Add("ER704", "잘못된데이터를수신");
            dictionary.Add("ER705", "미사용 금액권 번호");
            dictionary.Add("ER706", "이미 교환취소");
            dictionary.Add("ER707", "서버 장애 발생");
            dictionary.Add("ER708", "해당하는 카드 승인번호가 미존재");
            dictionary.Add("ER709", "미교환상태여서 교환취소 불가");
            dictionary.Add("ER710", "교환취소 가능기간이 지나서 교환 불가");
            dictionary.Add("ER711", "입력 승인번호 오류");
            dictionary.Add("ER712", "금액상품권의 잔액이 부족");
        }

        public string getProductName(String cc)
        {
            return "";
            /*
            HttpAPIs httpAPI = new HttpAPIs();
            iniUtil ini = new iniUtil("");
            String url = "http://b2b.giftsmartcon.com/coupon/WebCardExList.sc"
                + "?PINNUMBER=" + couponCode
                + "&SUPID=" + EXCHANGEID
                + "&FRANCHISE_ID=" + ini.ReadRegistry("companyCode");

            gResultXml = httpAPI.GetServerResponse(new Uri(url));
            if (gResultXml.Contains("euc-kr"))
            {
                System.Text.Encoding euckr = System.Text.Encoding.GetEncoding(51949);
                byte[] euckrBytes = System.Web.HttpUtility.UrlDecodeToBytes(gResultXml);
                gResultXml = euckr.GetString(euckrBytes);
            }

            using (XmlReader reader = XmlReader.Create(new StringReader(gResultXml)))
            {

                while (reader.ReadToFollowing("LOGINFO"))
                {
                    reader.ReadToFollowing("EXCHANGE_DATE");
                    String useDate = reader.ReadElementContentAsString();
                }
            }*/
        }

        public string Lookup(String couponCode)
        {

            //String getProductName(couponCode);
            String resultString = "";

            HttpAPIs httpAPI = new HttpAPIs();

            iniUtil ini = new iniUtil("");
            String url = APIBASE
                + "?FUNC_CODE=7000"
                + "&PRD_COMPANY_ID=" + EXCHANGEID
                + "&AUTH_DATE=" + System.DateTime.Now.ToString("yyyyMMdd")
                + "&AMT_NO=" + couponCode
                + "&FRANCHISE_ID=" + Machine.ini.IniReadValue("SMARTCON", "companyCode")
                + "&FRANCHISE_NM=" + System.Web.HttpUtility.UrlEncode(m_shopName, System.Text.Encoding.GetEncoding("euc-kr"));


            /*
             * http://smart.gifticard.com:8888/posauthinter/authreq.gc?FUNC_CODE=7000&PRD_
COMPANY_ID=RC0046&AUTH_DATE=20150504&AMT_NO=003908979764&FRANCHISE_ID=smartc
on_webauth&FRANCHISE_NM=smartcon_webauth
             */
            WebClient WClient = new WebClient();
            WClient.Encoding = System.Text.Encoding.GetEncoding("euc-kr");
            String data = WClient.DownloadString(url);



            /*
            gResultXml = httpAPI.GetServerResponse(new Uri(url));
            System.Text.Encoding euckr = System.Text.Encoding.GetEncoding("euc-kr");
            byte[] euckrBytes = System.Web.HttpUtility.UrlDecodeToBytes(gResultXml);
            gResultXml = euckr.GetString(euckrBytes);
            */
            gResultXml = data;

            gErrorCode = getXmlItemValue("ERROR_CODE");
            if (gErrorCode.Equals("ER000"))
            {
                SELL_PRICE = getXmlItemValue("SELL_PRICE");
                resultString = gCompanyName + " " + SELL_PRICE + "원권\r\n" + "잔액 " + getXmlItemValue("BALANCE") + "원";
                /*
                    +"\r\n"
                    + "금액을 입력하세요.";
                 * */
            }
            else
            {
                if (dictionary.ContainsKey(gErrorCode))
                {
                    resultString = dictionary[gErrorCode];
                }
                else
                {
                    resultString = "알 수 없는 에러 발생(" + gErrorCode + ")";
                }
            }

            return resultString;
        }

        public string Accept(String couponCode, String amount)
        {
            String resultString = "";

            iniUtil ini = new iniUtil("");
            HttpAPIs httpAPI = new HttpAPIs();
            String url = APIBASE
                + "?FUNC_CODE=7001"
                + "&PRD_COMPANY_ID=" + EXCHANGEID
                + "&AUTH_DATE=" + System.DateTime.Now.ToString("yyyyMMdd")
                + "&AMT_NO=" + couponCode
                + "&USE_AMT=" + amount
                + "&FRANCHISE_ID=" + Machine.ini.IniReadValue("SMARTCON", "companyCode")
                + "&FRANCHISE_NM=" + System.Web.HttpUtility.UrlEncode(m_shopName, System.Text.Encoding.GetEncoding("euc-kr"));

            gResultXml = httpAPI.GetServerResponse(new Uri(url));
            System.Text.Encoding euckr = System.Text.Encoding.GetEncoding(51949);
            byte[] euckrBytes = System.Web.HttpUtility.UrlDecodeToBytes(gResultXml);
            gResultXml =  euckr.GetString(euckrBytes);

           gErrorCode = getXmlItemValue("ERROR_CODE");
            if (gErrorCode.Equals("ER000"))
            {
                resultString = gCompanyName + " " + SELL_PRICE + "원권\r\n" + "잔액 " + getXmlItemValue("BALANCE") + "원";
                /*
                    +"\r\n"
                    + "금액을 입력하세요.";
                 * */
            }
            else
            {
                if (dictionary.ContainsKey(gErrorCode))
                {
                    resultString = dictionary[gErrorCode];
                }
                else
                {
                    resultString = "알 수 없는 에러 발생(" + gErrorCode + ")";
                }
            
            }

            return resultString;
        }

        public string AcceptList(String couponCode)
        {
            HttpAPIs httpAPI = new HttpAPIs();
            iniUtil ini = new iniUtil("");
            String url = "http://b2b.giftsmartcon.com/coupon/WebCardExList.sc"
                + "?PINNUMBER=" + couponCode
                + "&SUPID=" + EXCHANGEID
                + "&FRANCHISE_ID=" + Machine.ini.IniReadValue("SMARTCON", "companyCode");

            gResultXml = httpAPI.GetServerResponse(new Uri(url));
            if (gResultXml.Contains("euc-kr"))
            {
                System.Text.Encoding euckr = System.Text.Encoding.GetEncoding(51949);
                byte[] euckrBytes = System.Web.HttpUtility.UrlDecodeToBytes(gResultXml);
                gResultXml = euckr.GetString(euckrBytes);
            }

            // 넘어온 result를 배열로 관리한다.
            acceptLIst.Clear();
            using (XmlReader reader = XmlReader.Create(new StringReader(gResultXml)))
            {

                while (reader.ReadToFollowing("LOGINFO"))
                {
                    reader.ReadToFollowing("EXCHANGE_DATE");
                    String useDate = reader.ReadElementContentAsString();

                    reader.ReadToFollowing("EXCHANGE_NUM");
                    String EXCHANGENUM = reader.ReadElementContentAsString();

                    reader.ReadToFollowing("INTERCHANGE_STATUS");
                    String INTERCHANGE_STATUS = reader.ReadElementContentAsString();

                    reader.ReadToFollowing("USE_AMOUNT");
                    String USEAMOUNT = reader.ReadElementContentAsString();

                    if (INTERCHANGE_STATUS.Equals("1"))
                    {
                        AcceptItem item = new AcceptItem();
                        item.useDate = useDate;
                        item.amount = USEAMOUNT;
                        item.EXCHANGENUM = EXCHANGENUM;

                        acceptLIst.Add(item);
                    }
                }

                
            }

            return gResultXml;
        }

        public string Cancel(String couponCode, String EXCHANGENUM)
        {
            String resultString = "";

            HttpAPIs httpAPI = new HttpAPIs();
            iniUtil ini = new iniUtil("");
            String url = APIBASE
                + "?FUNC_CODE=7002"
                + "&PRD_COMPANY_ID=" + EXCHANGEID
                + "&AUTH_DATE=" + System.DateTime.Now.ToString("yyyyMMdd")
                + "&AMT_NO=" + couponCode
                + "&AUTH_NO=" + EXCHANGENUM
                + "&FRANCHISE_ID=" + Machine.ini.IniReadValue("SMARTCON","companyCode") 
                + "&FRANCHISE_NM=" + System.Web.HttpUtility.UrlEncode(m_shopName, System.Text.Encoding.GetEncoding("euc-kr"));

            gResultXml = httpAPI.GetServerResponse(new Uri(url));

            gErrorCode = getXmlItemValue("ERROR_CODE");
            if (gErrorCode.Equals("ER000"))
            {
                resultString = gCompanyName + " " + SELL_PRICE + "원권\r\n" + "잔액 " + getXmlItemValue("BALANCE") + "원";
                /*
                    +"\r\n"
                    + "금액을 입력하세요.";
                 * */
            }
            else
            {
                if (dictionary.ContainsKey(gErrorCode))
                {
                    resultString = dictionary[gErrorCode];
                }
                else
                {
                    resultString = "알 수 없는 에러 발생(" + gErrorCode + ")";
                }

            }

            return resultString;
        }

        
        public String getXmlItemValue(String key)
        {
            String strVal = "";

            using (XmlReader reader = XmlReader.Create(new StringReader(gResultXml)))
            {
                reader.ReadToFollowing(key);

                try
                {
                    strVal = reader.ReadElementContentAsString();
                }
                catch (Exception ee)
                {
                    System.Console.WriteLine(ee.ToString());
                }
            }

            return strVal;
        }



    }


}
