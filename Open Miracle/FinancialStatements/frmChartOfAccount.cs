//This is a source code or part of OpenMiracle project
//Copyright (C) 2013  Cybrosys Technologies Pvt.Ltd
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Open_Miracle
{
    public partial class frmChartOfAccount : Form
    {

        #region Public Variables
        /// <summary>
        /// Public variable declaration part
        /// </summary>
        DataTable dtblItems = new DataTable();
        #endregion
        #region Functions
        /// <summary>
        /// Creates an instance of frmChartOfAccount class
        /// </summary>
        public frmChartOfAccount()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Function to fill ledgers as TreeMode
        /// </summary>
        /// <param name="tn"></param>
        public void FillTree(TreeNode tn)
        {
            try
            {
                DataTable dtb = new DataTable();
                AccountGroupSP spAccountGroup = new AccountGroupSP();
                dtb = spAccountGroup.AccountGroupViewAllByGroupUnder(Convert.ToDecimal(tn.Name));
                AccountLedgerSP ledgerSP = new AccountLedgerSP();
                if (dtb.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtb.Rows)
                    {
                        tn.Nodes.Add(dr["accountGroupId"].ToString(), dr["accountGroupName"].ToString());
                        tn.ExpandAll();
                        if (tn.LastNode != null)
                        {
                            tn.LastNode.ForeColor = Color.Red;
                        }
                        else
                        {
                            tn.LastNode.ForeColor = Color.Blue;
                        }
                    }
                    foreach (TreeNode tn1 in tn.Nodes)
                    {
                        FillTree(tn1);
                        DataTable dtb1 = ledgerSP.AccountLedgerViewAllByLedgerName(Convert.ToDecimal(tn1.Name));
                        foreach (DataRow dr in dtb1.Rows)
                        {
                            tn1.Nodes.Add(dr["ledgerId"].ToString(), dr["ledgerName"].ToString());
                            tn1.ExpandAll();
                            if (tn1.LastNode != null)
                            {
                                tn1.LastNode.ForeColor = Color.Blue;
                            }
                            else
                            {
                                tn.LastNode.ForeColor = Color.Red;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("COA:1" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to Display Hyphen on child ledgers
        /// </summary>
        /// <param name="Node"></param>
        public void NodePrint(TreeNode Node)
        {
            try
            {
                string hyfens = ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>";
                foreach (TreeNode child in Node.Nodes)
                {
                    string hyf = hyfens.Substring(0, child.Level * 1);
                    DataRow dr = dtblItems.NewRow();
                    dr["name"] = " " + hyf + "   " + child.Text;
                    if (child.ForeColor != Color.Red)
                    {
                        dr["type"] = "ledger";
                    }
                    else
                    {
                        dr["type"] = "account";
                    }
                    dtblItems.Rows.Add(dr);
                    if (child.IsExpanded)
                    {
                        NodePrint(child);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("COA:2" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        #region Events
       
        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmChartOfAccount_Load(object sender, EventArgs e)
        {
            try
            {
                dtblItems.Columns.Add("name");
                dtblItems.Columns.Add("type");
                AccountGroupSP spAccountGroup = new AccountGroupSP();
                DataTable dtbl = new DataTable();
                dtbl = spAccountGroup.AccountGroupViewAllByGroupUnder(-1);
                foreach (DataRow dr in dtbl.Rows)
                {
                    tvChartOfAccount.Nodes.Add(dr["accountGroupId"].ToString(), dr["accountGroupName"].ToString());
                }
                foreach (TreeNode tn1 in tvChartOfAccount.Nodes)
                {
                    FillTree(tn1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("COA:3" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// On 'Print' button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                dtblItems.Clear();
                foreach (TreeNode child in tvChartOfAccount.Nodes)
                {
                    DataRow dr = dtblItems.NewRow();
                    dr["name"] = child.Text;
                    dr["type"] = "group";
                    dtblItems.Rows.Add(dr);
                    if (child.IsExpanded)
                    {
                        NodePrint(child);
                    }
                }
                frmReport frmReportView = new frmReport();
                frmReportView.MdiParent = formMDI.MDIObj;
                frmReportView.PrintChartOfAccounts(dtblItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show("COA:4" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        #region Navigations
        /// <summary>
        /// Escape key navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmChartOfAccount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    if (PublicVariables.isMessageClose)
                    {
                        Messages.CloseMessage(this);
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("COA:5" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}
