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
//Summary description for AccountLedgerInfo    
//</summary>    
namespace Open_Miracle    
{    
class AccountLedgerInfo    
{    
    private decimal _ledgerId;    
    private decimal _accountGroupId;    
    private string _ledgerName;    
    private decimal _openingBalance;    
    private string _crOrDr;    
    private string _narration;    
    private string _mailingName;    
    private string _address;    
    private string _state;    
    private string _phone;
    private string _mobile;    
    private string _email;    
    private int _creditPeriod;    
    private decimal _creditLimit;
    private decimal _pricinglevelId;    
    private bool _billByBill;    
    private string _tin;    
    private string _cst;    
    private string _pan;    
    private decimal _routeId;    
    private string _bankAccountNumber;    
    private string _branchName;    
    private string _branchCode;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;    
    private decimal _areaId;
    private bool _isDefault;  
    
    public decimal LedgerId    
    {    
        get { return _ledgerId; }    
        set { _ledgerId = value; }    
    }    
    public decimal AccountGroupId    
    {    
        get { return _accountGroupId; }    
        set { _accountGroupId = value; }    
    }    
    public string LedgerName    
    {    
        get { return _ledgerName; }    
        set { _ledgerName = value; }    
    }    
    public decimal OpeningBalance    
    {    
        get { return _openingBalance; }    
        set { _openingBalance = value; }    
    }    
    public string CrOrDr    
    {    
        get { return _crOrDr; }    
        set { _crOrDr = value; }    
    }    
    public string Narration    
    {    
        get { return _narration; }    
        set { _narration = value; }    
    }    
    public string MailingName    
    {    
        get { return _mailingName; }    
        set { _mailingName = value; }    
    }    
    public string Address    
    {    
        get { return _address; }    
        set { _address = value; }    
    }    
    public string State    
    {    
        get { return _state; }    
        set { _state = value; }    
    }    
    public string Phone    
    {    
        get { return _phone; }    
        set { _phone = value; }    
    }    
    public string Mobile 
    {
        get { return _mobile; }
        set { _mobile = value; }    
    }    
    public string Email    
    {    
        get { return _email; }    
        set { _email = value; }    
    }    
    public int CreditPeriod    
    {    
        get { return _creditPeriod; }    
        set { _creditPeriod = value; }    
    }    
    public decimal CreditLimit    
    {    
        get { return _creditLimit; }    
        set { _creditLimit = value; }    
    }
    public decimal PricinglevelId    
    {
        get { return _pricinglevelId; }
        set { _pricinglevelId = value; }    
    }    
    public bool BillByBill    
    {    
        get { return _billByBill; }    
        set { _billByBill = value; }    
    }    
    public string Tin    
    {    
        get { return _tin; }    
        set { _tin = value; }    
    }    
    public string Cst    
    {    
        get { return _cst; }    
        set { _cst = value; }    
    }    
    public string Pan    
    {    
        get { return _pan; }    
        set { _pan = value; }    
    }    
    public decimal RouteId    
    {    
        get { return _routeId; }    
        set { _routeId = value; }    
    }    
    public string BankAccountNumber    
    {    
        get { return _bankAccountNumber; }    
        set { _bankAccountNumber = value; }    
    }    
    public string BranchName    
    {    
        get { return _branchName; }    
        set { _branchName = value; }    
    }    
    public string BranchCode    
    {    
        get { return _branchCode; }    
        set { _branchCode = value; }    
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
    public decimal AreaId    
    {    
        get { return _areaId; }    
        set { _areaId = value; }    
    }
    public bool IsDefault
    {
        get { return _isDefault; }
        set { _isDefault = value; }
    } 
}    
}
