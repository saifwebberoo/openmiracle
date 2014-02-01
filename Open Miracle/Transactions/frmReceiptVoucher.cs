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
using System.Collections;
namespace Open_Miracle
{
    public partial class frmReceiptVoucher : Form
    {


        #region Public Variables
        frmCurrencyDetails frmCurrencyObj = new frmCurrencyDetails();//to use in call from currency function
        frmPartyBalance frmPartyBalanceObj = new frmPartyBalance();//to use in call from perty balance function
        frmLedgerPopup frmLedgerPopupObj = new frmLedgerPopup();//to use in call from ledger popup function
        frmAccountLedger frmAccountLedgerObj = new frmAccountLedger();//to use in call from account ledger function
        frmReceiptRegister frmReceiptRegisterObj = null;//to use in call from Receipt register function
        frmReceiptReport frmReceiptReportObj = null;//to use in call from Receipt report function
        frmDayBook frmDayBookObj = null;//to use in call from DayBook function
        frmAgeingReport frmAgeingObj = null;//to use in call from DayBook function
        frmChequeReport frmChequeReportObj = null; //to use in call from CheueReport function
        frmVoucherSearch frmVoucherSearch = null;
        string strLedgerId;
        string strVoucherNo = string.Empty;
        string strInvoiceNo = string.Empty;
        string strPrefix = string.Empty;
        string strSuffix = string.Empty;
        int inNarrationCount = 0;
        string tableName = "ReceiptMaster";//to get the table name in voucher type selection
        bool isAutomatic = true;
        bool isUpdated = false;//to check wheteher the using mode is save or update
        bool isValueChanged = false;//to check column missing
        DataTable dtblPartyBalance = new DataTable();//to store party balance entries while clicking btn_Save in Receipt voucher
        decimal decReceiptVoucherTypeId = 0;//to get the selected voucher type id from frmVoucherTypeSelection
        decimal decRecieptmasterId = 0;//to get the Receipt master id from Receipt register
        decimal decDailySuffixPrefixId = 0;//to store the selected voucher type's suffixpreffixid from frmVoucherTypeSelection
        decimal decSelectedCurrencyRate = 0;//to store the selected currency rate
        decimal decAmount = 0;//to find the total amount 
        decimal decConvertRate = 0;//to find the amont after converted into converted rate by multiplying with exchange rate
        ArrayList arrlstOfDeletedPartyBalanceRow;
        ArrayList arrlstOfRemovedLedgerPostingId = new ArrayList();
        ArrayList arrlstOfRemove = new ArrayList();
        SettingsSP spSettings = new SettingsSP();//to select data from settings table
        int inArrOfRemoveIndex = 0;//number of rows removed by clicking remove button
        frmBillallocation frmBillallocationObj = null;
        public string strVocherNo;
        int inUpdatingRowIndexForPartyRemove = -1;
        decimal decUpdatingLedgerForPartyremove = 0;
        bool isValueChange = true;
        frmLedgerDetails frmLedgerDetailsObj;
        #endregion
        #region Functions
        /// <summary>
        /// Create an instance for frmReceiptVoucher class
        /// </summary>
        public frmReceiptVoucher()
        {
            InitializeComponent();
        }
        /// <summary>
        /// BankOrCashComboFill function
        /// </summary>
        private void BankOrCashComboFill()
        {
            try
            {
                TransactionsGeneralFill TransactionGeneralFillObj = new TransactionsGeneralFill();
                TransactionGeneralFillObj.CashOrBankComboFill(cmbCashOrBank, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV1:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// GridCurrencyComboFill function
        /// </summary>
        private void GridCurrencyComboFill()
        {
            try
            {
                DataTable dtbl = new DataTable();
                TransactionsGeneralFill Obj = new TransactionsGeneralFill();
                dtbl = Obj.CurrencyComboByDate(Convert.ToDateTime(txtDate.Text));
                DataRow drow = dtbl.NewRow();
                drow["currencyName"] = string.Empty;
                drow["exchangeRateId"] = "0";
                dtbl.Rows.InsertAt(drow, 0);
                dgvcmbCurrency.DataSource = dtbl;
                dgvcmbCurrency.DisplayMember = "currencyName";
                dgvcmbCurrency.ValueMember = "exchangeRateId";
                SettingsSP spSettings = new SettingsSP();
                if (spSettings.SettingsStatusCheck("MultiCurrency") == "Yes")
                {
                    dgvcmbCurrency.ReadOnly = false;
                }
                else
                {
                    dgvcmbCurrency.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV2:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to call this form from frmVoucherSearch to view details and for updation 
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="decId"></param>
        public void CallThisFormFromVoucherSearch(frmVoucherSearch frm, decimal decId)
        {
            try
            {
                this.frmVoucherSearch = frm;
                decRecieptmasterId = decId;
                btnClear.Text = "New";
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
                FillFunction();
                this.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV3:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to call this form from frmCurrencyDetails to view details and for updation
        /// </summary>
        /// <param name="frmCurrencyDetails"></param>
        /// <param name="decId"></param>
        public void CallFromCurrenCyDetails(frmCurrencyDetails frmCurrencyDetails, decimal decId)
        {
            try
            {
                base.Show();
                this.frmCurrencyObj = frmCurrencyDetails;
                GridCurrencyComboFill();
                dgvReceiptVoucher.CurrentRow.Cells["dgvcmbCurrency"].Value = decId;
                frmCurrencyObj.Close();
                frmCurrencyObj = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV4:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Delete Function
        /// </summary>
        /// <param name="decMasterId"></param>
        public void Delete(decimal decMasterId)
        {
            try
            {
                ReceiptMasterSP SpReceiptMaster = new ReceiptMasterSP();
                PartyBalanceSP SpPartyBalance = new PartyBalanceSP();
                if (!SpPartyBalance.PartyBalanceCheckReference(decReceiptVoucherTypeId, strVoucherNo))
                {
                    SpReceiptMaster.ReceiptVoucherDelete(decRecieptmasterId, decReceiptVoucherTypeId, strVoucherNo);
                    Messages.DeletedMessage();
                    if (frmReceiptRegisterObj != null)
                    {
                        this.Close();
                        frmReceiptRegisterObj.CallFromReceiptVoucher(this);
                    }
                    else if (frmReceiptReportObj != null)
                    {
                        this.Close();
                        frmReceiptReportObj.CallFromReceiptVoucher(this);
                    }
                    else if (frmLedgerDetailsObj != null)
                    {
                        this.Close();
                    }
                    else if (frmVoucherSearch != null)
                    {
                        this.Close();
                        frmVoucherSearch.GridFill();
                    }
                    if (frmDayBookObj != null)
                    {
                        this.Close();
                    }
                    if (frmBillallocationObj != null)
                    {
                        this.Close();
                    }
                }
                else
                {
                    Messages.InformationMessage("Reference exist. Cannot delete");
                    txtDate.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV5:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Clear function
        /// </summary>
        public void Clear()
        {
            try
            {
                TransactionsGeneralFill obj = new TransactionsGeneralFill();
                if (btnSave.Text == "Update")
                {
                    if (frmReceiptRegisterObj != null)
                    {
                        frmReceiptRegisterObj.Close();
                    }
                }
                if (isAutomatic == true)
                {
                    SalaryVoucherMasterSP spMaster = new SalaryVoucherMasterSP();
                    ReceiptMasterSP SpReceiptMaster = new ReceiptMasterSP();

                    if (strVoucherNo == string.Empty)
                    {
                        strVoucherNo = "0";
                    }
                    strVoucherNo = obj.VoucherNumberAutomaicGeneration(decReceiptVoucherTypeId, Convert.ToDecimal(strVoucherNo), dtpDate.Value, tableName);
                    if (Convert.ToDecimal(strVoucherNo) != SpReceiptMaster.ReceiptMasterGetMax(decReceiptVoucherTypeId) + 1)
                    {
                        strVoucherNo = SpReceiptMaster.ReceiptMasterGetMax(decReceiptVoucherTypeId).ToString();
                        strVoucherNo = obj.VoucherNumberAutomaicGeneration(decReceiptVoucherTypeId, Convert.ToDecimal(strVoucherNo), dtpDate.Value, tableName);
                        if (SpReceiptMaster.ReceiptMasterGetMax(decReceiptVoucherTypeId) == 0)
                        {
                            strVoucherNo = "0";
                            strVoucherNo = obj.VoucherNumberAutomaicGeneration(decReceiptVoucherTypeId, Convert.ToDecimal(strVoucherNo), dtpDate.Value, tableName);
                        }
                    }

                    SuffixPrefixSP spSuffisprefix = new SuffixPrefixSP();
                    SuffixPrefixInfo infoSuffixPrefix = new SuffixPrefixInfo();
                    infoSuffixPrefix = spSuffisprefix.GetSuffixPrefixDetails(decReceiptVoucherTypeId, dtpDate.Value);
                    strPrefix = infoSuffixPrefix.Prefix;
                    strSuffix = infoSuffixPrefix.Suffix;
                    strInvoiceNo = strPrefix + strVoucherNo + strSuffix;
                    txtVoucherNo.Text = strInvoiceNo;
                    txtVoucherNo.ReadOnly = true;
                }
                else
                {
                    txtVoucherNo.Text = string.Empty;
                    txtVoucherNo.ReadOnly = false;
                }
                dtpDate.MinDate = PublicVariables._dtFromDate;
                dtpDate.MaxDate = PublicVariables._dtToDate;
                dtpDate.Value = PublicVariables._dtCurrentDate;
                cmbCashOrBank.SelectedIndex = -1;
                txtNarration.Text = string.Empty;
                txtTotal.Text = string.Empty;
                dgvReceiptVoucher.ClearSelection();
                dgvReceiptVoucher.Rows.Clear();
                btnDelete.Enabled = false;
                btnSave.Text = "Save";
                cbxPrintafterSave.Checked = false;
                dtblPartyBalance.Clear();
                if (isAutomatic)
                {
                    txtDate.Select();
                }
                else
                {
                    txtVoucherNo.Focus();
                }
                SettingsSP spSettings = new SettingsSP();
                if (spSettings.SettingsStatusCheck("TickPrintAfterSave") == "Yes")
                {
                    cbxPrintafterSave.Checked = true;
                }
                else
                {
                    cbxPrintafterSave.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV6:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Serial no generation for Data grid
        /// </summary>
        public void SerialNumberGeneration()
        {
            try
            {
                int inRowSlNo = 1;
                foreach (DataGridViewRow dr in dgvReceiptVoucher.Rows)
                {
                    dr.Cells["dgvtxtSlNo"].Value = inRowSlNo;
                    inRowSlNo++;
                    if (dr.Index == dgvReceiptVoucher.Rows.Count - 2)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV7:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to call this form from VoucherType Selection form
        /// </summary>
        /// <param name="decVoucherTypeId1"></param>
        /// <param name="strReceiptVoucherTypeName"></param>
        public void CallFromVoucherTypeSelection(decimal decVoucherTypeId1, string strReceiptVoucherTypeName)
        {
            try
            {
                btnDelete.Enabled = true;
                decReceiptVoucherTypeId = decVoucherTypeId1;
                VoucherTypeSP spVoucherType = new VoucherTypeSP();
                isAutomatic = spVoucherType.CheckMethodOfVoucherNumbering(decReceiptVoucherTypeId);
                SuffixPrefixSP spSuffisprefix = new SuffixPrefixSP();
                SuffixPrefixInfo infoSuffixPrefix = new SuffixPrefixInfo();
                infoSuffixPrefix = spSuffisprefix.GetSuffixPrefixDetails(decReceiptVoucherTypeId, dtpDate.Value);
                decDailySuffixPrefixId = infoSuffixPrefix.SuffixprefixId;
                strPrefix = infoSuffixPrefix.Prefix;
                strSuffix = infoSuffixPrefix.Suffix;
                base.Show();
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV8:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Grid ledger combobox fill
        /// </summary>
        public void GridLedgerComboFill()
        {
            try
            {
                DataTable dtbl = new DataTable();
                TransactionsGeneralFill obj = new TransactionsGeneralFill();
                dtbl = obj.AccountLedgerComboFill();
                DataRow drow = dtbl.NewRow();
                drow["ledgerName"] = string.Empty;
                drow["ledgerId"] = "0";
                dtbl.Rows.InsertAt(drow, 0);
                dgvcmbAccountLedger.DataSource = dtbl;
                dgvcmbAccountLedger.ValueMember = "ledgerId";
                dgvcmbAccountLedger.DisplayMember = "ledgerName";
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV9:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Party balance save or edit
        /// </summary>
        /// <param name="inJ"></param>
        public void PartyBalanceAddOrEdit(int inJ)
        {
            try
            {
                int inTableRowCount = dtblPartyBalance.Rows.Count;
                PartyBalanceInfo InfopartyBalance = new PartyBalanceInfo();
                PartyBalanceSP spPartyBalance = new PartyBalanceSP();
                InfopartyBalance.Debit = 0;
                InfopartyBalance.CreditPeriod = 0;
                InfopartyBalance.Date = dtpDate.Value;
                InfopartyBalance.Credit = Convert.ToDecimal(dtblPartyBalance.Rows[inJ]["Amount"].ToString());
                InfopartyBalance.ExchangeRateId = Convert.ToDecimal(dtblPartyBalance.Rows[inJ]["CurrencyId"].ToString());
                InfopartyBalance.Extra1 = string.Empty;
                InfopartyBalance.Extra2 = string.Empty;
                InfopartyBalance.ExtraDate = DateTime.Now;
                InfopartyBalance.FinancialYearId = PublicVariables._decCurrentFinancialYearId;
                InfopartyBalance.LedgerId = Convert.ToDecimal(dtblPartyBalance.Rows[inJ]["LedgerId"].ToString());
                InfopartyBalance.ReferenceType = dtblPartyBalance.Rows[inJ]["ReferenceType"].ToString();
                if (dtblPartyBalance.Rows[inJ]["ReferenceType"].ToString() == "New" || dtblPartyBalance.Rows[inJ]["ReferenceType"].ToString() == "OnAccount")
                {
                    InfopartyBalance.AgainstInvoiceNo = dtblPartyBalance.Rows[inJ]["AgainstInvoiceNo"].ToString();
                    InfopartyBalance.AgainstVoucherNo = dtblPartyBalance.Rows[inJ]["AgainstVoucherNo"].ToString();
                    InfopartyBalance.AgainstVoucherTypeId = Convert.ToDecimal(dtblPartyBalance.Rows[inJ]["AgainstVoucherTypeId"].ToString());//decPaymentVoucherTypeId;
                    InfopartyBalance.VoucherTypeId = decReceiptVoucherTypeId;
                    InfopartyBalance.InvoiceNo = txtVoucherNo.Text;

                    InfopartyBalance.VoucherNo = strVoucherNo;

                }
                else
                {
                    InfopartyBalance.ExchangeRateId = Convert.ToDecimal(dtblPartyBalance.Rows[inJ]["OldExchangeRate"].ToString());
                    InfopartyBalance.AgainstInvoiceNo = txtVoucherNo.Text.Trim();

                    InfopartyBalance.AgainstVoucherNo = strVoucherNo;

                    InfopartyBalance.AgainstVoucherTypeId = decReceiptVoucherTypeId;
                    InfopartyBalance.VoucherTypeId = Convert.ToDecimal(dtblPartyBalance.Rows[inJ]["AgainstVoucherTypeId"].ToString());
                    InfopartyBalance.VoucherNo = dtblPartyBalance.Rows[inJ]["AgainstVoucherNo"].ToString();
                    InfopartyBalance.InvoiceNo = dtblPartyBalance.Rows[inJ]["AgainstInvoiceNo"].ToString();
                }
                if (dtblPartyBalance.Rows[inJ]["PartyBalanceId"].ToString() == "0")
                {
                    spPartyBalance.PartyBalanceAdd(InfopartyBalance);
                }
                else
                {
                    InfopartyBalance.PartyBalanceId = Convert.ToDecimal(dtblPartyBalance.Rows[inJ]["partyBalanceId"]);
                    spPartyBalance.PartyBalanceEdit(InfopartyBalance);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV10:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        ///the function for Delete PartyBalance Of a Removed Row 
        /// </summary>
        public void DeletePartyBalanceOfRemovedRow()
        {
            PartyBalanceSP spPartyBalance = new PartyBalanceSP();
            try
            {
                foreach (object obj in arrlstOfDeletedPartyBalanceRow)
                {
                    string str = Convert.ToString(obj);
                    spPartyBalance.PartyBalanceDelete(Convert.ToDecimal(str));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV11:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Save function
        /// </summary>
        public void Save()
        {
            try
            {
                int inGridRowCount = dgvReceiptVoucher.RowCount;
                int inTableRowCount = dtblPartyBalance.Rows.Count;
                int inB = 0;
                ReceiptMasterInfo InfoReceiptMaster = new ReceiptMasterInfo();
                ReceiptMasterSP SpReceiptMaster = new ReceiptMasterSP();
                ReceiptDetailsInfo InfoReceiptDetails = new ReceiptDetailsInfo();
                ReceiptDetailsSP SpReceiptDetails = new ReceiptDetailsSP();
                PartyBalanceSP SpPartyBalance = new PartyBalanceSP();
                PartyBalanceInfo InfopartyBalance = new PartyBalanceInfo();
                InfoReceiptMaster.Date = dtpDate.Value;
                InfoReceiptMaster.Extra1 = string.Empty;
                InfoReceiptMaster.Extra2 = string.Empty;
                InfoReceiptMaster.FinancialYearId = PublicVariables._decCurrentFinancialYearId;
                InfoReceiptMaster.LedgerId = Convert.ToDecimal(cmbCashOrBank.SelectedValue.ToString());
                InfoReceiptMaster.Narration = txtNarration.Text;
                decimal decTotalAmount = TotalAmountCalculation();
                InfoReceiptMaster.TotalAmount = decTotalAmount;
                InfoReceiptMaster.UserId = PublicVariables._decCurrentUserId;
                if (!isAutomatic)
                {
                    InfoReceiptMaster.VoucherNo = txtVoucherNo.Text.Trim();
                    InfoReceiptMaster.InvoiceNo = txtVoucherNo.Text.Trim();
                    InfoReceiptMaster.SuffixPrefixId = 0;
                }
                else
                {
                    InfoReceiptMaster.VoucherNo = strVoucherNo;
                    InfoReceiptMaster.InvoiceNo = strInvoiceNo;
                    InfoReceiptMaster.SuffixPrefixId = decDailySuffixPrefixId;
                }
                InfoReceiptMaster.VoucherTypeId = decReceiptVoucherTypeId;
                decimal decReceiptMasterId = SpReceiptMaster.ReceiptMasterAdd(InfoReceiptMaster);
                if (decReceiptMasterId != 0)
                {
                    MasterLedgerPosting();
                }
                for (int inI = 0; inI < inGridRowCount - 1; inI++)
                {
                    if (dgvReceiptVoucher.Rows[inI].HeaderCell.Value.ToString() != "X")
                    {
                        InfoReceiptDetails.Amount = Convert.ToDecimal(dgvReceiptVoucher.Rows[inI].Cells["dgvtxtAmount"].Value.ToString());
                        InfoReceiptDetails.ExchangeRateId = Convert.ToDecimal(dgvReceiptVoucher.Rows[inI].Cells["dgvcmbCurrency"].Value.ToString());
                        InfoReceiptDetails.Extra1 = string.Empty;
                        InfoReceiptDetails.Extra2 = string.Empty;
                        InfoReceiptDetails.LedgerId = Convert.ToDecimal(dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value.ToString());
                        InfoReceiptDetails.ReceiptMasterId = decReceiptMasterId;
                        if (dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value != null && dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value.ToString() != string.Empty)
                        {
                            InfoReceiptDetails.LedgerId = Convert.ToDecimal(dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value.ToString());
                        }
                        if (dgvReceiptVoucher.Rows[inI].Cells["dgvtxtChequeNo"].Value != null && dgvReceiptVoucher.Rows[inI].Cells["dgvtxtChequeNo"].Value.ToString() != string.Empty)
                        {
                            InfoReceiptDetails.ChequeNo = dgvReceiptVoucher.Rows[inI].Cells["dgvtxtChequeNo"].Value.ToString();
                            if (dgvReceiptVoucher.Rows[inI].Cells["dgvtxtChequeDate"].Value != null && dgvReceiptVoucher.Rows[inI].Cells["dgvtxtChequeDate"].Value.ToString() != string.Empty)
                            {
                                InfoReceiptDetails.ChequeDate = Convert.ToDateTime(dgvReceiptVoucher.Rows[inI].Cells["dgvtxtChequeDate"].Value);
                            }
                            else
                            {
                                InfoReceiptDetails.ChequeDate = DateTime.Now;
                            }
                        }
                        else
                        {
                            InfoReceiptDetails.ChequeNo = string.Empty;
                            InfoReceiptDetails.ChequeDate = DateTime.Now;
                        }
                        decimal decReceiptDetailsId = SpReceiptDetails.ReceiptDetailsAdd(InfoReceiptDetails);
                        if (decReceiptDetailsId != 0)
                        {
                            for (int inJ = 0; inJ < inTableRowCount; inJ++)
                            {
                                if (dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value.ToString() == dtblPartyBalance.Rows[inJ]["LedgerId"].ToString())
                                {
                                    PartyBalanceAddOrEdit(inJ);
                                }
                            }
                            inB++;
                            DetailsLedgerPosting(inI, decReceiptDetailsId);
                        }
                    }
                }

                Messages.SavedMessage();
                if (cbxPrintafterSave.Checked)
                {
                    if (spSettings.SettingsStatusCheck("Printer") == "Dot Matrix")
                    {
                        PrintForDotMatrix(decReceiptMasterId);
                    }
                    else
                    {
                        Print(decReceiptMasterId);
                    }
                }
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV12:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Edit function
        /// </summary>
        /// <param name="decMasterId"></param>
        public void Edit(decimal decMasterId)
        {
            try
            {
                int inRowCount = dgvReceiptVoucher.RowCount;
                int inTableRowCount = dtblPartyBalance.Rows.Count;
                int inB = 0;
                ReceiptMasterInfo InfoReceiptMaster = new ReceiptMasterInfo();
                ReceiptMasterSP SpReceiptMaster = new ReceiptMasterSP();
                ReceiptDetailsInfo InfoReceiptDetails = new ReceiptDetailsInfo();
                ReceiptDetailsSP SpReceiptDetails = new ReceiptDetailsSP();
                LedgerPostingSP SpLedgerPosting = new LedgerPostingSP();
                LedgerPostingInfo InfoLegerPosting = new LedgerPostingInfo();
                PartyBalanceInfo InfopartyBalance = new PartyBalanceInfo();
                PartyBalanceSP SpPartyBalance = new PartyBalanceSP();
                BankReconciliationSP SpBankReconcilation = new BankReconciliationSP();
                InfoReceiptMaster.Date = dtpDate.Value;
                InfoReceiptMaster.ReceiptMasterId = decMasterId;
                InfoReceiptMaster.Extra1 = string.Empty;
                InfoReceiptMaster.Extra2 = string.Empty;
                InfoReceiptMaster.ExtraDate = DateTime.Now;
                InfoReceiptMaster.FinancialYearId = PublicVariables._decCurrentFinancialYearId;
                InfoReceiptMaster.LedgerId = Convert.ToDecimal(cmbCashOrBank.SelectedValue.ToString());
                InfoReceiptMaster.Narration = txtNarration.Text.Trim();
                decimal decTotalAmount = TotalAmountCalculation();
                InfoReceiptMaster.TotalAmount = decTotalAmount;
                InfoReceiptMaster.UserId = PublicVariables._decCurrentUserId;
                if (!isAutomatic)
                {
                    InfoReceiptMaster.VoucherNo = txtVoucherNo.Text.Trim();
                    InfoReceiptMaster.InvoiceNo = txtVoucherNo.Text.Trim();
                    InfoReceiptMaster.SuffixPrefixId = 0;
                }
                else
                {
                    InfoReceiptMaster.VoucherNo = strVoucherNo;
                    InfoReceiptMaster.InvoiceNo = strInvoiceNo;
                    InfoReceiptMaster.SuffixPrefixId = decDailySuffixPrefixId;
                }
                InfoReceiptMaster.VoucherTypeId = decReceiptVoucherTypeId;
                decimal decEffectRow = SpReceiptMaster.ReceiptMasterEdit(InfoReceiptMaster);
                if (decEffectRow != 0)
                {
                    MasterLedgerPostingEdit();
                }
                foreach (object obj in arrlstOfRemove)
                {
                    string str = Convert.ToString(obj);
                    SpReceiptDetails.ReceiptDetailsDelete(Convert.ToDecimal(str));
                    SpLedgerPosting.LedgerPostDeleteByDetailsId(Convert.ToDecimal(str), strVoucherNo, decReceiptVoucherTypeId);
                }
                decimal decReceiptDetailsId1 = 0;
                SpLedgerPosting.LedgerPostingDeleteByVoucherNoVoucherTypeIdAndLedgerId(strVoucherNo, decReceiptVoucherTypeId, 12);
                for (int inI = 0; inI < inRowCount - 1; inI++)
                {
                    InfoReceiptDetails.Amount = Convert.ToDecimal(dgvReceiptVoucher.Rows[inI].Cells["dgvtxtAmount"].Value.ToString());
                    InfoReceiptDetails.ExchangeRateId = Convert.ToDecimal(dgvReceiptVoucher.Rows[inI].Cells["dgvcmbCurrency"].Value.ToString());
                    InfoReceiptDetails.Extra1 = string.Empty;
                    InfoReceiptDetails.Extra2 = string.Empty;
                    InfoReceiptDetails.ReceiptMasterId = InfoReceiptMaster.ReceiptMasterId;
                    if (dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value != null && dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value.ToString() != string.Empty)
                    {
                        InfoReceiptDetails.LedgerId = Convert.ToDecimal(dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value.ToString());
                    }
                    if (dgvReceiptVoucher.Rows[inI].Cells["dgvtxtChequeNo"].Value != null && dgvReceiptVoucher.Rows[inI].Cells["dgvtxtChequeNo"].Value.ToString() != string.Empty)
                    {
                        InfoReceiptDetails.ChequeNo = dgvReceiptVoucher.Rows[inI].Cells["dgvtxtChequeNo"].Value.ToString();
                        if (dgvReceiptVoucher.Rows[inI].Cells["dgvtxtChequeDate"].Value != null && dgvReceiptVoucher.Rows[inI].Cells["dgvtxtChequeDate"].Value.ToString() != string.Empty)
                        {
                            InfoReceiptDetails.ChequeDate = Convert.ToDateTime(dgvReceiptVoucher.Rows[inI].Cells["dgvtxtChequeDate"].Value);
                        }
                        else
                        {
                            InfoReceiptDetails.ChequeDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        InfoReceiptDetails.ChequeNo = string.Empty;
                        InfoReceiptDetails.ChequeDate = DateTime.Now;
                    }
                    if (dgvReceiptVoucher.Rows[inI].Cells["dgvtxtReceiptDetailsId"].Value == null || dgvReceiptVoucher.Rows[inI].Cells["dgvtxtReceiptDetailsId"].Value.ToString() == string.Empty)//if new rows are added
                    {
                        if (dgvReceiptVoucher.Rows[inI].HeaderCell.Value.ToString() != "X")
                        {
                            decimal decReceiptDetailsId = SpReceiptDetails.ReceiptDetailsAdd(InfoReceiptDetails);
                            if (decReceiptDetailsId != 0)
                            {
                                for (int inJ = 0; inJ < inTableRowCount; inJ++)
                                {
                                    if (dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value.ToString() == dtblPartyBalance.Rows[inJ]["LedgerId"].ToString())
                                    {
                                        PartyBalanceAddOrEdit(inJ);
                                    }
                                }
                                inB++;
                                DetailsLedgerPosting(inI, decReceiptDetailsId);
                            }
                        }
                    }
                    else
                    {
                        if (dgvReceiptVoucher.Rows[inI].HeaderCell.Value.ToString() != "X")
                        {
                            InfoReceiptDetails.ReceiptDetailsId = Convert.ToDecimal(dgvReceiptVoucher.Rows[inI].Cells["dgvtxtreceiptDetailsId"].Value.ToString());
                            decimal decReceiptDetailsId = SpReceiptDetails.ReceiptDetailsEdit(InfoReceiptDetails);
                            if (decReceiptDetailsId != 0)
                            {
                                for (int inJ = 0; inJ < inTableRowCount; inJ++)
                                {
                                    if (dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value.ToString() == dtblPartyBalance.Rows[inJ]["LedgerId"].ToString())
                                    {
                                        PartyBalanceAddOrEdit(inJ);
                                    }
                                }
                                inB++;
                                decReceiptDetailsId = InfoReceiptDetails.ReceiptDetailsId;
                                decimal decLedgerPostId = Convert.ToDecimal(dgvReceiptVoucher.Rows[inI].Cells["dgvtxtLedgerPostingId"].Value.ToString());
                                DetailsLedgerPostingEdit(inI, decLedgerPostId, decReceiptDetailsId1);
                            }
                        }
                        else
                        {
                            decimal decDetailsId = Convert.ToDecimal(dgvReceiptVoucher.Rows[inI].Cells["dgvtxtreceiptDetailsId"].Value.ToString());
                            SpReceiptDetails.ReceiptDetailsDelete(decDetailsId);
                            SpLedgerPosting.LedgerPostDeleteByDetailsId(decDetailsId, strVoucherNo, decReceiptVoucherTypeId);
                            for (int inJ = 0; inJ < dtblPartyBalance.Rows.Count; inJ++)
                            {
                                if (dtblPartyBalance.Rows.Count == inJ)
                                {
                                    break;
                                }
                                if (dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value != null && dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value.ToString() != string.Empty)
                                {
                                    if (dtblPartyBalance.Rows[inJ]["LedgerId"].ToString() == dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value.ToString())
                                    {
                                        if (dtblPartyBalance.Rows[inJ]["PartyBalanceId"].ToString() != "0")
                                        {
                                            arrlstOfDeletedPartyBalanceRow.Add(dtblPartyBalance.Rows[inJ]["PartyBalanceId"]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                DeletePartyBalanceOfRemovedRow();
                isUpdated = true;
                Messages.UpdatedMessage();
                if (cbxPrintafterSave.Checked)
                {
                    if (spSettings.SettingsStatusCheck("Printer") == "Dot Matrix")
                    {
                        PrintForDotMatrix(InfoReceiptMaster.ReceiptMasterId);
                    }
                    else
                    {
                        Print(InfoReceiptMaster.ReceiptMasterId);
                    }
                }
                if (frmReceiptRegisterObj != null)
                {
                    this.Close();
                    frmReceiptRegisterObj.CallFromReceiptVoucher(this);
                }
                if (frmReceiptReportObj != null)
                {
                    this.Close();
                    frmReceiptReportObj.CallFromReceiptVoucher(this);
                }
                if (frmDayBookObj != null)
                {
                    this.Close();
                }
                if (frmBillallocationObj != null)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV13:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Print function 
        /// </summary>
        /// <param name="decReceiptMasterId"></param>
        public void Print(decimal decReceiptMasterId)
        {
            try
            {
                ReceiptMasterSP SpReceiptMaster = new ReceiptMasterSP();
                DataSet dsReceiptVoucher = SpReceiptMaster.ReceiptVoucherPrinting(decReceiptMasterId);// PublicVariables._decCurrentCompanyId);
                frmReport frmReport = new frmReport();
                frmReport.MdiParent = formMDI.MDIObj;
                frmReport.ReceiptVoucherPrinting(dsReceiptVoucher);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rv14:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Print function for dotmatrix 
        /// </summary>
        /// <param name="decReceiptMasterId"></param>
        public void PrintForDotMatrix(decimal decReceiptMasterId)
        {
            try
            {
                DataTable dtblOtherDetails = new DataTable();
                CompanySP spComapany = new CompanySP();
                dtblOtherDetails = spComapany.CompanyViewForDotMatrix();
                DataTable dtblGridDetails = new DataTable();
                dtblGridDetails.Columns.Add("SlNo");
                dtblGridDetails.Columns.Add("Account Ledger");
                dtblGridDetails.Columns.Add("Amount");
                dtblGridDetails.Columns.Add("Currency");
                dtblGridDetails.Columns.Add("Cheque No");
                dtblGridDetails.Columns.Add("Cheque Date");
                int inRowCount = 0;
                foreach (DataGridViewRow dRow in dgvReceiptVoucher.Rows)
                {
                    if (dRow.HeaderCell.Value != null && dRow.HeaderCell.Value.ToString() != "X")
                    {
                        if (!dRow.IsNewRow)
                        {
                            DataRow dr = dtblGridDetails.NewRow();
                            dr["SlNo"] = ++inRowCount;
                            dr["Account Ledger"] = dRow.Cells["dgvcmbAccountLedger"].FormattedValue.ToString();
                            dr["Amount"] = dRow.Cells["dgvtxtAmount"].Value.ToString();
                            dr["Currency"] = dRow.Cells["dgvcmbCurrency"].FormattedValue.ToString();
                            dr["Cheque No"] = (dRow.Cells["dgvtxtChequeNo"].Value == null ? "" : dRow.Cells["dgvtxtChequeNo"].Value.ToString());
                            dr["Cheque Date"] = (dRow.Cells["dgvtxtChequeDate"].Value == null ? "" : dRow.Cells["dgvtxtChequeDate"].Value.ToString());
                            dtblGridDetails.Rows.Add(dr);
                        }
                    }
                }
                dtblOtherDetails.Columns.Add("voucherNo");
                dtblOtherDetails.Columns.Add("date");
                dtblOtherDetails.Columns.Add("totalAmount");
                dtblOtherDetails.Columns.Add("ledgerName");
                dtblOtherDetails.Columns.Add("Narration");
                dtblOtherDetails.Columns.Add("AmountInWords");
                dtblOtherDetails.Columns.Add("Declaration");
                dtblOtherDetails.Columns.Add("Heading1");
                dtblOtherDetails.Columns.Add("Heading2");
                dtblOtherDetails.Columns.Add("Heading3");
                dtblOtherDetails.Columns.Add("Heading4");
                DataRow dRowOther = dtblOtherDetails.Rows[0];
                dRowOther["voucherNo"] = txtVoucherNo.Text;
                dRowOther["date"] = txtDate.Text;
                dRowOther["totalAmount"] = txtTotal.Text;
                dRowOther["ledgerName"] = cmbCashOrBank.Text;
                dRowOther["Narration"] = txtNarration.Text;
                dRowOther["address"] = (dtblOtherDetails.Rows[0]["address"].ToString().Replace("\n", ", ")).Replace("\r", ", ");
                dRowOther["AmountInWords"] = new NumToText().AmountWords(Convert.ToDecimal(txtTotal.Text), PublicVariables._decCurrencyId);
                VoucherTypeSP spVoucherType = new VoucherTypeSP();
                DataTable dtblDeclaration = spVoucherType.DeclarationAndHeadingGetByVoucherTypeId(decReceiptVoucherTypeId);
                dRowOther["Declaration"] = dtblDeclaration.Rows[0]["Declaration"].ToString();
                dRowOther["Heading1"] = dtblDeclaration.Rows[0]["Heading1"].ToString();
                dRowOther["Heading2"] = dtblDeclaration.Rows[0]["Heading2"].ToString();
                dRowOther["Heading3"] = dtblDeclaration.Rows[0]["Heading3"].ToString();
                dRowOther["Heading4"] = dtblDeclaration.Rows[0]["Heading4"].ToString();
                int inFormId = spVoucherType.FormIdGetForPrinterSettings(Convert.ToInt32(dtblDeclaration.Rows[0]["masterId"].ToString()));
                PrintWorks.DotMatrixPrint.PrintDesign(inFormId, dtblOtherDetails, dtblGridDetails, dtblOtherDetails);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rv15:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to call this form from Call FromPartyBalance to view details and for updation 
        /// </summary>
        /// <param name="frmPartyBalance"></param>
        /// <param name="decId"></param>
        /// <param name="dtbl"></param>
        /// <param name="arrlstOfRemovedRow"></param>
        public void CallFromPartyBalance(frmPartyBalance frmPartyBalance, decimal decId, DataTable dtbl, ArrayList arrlstOfRemovedRow)
        {
            try
            {
                btnDelete.Enabled = true;
                this.frmPartyBalanceObj = frmPartyBalance;
                dgvReceiptVoucher.CurrentRow.Cells["dgvtxtAmount"].Value = decId.ToString();
                frmPartyBalanceObj.Close();
                frmPartyBalanceObj = null;
                dtblPartyBalance = dtbl;
                arrlstOfDeletedPartyBalanceRow = arrlstOfRemovedRow;
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV16:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to call this form from Call frmReceiptRegister to view details and for updation 
        /// </summary>
        /// <param name="frmReceiptRegister"></param>
        /// <param name="decmasterId"></param>
        public void CallFromReceiptRegister(frmReceiptRegister frmReceiptRegister, decimal decmasterId)
        {
            try
            {
                base.Show();
                dgvReceiptVoucher.ClearSelection();
                btnDelete.Enabled = true;
                this.frmReceiptRegisterObj = frmReceiptRegister;
                frmReceiptRegisterObj.Enabled = false;
                btnDelete.Enabled = true;
                btnSave.Text = "Update";
                decRecieptmasterId = decmasterId;
                FillFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV17:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to call this form from Call frmReceiptReport to view details and for updation 
        /// </summary>
        /// <param name="frmReceiptReport"></param>
        /// <param name="decmasterId"></param>
        public void CallFromReceiptReport(frmReceiptReport frmReceiptReport, decimal decmasterId)
        {
            try
            {
                base.Show();
                btnDelete.Enabled = true;
                this.frmReceiptReportObj = frmReceiptReport;
                frmReceiptReportObj.Enabled = false;
                btnDelete.Enabled = true;
                btnSave.Text = "Update";
                decRecieptmasterId = decmasterId;
                FillFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV18:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Total Amount Calculation 
        /// </summary>
        public void TotalAmount()
        {
            try
            {
                decimal decTotalAmount = 0;
                decimal decSelectedCurrencyRate = 0;
                ExchangeRateSP SpExchangRate = new ExchangeRateSP();
                foreach (DataGridViewRow dr in dgvReceiptVoucher.Rows)
                {
                    if (dr.Cells["dgvtxtAmount"].Value != null && dr.Cells["dgvtxtAmount"].Value.ToString() != string.Empty)
                    {
                        if (dr.Cells["dgvcmbCurrency"].Value != null)
                        {
                            decSelectedCurrencyRate = SpExchangRate.GetExchangeRateByExchangeRateId(Convert.ToDecimal(dr.Cells["dgvcmbCurrency"].Value.ToString()));//Exchange rate of grid's row
                            decTotalAmount = decTotalAmount + (Convert.ToDecimal(dr.Cells["dgvtxtAmount"].Value.ToString()) * decSelectedCurrencyRate);
                        }
                        else
                        {
                            decTotalAmount = decTotalAmount + Convert.ToDecimal(dr.Cells["dgvtxtAmount"].Value.ToString());
                        }
                    }
                }
                txtTotal.Text = Math.Round(decTotalAmount, PublicVariables._inNoOfDecimalPlaces).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV19:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// To calculate total amount when "X" is header text in grid
        /// </summary>
        /// <returns></returns>
        public decimal TotalAmountCalculation()
        {
            decimal decTotal = 0;
            decimal decSelectedCurrencyRate = 0;
            ExchangeRateSP SpExchangRate = new ExchangeRateSP();
            try
            {
                for (int inI = 0; inI < dgvReceiptVoucher.RowCount - 1; inI++)
                {
                    if (dgvReceiptVoucher.Rows[inI].HeaderCell.Value.ToString() != "X")
                    {
                        decSelectedCurrencyRate = SpExchangRate.GetExchangeRateByExchangeRateId(Convert.ToDecimal(dgvReceiptVoucher.Rows[inI].Cells["dgvcmbCurrency"].Value.ToString()));//Exchange rate of grid's row
                        decTotal = decTotal + (Convert.ToDecimal(dgvReceiptVoucher.Rows[inI].Cells["dgvtxtAmount"].Value.ToString()) * decSelectedCurrencyRate);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV20:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return decTotal;
        }
        /// <summary>
        /// Ledger Posting master details
        /// </summary>
        public void MasterLedgerPosting()
        {
            try
            {
                LedgerPostingInfo InfoLedgerPosting = new LedgerPostingInfo();
                LedgerPostingSP SpLedgerPosting = new LedgerPostingSP();
                ExchangeRateSP SpExchangRate = new ExchangeRateSP();
                InfoLedgerPosting.Debit = Convert.ToDecimal(txtTotal.Text.ToString());
                InfoLedgerPosting.Date = dtpDate.Value;
                InfoLedgerPosting.Credit = 0;
                InfoLedgerPosting.DetailsId = 0;
                InfoLedgerPosting.Extra1 = string.Empty;
                InfoLedgerPosting.Extra2 = string.Empty;
                InfoLedgerPosting.InvoiceNo = strInvoiceNo;
                InfoLedgerPosting.ChequeNo = string.Empty;
                InfoLedgerPosting.ChequeDate = DateTime.Now;
                InfoLedgerPosting.LedgerId = Convert.ToDecimal(cmbCashOrBank.SelectedValue.ToString());
                if (!isAutomatic)
                {
                    InfoLedgerPosting.VoucherNo = txtVoucherNo.Text.Trim();
                }
                else
                {
                    InfoLedgerPosting.VoucherNo = strVoucherNo;
                }
                InfoLedgerPosting.VoucherTypeId = decReceiptVoucherTypeId;
                InfoLedgerPosting.YearId = PublicVariables._decCurrentFinancialYearId;
                SpLedgerPosting.LedgerPostingAdd(InfoLedgerPosting);
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV21:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Ledger posting Edit function
        /// </summary>
        public void MasterLedgerPostingEdit()
        {
            try
            {
                LedgerPostingInfo InfoLedgerPosting = new LedgerPostingInfo();
                LedgerPostingSP SpLedgerPosting = new LedgerPostingSP();
                ExchangeRateSP SpExchangRate = new ExchangeRateSP();
                InfoLedgerPosting.Debit = Convert.ToDecimal(txtTotal.Text.ToString());
                InfoLedgerPosting.Date = dtpDate.Value;
                InfoLedgerPosting.Credit = 0;
                InfoLedgerPosting.DetailsId = 0;
                InfoLedgerPosting.Extra1 = string.Empty;
                InfoLedgerPosting.Extra2 = string.Empty;
                InfoLedgerPosting.InvoiceNo = strInvoiceNo;
                InfoLedgerPosting.ChequeNo = string.Empty;
                InfoLedgerPosting.ChequeDate = DateTime.Now;
                InfoLedgerPosting.LedgerId = Convert.ToDecimal(cmbCashOrBank.SelectedValue.ToString());
                if (!isAutomatic)
                {
                    InfoLedgerPosting.VoucherNo = txtVoucherNo.Text.Trim();
                }
                else
                {
                    InfoLedgerPosting.VoucherNo = strVoucherNo;
                }
                InfoLedgerPosting.VoucherTypeId = decReceiptVoucherTypeId;
                InfoLedgerPosting.YearId = PublicVariables._decCurrentFinancialYearId;
                SpLedgerPosting.LedgerPostingEditByVoucherTypeAndVoucherNo(InfoLedgerPosting);
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV22:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Ledger posting details Save
        /// </summary>
        /// <param name="inA"></param>
        /// <param name="decreceiptDetailsId"></param>
        public void DetailsLedgerPosting(int inA, decimal decreceiptDetailsId)
        {
            LedgerPostingInfo InfoLedgerPosting = new LedgerPostingInfo();
            LedgerPostingSP SpLedgerPosting = new LedgerPostingSP();
            ExchangeRateSP SpExchangRate = new ExchangeRateSP();
            SettingsSP spSettings = new SettingsSP();
            decimal decOldExchange = 0;
            decimal decNewExchangeRate = 0;
            decimal decNewExchangeRateId = 0;
            decimal decOldExchangeId = 0;
            decConvertRate = 0;
            try
            {
                if (!dgvReceiptVoucher.Rows[inA].Cells["dgvtxtAmount"].ReadOnly)
                {

                    decimal d = Convert.ToDecimal(dgvReceiptVoucher.Rows[inA].Cells["dgvcmbCurrency"].Value.ToString());
                    InfoLedgerPosting.LedgerId = Convert.ToDecimal(dgvReceiptVoucher.Rows[inA].Cells["dgvcmbAccountLedger"].Value.ToString());


                    decSelectedCurrencyRate = SpExchangRate.GetExchangeRateByExchangeRateId(Convert.ToDecimal(dgvReceiptVoucher.Rows[inA].Cells["dgvcmbCurrency"].Value.ToString()));
                    decAmount = Convert.ToDecimal(dgvReceiptVoucher.Rows[inA].Cells["dgvtxtAmount"].Value.ToString());
                    decConvertRate = decAmount * decSelectedCurrencyRate;


                    InfoLedgerPosting.Date = dtpDate.Value;
                    InfoLedgerPosting.Debit = 0;
                    InfoLedgerPosting.Credit = decConvertRate;
                    InfoLedgerPosting.DetailsId = decreceiptDetailsId;
                    InfoLedgerPosting.Extra1 = string.Empty;
                    InfoLedgerPosting.Extra2 = string.Empty;
                    InfoLedgerPosting.InvoiceNo = txtVoucherNo.Text;
                    if (dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeNo"].Value != null && dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeNo"].Value.ToString() != string.Empty)
                    {
                        InfoLedgerPosting.ChequeNo = dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeNo"].Value.ToString();
                        if (dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeDate"].Value != null && dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeDate"].Value.ToString() != string.Empty)
                        {
                            InfoLedgerPosting.ChequeDate = Convert.ToDateTime(dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeDate"].Value.ToString());
                        }
                        else
                            InfoLedgerPosting.ChequeDate = DateTime.Now;
                    }
                    else
                    {
                        InfoLedgerPosting.ChequeNo = string.Empty;
                        InfoLedgerPosting.ChequeDate = DateTime.Now;
                    }


                    InfoLedgerPosting.VoucherNo = strVoucherNo;

                    InfoLedgerPosting.VoucherTypeId = decReceiptVoucherTypeId;
                    InfoLedgerPosting.YearId = PublicVariables._decCurrentFinancialYearId;
                    SpLedgerPosting.LedgerPostingAdd(InfoLedgerPosting);
                }
                else
                {
                    InfoLedgerPosting.Date = dtpDate.Value;

                    InfoLedgerPosting.Extra1 = string.Empty;
                    InfoLedgerPosting.Extra2 = string.Empty;
                    InfoLedgerPosting.InvoiceNo = strInvoiceNo;
                    InfoLedgerPosting.VoucherTypeId = decReceiptVoucherTypeId;
                    InfoLedgerPosting.YearId = PublicVariables._decCurrentFinancialYearId;

                    InfoLedgerPosting.Debit = 0;
                    InfoLedgerPosting.LedgerId = Convert.ToDecimal(dgvReceiptVoucher.Rows[inA].Cells["dgvcmbAccountLedger"].Value.ToString());
                    InfoLedgerPosting.VoucherNo = strVoucherNo;
                    InfoLedgerPosting.DetailsId = decreceiptDetailsId;
                    InfoLedgerPosting.InvoiceNo = txtVoucherNo.Text.Trim();
                    if (dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeNo"].Value != null && dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeNo"].Value.ToString() != string.Empty)
                    {
                        InfoLedgerPosting.ChequeNo = dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeNo"].Value.ToString();
                        if (dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeDate"].Value != null && dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeDate"].Value.ToString() != string.Empty)
                        {
                            InfoLedgerPosting.ChequeDate = Convert.ToDateTime(dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeDate"].Value.ToString());
                        }
                        else
                            InfoLedgerPosting.ChequeDate = DateTime.Now;
                    }
                    else
                    {
                        InfoLedgerPosting.ChequeNo = string.Empty;
                        InfoLedgerPosting.ChequeDate = DateTime.Now;
                    }

                    foreach (DataRow dr in dtblPartyBalance.Rows)
                    {
                        if (InfoLedgerPosting.LedgerId == Convert.ToDecimal(dr["LedgerId"].ToString()))
                        {
                            decOldExchange = Convert.ToDecimal(dr["OldExchangeRate"].ToString());
                            decNewExchangeRateId = Convert.ToDecimal(dr["CurrencyId"].ToString());
                            decSelectedCurrencyRate = SpExchangRate.GetExchangeRateByExchangeRateId(decOldExchange);
                            decAmount = Convert.ToDecimal(dr["Amount"].ToString());
                            decConvertRate = decConvertRate + (decAmount * decSelectedCurrencyRate);

                        }
                    }
                    InfoLedgerPosting.Credit = decConvertRate;
                    SpLedgerPosting.LedgerPostingAdd(InfoLedgerPosting);

                    InfoLedgerPosting.LedgerId = 12;
                    foreach (DataRow dr in dtblPartyBalance.Rows)
                    {
                        if (Convert.ToDecimal(dgvReceiptVoucher.Rows[inA].Cells["dgvcmbAccountLedger"].Value.ToString()) == Convert.ToDecimal(dr["LedgerId"].ToString()))
                        {
                            if (dr["ReferenceType"].ToString() == "Against")
                            {
                                decNewExchangeRateId = Convert.ToDecimal(dr["CurrencyId"].ToString());
                                decNewExchangeRate = SpExchangRate.GetExchangeRateByExchangeRateId(decNewExchangeRateId);
                                decOldExchangeId = Convert.ToDecimal(dr["OldExchangeRate"].ToString());
                                decOldExchange = SpExchangRate.GetExchangeRateByExchangeRateId(decOldExchangeId);
                                decAmount = Convert.ToDecimal(dr["Amount"].ToString());
                                decimal decForexAmount = (decAmount * decNewExchangeRate) - (decAmount * decOldExchange);
                                if (decForexAmount >= 0)
                                {

                                    InfoLedgerPosting.Credit = decForexAmount;
                                    InfoLedgerPosting.Debit = 0;
                                }
                                else
                                {
                                    InfoLedgerPosting.Debit = -1 * decForexAmount;
                                    InfoLedgerPosting.Credit = 0;
                                }
                                SpLedgerPosting.LedgerPostingAdd(InfoLedgerPosting);
                            }
                        }

                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("RV23:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }





        /// <summary>
        /// Ledger posting details Edit
        /// </summary>
        /// <param name="inA"></param>
        /// <param name="decLedgerPostingId"></param>
        /// <param name="decreceiptDetailsId"></param>
        public void DetailsLedgerPostingEdit(int inA, decimal decLedgerPostingId, decimal decreceiptDetailsId)
        {
            LedgerPostingInfo InfoLedgerPosting = new LedgerPostingInfo();
            LedgerPostingSP SpLedgerPosting = new LedgerPostingSP();
            ExchangeRateSP SpExchangRate = new ExchangeRateSP();
            decimal decOldExchange = 0;
            decimal decNewExchangeRate = 0;
            decimal decNewExchangeRateId = 0;
            decimal decOldExchangeId = 0;
            decConvertRate = 0;
            try
            {
                if (!dgvReceiptVoucher.Rows[inA].Cells["dgvtxtAmount"].ReadOnly)
                {
                    decSelectedCurrencyRate = SpExchangRate.GetExchangeRateByExchangeRateId(Convert.ToDecimal(dgvReceiptVoucher.Rows[inA].Cells["dgvcmbCurrency"].Value.ToString()));
                    decAmount = Convert.ToDecimal(dgvReceiptVoucher.Rows[inA].Cells["dgvtxtAmount"].Value.ToString());
                    decConvertRate = decAmount * decSelectedCurrencyRate;
                    InfoLedgerPosting.Debit = 0;
                    InfoLedgerPosting.Date = dtpDate.Value;
                    InfoLedgerPosting.Credit = decConvertRate;
                    InfoLedgerPosting.DetailsId = decreceiptDetailsId;
                    InfoLedgerPosting.Extra1 = string.Empty;
                    InfoLedgerPosting.Extra2 = string.Empty;
                    InfoLedgerPosting.InvoiceNo = strInvoiceNo;
                    if (dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeNo"].Value != null && dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeNo"].Value.ToString() != string.Empty)
                    {
                        InfoLedgerPosting.ChequeNo = dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeNo"].Value.ToString();
                        if (dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeDate"].Value != null && dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeDate"].Value.ToString() != string.Empty)
                        {
                            InfoLedgerPosting.ChequeDate = Convert.ToDateTime(dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeDate"].Value.ToString());
                        }
                        else
                            InfoLedgerPosting.ChequeDate = DateTime.Now;
                    }
                    else
                    {
                        InfoLedgerPosting.ChequeNo = string.Empty;
                        InfoLedgerPosting.ChequeDate = DateTime.Now;
                    }
                    InfoLedgerPosting.LedgerId = Convert.ToDecimal(dgvReceiptVoucher.Rows[inA].Cells["dgvcmbAccountLedger"].Value.ToString());
                    if (!isAutomatic)
                    {
                        InfoLedgerPosting.VoucherNo = txtVoucherNo.Text.Trim();
                    }
                    else
                    {
                        InfoLedgerPosting.VoucherNo = strVoucherNo;
                    }
                    InfoLedgerPosting.VoucherTypeId = decReceiptVoucherTypeId;
                    InfoLedgerPosting.YearId = PublicVariables._decCurrentFinancialYearId;
                    InfoLedgerPosting.LedgerPostingId = decLedgerPostingId;
                    SpLedgerPosting.LedgerPostingEdit(InfoLedgerPosting);
                }
                else
                {
                    InfoLedgerPosting.Date = dtpDate.Value;
                    InfoLedgerPosting.Extra1 = string.Empty;
                    InfoLedgerPosting.Extra2 = string.Empty;
                    InfoLedgerPosting.InvoiceNo = strInvoiceNo;
                    InfoLedgerPosting.VoucherTypeId = decReceiptVoucherTypeId;
                    InfoLedgerPosting.YearId = PublicVariables._decCurrentFinancialYearId;
                    InfoLedgerPosting.Debit = 0;
                    InfoLedgerPosting.LedgerId = Convert.ToDecimal(dgvReceiptVoucher.Rows[inA].Cells["dgvcmbAccountLedger"].Value.ToString());
                    InfoLedgerPosting.VoucherNo = strVoucherNo;
                    InfoLedgerPosting.DetailsId = decreceiptDetailsId;
                    InfoLedgerPosting.InvoiceNo = txtVoucherNo.Text.Trim();
                    if (dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeNo"].Value != null && dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeNo"].Value.ToString() != string.Empty)
                    {
                        InfoLedgerPosting.ChequeNo = dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeNo"].Value.ToString();
                        if (dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeDate"].Value != null && dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeDate"].Value.ToString() != string.Empty)
                        {
                            InfoLedgerPosting.ChequeDate = Convert.ToDateTime(dgvReceiptVoucher.Rows[inA].Cells["dgvtxtChequeDate"].Value.ToString());
                        }
                        else
                            InfoLedgerPosting.ChequeDate = DateTime.Now;
                    }
                    else
                    {
                        InfoLedgerPosting.ChequeNo = string.Empty;
                        InfoLedgerPosting.ChequeDate = DateTime.Now;
                    }
                    foreach (DataRow dr in dtblPartyBalance.Rows)
                    {
                        if (InfoLedgerPosting.LedgerId == Convert.ToDecimal(dr["LedgerId"].ToString()))
                        {
                            decOldExchange = Convert.ToDecimal(dr["OldExchangeRate"].ToString());
                            decNewExchangeRateId = Convert.ToDecimal(dr["CurrencyId"].ToString());
                            decSelectedCurrencyRate = SpExchangRate.GetExchangeRateByExchangeRateId(decOldExchange);
                            decAmount = Convert.ToDecimal(dr["Amount"].ToString());
                            decConvertRate = decConvertRate + (decAmount * decSelectedCurrencyRate);

                        }
                    }
                    InfoLedgerPosting.Credit = decConvertRate;
                    InfoLedgerPosting.LedgerPostingId = decLedgerPostingId;
                    SpLedgerPosting.LedgerPostingEdit(InfoLedgerPosting);


                    InfoLedgerPosting.LedgerId = 12;
                    foreach (DataRow dr in dtblPartyBalance.Rows)
                    {
                        if (Convert.ToDecimal(dgvReceiptVoucher.Rows[inA].Cells["dgvcmbAccountLedger"].Value.ToString()) == Convert.ToDecimal(dr["LedgerId"].ToString()))
                        {
                            if (dr["ReferenceType"].ToString() == "Against")
                            {
                                decNewExchangeRateId = Convert.ToDecimal(dr["CurrencyId"].ToString());
                                decNewExchangeRate = SpExchangRate.GetExchangeRateByExchangeRateId(decNewExchangeRateId);
                                decOldExchangeId = Convert.ToDecimal(dr["OldExchangeRate"].ToString());
                                decOldExchange = SpExchangRate.GetExchangeRateByExchangeRateId(decOldExchangeId);
                                decAmount = Convert.ToDecimal(dr["Amount"].ToString());
                                decimal decForexAmount = (decAmount * decNewExchangeRate) - (decAmount * decOldExchange);
                                if (decForexAmount >= 0)
                                {

                                    InfoLedgerPosting.Credit = decForexAmount;
                                    InfoLedgerPosting.Debit = 0;
                                }
                                else
                                {
                                    InfoLedgerPosting.Debit = -1 * decForexAmount;
                                    InfoLedgerPosting.Credit = 0;
                                }
                                SpLedgerPosting.LedgerPostingAdd(InfoLedgerPosting);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV24:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Remove Function
        /// </summary>
        public void Remove()
        {
            try
            {
                if (dgvReceiptVoucher.CurrentRow != null)
                {
                    if (dgvReceiptVoucher.CurrentRow.Index != dgvReceiptVoucher.NewRowIndex)
                    {
                        if (Convert.ToInt32(dgvReceiptVoucher.CurrentRow.Cells["dgvtxtSlNo"].Value.ToString()) < dgvReceiptVoucher.RowCount)
                        {
                            if (btnSave.Text == "Update")
                            {
                                if (dgvReceiptVoucher.CurrentRow.Cells["dgvtxtreceiptDetailsId"].Value != null && dgvReceiptVoucher.CurrentRow.Cells["dgvtxtreceiptDetailsId"].Value.ToString() != string.Empty)
                                {
                                    arrlstOfRemove.Add(dgvReceiptVoucher.CurrentRow.Cells["dgvtxtreceiptDetailsId"].Value.ToString());
                                    arrlstOfRemovedLedgerPostingId.Add(dgvReceiptVoucher.CurrentRow.Cells["dgvtxtLedgerPostingId"].Value.ToString());
                                    inArrOfRemoveIndex++;
                                }
                            }
                            int inTableRowCount = dtblPartyBalance.Rows.Count;
                            for (int inI = 0; inI < inTableRowCount; inI++)
                            {
                                if (dtblPartyBalance.Rows.Count == inI)
                                {
                                    break;
                                }
                                if (dtblPartyBalance.Rows[inI]["LedgerId"].ToString() == dgvReceiptVoucher.CurrentRow.Cells["dgvcmbAccountLedger"].Value.ToString())
                                {
                                    if (dtblPartyBalance.Rows[inI]["PartyBalanceId"].ToString() != "0")
                                    {
                                        arrlstOfDeletedPartyBalanceRow.Add(dtblPartyBalance.Rows[inI]["PartyBalanceId"]);
                                    }
                                    dtblPartyBalance.Rows.RemoveAt(inI);
                                    inI--;
                                }
                            }
                            if (inUpdatingRowIndexForPartyRemove == dgvReceiptVoucher.CurrentRow.Index)
                            {
                                inUpdatingRowIndexForPartyRemove = -1;
                                decUpdatingLedgerForPartyremove = 0;
                            }
                            dgvReceiptVoucher.Rows.RemoveAt(this.dgvReceiptVoucher.CurrentRow.Index);
                            SerialNumberGeneration();
                            TotalAmount();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV25:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Checking the column missing or invalid column in grid
        /// </summary>
        public void CheckColumnMissing()
        {
            try
            {
                if (dgvReceiptVoucher.CurrentRow != null)
                {
                    if (!isValueChanged)
                    {
                        if (dgvReceiptVoucher.CurrentRow.Cells["dgvcmbAccountLedger"].FormattedValue == null || dgvReceiptVoucher.CurrentRow.Cells["dgvcmbAccountLedger"].FormattedValue.ToString().Trim() == string.Empty)
                        {
                            isValueChanged = true;
                            dgvReceiptVoucher.CurrentRow.HeaderCell.Value = "X";
                            dgvReceiptVoucher.CurrentRow.HeaderCell.Style.ForeColor = Color.Red;
                        }
                        else if (dgvReceiptVoucher.CurrentRow.Cells["dgvtxtAmount"].Value == null || dgvReceiptVoucher.CurrentRow.Cells["dgvtxtAmount"].Value.ToString().Trim() == string.Empty)
                        {
                            isValueChanged = true;
                            dgvReceiptVoucher.CurrentRow.HeaderCell.Value = "X";
                            dgvReceiptVoucher.CurrentRow.HeaderCell.Style.ForeColor = Color.Red;
                        }
                        else if (dgvReceiptVoucher.CurrentRow.Cells["dgvcmbCurrency"].FormattedValue == null || dgvReceiptVoucher.CurrentRow.Cells["dgvcmbCurrency"].FormattedValue.ToString().Trim() == string.Empty)
                        {
                            isValueChanged = true;
                            dgvReceiptVoucher.CurrentRow.HeaderCell.Value = "X";
                            dgvReceiptVoucher.CurrentRow.HeaderCell.Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            isValueChanged = true;
                            dgvReceiptVoucher.CurrentRow.HeaderCell.Value = string.Empty;
                        }
                    }
                    isValueChanged = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV26:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Save or edit function and checking the invalid entries
        /// </summary>
        public void SaveOrEdit()
        {
            try
            {
                ReceiptMasterSP SpReceiptMaster = new ReceiptMasterSP();
                int inIfGridColumnMissing = 0;
                int inRowCount = dgvReceiptVoucher.RowCount;
                ArrayList arrLst = new ArrayList();
                string output = string.Empty;
                if (txtVoucherNo.Text == string.Empty)
                {
                    Messages.InformationMessage("Enter voucher number.");
                    txtVoucherNo.Focus();
                    inIfGridColumnMissing = 1;
                }
                else if (cmbCashOrBank.SelectedIndex == -1)
                {
                    Messages.InformationMessage("Select any bank or cash.");
                    cmbCashOrBank.Focus();
                    inIfGridColumnMissing = 1;
                }
                else if (inRowCount == 1)
                {
                    Messages.InformationMessage("Can't save without atleat one complete details");
                    dgvReceiptVoucher.Focus();
                    inIfGridColumnMissing = 1;
                }
                else if (Convert.ToDecimal(txtTotal.Text) == 0)
                {
                    Messages.InformationMessage("Can't save total amount as Zero");
                    dgvReceiptVoucher.Focus();
                }
                else
                {
                    int inJ = 0;
                    for (int inI = 0; inI < inRowCount - 1; inI++)
                    {
                        if (dgvReceiptVoucher.Rows[inI].HeaderCell.Value.ToString() == "X")
                        {
                            arrLst.Add(Convert.ToString(inI + 1));
                            inIfGridColumnMissing = 1;
                            inJ++;
                        }
                    }
                    if (inJ != 0)
                    {
                        if (inJ == inRowCount - 1)
                        {
                            Messages.InformationMessage("Can't save without atleat one complete details");
                            inIfGridColumnMissing = 1;
                        }
                        else
                        {
                            foreach (object obj in arrLst)
                            {
                                string str = Convert.ToString(obj);
                                if (str != null)
                                {
                                    output += str + ",";
                                }
                                else
                                {
                                    break;
                                }
                            }
                            bool isOk = Messages.UpdateMessageCustom("Row No " + output + " not completed.Do you want to continue?");
                            if (isOk == true)
                            {
                                inIfGridColumnMissing = 0;
                            }
                            else
                            {
                                inIfGridColumnMissing = 1;
                            }
                        }
                    }
                    if (inIfGridColumnMissing == 0)
                    {
                        if (btnSave.Text == "Save")
                        {
                            if (!isAutomatic)
                            {
                                if (SpReceiptMaster.ReceiptVoucherCheckExistence(txtVoucherNo.Text.Trim(), decReceiptVoucherTypeId, 0))
                                {
                                    Messages.InformationMessage("Voucher number already exist");
                                }
                                else
                                {
                                    if (PublicVariables.isMessageAdd)
                                    {
                                        if (Messages.SaveMessage())
                                        {
                                            Save();
                                        }
                                    }
                                    else
                                    {
                                        Save();
                                    }
                                }
                            }
                            else
                            {
                                if (PublicVariables.isMessageAdd)
                                {
                                    if (Messages.SaveMessage())
                                    {
                                        Save();
                                    }
                                }
                                else
                                {
                                    Save();
                                }
                            }
                        }
                        else if (btnSave.Text == "Update")
                        {
                            if (!isAutomatic)
                            {
                                if (SpReceiptMaster.ReceiptVoucherCheckExistence(txtVoucherNo.Text.Trim(), decReceiptVoucherTypeId, decRecieptmasterId))
                                {
                                    Messages.InformationMessage("Voucher number already exist");
                                    txtVoucherNo.Focus();
                                }
                                else
                                {
                                    if (PublicVariables.isMessageEdit)
                                    {
                                        if (Messages.UpdateMessage())
                                        {
                                            Edit(decRecieptmasterId);
                                        }
                                    }
                                    else
                                    {
                                        Edit(decRecieptmasterId);
                                    }
                                }
                            }
                            else
                            {
                                if (PublicVariables.isMessageEdit)
                                {
                                    if (Messages.UpdateMessage())
                                    {
                                        Edit(decRecieptmasterId);
                                    }
                                }
                                else
                                {
                                    Edit(decRecieptmasterId);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV27:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to call frmLedgerPopup form to select and view Ledger
        /// </summary>
        /// <param name="frmLedgerPopup"></param>
        /// <param name="decId"></param>
        /// <param name="str"></param>
        public void CallFromLedgerPopup(frmLedgerPopup frmLedgerPopup, decimal decId, string str)
        {
            try
            {
                this.Enabled = true;
                this.frmLedgerPopupObj = frmLedgerPopup;
                if (str == "CashOrBank")
                {
                    TransactionsGeneralFill obj = new TransactionsGeneralFill();
                    obj.CashOrBankComboFill(cmbCashOrBank, false);
                    cmbCashOrBank.SelectedValue = decId;
                    cmbCashOrBank.Focus();
                }
                else
                {
                    dgvReceiptVoucher.CurrentRow.Cells["dgvcmbAccountLedger"].Value = decId;
                    dgvReceiptVoucher.Focus();
                }
                frmLedgerPopupObj.Close();
                frmLedgerPopupObj = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV28:" + ex.Message, "Open Miracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to fill Account ledger combobox while return from Account ledger creation when creating new ledger 
        /// </summary>
        /// <param name="decId"></param>
        /// <param name="str"></param>
        public void ReturnFromAccountLedgerForm(decimal decId, string str)//From AccountLedger form
        {
            try
            {
                if (str == "CashOrBank")
                {
                    if (decId != 0)
                    {
                        TransactionsGeneralFill Obj = new TransactionsGeneralFill();
                        Obj.CashOrBankComboFill(cmbCashOrBank, false);
                        cmbCashOrBank.SelectedValue = decId.ToString();
                    }
                    cmbCashOrBank.Focus();
                }
                else
                {
                    if (decId != 0)
                    {

                        DataTable dtbl = new DataTable();
                        AccountLedgerSP spAccountLedger = new AccountLedgerSP();
                        dtbl = spAccountLedger.AccountLedgerViewAll();
                        DataGridViewComboBoxCell dgvccCashOrBank = (DataGridViewComboBoxCell)dgvReceiptVoucher[dgvReceiptVoucher.Columns["dgvcmbAccountLedger"].Index, dgvReceiptVoucher.CurrentRow.Index];
                        dgvccCashOrBank.DataSource = dtbl;
                        dgvccCashOrBank.ValueMember = "ledgerId";
                        dgvccCashOrBank.DisplayMember = "ledgerName";
                        dgvReceiptVoucher.CurrentRow.Cells["dgvcmbAccountLedger"].Value = decId;
                    }
                }
                this.Enabled = true;
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV29:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to call this form from frmBillallocation to view details and for updation 
        /// </summary>
        /// <param name="frmBillallocation"></param>
        /// <param name="decReceiptId"></param>
        public void CallFromBillAllocation(frmBillallocation frmBillallocation, decimal decReceiptId)
        {
            try
            {
                frmBillallocation.Enabled = false;
                base.Show();
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
                frmBillallocationObj = frmBillallocation;
                decRecieptmasterId = decReceiptId;
                FillFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV30:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to call this form from frmDayBook to view details and for updation 
        /// </summary>
        /// <param name="frmDayBook"></param>
        /// <param name="decMasterId"></param>
        public void callFromDayBook(frmDayBook frmDayBook, decimal decMasterId)
        {
            try
            {
                base.Show();
                frmDayBook.Enabled = false;
                this.frmDayBookObj = frmDayBook;
                btnDelete.Enabled = true;
                btnSave.Text = "Update";
                decRecieptmasterId = decMasterId;
                FillFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV31:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to call this form from frmChequeReport to view details and for updation 
        /// </summary>
        /// <param name="frmChequeReport"></param>
        /// <param name="decMasterId"></param>
        public void CallFromChequeReport(frmChequeReport frmChequeReport, decimal decMasterId)
        {
            try
            {
                base.Show();
                frmChequeReport.Enabled = false;
                this.frmChequeReportObj = frmChequeReport;
                btnDelete.Enabled = true;
                btnSave.Text = "Update";
                decRecieptmasterId = decMasterId;
                FillFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV32:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to call this form from frmAgeingReport to view details and for updation 
        /// </summary>
        /// <param name="frmAgeing"></param>
        /// <param name="decMasterId"></param>
        public void callFromAgeing(frmAgeingReport frmAgeing, decimal decMasterId)
        {
            try
            {
                base.Show();
                frmAgeing.Enabled = false;
                this.frmAgeingObj = frmAgeing;
                btnDelete.Enabled = true;
                btnSave.Text = "Update";
                decRecieptmasterId = decMasterId;
                FillFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV33:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Fill function for updation
        /// </summary>
        public void FillFunction()
        {
            try
            {
                isValueChange = false;
                ReceiptMasterInfo InfoReceiptMaster = new ReceiptMasterInfo();
                ReceiptMasterSP SpReceiptMaster = new ReceiptMasterSP();
                ReceiptDetailsInfo InfoReceiptDetails = new ReceiptDetailsInfo();
                ReceiptDetailsSP SpReceiptDetails = new ReceiptDetailsSP();
                PartyBalanceSP SpPartyBalance = new PartyBalanceSP();
                LedgerPostingSP SpLedgerPosting = new LedgerPostingSP();
                VoucherTypeSP SpVoucherType = new VoucherTypeSP();
                AccountGroupSP spAccountGroup = new AccountGroupSP();
                AccountLedgerSP SpAccountLedger = new AccountLedgerSP();
                InfoReceiptMaster = SpReceiptMaster.ReceiptMasterViewByMasterId(decRecieptmasterId);
                isAutomatic = SpVoucherType.CheckMethodOfVoucherNumbering(InfoReceiptMaster.VoucherTypeId);
                if (isAutomatic)
                {
                    txtVoucherNo.ReadOnly = true;
                    txtVoucherNo.Text = InfoReceiptMaster.InvoiceNo;
                }
                else
                {
                    txtVoucherNo.ReadOnly = false;
                    txtVoucherNo.Text = InfoReceiptMaster.VoucherNo;
                }
                dtpDate.Value = InfoReceiptMaster.Date;
                cmbCashOrBank.SelectedValue = InfoReceiptMaster.LedgerId;
                txtNarration.Text = InfoReceiptMaster.Narration;
                txtTotal.Text = InfoReceiptMaster.TotalAmount.ToString();
                decDailySuffixPrefixId = InfoReceiptMaster.SuffixPrefixId;
                decReceiptVoucherTypeId = InfoReceiptMaster.VoucherTypeId;
                strVoucherNo = InfoReceiptMaster.VoucherNo;
                strInvoiceNo = InfoReceiptMaster.InvoiceNo;
                DataTable dtbl = new DataTable();
                dtbl = SpReceiptDetails.ReceiptDetailsViewByMasterId(decRecieptmasterId);
                for (int inI = 0; inI < dtbl.Rows.Count; inI++)
                {
                    dgvReceiptVoucher.Rows.Add();
                    dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value = Convert.ToDecimal(dtbl.Rows[inI]["ledgerId"].ToString());
                    dgvReceiptVoucher.Rows[inI].Cells["dgvtxtreceiptMasterId"].Value = dtbl.Rows[inI]["receiptMasterId"].ToString();
                    dgvReceiptVoucher.Rows[inI].Cells["dgvtxtreceiptDetailsId"].Value = dtbl.Rows[inI]["receiptDetailsId"].ToString();
                    dgvReceiptVoucher.Rows[inI].Cells["dgvtxtAmount"].Value = dtbl.Rows[inI]["amount"].ToString();
                    dgvReceiptVoucher.Rows[inI].Cells["dgvcmbCurrency"].Value = Convert.ToDecimal(dtbl.Rows[inI]["exchangeRateId"].ToString());
                    decimal decDetailsId1 = Convert.ToDecimal(dtbl.Rows[inI]["receiptDetailsId"].ToString());
                    decimal decLedgerPostingId = SpLedgerPosting.LedgerPostingIdFromDetailsId(decDetailsId1, strVoucherNo, decReceiptVoucherTypeId);
                    dgvReceiptVoucher.Rows[inI].Cells["dgvtxtLedgerPostingId"].Value = decLedgerPostingId.ToString();
                    decimal decLedgerId = Convert.ToDecimal(dtbl.Rows[inI]["ledgerId"].ToString());
                    bool IsBankAccount = spAccountGroup.AccountGroupwithLedgerId(decLedgerId);
                    decimal decI = Convert.ToDecimal(SpAccountLedger.AccountGroupIdCheck(dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].FormattedValue.ToString()));
                    if (decI > 0)
                    {
                        dgvReceiptVoucher.Rows[inI].Cells["dgvtxtAmount"].ReadOnly = true;
                        dgvReceiptVoucher.Rows[inI].Cells["dgvcmbCurrency"].ReadOnly = true;
                    }
                    else
                    {
                        dgvReceiptVoucher.Rows[inI].Cells["dgvtxtAmount"].ReadOnly = false;
                        dgvReceiptVoucher.Rows[inI].Cells["dgvcmbCurrency"].ReadOnly = false;
                    }
                    if (dtbl.Rows[inI]["chequeNo"].ToString() != string.Empty)
                    {
                        dgvReceiptVoucher.Rows[inI].Cells["dgvtxtChequeNo"].Value = dtbl.Rows[inI]["chequeNo"].ToString();
                        dgvReceiptVoucher.Rows[inI].Cells["dgvtxtChequeDate"].Value = Convert.ToDateTime(dtbl.Rows[inI]["chequeDate"].ToString()).ToString("dd-MMM-yyyy");
                    }
                    dgvReceiptVoucher.Rows[inI].HeaderCell.Value = string.Empty;
                }
                DataTable dtbl1 = new DataTable();
                dtbl1 = SpPartyBalance.PartyBalanceViewByVoucherNoAndVoucherType(decReceiptVoucherTypeId, strVoucherNo, InfoReceiptMaster.Date);
                dtblPartyBalance = dtbl1;
                isValueChange = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV34:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Function to call this form from frmLedgerDetails to view details and for updation 
        /// </summary>
        /// <param name="frmLedgerdetails"></param>
        /// <param name="decMasterId"></param>
        public void CallFromLedgerDetails(frmLedgerDetails frmLedgerdetails, decimal decMasterId)
        {
            try
            {
                base.Show();
                frmLedgerDetailsObj = frmLedgerdetails;
                frmLedgerDetailsObj.Enabled = false;
                btnDelete.Enabled = true;
                btnSave.Text = "Update";
                decRecieptmasterId = decMasterId;
                FillFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV35:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Adding the columns into the data table to perform the party balance entry's
        /// </summary>
        public void DataTableForPartyBalance()
        {
            try
            {
                dtblPartyBalance.Columns.Add("LedgerId", typeof(decimal));
                dtblPartyBalance.Columns.Add("AgainstVoucherTypeId", typeof(decimal));
                dtblPartyBalance.Columns.Add("AgainstVoucherNo", typeof(string));
                dtblPartyBalance.Columns.Add("ReferenceType", typeof(string));
                dtblPartyBalance.Columns.Add("Amount", typeof(decimal));
                dtblPartyBalance.Columns.Add("AgainstInvoiceNo", typeof(string));
                dtblPartyBalance.Columns.Add("DebitOrCredit", typeof(string));
                dtblPartyBalance.Columns.Add("CurrencyId", typeof(decimal));
                dtblPartyBalance.Columns.Add("PendingAmount", typeof(decimal));
                dtblPartyBalance.Columns.Add("PartyBalanceId", typeof(decimal));
                dtblPartyBalance.Columns.Add("VoucherTypeId", typeof(decimal));
                dtblPartyBalance.Columns.Add("VoucherNo", typeof(string));
                dtblPartyBalance.Columns.Add("InvoiceNo", typeof(string));
                dtblPartyBalance.Columns.Add("OldExchangeRate", typeof(decimal));
                arrlstOfDeletedPartyBalanceRow = new ArrayList();
            }
            catch (Exception ex)
            {

                MessageBox.Show("RV36:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmReceiptVoucher_Load(object sender, EventArgs e)
        {
            try
            {
                Clear();
                btnDelete.Enabled = false;
                dtpDate.Value = PublicVariables._dtCurrentDate;
                dtpDate.MinDate = PublicVariables._dtFromDate;
                dtpDate.MaxDate = PublicVariables._dtToDate;
                dtpDate.CustomFormat = "dd-MMMM-yyyy";
                BankOrCashComboFill();
                txtDate.Text = PublicVariables._dtCurrentDate.ToString("dd-MMM-yyyy");
                GridLedgerComboFill();
                DataTableForPartyBalance();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV37:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// textbox date leave 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDate_Leave(object sender, EventArgs e)
        {
            try
            {
                DateValidation DateValidationObj = new DateValidation();
                DateValidationObj.DateValidationFunction(txtDate);
                if (txtDate.Text == string.Empty)
                {
                    txtDate.Text = PublicVariables._dtCurrentDate.ToString("dd-MMM-yyyy");
                }
                string strDate = txtDate.Text;
                dtpDate.Value = Convert.ToDateTime(strDate.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV38:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// To add new ledger using these button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLedgerAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCashOrBank.SelectedValue != null)
                {
                    strLedgerId = cmbCashOrBank.SelectedValue.ToString();
                }
                else
                {
                    strLedgerId = string.Empty;
                }
                frmAccountLedger frmAccountLedgerObj = new frmAccountLedger();
                frmAccountLedgerObj.MdiParent = formMDI.MDIObj;
                frmAccountLedger open = Application.OpenForms["frmAccountLedger"] as frmAccountLedger;
                if (open == null)
                {
                    frmAccountLedgerObj.WindowState = FormWindowState.Normal;
                    frmAccountLedgerObj.MdiParent = formMDI.MDIObj;
                    frmAccountLedgerObj.CallFromReceiptVoucher(this, "CashOrBank");
                }
                else
                {
                    open.MdiParent = formMDI.MDIObj;
                    open.BringToFront();
                    open.CallFromReceiptVoucher(this, "CashOrBank");
                    if (open.WindowState == FormWindowState.Minimized)
                    {
                        open.WindowState = FormWindowState.Normal;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV39:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Basic calculations in grid cell value change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceiptVoucher_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (isValueChange)
                {
                    if (e.RowIndex != -1 && e.ColumnIndex != -1)
                    {
                        TotalAmount();
                        if (dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvcmbAccountLedger"].Value != null && dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvcmbAccountLedger"].Value.ToString() != string.Empty)
                        {
                            if (dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvcmbCurrency"].Value == null || dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvcmbCurrency"].Value.ToString() == string.Empty)
                            {
                                dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvcmbCurrency"].Value = Convert.ToDecimal(1); //decExchangeRateId;
                            }
                        }
                        AccountGroupSP spAccountGroup = new AccountGroupSP();
                        AccountLedgerSP spAccountLedger = new AccountLedgerSP();
                        if (dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvcmbAccountLedger"].Value != null && dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvcmbAccountLedger"].Value.ToString().Trim() != string.Empty)
                        {
                            if (dgvReceiptVoucher.CurrentCell.ColumnIndex == dgvReceiptVoucher.CurrentRow.Cells["dgvcmbAccountLedger"].ColumnIndex)
                            {
                                if (inUpdatingRowIndexForPartyRemove != -1)
                                {
                                    int inTableRowCount = dtblPartyBalance.Rows.Count;
                                    for (int inJ = 0; inJ < inTableRowCount; inJ++)
                                    {
                                        if (dtblPartyBalance.Rows.Count == inJ)
                                        {
                                            break;
                                        }
                                        if (Convert.ToDecimal(dtblPartyBalance.Rows[inJ]["ledgerId"].ToString()) == decUpdatingLedgerForPartyremove)
                                        {
                                            if (dtblPartyBalance.Rows[inJ]["PartyBalanceId"].ToString() != "0")
                                            {
                                                arrlstOfDeletedPartyBalanceRow.Add(dtblPartyBalance.Rows[inJ]["PartyBalanceId"]);
                                            }
                                            dtblPartyBalance.Rows.RemoveAt(inJ);
                                            inJ--;
                                        }
                                    }
                                    dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvtxtAmount"].Value = string.Empty;
                                    decUpdatingLedgerForPartyremove = 0;
                                    inUpdatingRowIndexForPartyRemove = -1;
                                }
                                decimal decLedgerId = Convert.ToDecimal(dgvReceiptVoucher.CurrentRow.Cells["dgvcmbAccountLedger"].Value.ToString());
                                bool isBanAccount = spAccountGroup.AccountGroupwithLedgerId(decLedgerId);
                                decimal decCount = Convert.ToDecimal(spAccountLedger.AccountGroupIdCheck(dgvReceiptVoucher.CurrentRow.Cells["dgvcmbAccountLedger"].FormattedValue.ToString()));
                                if (decCount > 0)
                                {
                                    dgvReceiptVoucher.CurrentRow.Cells["dgvtxtAmount"].ReadOnly = true;
                                    dgvReceiptVoucher.CurrentRow.Cells["dgvtxtAmount"].Value = string.Empty;
                                    dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvcmbCurrency"].Value = Convert.ToDecimal(1);
                                    dgvReceiptVoucher.CurrentRow.Cells["dgvcmbCurrency"].ReadOnly = true;
                                }
                                else
                                {
                                    dgvReceiptVoucher.CurrentRow.Cells["dgvtxtAmount"].ReadOnly = false;
                                    SettingsSP spSettings = new SettingsSP();
                                    if (spSettings.SettingsStatusCheck("MultiCurrency") == "Yes")
                                    {
                                        dgvcmbCurrency.ReadOnly = false;
                                    }
                                    else
                                    {
                                        dgvcmbCurrency.ReadOnly = true;

                                    }
                                }
                            }
                        }
                        CheckColumnMissing();
                        DateValidation objDateValidation = new DateValidation();
                        TextBox txtChequeText = new TextBox();
                        if (dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvtxtChequeDate"].Value != null)
                        {
                            txtChequeText.Text = dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvtxtChequeDate"].Value.ToString();
                            bool isDate = objDateValidation.DateValidationFunction(txtChequeText);
                            if (isDate)
                            {
                                dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvtxtChequeDate"].Value = txtChequeText.Text;
                            }
                            else
                            {
                                dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvtxtChequeDate"].Value = PublicVariables._dtCurrentDate.ToString("dd-MMM-yyyy");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV40:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Save button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckUserPrivilege.PrivilegeCheck(PublicVariables._decCurrentUserId, this.Name, btnSave.Text))
                {
                    SaveOrEdit();
                }
                else
                {
                    Messages.NoPrivillageMessage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV41:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Grid cell click event for open party balance porm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceiptVoucher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    AccountLedgerSP SpAccountLedger = new AccountLedgerSP();
                    if (dgvReceiptVoucher.CurrentCell.ColumnIndex == dgvReceiptVoucher.Columns["dgvbtnAgainst"].Index)
                    {
                        decimal decI = Convert.ToDecimal(SpAccountLedger.AccountGroupIdCheck(dgvReceiptVoucher.CurrentRow.Cells["dgvcmbAccountLedger"].FormattedValue.ToString()));
                        if (decI > 0)
                        {
                            frmPartyBalanceObj = new frmPartyBalance();
                            frmPartyBalanceObj.MdiParent = formMDI.MDIObj;
                            decimal decLedgerId = Convert.ToDecimal(dgvReceiptVoucher.CurrentRow.Cells["dgvcmbAccountLedger"].Value.ToString());
                            if (!isAutomatic)
                            {
                                frmPartyBalanceObj.CallFromReceiptVoucher(this, decLedgerId, dtblPartyBalance, decReceiptVoucherTypeId, txtVoucherNo.Text, Convert.ToDateTime(txtDate.Text), arrlstOfDeletedPartyBalanceRow);
                            }
                            else
                            {
                                frmPartyBalanceObj.CallFromReceiptVoucher(this, decLedgerId, dtblPartyBalance, decReceiptVoucherTypeId, strVoucherNo, Convert.ToDateTime(txtDate.Text), arrlstOfDeletedPartyBalanceRow);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV42:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// call the function to generate serial no in data grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceiptVoucher_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if (dgvReceiptVoucher.Rows.Count != 1)
                    SerialNumberGeneration();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV43:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Link button click to remove a row from grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnklblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (dgvReceiptVoucher.SelectedCells.Count > 0 && dgvReceiptVoucher.CurrentRow != null)
                {
                    if (!dgvReceiptVoucher.Rows[dgvReceiptVoucher.CurrentRow.Index].IsNewRow)
                    {
                        if (MessageBox.Show("Do you want to remove current row ?", "OpenMiracle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Remove();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV44:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Calling the key press event for Validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceiptVoucher_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                DataGridViewTextBoxEditingControl txt = e.Control as DataGridViewTextBoxEditingControl;
                if (dgvReceiptVoucher.CurrentCell.ColumnIndex == dgvReceiptVoucher.Columns["dgvtxtAmount"].Index)
                {
                    txt.KeyPress += dgvtxtAmount_KeyPress;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV45:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Data error event for unhandld exceptions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceiptVoucher_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                if (e.Exception.Message == "DataGridViewComboBoxCell value is not valid.")
                {
                    object value = dgvReceiptVoucher.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (!((DataGridViewComboBoxColumn)dgvReceiptVoucher.Columns[e.ColumnIndex]).Items.Contains(value))
                    {
                        e.ThrowException = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV46:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Delete button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckUserPrivilege.PrivilegeCheck(PublicVariables._decCurrentUserId, this.Name, btnDelete.Text))
                {
                    if (btnSave.Text == "Update")
                    {
                        if (PublicVariables.isMessageDelete)
                        {
                            if (Messages.DeleteMessage())
                            {
                                Delete(decRecieptmasterId);
                            }
                        }
                        else
                        {
                            Delete(decRecieptmasterId);
                        }
                    }
                }
                else
                {
                    Messages.NoPrivillageMessage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV47:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// change the date textbox based on dtp value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime date = this.dtpDate.Value;
                this.txtDate.Text = date.ToString("dd-MMM-yyyy");
                GridCurrencyComboFill();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV48:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// To remove a item from combobox ledger that are already selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceiptVoucher_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                inUpdatingRowIndexForPartyRemove = -1;
                decUpdatingLedgerForPartyremove = 0;
                DataTable dtbl = new DataTable();
                AccountLedgerSP spAccountLedger = new AccountLedgerSP();
                if (dgvReceiptVoucher.CurrentCell.ColumnIndex == dgvReceiptVoucher.Columns["dgvcmbAccountLedger"].Index)
                {
                    dtbl = spAccountLedger.AccountLedgerViewAll();
                    if (dtbl.Rows.Count < 2)
                    {
                        DataRow dr = dtbl.NewRow();
                        dr[0] = string.Empty;
                        dr[1] = string.Empty;
                        dtbl.Rows.InsertAt(dr, 0);
                    }
                    if (dgvReceiptVoucher.RowCount > 1)
                    {
                        int inGridRowCount = dgvReceiptVoucher.RowCount;
                        for (int inI = 0; inI < inGridRowCount - 1; inI++)
                        {
                            if (inI != e.RowIndex)
                            {
                                int inTableRowcount = dtbl.Rows.Count;
                                for (int inJ = 0; inJ < inTableRowcount; inJ++)
                                {
                                    if (dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value != null && dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value.ToString() != string.Empty)
                                    {
                                        if (dtbl.Rows[inJ]["ledgerId"].ToString() == dgvReceiptVoucher.Rows[inI].Cells["dgvcmbAccountLedger"].Value.ToString())
                                        {
                                            dtbl.Rows.RemoveAt(inJ);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    DataGridViewComboBoxCell dgvccCashOrBank = (DataGridViewComboBoxCell)dgvReceiptVoucher[dgvReceiptVoucher.Columns["dgvcmbAccountLedger"].Index, e.RowIndex];
                    dgvccCashOrBank.DataSource = dtbl;
                    dgvccCashOrBank.ValueMember = "ledgerId";
                    dgvccCashOrBank.DisplayMember = "ledgerName";
                }
                if (dgvReceiptVoucher.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name == "dgvcmbAccountLedger")
                {
                    if (dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvcmbAccountLedger"].Value != null && dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvcmbAccountLedger"].Value.ToString() != string.Empty)
                    {
                        if (spAccountLedger.AccountGroupIdCheck(dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvcmbAccountLedger"].FormattedValue.ToString()))
                        {
                            inUpdatingRowIndexForPartyRemove = e.RowIndex;
                            decUpdatingLedgerForPartyremove = Convert.ToDecimal(dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvcmbAccountLedger"].Value.ToString());
                        }
                    }
                }
                if (dgvReceiptVoucher.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name == "dgvcmbDrOrCr")
                {
                    if (dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvcmbAccountLedger"].Value != null && dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvcmbAccountLedger"].Value.ToString() != string.Empty)
                    {
                        if (spAccountLedger.AccountGroupIdCheck(dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvcmbAccountLedger"].FormattedValue.ToString()))
                        {
                            inUpdatingRowIndexForPartyRemove = e.RowIndex;
                            decUpdatingLedgerForPartyremove = Convert.ToDecimal(dgvReceiptVoucher.Rows[e.RowIndex].Cells["dgvcmbAccountLedger"].Value.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV49:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Cleare button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                if (frmAgeingObj != null)
                {
                    frmAgeingObj.Close();
                    frmAgeingObj = null;
                }
                if (frmBillallocationObj != null)
                {
                    frmBillallocationObj.Close();
                    frmBillallocationObj = null;
                }
                if (frmChequeReportObj != null)
                {
                    frmChequeReportObj.Close();
                    frmChequeReportObj = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV50:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Close button click
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
                MessageBox.Show("RV51:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// frmReceiptVoucher closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmReceiptVoucher_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (frmReceiptReportObj != null)
                {
                    frmReceiptReportObj.Enabled = true;
                    frmReceiptReportObj.CallFromReceiptVoucher(this);
                }
                if (frmReceiptRegisterObj != null)
                {
                    frmReceiptRegisterObj.Enabled = true;
                    frmReceiptRegisterObj.CallFromReceiptVoucher(this);
                }
                if (frmBillallocationObj != null)
                {
                    frmBillallocationObj.Enabled = true;
                    frmBillallocationObj.BillAllocationGridFill();
                }
                if (frmDayBookObj != null)
                {
                    frmDayBookObj.Enabled = true;
                    frmDayBookObj.dayBookGridFill();
                    frmDayBookObj = null;
                }
                if (frmChequeReportObj != null)
                {
                    frmChequeReportObj.Enabled = true;
                    frmChequeReportObj.ChequeReportFillGrid();
                }
                if (frmAgeingObj != null)
                {
                    frmAgeingObj.Enabled = true;
                    frmAgeingObj.FillGrid();
                }
                if (frmLedgerDetailsObj != null)
                {
                    frmLedgerDetailsObj.Enabled = true;
                    frmLedgerDetailsObj.LedgerDetailsView();
                }
                if (frmVoucherSearch != null)
                {
                    frmVoucherSearch.Enabled = true;
                    frmVoucherSearch.GridFill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV52:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// to commit the each and every changes in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceiptVoucher_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvReceiptVoucher.IsCurrentCellDirty)
                {
                    dgvReceiptVoucher.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV53:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// To set the gridview cells are edit mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceiptVoucher_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvReceiptVoucher.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewComboBoxCell)
                {
                    dgvReceiptVoucher.EditMode = DataGridViewEditMode.EditOnEnter;
                }
                else
                {
                    dgvReceiptVoucher.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV54:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// For Decimal validation in amount field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvtxtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (dgvReceiptVoucher.CurrentCell != null)
                {
                    if (dgvReceiptVoucher.Columns[dgvReceiptVoucher.CurrentCell.ColumnIndex].Name == "dgvtxtAmount")
                    {
                        Common.DecimalValidation(sender, e, false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV55:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Navigation

        /// <summary>
        /// For Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbCashOrBank.Focus();
                }
                if (e.KeyCode == Keys.Back)
                {
                    if (txtDate.Text == string.Empty || txtDate.SelectionStart == 0)
                    {
                        if (txtVoucherNo.ReadOnly == false)
                        {
                            txtVoucherNo.Focus();
                            txtVoucherNo.SelectionStart = 0;
                            txtVoucherNo.SelectionLength = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV56:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// For Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCashOrBank_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dgvReceiptVoucher.Focus();
                    dgvReceiptVoucher.ClearSelection();
                    if (dgvReceiptVoucher.Rows.Count > 0)
                    {
                        dgvReceiptVoucher.CurrentCell = dgvReceiptVoucher.Rows[0].Cells[0];
                        dgvReceiptVoucher.Rows[0].Cells[0].Selected = true;
                    }
                }
                if (e.KeyCode == Keys.Back)
                {
                    if (cmbCashOrBank.SelectionStart == 0 || cmbCashOrBank.Text == string.Empty)
                    {
                        txtDate.Focus();
                        txtDate.SelectionStart = 0;
                        txtDate.SelectionLength = 0;
                    }
                }
                if (e.KeyCode == Keys.C && Control.ModifierKeys == Keys.Alt)
                {
                    SendKeys.Send("{F10}");
                    btnLedgerAdd_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV57:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Form key down for Quick access
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmReceiptVoucher_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (cmbCashOrBank.Focused == true)
                {
                    if (e.KeyCode == Keys.F && Control.ModifierKeys == Keys.Control)
                    {
                        if (cmbCashOrBank.SelectedIndex != -1)
                        {
                            frmLedgerPopupObj = new frmLedgerPopup();
                            frmLedgerPopupObj.MdiParent = formMDI.MDIObj;
                            frmLedgerPopupObj.CallFromReceiptVoucher(this, Convert.ToDecimal(cmbCashOrBank.SelectedValue.ToString()), "CashOrBank");
                        }
                        else
                        {
                            Messages.InformationMessage("Select any cash or bank account");
                            cmbCashOrBank.Text = string.Empty;
                        }
                    }
                }
                else
                {
                    if (dgvReceiptVoucher.CurrentRow != null)
                    {
                        if (dgvReceiptVoucher.CurrentCell.ColumnIndex == dgvReceiptVoucher.Columns["dgvcmbAccountLedger"].Index)
                        {
                            if (e.KeyCode == Keys.F && Control.ModifierKeys == Keys.Control)
                            {
                                btnSave.Focus();
                                dgvReceiptVoucher.Focus();
                                if (dgvReceiptVoucher.CurrentRow.Cells["dgvcmbAccountLedger"].Value != null && dgvReceiptVoucher.CurrentRow.Cells["dgvcmbAccountLedger"].Value.ToString() != string.Empty)
                                {
                                    frmLedgerPopupObj = new frmLedgerPopup();
                                    frmLedgerPopupObj.MdiParent = formMDI.MDIObj;
                                    frmLedgerPopupObj.CallFromReceiptVoucher(this, Convert.ToDecimal(dgvReceiptVoucher.CurrentRow.Cells["dgvcmbAccountLedger"].Value.ToString()), string.Empty);
                                }
                                else
                                {
                                    Messages.InformationMessage("Select any ledger");
                                }
                            }
                            if (e.KeyCode == Keys.C && Control.ModifierKeys == Keys.Alt)
                            {
                                frmAccountLedgerObj = new frmAccountLedger();
                                frmAccountLedgerObj.MdiParent = formMDI.MDIObj;
                                frmAccountLedgerObj.CallFromReceiptVoucher(this, string.Empty);
                            }
                        }
                        else if (dgvReceiptVoucher.CurrentCell.ColumnIndex == dgvReceiptVoucher.Columns["dgvcmbCurrency"].Index)
                        {
                            if (e.KeyCode == Keys.F && Control.ModifierKeys == Keys.Control)
                            {
                                if (dgvReceiptVoucher.CurrentRow.Cells["dgvcmbCurrency"].Value != null && dgvReceiptVoucher.CurrentRow.Cells["dgvcmbCurrency"].Value.ToString() != string.Empty)
                                {
                                    frmCurrencyObj = new frmCurrencyDetails();
                                    frmCurrencyObj.MdiParent = formMDI.MDIObj;
                                    frmCurrencyObj.CallFromReceiptVoucher(this, Convert.ToDecimal(dgvReceiptVoucher.CurrentRow.Cells["dgvcmbCurrency"].Value.ToString()));
                                }
                                else
                                {
                                    Messages.InformationMessage("Select any currency ");
                                }
                            }
                        }
                    }
                }
                if (e.KeyCode == Keys.S && Control.ModifierKeys == Keys.Control)
                {
                    btnSave_Click(sender, e);
                }
                if (e.KeyCode == Keys.D && Control.ModifierKeys == Keys.Control)
                {
                    btnDelete_Click(sender, e);
                }
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
                MessageBox.Show("RV58:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// For enter key navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceiptVoucher_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgvReceiptVoucher.CurrentCell == dgvReceiptVoucher.Rows[dgvReceiptVoucher.Rows.Count - 1].Cells["dgvtxtChequeDate"])
                    {
                        txtNarration.Focus();
                        txtNarration.SelectionStart = 0;
                        txtNarration.SelectionLength = 0;
                    }
                    //else
                    //{
                    //    DataGridViewComboBoxColumn comboBox = new DataGridViewComboBoxColumn();
                        //dgvcmbAccountLedger.AutoComplete = true;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV59:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// For Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    if (txtNarration.Text == string.Empty || txtNarration.SelectionStart == 0)
                    {
                        dgvReceiptVoucher.Focus();
                        dgvReceiptVoucher.ClearSelection();
                        if (dgvReceiptVoucher.Rows.Count > 0)
                        {
                            dgvReceiptVoucher.CurrentCell = dgvReceiptVoucher.Rows[dgvReceiptVoucher.Rows.Count - 1].Cells["dgvtxtChequeDate"];
                            dgvReceiptVoucher.Rows[dgvReceiptVoucher.Rows.Count - 1].Cells[8].Selected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV60:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// For Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    txtNarration.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV61:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// For Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtVoucherNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtVoucherNo.ReadOnly == false)
                    {
                        txtDate.Focus();
                        txtDate.SelectionLength = 0;
                        txtDate.SelectionStart = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV62:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// For Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNarration_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    inNarrationCount++;
                    if (inNarrationCount == 2)
                    {
                        inNarrationCount = 0;
                        btnSave.Focus();
                    }
                }
                else
                {
                    inNarrationCount = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV63:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// For Enter key and backspace navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNarration_Enter(object sender, EventArgs e)
        {
            try
            {
                inNarrationCount = 0;
                txtNarration.Text = txtNarration.Text.Trim();
                if (txtNarration.Text == string.Empty)
                {
                    txtNarration.SelectionStart = 0;
                    txtNarration.SelectionLength = 0;
                    txtNarration.Focus();
                }
                else
                {
                    txtNarration.SelectionStart = txtNarration.Text.Length;
                    txtNarration.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV64:" + ex.Message, "OpenMiracle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

    }
}
