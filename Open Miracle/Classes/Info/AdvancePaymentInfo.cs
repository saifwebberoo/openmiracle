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
//Summary description for AdvancePaymentInfo    
//</summary>    
namespace Open_Miracle    
{    
class AdvancePaymentInfo    
{    
    private decimal _advancePaymentId;    
    private decimal _employeeId;    
    private decimal _ledgerId;
    private string _voucherNo;    
    private string _invoiceNo;    
    private DateTime _date;    
    private decimal _amount;    
    private DateTime _salaryMonth;    
    private string _chequenumber;    
    private DateTime _chequeDate;    
    private string _narration;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;    
    private decimal _suffixPrefixId;    
    private decimal _voucherTypeId;
    private decimal _financialYearId;  
    
    public decimal AdvancePaymentId    
    {    
        get { return _advancePaymentId; }    
        set { _advancePaymentId = value; }    
    }    
    public decimal EmployeeId    
    {    
        get { return _employeeId; }    
        set { _employeeId = value; }    
    } 
   
    public decimal LedgerId    
    {    
        get { return _ledgerId; }    
        set { _ledgerId = value; }    
    }
    public string VoucherNo    
    {
        get { return _voucherNo; }
        set { _voucherNo = value; }    
    }    
    public string InvoiceNo   
    {    
        get { return _invoiceNo; }    
        set { _invoiceNo = value; }    
    }    
    public DateTime Date    
    {    
        get { return _date; }    
        set { _date = value; }    
    }    
    public decimal Amount    
    {    
        get { return _amount; }    
        set { _amount = value; }    
    }    
    public DateTime SalaryMonth    
    {    
        get { return _salaryMonth; }    
        set { _salaryMonth = value; }    
    }    
    public string Chequenumber    
    {    
        get { return _chequenumber; }    
        set { _chequenumber = value; }    
    }    
    public DateTime ChequeDate    
    {    
        get { return _chequeDate; }    
        set { _chequeDate = value; }    
    }    
    public string Narration    
    {    
        get { return _narration; }    
        set { _narration = value; }    
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
    public decimal SuffixPrefixId    
    {    
        get { return _suffixPrefixId; }    
        set { _suffixPrefixId = value; }    
    }    
    public decimal VoucherTypeId    
    {    
        get { return _voucherTypeId; }    
        set { _voucherTypeId = value; }    
    }
    public decimal FinancialYearId 
    {
        get { return _financialYearId; }
        set { _financialYearId = value; }
    }
   
}    
}
