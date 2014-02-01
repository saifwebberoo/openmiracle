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
//Summary description for StockPostingInfo    
//</summary>    
namespace Open_Miracle    
{    
class StockPostingInfo    
{    
    private decimal _stockPostingId;    
    private DateTime _date;    
    private decimal _voucherTypeId;    
    private string _voucherNo;    
    private string _invoiceNo;    
    private decimal _productId;    
    private decimal _batchId;    
    private decimal _unitId;    
    private decimal _godownId;    
    private decimal _rackId;    
    private decimal _againstVoucherTypeId;    
    private string _againstInvoiceNo;    
    private string _againstVoucherNo;    
    private decimal _inwardQty;    
    private decimal _outwardQty;    
    private decimal _rate;    
    private decimal _financialYearId;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;    
    
    public decimal StockPostingId    
    {    
        get { return _stockPostingId; }    
        set { _stockPostingId = value; }    
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
    public string InvoiceNo    
    {    
        get { return _invoiceNo; }    
        set { _invoiceNo = value; }    
    }    
    public decimal ProductId    
    {    
        get { return _productId; }    
        set { _productId = value; }    
    }    
    public decimal BatchId    
    {    
        get { return _batchId; }    
        set { _batchId = value; }    
    }    
    public decimal UnitId    
    {    
        get { return _unitId; }    
        set { _unitId = value; }    
    }    
    public decimal GodownId    
    {    
        get { return _godownId; }    
        set { _godownId = value; }    
    }    
    public decimal RackId    
    {    
        get { return _rackId; }    
        set { _rackId = value; }    
    }    
    public decimal AgainstVoucherTypeId    
    {    
        get { return _againstVoucherTypeId; }    
        set { _againstVoucherTypeId = value; }    
    }    
    public string AgainstInvoiceNo    
    {    
        get { return _againstInvoiceNo; }    
        set { _againstInvoiceNo = value; }    
    }    
    public string AgainstVoucherNo    
    {    
        get { return _againstVoucherNo; }    
        set { _againstVoucherNo = value; }    
    }    
    public decimal InwardQty    
    {    
        get { return _inwardQty; }    
        set { _inwardQty = value; }    
    }    
    public decimal OutwardQty    
    {    
        get { return _outwardQty; }    
        set { _outwardQty = value; }    
    }    
    public decimal Rate    
    {    
        get { return _rate; }    
        set { _rate = value; }    
    }    
    public decimal FinancialYearId    
    {    
        get { return _financialYearId; }    
        set { _financialYearId = value; }    
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
