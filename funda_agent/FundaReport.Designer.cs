namespace funda_agent
{
    partial class FundaReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FundaReport));
            this.buttonClose = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.buttonMenu1 = new System.Windows.Forms.Button();
            this.buttonMenu2 = new System.Windows.Forms.Button();
            this.buttonMenu3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonMenu4 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(1050, 671);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(88, 91);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "CLOSE";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(23, 25);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1152, 789);
            this.webBrowser1.TabIndex = 3;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // buttonMenu1
            // 
            this.buttonMenu1.Location = new System.Drawing.Point(14, 15);
            this.buttonMenu1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonMenu1.Name = "buttonMenu1";
            this.buttonMenu1.Size = new System.Drawing.Size(107, 79);
            this.buttonMenu1.TabIndex = 0;
            this.buttonMenu1.Text = "Now";
            this.buttonMenu1.UseVisualStyleBackColor = true;
            this.buttonMenu1.Click += new System.EventHandler(this.buttonMenu1_Click);
            // 
            // buttonMenu2
            // 
            this.buttonMenu2.Location = new System.Drawing.Point(128, 15);
            this.buttonMenu2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonMenu2.Name = "buttonMenu2";
            this.buttonMenu2.Size = new System.Drawing.Size(107, 79);
            this.buttonMenu2.TabIndex = 1;
            this.buttonMenu2.Text = "매출 분석";
            this.buttonMenu2.UseVisualStyleBackColor = true;
            this.buttonMenu2.Click += new System.EventHandler(this.buttonMenu2_Click);
            // 
            // buttonMenu3
            // 
            this.buttonMenu3.Location = new System.Drawing.Point(242, 15);
            this.buttonMenu3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonMenu3.Name = "buttonMenu3";
            this.buttonMenu3.Size = new System.Drawing.Size(107, 79);
            this.buttonMenu3.TabIndex = 2;
            this.buttonMenu3.Text = "상권 분석";
            this.buttonMenu3.UseVisualStyleBackColor = true;
            this.buttonMenu3.Click += new System.EventHandler(this.buttonMenu3_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonMenu4);
            this.panel1.Controls.Add(this.buttonMenu3);
            this.panel1.Controls.Add(this.buttonMenu2);
            this.panel1.Controls.Add(this.buttonMenu1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1152, 117);
            this.panel1.TabIndex = 4;
            // 
            // buttonMenu4
            // 
            this.buttonMenu4.Location = new System.Drawing.Point(1053, 15);
            this.buttonMenu4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonMenu4.Name = "buttonMenu4";
            this.buttonMenu4.Size = new System.Drawing.Size(86, 79);
            this.buttonMenu4.TabIndex = 3;
            this.buttonMenu4.Text = "환경 설정";
            this.buttonMenu4.UseVisualStyleBackColor = true;
            this.buttonMenu4.Click += new System.EventHandler(this.buttonMenu4_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buttonClose);
            this.splitContainer1.Panel2.Controls.Add(this.webBrowser1);
            this.splitContainer1.Size = new System.Drawing.Size(1152, 911);
            this.splitContainer1.SplitterDistance = 117;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 5;
            // 
            // FundaReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 911);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FundaReport";
            this.ShowIcon = false;
            this.Text = "FundaReport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FundaReport_FormClosing);
            this.Load += new System.EventHandler(this.FundaReport_Load);
            this.SizeChanged += new System.EventHandler(this.FundaReport_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button buttonMenu1;
        private System.Windows.Forms.Button buttonMenu2;
        private System.Windows.Forms.Button buttonMenu3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonMenu4;
    }
}