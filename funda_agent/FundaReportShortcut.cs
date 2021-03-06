﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using funda_interface;

namespace funda_agent
{
    public partial class FundaReportShortcut : Form
    {
        private FundaInterface my_funda;

        public FundaReportShortcut()
        {

            my_funda = new FundaInterface();
            my_funda.Initilize(
                System.Environment.CurrentDirectory + @"\funda_interface"
                );
            my_funda.Start();

            InitializeComponent();
        }

        public void UpdateOpacity(String pp)
        {
            pp = pp.Replace("%","");
            if (pp == "없음")
            {
                this.Visible = false;
                return;
            }
            this.Visible = true;
            try
            {
                this.Opacity = double.Parse(pp) * 0.01;
            }
            catch(Exception ee)
            {
                this.Opacity = 0.3;
            }
        }
        private void FundaReportShortcut_Load(object sender, EventArgs e)
        {

            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Size = new Size(85, 40);
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Program.mFundaReport.Show();
        }
        private Point startPos;
        private bool bDrag;
        private void FundaReportShortcut_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int diffX = e.X - startPos.X;
                int diffY = e.Y - startPos.Y;
                if (diffX * diffX + diffY * diffY > 5)
                {
                    this.Left += diffX;
                    this.Top += diffY;
                }

                if (diffX * diffX + diffY * diffY > 36)
                {
                    bDrag = true;
                }
            }
        }

        private void FundaReportShortcut_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && bDrag == false)
            {
                startPos.X = e.X;
                startPos.Y = e.Y;
            }
        }

        private void FundaReportShortcut_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && bDrag == true)
            {
                bDrag = false;
                //saveLocation(this.Location.X, this.Location.Y);
            }
        }

        private void FundaReportShortcut_DoubleClick(object sender, EventArgs e)
        {
            Program.mFundaReport.Show();
            Program.mFundaReport.Nav();

        }

        private void FundaReportShortcut_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }
    }
}
