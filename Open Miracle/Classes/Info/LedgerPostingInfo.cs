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
using System.Text;    
//<summary>    
//Summary description for LedgerPostingInfo    
//</summary>    
namespace Open_Miracle    
{    
class LedgerPostingInfo    
{    
    private decimal _ledgerPostingId;    
    private DateTime _date;    
    private decimal _voucherTypeId;     
    private string _voucherNo;    
    private decimal _ledgerId;    
    private decimal _debit;    
    private decimal _credit;    
     
    private decimal _detailsId;    
    private decimal _yearId;    
    private string _invoiceNo;
    private string _chequeNo;
    private DateTime _chequeDate;
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;  
  
    
    public decimal LedgerPostingId    
    {    
        get { return _ledgerPostingId; }    
        set { _ledgerPostingId = value; }    
    }    
    public DateTime Date    
    {    
        get { return _date; }    
        set { _date = value; }    
    }    
    public decimal VoucherTypeId    
    {
        get { return _voucherTypeId; }
        set { _voucherTypeId = value; }    
    }    
    public string VoucherNo    
    {    
        get { return _voucherNo; }    
        set { _voucherNo = value; }    
    }    
    public decimal LedgerId    
    {    
        get { return _ledgerId; }    
        set { _ledgerId = value; }    
    }    
    public decimal Debit    
    {    
        get { return _debit; }    
        set { _debit = value; }    
    }    
    public decimal Credit    
    {    
        get { return _credit; }    
        set { _credit = value; }    
    }    
  
    public decimal DetailsId    
    {    
        get { return _detailsId; }    
        set { _detailsId = value; }    
    }    
    public decimal YearId    
    {    
        get { return _yearId; }    
        set { _yearId = value; }    
    }    
    public string InvoiceNo   
    {    
        get { return _invoiceNo; }    
        set { _invoiceNo = value; }    
    }
    public string ChequeNo
    {
        get { return _chequeNo; }
        set { _chequeNo = value; }
    }
    public DateTime ChequeDate
    {
        get { return _chequeDate; }
        set { _chequeDate = value; }
    }
    public DateTime ExtraDate    
    {    
        get { return _extraDate; }    
        set { _extraDate = value; }    
    }    
    public string Extra1    
    {    
        get { return _extra1; }    
        set { _extra1 = value; }    
    }    
    public string Extra2    
    {    
        get { return _extra2; }    
        set { _extra2 = value; }    
    }    
    
}    
}
