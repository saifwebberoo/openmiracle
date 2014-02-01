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
//Summary description for VoucherTypeInfo    
//</summary>    
namespace Open_Miracle    
{    
class VoucherTypeInfo    
{    
    private decimal _voucherTypeId;    
    private string _voucherTypeName;    
    private string _typeOfVoucher;    
    private string _methodOfVoucherNumbering;    
    private bool _isTaxApplicable;    
    private string _narration;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;    
    private bool _isActive;
    private bool _isDefault;
    private int _masterId;
    private string _declarartion;
    private string _heading1;
    private string _heading2;
    private string _heading3;
    private string _heading4;


    public decimal VoucherTypeId    
    {    
        get { return _voucherTypeId; }    
        set { _voucherTypeId = value; }    
    }    
    public string VoucherTypeName    
    {    
        get { return _voucherTypeName; }    
        set { _voucherTypeName = value; }    
    }    
    public string TypeOfVoucher    
    {    
        get { return _typeOfVoucher; }    
        set { _typeOfVoucher = value; }    
    }    
    public string MethodOfVoucherNumbering    
    {    
        get { return _methodOfVoucherNumbering; }    
        set { _methodOfVoucherNumbering = value; }    
    }    
    public bool IsTaxApplicable    
    {    
        get { return _isTaxApplicable; }    
        set { _isTaxApplicable = value; }    
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
    public bool IsActive    
    {    
        get { return _isActive; }    
        set { _isActive = value; }    
    }
    public bool IsDefault
    {
        get { return _isDefault; }
        set { _isDefault = value; }
    }
    public int MasterId
    {
        get { return _masterId; }
        set { _masterId = value; }
    }
    public string Declarartion
    {
        get { return _declarartion; }
        set { _declarartion = value; }
    }
    public string Heading1
    {
        get { return _heading1; }
        set { _heading1 = value; }
    }
    public string Heading2
    {
        get { return _heading2; }
        set { _heading2 = value; }
    }
    public string Heading3
    {
        get { return _heading3; }
        set { _heading3 = value; }
    }
    public string Heading4
    {
        get { return _heading4; }
        set { _heading4 = value; }
    }
}    
}
