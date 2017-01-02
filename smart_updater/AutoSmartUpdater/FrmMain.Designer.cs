namespace AutoSmartUpdater
{
    partial class FrmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.ChkShowUpgradeInfo = new System.Windows.Forms.CheckBox();
            this.LbStatus = new System.Windows.Forms.Label();
            this.LbFileTransfer = new System.Windows.Forms.Label();
            this.LbFileName = new System.Windows.Forms.Label();
            this.LbChecking = new System.Windows.Forms.Label();
            this.LbConnecting = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LbComplete = new System.Windows.Forms.Label();
            this.LbDownloading = new System.Windows.Forms.Label();
            this.Status1 = new System.Windows.Forms.Panel();
            this.TitlePanel = new System.Windows.Forms.Panel();
            this.BtnExit = new System.Windows.Forms.Button();
            this.TotalDownloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.PartialDownloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.chckAutoProgramStart = new System.Windows.Forms.CheckBox();
            this.StatusPanel.SuspendLayout();
            this.TitlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkShowUpgradeInfo
            // 
            this.ChkShowUpgradeInfo.AutoSize = true;
            this.ChkShowUpgradeInfo.Location = new System.Drawing.Point(13, 220);
            this.ChkShowUpgradeInfo.Name = "ChkShowUpgradeInfo";
            this.ChkShowUpgradeInfo.Size = new System.Drawing.Size(95, 16);
            this.ChkShowUpgradeInfo.TabIndex = 14;
            this.ChkShowUpgradeInfo.Text = "Readme file.";
            this.ChkShowUpgradeInfo.UseVisualStyleBackColor = true;
            // 
            // LbStatus
            // 
            this.LbStatus.AutoSize = true;
            this.LbStatus.Location = new System.Drawing.Point(13, 170);
            this.LbStatus.Name = "LbStatus";
            this.LbStatus.Size = new System.Drawing.Size(48, 12);
            this.LbStatus.TabIndex = 9;
            this.LbStatus.Text = "Overall:";
            // 
            // LbFileTransfer
            // 
            this.LbFileTransfer.AutoSize = true;
            this.LbFileTransfer.Location = new System.Drawing.Point(13, 125);
            this.LbFileTransfer.Name = "LbFileTransfer";
            this.LbFileTransfer.Size = new System.Drawing.Size(82, 12);
            this.LbFileTransfer.TabIndex = 8;
            this.LbFileTransfer.Text = "Downloading:";
            // 
            // LbFileName
            // 
            this.LbFileName.AutoSize = true;
            this.LbFileName.Location = new System.Drawing.Point(13, 106);
            this.LbFileName.Name = "LbFileName";
            this.LbFileName.Size = new System.Drawing.Size(65, 12);
            this.LbFileName.TabIndex = 6;
            this.LbFileName.Text = "File name:";
            // 
            // LbChecking
            // 
            this.LbChecking.AutoSize = true;
            this.LbChecking.Enabled = false;
            this.LbChecking.Location = new System.Drawing.Point(129, 35);
            this.LbChecking.Name = "LbChecking";
            this.LbChecking.Size = new System.Drawing.Size(117, 12);
            this.LbChecking.TabIndex = 1;
            this.LbChecking.Text = "Version Checking...";
            // 
            // LbConnecting
            // 
            this.LbConnecting.AutoSize = true;
            this.LbConnecting.Location = new System.Drawing.Point(16, 35);
            this.LbConnecting.Name = "LbConnecting";
            this.LbConnecting.Size = new System.Drawing.Size(81, 12);
            this.LbConnecting.TabIndex = 1;
            this.LbConnecting.Text = "Connecting...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(167, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nohungry Smart Updater";
            // 
            // StatusPanel
            // 
            this.StatusPanel.BackColor = System.Drawing.Color.White;
            this.StatusPanel.Controls.Add(this.panel3);
            this.StatusPanel.Controls.Add(this.panel2);
            this.StatusPanel.Controls.Add(this.panel1);
            this.StatusPanel.Controls.Add(this.LbComplete);
            this.StatusPanel.Controls.Add(this.LbDownloading);
            this.StatusPanel.Controls.Add(this.LbChecking);
            this.StatusPanel.Controls.Add(this.LbConnecting);
            this.StatusPanel.Controls.Add(this.Status1);
            this.StatusPanel.Location = new System.Drawing.Point(13, 41);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(485, 58);
            this.StatusPanel.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::AutoSmartUpdater.Properties.Resources.complete;
            this.panel3.Location = new System.Drawing.Point(422, 7);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(23, 24);
            this.panel3.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::AutoSmartUpdater.Properties.Resources.download;
            this.panel2.Location = new System.Drawing.Point(295, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(23, 24);
            this.panel2.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::AutoSmartUpdater.Properties.Resources.check;
            this.panel1.Location = new System.Drawing.Point(168, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(23, 24);
            this.panel1.TabIndex = 2;
            // 
            // LbComplete
            // 
            this.LbComplete.AutoSize = true;
            this.LbComplete.Enabled = false;
            this.LbComplete.Location = new System.Drawing.Point(395, 35);
            this.LbComplete.Name = "LbComplete";
            this.LbComplete.Size = new System.Drawing.Size(78, 12);
            this.LbComplete.TabIndex = 1;
            this.LbComplete.Text = "Completed...";
            // 
            // LbDownloading
            // 
            this.LbDownloading.AutoSize = true;
            this.LbDownloading.Enabled = false;
            this.LbDownloading.Location = new System.Drawing.Point(270, 35);
            this.LbDownloading.Name = "LbDownloading";
            this.LbDownloading.Size = new System.Drawing.Size(90, 12);
            this.LbDownloading.TabIndex = 1;
            this.LbDownloading.Text = "Downloading...";
            // 
            // Status1
            // 
            this.Status1.BackgroundImage = global::AutoSmartUpdater.Properties.Resources.connect;
            this.Status1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Status1.Location = new System.Drawing.Point(41, 7);
            this.Status1.Name = "Status1";
            this.Status1.Size = new System.Drawing.Size(23, 24);
            this.Status1.TabIndex = 0;
            // 
            // TitlePanel
            // 
            this.TitlePanel.BackColor = System.Drawing.Color.SlateGray;
            this.TitlePanel.Controls.Add(this.label1);
            this.TitlePanel.Location = new System.Drawing.Point(13, 10);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new System.Drawing.Size(485, 24);
            this.TitlePanel.TabIndex = 10;
            // 
            // BtnExit
            // 
            this.BtnExit.Enabled = false;
            this.BtnExit.Location = new System.Drawing.Point(397, 233);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(100, 25);
            this.BtnExit.TabIndex = 7;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // TotalDownloadProgressBar
            // 
            this.TotalDownloadProgressBar.Location = new System.Drawing.Point(12, 187);
            this.TotalDownloadProgressBar.Name = "TotalDownloadProgressBar";
            this.TotalDownloadProgressBar.Size = new System.Drawing.Size(485, 22);
            this.TotalDownloadProgressBar.TabIndex = 12;
            // 
            // PartialDownloadProgressBar
            // 
            this.PartialDownloadProgressBar.Location = new System.Drawing.Point(12, 142);
            this.PartialDownloadProgressBar.Name = "PartialDownloadProgressBar";
            this.PartialDownloadProgressBar.Size = new System.Drawing.Size(485, 22);
            this.PartialDownloadProgressBar.TabIndex = 11;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(293, 233);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(100, 25);
            this.BtnCancel.TabIndex = 15;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // chckAutoProgramStart
            // 
            this.chckAutoProgramStart.AutoSize = true;
            this.chckAutoProgramStart.Checked = true;
            this.chckAutoProgramStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckAutoProgramStart.Location = new System.Drawing.Point(13, 242);
            this.chckAutoProgramStart.Name = "chckAutoProgramStart";
            this.chckAutoProgramStart.Size = new System.Drawing.Size(224, 16);
            this.chckAutoProgramStart.TabIndex = 14;
            this.chckAutoProgramStart.Text = "Start auto run after program update.";
            this.chckAutoProgramStart.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(510, 274);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.chckAutoProgramStart);
            this.Controls.Add(this.ChkShowUpgradeInfo);
            this.Controls.Add(this.LbStatus);
            this.Controls.Add(this.LbFileTransfer);
            this.Controls.Add(this.LbFileName);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.TitlePanel);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.TotalDownloadProgressBar);
            this.Controls.Add(this.PartialDownloadProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "스마트 자동 업데이터";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.StatusPanel.ResumeLayout(false);
            this.StatusPanel.PerformLayout();
            this.TitlePanel.ResumeLayout(false);
            this.TitlePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox ChkShowUpgradeInfo;
        private System.Windows.Forms.Label LbStatus;
        private System.Windows.Forms.Label LbFileTransfer;
        private System.Windows.Forms.Label LbFileName;
        private System.Windows.Forms.Label LbChecking;
        private System.Windows.Forms.Label LbConnecting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Status1;
        private System.Windows.Forms.Panel StatusPanel;
        private System.Windows.Forms.Label LbComplete;
        private System.Windows.Forms.Label LbDownloading;
        private System.Windows.Forms.Panel TitlePanel;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.ProgressBar TotalDownloadProgressBar;
        private System.Windows.Forms.ProgressBar PartialDownloadProgressBar;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.CheckBox chckAutoProgramStart;
    }
}

