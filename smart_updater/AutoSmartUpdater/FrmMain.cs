using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using UpdateChecker;

namespace AutoSmartUpdater
{
    public partial class FrmMain : Form
    {
        private string UPDATE_SERVER_URL = "http://res.9flava.com/posupdate/";
        private string UPDATE_FILE_URL = "filelist.txt";
        private string PROCESS_NAME = "FundaAgent.exe";

        private List<string> m_downloadFileList;         // 서버로부터 다운로드 받을 파일들
        private WebClient m_updateServerObject;         // 업데이트 서버 
        private Uri m_updateServerURL;                  // 업데이트 서버 주소
        private Uri m_updateFileURL;                    // 업데이트 정보가 포함된 파일 주소
        private Stream m_stream;                        // 스트림
        private DirectoryInfo m_programDirectory;       // 프로그램 디렉토리        
        private int m_nDownloadFileIndex = 0;           // 다운로드 파일 인덱스
        private bool m_bCanceld = false;

        private CUpdateChecker updateChecker = null;
        public bool IsAdministrator()
        {
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            if (null != identity)
            {
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }
            return false;
        }
        public FrmMain()
        {
            InitializeComponent();
            if (IsAdministrator())
            {

                int p;
                p = 0;
            }
            try
            {
                if (versionCheck())
                {
                    //ReadINIFile();

                    // 초기화
                    m_updateServerObject = new WebClient();
                    m_updateServerObject.OpenReadCompleted += new OpenReadCompletedEventHandler(m_updateServerObject_OpenReadCompleted);
                    m_updateServerObject.DownloadProgressChanged += new DownloadProgressChangedEventHandler(m_updateServerObject_DownloadProgressChanged);
                    m_updateServerObject.DownloadFileCompleted += new AsyncCompletedEventHandler(m_updateServerObject_DownloadFileCompleted);

                    //m_updateServerURL = new Uri(UPDATE_SERVER_URL);
                    //m_updateFileURL = new Uri(UPDATE_SERVER_URL + UPDATE_FILE_URL);

                    // 프로그램 설치 경로에 다운로드
                    m_programDirectory = new DirectoryInfo(Application.StartupPath);
                    PROCESS_NAME = updateChecker.GetExecutionFileName();
                    TotalDownloadProgressBar.Value = 0;
                    PartialDownloadProgressBar.Value = 0;
                }
                else
                {
                    //실행파일 실행
                    String exeName;
                    exeName = Application.StartupPath + "\\" + updateChecker.GetExecutionFileName();
                    Process.Start(Application.StartupPath + "\\" + updateChecker.GetExecutionFileName());
                }
            }
            catch
            {
                //실행 파일이 없는 경우 error
            }

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //ConnectToUpdateServer();
            DownloadUpdateFile();
        }

        private bool versionCheck()
        {
            bool isUpdate = false;
            updateChecker = new CUpdateChecker();
            //update를 위한 xml정보를 가져온다.
            if (updateChecker.DownloadXmlFromConfigUrl())
            {                
                if (updateChecker.hasNewUpdateExists())
                {
                    m_downloadFileList = updateChecker.GetUpdateList();
                    if (m_downloadFileList.Count > 0)
                    {
                        isUpdate = true;
                    }
                    else
                    {
                        isUpdate = false;
                    }
                }
                else
                {
                    isUpdate = false;
                }
            }
            return isUpdate;
        }
        private void ReadINIFile()
        {
            try
            {
                FileStream fs = new FileStream(Application.StartupPath + @"\start.ini", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);

                UPDATE_SERVER_URL = sr.ReadLine();
                UPDATE_FILE_URL = sr.ReadLine();
                PROCESS_NAME = sr.ReadLine();

                sr.Close();
                fs.Close();
            }
            catch
            {
                MessageBox.Show("Failed to read a INI file!");
            }
        }

        /// <summary>
        /// 업데이트 서버와 연결을 시도한다.
        /// </summary>
        private void ConnectToUpdateServer()
        {
            try
            {
                SetStatus(0);

                m_updateServerObject.OpenReadAsync(m_updateFileURL);                      
            }
            catch
            {
                MessageBox.Show("Please check yout network connection is reachable.");
            }
        }

        /// <summary>
        /// 비동기적으로 업데이트 서버로부터 스트림을 읽어오는 작업의 완료 이벤트를 처리한다.        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_updateServerObject_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            try
            {
                // 읽어온 스트림을 체크한다.
                m_stream = e.Result;

                // 업데이트 정보를 확인한다.
                if (m_stream != null)
                    CheckUpdateInfo();

            }
            catch
            {
                MessageBox.Show("Failed to connect the update server.");

                Application.Exit();
            }
        }

        /// <summary>
        /// 서버로부터 업데이트 정보를 확인한다.
        /// </summary>
        private void CheckUpdateInfo()
        {
            try
            {
                SetStatus(1);

                StreamReader sr = new StreamReader(m_stream);
                m_downloadFileList = new List<string>();

                // 모든 스트림을 읽는다.
                while (!sr.EndOfStream)
                    m_downloadFileList.Add(sr.ReadLine().Trim());

                sr.Close();

                DownloadUpdateFile();
            }
            catch
            {
                MessageBox.Show("Failed to check the update information.");

                Application.Exit();
            }
        }

