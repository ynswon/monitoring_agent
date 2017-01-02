using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Windows.Forms;
namespace funda_agent
{
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
