﻿namespace funda_smartcon1
{
    partial class SmartconShortcut
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
            this.SuspendLayout();
            // 
            // FundaReportShortcut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::funda_smartcon1.Properties.Resources.s_button;
            this.ClientSize = new System.Drawing.Size(68, 68);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FundaReportShortcut";
            this.Opacity = 0.5D;
            this.Text = "FundaReportShortcut";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FundaReportShortcut_FormClosing);
            this.Load += new System.EventHandler(this.FundaReportShortcut_Load);
            this.DoubleClick += new System.EventHandler(this.FundaReportShortcut_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FundaReportShortcut_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FundaReportShortcut_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FundaReportShortcut_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

    }
}