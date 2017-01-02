using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace funda_smartcon1
{
    public partial class AcceptList : Form
    {
        ArrayList acceptList;
        public String selEXCHANGENUM = "";

        public AcceptList(ArrayList _acceptList)
        {
            InitializeComponent();

            acceptList = _acceptList;
        }

        private void AcceptList_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < acceptList.Count; i++)
            {
                AcceptItem item = (AcceptItem)acceptList[i];
                String strItem = item.amount + "원(" + item.useDate + ")";
                listbox1.Items.Add(strItem);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (listbox1.SelectedIndex > -1)
            {
                AcceptItem item = (AcceptItem)acceptList[listbox1.SelectedIndex];
                selEXCHANGENUM = item.EXCHANGENUM;
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            selEXCHANGENUM = "";
            Close();
        }
    }
}
