using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

using System.Windows.Forms;


using System.Diagnostics;
using AutoSmartUpdater;


namespace UpdateChecker
{
    /// <summary>
    /// 새로운 업데이트가 존재하는지 체크한다.
    ///   
    /// 2009.07.29 - Yong-Min Park
    /// 
    /// </summary>
    public class CUpdateChecker
    {
        private string updateServerAddress = "http://14.63.215.51:880/update_funda_agent/";                               // 업데이트 서버 주소
        
        private WebClient webClient = null;                                                                                      // 업데이트 서버와 연결하여 버전 정보를 읽어오기 위한 인스턴스
        private string LastError = string.Empty;                                                                                  // 에러 정보

        private String configFilePath = Application.StartupPath + "/config.ini";
        private IniFile config;
        private XmlDom mXmlDom;

        private String executionFileName = null;
        private List<string> m_downloadFileList = null;

        public CUpdateChecker()
        {
            webClient = new WebClient();
            config = new IniFile(configFilePath);
            mXmlDom = new XmlDom();
            m_downloadFileList = new List<string>();
        }
        
        public String GetExecutionFileName()
        {
            if (executionFileName == null) executionFileName = "FundaAgent.exe";

            return executionFileName;
        }
        

        /// <summary>
        /// Configuration File에서 url을 읽어온다
        /// </summary>
        /// <param name="FileName">업데이트 서버 주소로 부터 XML을 받아온다</param>
        /// <returns></returns>
        public bool DownloadXmlFromConfigUrl()
        {
            try
            {
                string updateUrl = config.IniReadValue("update", "url");
                string branchId = config.IniReadValue("shop", "branchid");
                string type = config.IniReadValue("shop", "type");

                if (type.Equals("")) type = "default";                
                if(!updateUrl.Equals("")) updateServerAddress = updateUrl;

                WebClient WClient = new WebClient();
                string downloadUrl = updateServerAddress + "?bid=" + branchId + "&type=" + type;
                downloadUrl = updateServerAddress + "updateinfo.xml"; 
                WClient.DownloadFile(downloadUrl, Environment.CurrentDirectory + @"\updateinfo.xml");
                
                //versionInfoFileName = sr.ReadLine().Trim();
                mXmlDom.ReadXMLFromFile("updateinfo.xml");
                
            }
            catch(Exception ee)
            {
               return false;
            }
            return true;
        }

        public List<string> GetUpdateList()
        {

            return mXmlDom.GetItemListByTagName("file");
        }
        public bool hasNewUpdateExists()
        {
            bool isUpdate = false;
            try
            {                
                executionFileName = mXmlDom.GetInnerTextByTagName("executionfile");
                String newVersionStr = mXmlDom.GetInnerTextByTagName("version");
                String isBackupStr = mXmlDom.GetAttributeValue("version", "isbackup");
                bool isBackup = false;
                if(!isBackupStr.Equals("")) isBackup = Boolean.Parse(isBackupStr);
                if (isBackup == true)
                {
                    isUpdate =  true;
                }
                else
                {
                    Version newVer = new Version(newVersionStr);

                    //execution file이 없는 경우의 처리
                    FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(Application.StartupPath + "\\" + executionFileName);
                    Version curVer = new Version(myFileVersionInfo.FileVersion);
                    if (newVer.CompareTo(curVer) > 0)
                        isUpdate =  true;
                    else
                        isUpdate =  false;
                }                
            }
            catch
            {
                LastError = "버전 정보를 확인할 수 없습니다.";                
            }

            return isUpdate ;
        }


        /// <summary>
        /// 에러 정보를 반환한다.
        /// </summary>
        /// <returns></returns>
        public string GetLastError() { return LastError; }

    } // end of class
} // end of namespace
