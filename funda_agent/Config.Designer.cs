namespace funda_agent
{
    partial class Config
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBoxPosComPort = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxPosBaudRate = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labelInputBaudRate = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage0 = new System.Windows.Forms.TabPage();
            this.comboBoxTrans = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxApiKey = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxStoreId = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.buttonOK = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.textBoxWepassStoreID = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage0.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "POS PROG -> 출력COM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(255, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "출력COM(FUNDA Agent) -> 실제프린터COM";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16",
            "COM17",
            "COM18",
            "COM19",
            "COM20"});
            this.comboBox1.Location = new System.Drawing.Point(91, 162);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(102, 20);
            this.comboBox1.TabIndex = 2;
            // 
            // comboBoxPosComPort
            // 
            this.comboBoxPosComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPosComPort.FormattingEnabled = true;
            this.comboBoxPosComPort.Items.AddRange(new object[] {
            "자동 선택",
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16",
            "COM17",
            "COM18",
            "COM19",
            "COM20"});
            this.comboBoxPosComPort.Location = new System.Drawing.Point(91, 50);
            this.comboBoxPosComPort.Name = "comboBoxPosComPort";
            this.comboBoxPosComPort.Size = new System.Drawing.Size(102, 20);
            this.comboBoxPosComPort.TabIndex = 2;
            this.comboBoxPosComPort.SelectedIndexChanged += new System.EventHandler(this.comboBoxPosComPort_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(18, 229);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Test Print";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Gulim", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(351, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "현재 출력 모니터링";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "PORT";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(305, 22);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(282, 368);
            this.textBox1.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "PORT";
            this.label6.Click += new System.EventHandler(this.label4_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "Baud Rate";
            this.label7.Click += new System.EventHandler(this.label5_Click);
            // 
            // comboBoxPosBaudRate
            // 
            this.comboBoxPosBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPosBaudRate.FormattingEnabled = true;
            this.comboBoxPosBaudRate.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "86400",
            "115200"});
            this.comboBoxPosBaudRate.Location = new System.Drawing.Point(91, 74);
            this.comboBoxPosBaudRate.Name = "comboBoxPosBaudRate";
            this.comboBoxPosBaudRate.Size = new System.Drawing.Size(102, 20);
            this.comboBoxPosBaudRate.TabIndex = 2;
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "86400",
            "115200"});
            this.comboBox3.Location = new System.Drawing.Point(91, 186);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(102, 20);
            this.comboBox3.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "Baud Rate";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // labelInputBaudRate
            // 
            this.labelInputBaudRate.AutoSize = true;
            this.labelInputBaudRate.Location = new System.Drawing.Point(199, 77);
            this.labelInputBaudRate.Name = "labelInputBaudRate";
            this.labelInputBaudRate.Size = new System.Drawing.Size(53, 12);
            this.labelInputBaudRate.TabIndex = 8;
            this.labelInputBaudRate.Text = "감지속도";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(447, 450);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(164, 39);
            this.button3.TabIndex = 9;
            this.button3.Text = "CLOSE";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 62);
            this.button1.TabIndex = 10;
            this.button1.Text = "Set This Program As Development Mode";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(160, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(151, 62);
            this.button4.TabIndex = 10;
            this.button4.Text = "Set This Program As Normal Mode";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage0);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(13, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(601, 432);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage0
            // 
            this.tabPage0.Controls.Add(this.comboBoxTrans);
            this.tabPage0.Controls.Add(this.button5);
            this.tabPage0.Controls.Add(this.label11);
            this.tabPage0.Controls.Add(this.textBoxApiKey);
            this.tabPage0.Controls.Add(this.label9);
            this.tabPage0.Controls.Add(this.label8);
            this.tabPage0.Controls.Add(this.textBoxStoreId);
            this.tabPage0.Location = new System.Drawing.Point(4, 22);
            this.tabPage0.Name = "tabPage0";
            this.tabPage0.Size = new System.Drawing.Size(593, 406);
            this.tabPage0.TabIndex = 3;
            this.tabPage0.Text = "일반";
            this.tabPage0.UseVisualStyleBackColor = true;
            this.tabPage0.Click += new System.EventHandler(this.tabPage0_Click);
            // 
            // comboBoxTrans
            // 
            this.comboBoxTrans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTrans.FormattingEnabled = true;
            this.comboBoxTrans.Items.AddRange(new object[] {
            "0%",
            "10%",
            "30%",
            "50%",
            "70%",
            "90%"});
            this.comboBoxTrans.Location = new System.Drawing.Point(177, 85);
            this.comboBoxTrans.Name = "comboBoxTrans";
            this.comboBoxTrans.Size = new System.Drawing.Size(121, 20);
            this.comboBoxTrans.TabIndex = 8;
            this.comboBoxTrans.SelectedIndexChanged += new System.EventHandler(this.comboBoxTrans_SelectedIndexChanged);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(18, 284);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(217, 99);
            this.button5.TabIndex = 7;
            this.button5.Text = "설정 초기화";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 12);
            this.label11.TabIndex = 6;
            this.label11.Text = "API KEY";
            // 
            // textBoxApiKey
            // 
            this.textBoxApiKey.Location = new System.Drawing.Point(177, 54);
            this.textBoxApiKey.Name = "textBoxApiKey";
            this.textBoxApiKey.ReadOnly = true;
            this.textBoxApiKey.Size = new System.Drawing.Size(264, 21);
            this.textBoxApiKey.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "로고 투명도";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "상점 아이디";
            // 
            // textBoxStoreId
            // 
            this.textBoxStoreId.Location = new System.Drawing.Point(177, 19);
            this.textBoxStoreId.Name = "textBoxStoreId";
            this.textBoxStoreId.ReadOnly = true;
            this.textBoxStoreId.Size = new System.Drawing.Size(264, 21);
            this.textBoxStoreId.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.comboBox3);
            this.tabPage1.Controls.Add(this.labelInputBaudRate);
            this.tabPage1.Controls.Add(this.comboBoxPosBaudRate);
            this.tabPage1.Controls.Add(this.comboBoxPosComPort);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(593, 406);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "통신";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(593, 406);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "기타";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBoxWepassStoreID);
            this.tabPage3.Controls.Add(this.button7);
            this.tabPage3.Controls.Add(this.button6);
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.button4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(593, 406);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "관리자";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(277, 450);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(164, 39);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(121, 90);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(107, 40);
            this.button6.TabIndex = 11;
            this.button6.Text = "From Funda";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(121, 138);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(107, 40);
            this.button7.TabIndex = 11;
            this.button7.Text = "From Wepass";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // textBoxWepassStoreID
            // 
            this.textBoxWepassStoreID.Font = new System.Drawing.Font("Gulim", 15F);
            this.textBoxWepassStoreID.Location = new System.Drawing.Point(15, 142);
            this.textBoxWepassStoreID.Name = "textBoxWepassStoreID";
            this.textBoxWepassStoreID.Size = new System.Drawing.Size(100, 30);
            this.textBoxWepassStoreID.TabIndex = 12;
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 518);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Name = "Config";
            this.Text = "Funda";
            this.Load += new System.EventHandler(this.Config_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage0.ResumeLayout(false);
            this.tabPage0.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBoxPosComPort;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxPosBaudRate;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelInputBaudRate;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage0;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxStoreId;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxApiKey;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox comboBoxTrans;
        private System.Windows.Forms.TextBox textBoxWepassStoreID;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
    }
}