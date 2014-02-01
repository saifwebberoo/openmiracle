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
//Summary description for BudgetMasterInfo    
//</summary>    
namespace Open_Miracle    
{    
class BudgetMasterInfo    
{    
    private decimal _budgetMasterId;    
    private string _budgetName;    
    private string _type;    
    private decimal _totalDr;    
    private decimal _totalCr;    
    private DateTime _fromDate;    
    private DateTime _toDate;    
    private string _narration;    
    private string _extra1;    
    private string _extra2;    
    private DateTime _extraDate;    
    
    public decimal BudgetMasterId    
    {    
        get { return _budgetMasterId; }    
        set { _budgetMasterId = value; }    
    }    
    public string BudgetName    
    {    
        get { return _budgetName; }    
        set { _budgetName = value; }    
    }    
    public string Type    
    {    
        get { return _type; }    
        set { _type = value; }    
    }    
    public decimal TotalDr    
    {    
        get { return _totalDr; }    
        set { _totalDr = value; }    
    }    
    public decimal TotalCr    
    {    
        get { return _totalCr; }    
        set { _totalCr = value; }    
    }    
    public DateTime FromDate    
    {    
        get { return _fromDate; }    
        set { _fromDate = value; }    
    }    
    public DateTime ToDate    
    {    
        get { return _toDate; }    
        set { _toDate = value; }    
    }    
    public string Narration    
    {    
        get { return _narration; }    
        set { _narration = value; }    
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
    public DateTime ExtraDate    
    {    
        get { return _extraDate; }    
        set { _extraDate = value; }    
    }    
    
}    
}
