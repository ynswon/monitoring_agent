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

    class SmartconClient
    {
        public string gResultXml = "";
        public string gErrorCode = "";
        public string gStatusCode = "";

        String m_shopCode = "";
        String m_shopName = "인증테스트";
        String EXCHANGEID = "";


        const string VERSION = "0001";
        //public string APIBASE;  // = "http://b2b.giftsmartcon.com/edenredauth/smartconposauthproc.sc";
        /*
         * 안녕하세요 스마트콘 이봉기입니다.
            연동 규격서상 현재 URL 을 하기의
            URL로 변경 부탁 드립니다.
 
            변경 전 : http://b2b.giftsmartcon.com/edenredauth/smartconposauthproc.sc
            변경 후 : http://183.111.10.91:17080/edenredauth/smartconposauthproc.sc
         */
        public string APIBASE = "http://183.111.10.91:17080/edenredauth/smartconposauthproc.sc";

        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public SmartconClient(String APIURL, String ExchangeId, String shopName, String shopCode)
        {
            APIBASE = APIURL+"/edenredauth/smartconposauthproc.sc";
            APIBASE = "http://183.111.10.91:17080/edenredauth/smartconposauthproc.sc";
            EXCHANGEID = ExchangeId;
            m_shopCode = shopCode;
            m_shopName = shopName;

            dictionary.Add("E0000", "이미 사용된 상품입니다.");
            dictionary.Add("E0001", "스마트콘 연동 프로토콜 버전이 일치하지 않습니다.");
            dictionary.Add("E0002", "스마트콘에서 전송한 쿠폰 번호가 일치하지 않습니다.");
            dictionary.Add("E0003", "이 쿠폰은 본 매장에서 사용할 수 없습니다.");
            dictionary.Add("E0004", "쿠폰번호검증에 실패 했습니다. 다시 확인하시기 바랍니다.");
            dictionary.Add("E0005", "쿠폰 번호가 유효하지 않습니다.\n다시 확인하시기 바랍니다.");
//            dictionary.Add("E0006", "이미 20{0}년 {1}월 {2}일 {3}시 {4}분에\r\n{5}지점에서 사용된 쿠폰입니다.");
            dictionary.Add("E0006", "이미 20{0}년 {1}월 {2}일 {3}시 {4}분에\r\n사용된 쿠폰입니다.");
            dictionary.Add("E0007", "사용 기간이 경과 되어 사용할 수 없는 쿠폰입니다.");
            dictionary.Add("E0008", "잘못된 데이터를 수신 했습니다. 다시 시도해 주시기 바랍니다.");
            dictionary.Add("E0009", "사용 가능한 상품입니다.");
            dictionary.Add("E0010", "교환 취소된 쿠폰입니다.");
            dictionary.Add("E0011", "교환당일이 아니면 반품이 불가능합니다.");
            dictionary.Add("E0012", "수신번호가 일치하지 않습니다.");
            dictionary.Add("E0013", "헤더의 수신자번호처리 타입이 제휴사관련 동록 내용과  다릅니다.");
            dictionary.Add("E0100", "서버에서 에러가 발생했습니다.\r\n잠시 후 다시 시도해 주세요.");
            dictionary.Add("E0101", "해당하는 승인번호가 존재하지 않습니다.");
            dictionary.Add("E0102", "미 교환 상태여서 교환취소를 할 수 없습니다..");
            dictionary.Add("E0103", "해당 제휴사에서 교환되지 않았으므로 교환취소 할 수 없습니다.");
            dictionary.Add("E0104", "반품(교환취소)가능기간이 지나서 반품이 불가능합니다.");
            dictionary.Add("E0105", "망 취소가 정상 처리 되었습니다.");
            dictionary.Add("E0106", "입력 승인번호 오류입니다.");
            dictionary.Add("E0107", "교환처리 지점 코드가  일치하지 않습니다.");
        }

        public string GetProuctName(String couponCode)
        {
            string productName = "";

            String brName = ConvertToEucKr(m_shopName);
            HttpAPIs httpAPI = new HttpAPIs();
            iniUtil ini = new iniUtil("");
            String url = "http://b2b.giftsmartcon.com/coupon/WebPinCheck.sc"
                + "?PINNUMBER=" + couponCode
                + "&SUPID=" + m_shopCode
                + "&BRANCH_CODE=" + Machine.ini.IniReadValue("SMARTCON", "companyCode")
                + "&BRANCH_NAME=" + brName;

            gResultXml = httpAPI.GetServerResponse(new Uri(url));
            //System.Text.Encoding euckr = System.Text.Encoding.GetEncoding(51949);
            //byte[] euckrBytes = System.Web.HttpUtility.UrlDecodeToBytes(gResultXml);
            //gResultXml = euckr.GetString(euckrBytes);

            productName = getXmlItemValue("ProdName");

            return productName;
        }

        public string Lookup(String couponCode)
        {
            String resultString = "";
            String brName = ConvertToEucKr(m_shopName);

            HttpAPIs httpAPI = new HttpAPIs();
            String url = APIBASE
                + "?MsgSubCode=100"
                + "&supID=" + m_shopCode
                + "&CouponNum=" + couponCode
                + "&BranchCode=" + EXCHANGEID
                + "&BranchName=" + brName
                + "&PosCode=1"
                + "&PosDate=" + System.DateTime.Now.ToString("yyyyMMdd")
                + "&PosTime=" + System.DateTime.Now.ToString("hhmmss")
                + "&notuseSup=1";

            gResultXml = httpAPI.GetServerResponse(new Uri(url));
            //System.Text.Encoding euckr = System.Text.Encoding.GetEncoding(51949);
            System.Text.Encoding myencoding = System.Text.Encoding.UTF8;
            byte[] euckrBytes = System.Web.HttpUtility.UrlDecodeToBytes(gResultXml);
            gResultXml = myencoding.GetString(euckrBytes);


            gStatusCode = getXmlItemValue("StatusCode");
            if (gStatusCode.Equals("000"))
            {
                String productName = GetProuctName(couponCode);

                resultString = productName + "\r\n정상 상품입니다.";
            }
            else
            {
                gErrorCode = getXmlItemValue("ErrorCode");
                if (dictionary.ContainsKey(gErrorCode))
                {
                    resultString = dictionary[gErrorCode];
                    if (gErrorCode.Equals("E0006"))
                    {
                        String usedDate = getXmlItemValue("UsedDate");
                        String usedTime = getXmlItemValue("UsedTime");
                        String usedBranchName = getXmlItemValue("UsedBranchName");
                        resultString = String.Format(resultString
                            , usedDate.Substring(0, 2)
                            , usedDate.Substring(2, 2)
                            , usedDate.Substring(4, 2)
                            , usedTime.Substring(0, 2)
                            , usedTime.Substring(2, 2)
                            //, usedBranchName);
                            );
                    }
                }
                else
                {
                    resultString = "알 수 없는 에러 발생(" + gErrorCode + ")";
                }
            }

            return resultString;
        }

        public string Accept(String couponCode)
        {
            String resultString = "";
            String brName = ConvertToEucKr(m_shopName);

            HttpAPIs httpAPI = new HttpAPIs();
            String url = APIBASE
                + "?MsgSubCode=101"
                + "&supID=" + m_shopCode
                + "&CouponNum=" + couponCode
                + "&BranchCode=" + EXCHANGEID
                + "&BranchName=" + brName
                + "&PosCode=1"
                + "&PosDate=" + System.DateTime.Now.ToString("yyyyMMdd")
                + "&PosTime=" + System.DateTime.Now.ToString("hhmmss")
                + "&notuseSup=1";

            gResultXml = httpAPI.GetServerResponse(new Uri(url));
            System.Text.Encoding euckr = System.Text.Encoding.UTF8;
            byte[] euckrBytes = System.Web.HttpUtility.UrlDecodeToBytes(gResultXml);
            gResultXml =  euckr.GetString(euckrBytes);

            gStatusCode = getXmlItemValue("StatusCode");
            if (gStatusCode.Equals("000"))
            {
                String productName = GetProuctName(couponCode);

                resultString = productName + "\r\n정상 승인되었습니다.";
            }
            else
            {
                gErrorCode = getXmlItemValue("ErrorCode");
                if (dictionary.ContainsKey(gErrorCode))
                {
                    resultString = dictionary[gErrorCode];
                    if (gErrorCode.Equals("E0006"))
                    {
                        String usedDate = getXmlItemValue("UsedDate");
                        String usedTime = getXmlItemValue("UsedTime");
                        String usedBranchName = getXmlItemValue("UsedBranchName");
                        resultString = String.Format(resultString
                            , usedDate.Substring(0, 2)
                            , usedDate.Substring(2, 2)
                            , usedDate.Substring(4, 2)
                            , usedTime.Substring(0, 2)
                            , usedTime.Substring(2, 2)
                            //, usedBranchName);
                            );

                    }
                }
                else
                {
                    resultString = "알 수 없는 에러 발생(" + gErrorCode + ")";
                }
            }

            return resultString;
        }

        public string Cancel(String couponCode, String admitNum)
        {
            String resultString = "";

            HttpAPIs httpAPI = new HttpAPIs();
            String url = APIBASE
                + "?MsgSubCode=102"
                + "&supID=" + m_shopCode
                + "&CouponNum=" + couponCode
                + "&BranchCode=" + EXCHANGEID
                + "&BranchName=" + System.Web.HttpUtility.UrlEncode(m_shopName, System.Text.Encoding.GetEncoding("euc-kr"))
                + "&PosCode=1"
                + "&PosDate=" + System.DateTime.Now.ToString("yyyyMMdd")
                + "&PosTime=" + System.DateTime.Now.ToString("hhmmss")
                + "&notuseSup=1";
                //+ "&AdmitNum=" + admitNum;

            gResultXml = httpAPI.GetServerResponse(new Uri(url));

            gStatusCode = getXmlItemValue("StatusCode");
            if (gStatusCode.Equals("000"))
            {
                String productName = GetProuctName(couponCode);

                resultString = productName + "\r\n정상 취소되었습니다.";
            }
            else
            {
                gErrorCode = getXmlItemValue("ErrorCode");
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

                strVal = reader.ReadElementContentAsString();
            }

            return strVal;
        }

        public string ConvertToEucKr(string str)
        {
            Encoding euckr = Encoding.GetEncoding(51949);
            byte[] tmp = euckr.GetBytes(str);
            string res = "";
            foreach (byte b in tmp)
            {
                res += "%";
                res += string.Format("{0:X}", b);
            }
            return res;
            //return euckr.GetString(tmp);
        }

    }
}
