
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
//Summary description for PrivilegeInfo    
//</summary>    
namespace Open_Miracle    
{    
class PrivilegeInfo    
{    
    private decimal _privilegeId;    
    private decimal _userId;    
    private string _formName;    
    private string _action;
    private decimal _roleId;
    private DateTime _extraDate;    
    private string _exatra1;    
    private string _extra2;    
    
    public decimal PrivilegeId    
    {    
        get { return _privilegeId; }    
        set { _privilegeId = value; }    
    }    
    public decimal UserId    
    {    
        get { return _userId; }    
        set { _userId = value; }    
    }    
    public string FormName    
    {    
        get { return _formName; }    
        set { _formName = value; }    
    }    
    public string Action    
    {    
        get { return _action; }    
        set { _action = value; }    
    }
    public decimal RoleId
    {
        get { return _roleId; }
        set { _roleId = value; }
    }
    public DateTime ExtraDate    
    {    
        get { return _extraDate; }    
        set { _extraDate = value; }    
    }    
    public string Extra1    
    {    
        get { return _exatra1; }    
        set { _exatra1 = value; }    
    }    
    public string Extra2    
    {    
        get { return _extra2; }    
        set { _extra2 = value; }    
    }    
    
}    
}
