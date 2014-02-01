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
//Summary description for BrandInfo    
//</summary>    
namespace Open_Miracle    
{    
class BrandInfo    
{    
    private decimal _brandId;    
    private string _brandName;    
    private string _narration;
    private string _manufacturer;
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;
    
    
    public decimal BrandId    
    {    
        get { return _brandId; }    
        set { _brandId = value; }    
    }    
    public string BrandName    
    {    
        get { return _brandName; }    
        set { _brandName = value; }    
    }    
    public string Narration    
    {    
        get { return _narration; }    
        set { _narration = value; }    
    }
    public string Manufacturer
    {
        get { return _manufacturer; }
        set { _manufacturer = value; }
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
