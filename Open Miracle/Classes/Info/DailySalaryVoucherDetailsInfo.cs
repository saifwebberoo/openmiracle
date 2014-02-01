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
//Summary description for DailySalaryVoucherDetailsInfo    
//</summary>    
namespace Open_Miracle    
{    
class DailySalaryVoucherDetailsInfo    
{    
    private decimal _dailySalaryVoucherDetailsId;
    private decimal _dailySalaryVoucherMasterId;    
    private decimal _employeeId;    
    private decimal _wage;    
    private string _status;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;  
    
    public decimal DailySalaryVoucherDetailsId    
    {    
        get { return _dailySalaryVoucherDetailsId; }    
        set { _dailySalaryVoucherDetailsId = value; }    
    }    
    public decimal DailySalaryVocherMasterId    
    {
        get { return _dailySalaryVoucherMasterId; }
        set { _dailySalaryVoucherMasterId = value; }    
    }    
    public decimal EmployeeId    
    {    
        get { return _employeeId; }    
        set { _employeeId = value; }    
    }    
    public decimal Wage    
    {    
        get { return _wage; }    
        set { _wage = value; }    
    }    
    public string Status    
    {    
        get { return _status; }    
        set { _status = value; }    
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