        /// <summary>
        /// 업데이트 파일들을 다운로드 받는다.
        /// </summary>
        private void DownloadUpdateFile()
        {
            try
            {
                SetStatus(2);                
                
                TotalDownloadProgressBar.Step = (int)(100 / m_downloadFileList.Count);

                Uri updateFileURL = new Uri(m_downloadFileList[m_nDownloadFileIndex]);
                String fileName = getFileName(m_downloadFileList[m_nDownloadFileIndex]);
                m_updateServerObject.DownloadFileAsync(updateFileURL, m_programDirectory + "\\" + fileName);

                LbFileName.Text = "File name: " + fileName;
                LbStatus.Text = "Overall: " + (m_nDownloadFileIndex + 1).ToString() + "/" +
                    m_downloadFileList.Count;
                
            }
            catch
            {
                //MessageBox.Show("Failed to download the files.");
                Application.Exit();
            }
        }
        private String getFileName(String url)
        {       
            String fileName = null;
            if (url != null)
            {
                String[] strArr = url.Split('/');
                if (strArr.Length > 0)
                {
                    fileName = strArr[strArr.Length - 1];
                }
            }
            return fileName;
        }
        private void DownloadFileAsync(Uri updateFileURL, String path)
        {
            m_updateServerObject.DownloadFileAsync(updateFileURL, path);
        }
        /// <summary>
        /// 프로그램의 다운로드 진행 상황을 프로그레스 바에 업데이트 한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_updateServerObject_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                PartialDownloadProgressBar.Value = e.ProgressPercentage;
                LbFileTransfer.Text = "Downloading: " + e.BytesReceived.ToString() + " bytes /" +
                    e.TotalBytesToReceive.ToString() + " bytes";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 파일 전송을 완료하였다면..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_updateServerObject_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                m_nDownloadFileIndex++;

                if (m_nDownloadFileIndex < m_downloadFileList.Count)
                {
                    TotalDownloadProgressBar.PerformStep();

                    PartialDownloadProgressBar.Value = 0;

                    Uri updateFileURL = new Uri(m_downloadFileList[m_nDownloadFileIndex]);
                    String fileName = getFileName(m_downloadFileList[m_nDownloadFileIndex]);
                    LbFileName.Text = "File name: " + fileName;
                    LbStatus.Text = "Overall: " + (m_nDownloadFileIndex + 1).ToString() + "/" +
                        m_downloadFileList.Count;

                    m_updateServerObject.DownloadFileAsync(updateFileURL, m_programDirectory + "\\" + fileName);
                }
                else
                {
                    TotalDownloadProgressBar.PerformStep();
                    LbStatus.Text = "Overall: " + m_nDownloadFileIndex.ToString() + "/" +
                        m_downloadFileList.Count;

                    SetStatus(3);
                    BtnExit.Enabled = true;

                    // 취소되지 않았다면.!!
                    if (!m_bCanceld)
                    {
                        if (chckAutoProgramStart.Checked)
                            Process.Start(m_programDirectory + @"\" + PROCESS_NAME);
                    }

                    Application.Exit();
                }                
            }
            else
            {
                MessageBox.Show("Failed to update completely.");

                m_bCanceld = true;
                BtnExit.Enabled = true;
            }
        }

        // 로그 파일을 생성한다.
        private bool GenerateLogFile()
        {
            try
            {
                FileStream fs = new FileStream(m_programDirectory + @"\update_log.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine("Update info:");
                sw.WriteLine("Update date: {0}", DateTime.Today.ToLongDateString());
                sw.WriteLine("Update files:");

                for (int i = 0; i < m_downloadFileList.Count; i++)
                    sw.WriteLine("{0}", m_downloadFileList[i]);

                sw.Close();
                fs.Close();
            }
            catch
            {
                MessageBox.Show("Failed to create the log file.");

                return false;
            }

            return true;
        }


        /// <summary>
        /// 폼이 닫히기 전에 이벤트를 처리한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 업데이트 서버로부터 데이터를 읽어오던 스트림을 닫는다.
            if (m_stream != null)
                m_stream.Close();

            // 업데이트 서버를 해제한다.
            if(m_updateServerObject != null)
                m_updateServerObject.Dispose();

            // 업데이트 정보를 읽는다.
            //if (ChkShowUpgradeInfo.Checked)
            //{
            //    if (GenerateLogFile())
            //    {
            //        ProcessStartInfo pInfo = new ProcessStartInfo(m_programDirectory + @"\update_log.txt");

            //        pInfo.Verb = "open";

            //        Process.Start(pInfo);
            //    }
            //}
        }

        /// <summary>
        /// 진행 상황에 따라 업데이트 표시
        /// </summary>
        /// <param name="option">0: 연결, 1: 버전 확인, 2: 다운로드, 3: 완료</param>
        private void SetStatus(int option)
        {
            switch (option)
            {
                case 0:
                    LbConnecting.Enabled = true;
                    LbChecking.Enabled = false;
                    LbDownloading.Enabled = false;
                    LbComplete.Enabled = false;
                    break;
                case 1:
                    LbConnecting.Enabled = false;
                    LbChecking.Enabled = true;
                    LbDownloading.Enabled = false;
                    LbComplete.Enabled = false;
                    break;
                case 2:
                    LbConnecting.Enabled = false;
                    LbChecking.Enabled = false;
                    LbDownloading.Enabled = true;
                    LbComplete.Enabled = false;
                    break;
                default:
                    LbConnecting.Enabled = false;
                    LbChecking.Enabled = false;
                    LbDownloading.Enabled = false;
                    LbComplete.Enabled = true;
                    break;
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            // 취소되지 않았다면.!!
            if (!m_bCanceld)
            {
                if (chckAutoProgramStart.Checked)
                    Process.Start(m_programDirectory + @"\" + PROCESS_NAME);
            }

            Application.Exit();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            m_updateServerObject.CancelAsync();            
        }
    }
}