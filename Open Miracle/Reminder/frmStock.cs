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
    public partial class frmStock : Form
    {
        #region Variables
        /// <summary>
        /// Public variable declaration part
        /// </summary>
        frmReminderPopUp frmReminderPopupObj;
        #endregion
        #region Functions
        /// <summary>
        /// Creates an instance of frmStock class
        /// </summary>
        public frmStock()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Function to fill Brand combobox
        /// </summary>
        public void BrandComboFill()
        {
            try
            {
                DataTable dtbl = new DataTable();
                BrandSP spBrand = new BrandSP();
                dtbl = spBrand.BrandViewAll();
                DataRow dr = dtbl.NewRow();
                dr["brandName"] = "All";
                dr["brandId"] = 0;
                dtbl.Rows.InsertAt(dr, 0);
                cmbBrand.DataSource = dtbl;
                cmbBrand.DisplayMember = "brandName";
                cmbBrand.ValueMember = "brandId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:1" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to fill Tax combobox
        /// </summary>
        public void TaxComboFill()
        {
            try
            {
                DataTable dtbl = new DataTable();
                TaxSP spTax = new TaxSP();
                dtbl = spTax.TaxViewAllForProduct();
                DataRow dr = dtbl.NewRow();
                dr["taxname"] = "All";
                dr["taxId"] = 0;
                dtbl.Rows.InsertAt(dr, 0);
                cmbTax.DataSource = dtbl;
                cmbTax.DisplayMember = "taxName";
                cmbTax.ValueMember = "taxId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:2" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to fill ModelNo combobox
        /// </summary>
        public void ModelNoComboFill()
        {
            try
            {
                DataTable dtbl = new DataTable();
                ModelNoSP spModelNo = new ModelNoSP();
                dtbl = spModelNo.ModelNoViewAll();
                DataRow dr = dtbl.NewRow();
                dr["modelno"] = "All";
                dr["modelNoId"] = 0;
                dtbl.Rows.InsertAt(dr, 0);
                cmbModelNo.DataSource = dtbl;
                cmbModelNo.DisplayMember = "modelNo";
                cmbModelNo.ValueMember = "modelNoId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:3" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to fill ProductGroup combobox
        /// </summary>
        public void GroupComboFill()
        {
            try
            {
                DataTable dtbl = new DataTable();
                ProductGroupSP spProductGroup = new ProductGroupSP();
                dtbl = spProductGroup.ProductGroupViewAll();
                DataRow dr = dtbl.NewRow();
                dr["groupName"] = "All";
                dr["groupId"] = 0;
                dtbl.Rows.InsertAt(dr, 0);
                cmbGroup.DataSource = dtbl;
                cmbGroup.DisplayMember = "groupName";
                cmbGroup.ValueMember = "groupId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:4" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to fill Godown combobox
        /// </summary>
        public void GodownComboFill()
        {
            try
            {
                DataTable dtbl = new DataTable();
                GodownSP spGodown = new GodownSP();
                dtbl = spGodown.GodownViewAll();
                DataRow dr = dtbl.NewRow();
                dr["godownName"] = "All";
                dr["godownId"] = 0;
                dtbl.Rows.InsertAt(dr, 0);
                cmbGodown.DataSource = dtbl;
                cmbGodown.DisplayMember = "godownName";
                cmbGodown.ValueMember = "godownId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:5" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to fill Size combobox
        /// </summary>
        public void SizeComboFill()
        {
            try
            {
                DataTable dtbl = new DataTable();
                SizeSP spSize = new SizeSP();
                dtbl = spSize.SizeViewAll();
                DataRow dr = dtbl.NewRow();
                dr["size"] = "All";
                dr["sizeId"] = 0;
                dtbl.Rows.InsertAt(dr, 0);
                cmbSize.DataSource = dtbl;
                cmbSize.DisplayMember = "size";
                cmbSize.ValueMember = "sizeId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:6" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to fill Product combobox
        /// </summary>
        public void ProductNameComboFill()
        {
            try
            {
                ProductSP spproduct = new ProductSP();
                DataTable dtblProductName = new DataTable();
                dtblProductName = spproduct.ProductViewAllForComboBox();
                DataRow dr = dtblProductName.NewRow();
                dr["ProductName"] = "All";
                dr["ProductId"] = 0;
                dtblProductName.Rows.InsertAt(dr, 0);
                cmbProduct.DataSource = dtblProductName;
                cmbProduct.ValueMember = "productId";
                cmbProduct.DisplayMember = "productName";
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:7" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to fill Rack combobox
        /// </summary>
        public void RackComboFill()
        {
            try
            {
                RackSP spRack = new RackSP();
                DataTable dtbl = new DataTable();
                if (cmbGodown.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    dtbl = spRack.RackFillForStock(Convert.ToDecimal(cmbGodown.SelectedValue.ToString()));
                    cmbRack.DataSource = dtbl;
                    cmbRack.DisplayMember = "rackName";
                    cmbRack.ValueMember = "rackId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:8" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to fill Rack combobox
        /// </summary>
        public void RackComboFillForLoad()
        {
            try
            {
                RackSP spRack = new RackSP();
                DataTable dtbl = new DataTable();
                if (cmbGodown.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    dtbl = spRack.RackFillForStock(Convert.ToDecimal(cmbGodown.SelectedValue.ToString()));
                    DataRow dr = dtbl.NewRow();
                    dr["rackName"] = "All";
                    dr["rackId"] = 0;
                    dtbl.Rows.InsertAt(dr, 0);
                    cmbRack.DataSource = dtbl;
                    cmbRack.DisplayMember = "rackName";
                    cmbRack.ValueMember = "rackId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:9" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to reset form
        /// </summary>
        public void clear()
        {
            try
            {
                BrandComboFill();
                RackComboFill();
                ProductNameComboFill();
                SizeComboFill();
                GodownComboFill();
                GroupComboFill();
                ModelNoComboFill();
                TaxComboFill();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:10" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to fill Datagridview
        /// </summary>
        public void gridfill()
        {
            try
            {
                DataTable dtbl = new DataTable();
                string strCriteria = string.Empty;
                if (rbtnAll.Checked)
                {
                    strCriteria = "All";
                }
                if (rbtnMax.Checked)
                {
                    strCriteria = "Maximum Level";
                }
                if (rbtnMin.Checked)
                {
                    strCriteria = "Minimum Level";
                }
                if (rbtnNegativestock.Checked)
                {
                    strCriteria = "Negative Stock";
                }
                if (rbtnReorder.Checked)
                {
                    strCriteria = "Reorder Level";
                }
                ReminderSP SpRemainder = new ReminderSP();
                dtbl = SpRemainder.StockSearch(Convert.ToDecimal(cmbGroup.SelectedValue),
                    Convert.ToDecimal(cmbProduct.SelectedValue), Convert.ToDecimal(cmbBrand.SelectedValue),
                    Convert.ToDecimal(cmbSize.SelectedValue), Convert.ToDecimal(cmbModelNo.SelectedValue),
                    Convert.ToDecimal(cmbTax.SelectedValue), Convert.ToDecimal(cmbGodown.SelectedValue),
                    Convert.ToDecimal(cmbRack.SelectedValue), strCriteria);
                dgvStock.DataSource = dtbl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:11" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to call this form from frmReminderPopUp to view details
        /// </summary>
        /// <param name="frmReminder"></param>
        /// <param name="isFromNegativeStock"></param>
        public void CallFromNegativeStockReminder(frmReminderPopUp frmReminder, bool isFromNegativeStock)
        {
            try
            {
                base.Show();
                this.frmReminderPopupObj = frmReminder;
                frmReminderPopupObj.Enabled = false;
                rbtnNegativestock.Checked = true;
                btnSearch.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:12" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to call this form from frmReminderPopUp to view details
        /// </summary>
        /// <param name="frmReminder"></param>
        /// <param name="isFromMinStock"></param>
        public void CallFromMinStockReminder(frmReminderPopUp frmReminder, bool isFromMinStock)
        {
            try
            {
                base.Show();
                this.frmReminderPopupObj = frmReminder;
                frmReminderPopupObj.Enabled = false;
                rbtnMin.Checked = true;
                btnSearch.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:13" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to call this form from frmReminderPopUp to view details
        /// </summary>
        /// <param name="frmReminder"></param>
        /// <param name="isFromMaxStock"></param>
        public void CallFromMaxStockReminder(frmReminderPopUp frmReminder, bool isFromMaxStock)
        {
            try
            {
                base.Show();
                this.frmReminderPopupObj = frmReminder;
                frmReminderPopupObj.Enabled = false;
                rbtnMax.Checked = true;
                btnSearch.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:14" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to call this form from frmReminderPopUp to view details
        /// </summary>
        /// <param name="frmReminder"></param>
        /// <param name="isFromreorderLevel"></param>
        public void CallFromReorderStockReminder(frmReminderPopUp frmReminder, bool isFromreorderLevel)
        {
            try
            {
                base.Show();
                this.frmReminderPopupObj = frmReminder;
                frmReminderPopupObj.Enabled = false;
                rbtnReorder.Checked = true;
                btnSearch.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:15" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        #region Events
        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStock_Load(object sender, EventArgs e)
        {
            try
            {
                rbtnAll.Checked = true;
                BrandComboFill();
                TaxComboFill();
                ModelNoComboFill();
                ProductNameComboFill();
                GroupComboFill();
                GodownComboFill();
                SizeComboFill();
                RackComboFillForLoad();
                cmbGroup.Focus();
                gridfill();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:16" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// On 'Search' button click fills Datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                gridfill();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:17" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// On 'Close' button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("ST:18" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// On 'Reset' button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                cmbGroup.Focus();
                rbtnAll.Checked = true;
                gridfill();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:19" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Fills Rack on cmbGodown combobox SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbGodown_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbGodown.SelectedIndex > 0)
                {
                    RackComboFill();
                }
                else
                {
                    RackComboFillForLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:20" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Enables the objects of other forms on form closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStock_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (frmReminderPopupObj != null)
                {
                    frmReminderPopupObj.Enabled = true;
                    frmReminderPopupObj.Activate();
                    frmReminderPopupObj.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:21" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        #region Navigation
        /// <summary>
        /// Enter key navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbGroup_KeyDown(object sender, KeyEventArgs e)
        {
            {
                try
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        cmbProduct.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ST:22" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        /// <summary>
        /// Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbProduct_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbBrand.Focus();
                }
                else if (e.KeyCode == Keys.Back)
                {
                    cmbGroup.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:23" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbBrand_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbSize.Focus();
                }
                else if (e.KeyCode == Keys.Back)
                {
                    cmbProduct.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:24" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        /// <summary>
        /// Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSize_KeyDown(object sender, KeyEventArgs e)
        {
            {
                try
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        cmbModelNo.Focus();
                    }
                    else if (e.KeyCode == Keys.Back)
                    {
                        cmbBrand.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ST:25" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                }
            }
        }
        /// <summary>
        /// Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbModelNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbTax.Focus();
                }
                else if (e.KeyCode == Keys.Back)
                {
                    cmbSize.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:26" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        /// <summary>
        /// Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTax_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbGodown.Focus();
                }
                else if (e.KeyCode == Keys.Back)
                {
                    cmbModelNo.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:27" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        /// <summary>
        /// Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbGodown_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbRack.Focus();
                }
                else if (e.KeyCode == Keys.Back)
                {
                    cmbTax.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:28" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        /// <summary>
        /// Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRack_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    cmbGodown.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    rbtnAll.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:29" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    rbtnNegativestock.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    rbtnNegativestock.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:30" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnAll_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    cmbRack.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    rbtnNegativestock.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:31" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Escape key navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStock_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    if (PublicVariables.isMessageClose)
                    {
                        Messages.CloseMessage(this);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:32" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnNegativestock_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    rbtnAll.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    rbtnMin.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:33" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnMin_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    rbtnNegativestock.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    rbtnReorder.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:34" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnReorder_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    rbtnMin.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    rbtnMax.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:35" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnMax_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnSearch.Focus();
                }
                else if (e.KeyCode == Keys.Back)
                {
                    rbtnReorder.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:36" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    rbtnMax.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ST:37" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}
