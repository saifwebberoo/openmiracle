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
//Summary description for ProductGroupInfo    
//</summary>    
namespace Open_Miracle    
{    
class ProductGroupInfo    
{    
    private decimal _groupId;    
    private string _groupName;    
    private decimal _groupUnder;    
    private string _narration;    
    private string _extra1;    
    private string _extra2;    
    private DateTime _extraDate;    
    
    public decimal GroupId    
    {    
        get { return _groupId; }    
        set { _groupId = value; }    
    }    
    public string GroupName    
    {    
        get { return _groupName; }    
        set { _groupName = value; }    
    }    
    public decimal GroupUnder    
    {    
        get { return _groupUnder; }    
        set { _groupUnder = value; }    
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
