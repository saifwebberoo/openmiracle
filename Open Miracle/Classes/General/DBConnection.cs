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
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.ServiceProcess;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Diagnostics;
using System.Threading;

//<summary>    
//Summary description for DBConnection    
//</summary>    
namespace Open_Miracle
{
    public class DBConnection
    {
        protected SqlConnection sqlcon;
        //protected SqlConnection sqlconTest;
        string strServer = ".\\sqlexpress";
        string path = string.Empty;
        public DBConnection()
        {
            /*  */
            //-------------Single User----------------------//

            strServer = ".\\sqlexpress";
            if (File.Exists(Application.StartupPath + "\\sys.txt"))
            {
                strServer = File.ReadAllText(Application.StartupPath + "\\sys.txt"); // getting ip of server
            }
            
            if (PublicVariables._decCurrentCompanyId > 0)
            {
                path = Application.StartupPath + "\\Data\\" + PublicVariables._decCurrentCompanyId + "\\DBOpenMiracle.mdf";
            }
            else if (PublicVariables._decCurrentCompanyId == 0)
            {
                path = Application.StartupPath + "\\Data\\DBOpenMiracle.mdf";
            }
            else
            {
                path = Application.StartupPath + "\\Data\\COMP\\DBOpenMiracle.mdf";
            }


            sqlcon = new SqlConnection(@"Data Source=" + strServer + ";AttachDbFilename=" + path + ";Integrated Security=True;Connect Timeout=120;User Instance=True");
            //;Max Pool Size=200
              
           /*
           
            sqlcon = new SqlConnection(@"Data Source=192.168.2.112\SQLS2008;database=DBOpenMiracle;user id='open';password='miracle';Connect Timeout=30;"); 
               //----------------Server----------------------//
              */

            /// <summary>
            /// SQL Express may take time to start up due to AutoClose Behaviour of SQLEXPRESS
            /// Here we checking the sql connection and catches the logon error.
            /// </summary>

            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
            }
            catch (SqlException sEx)
            {

                int i = sEx.Number;
                if (i == 233)
                {
                    Reconnect();
                }
                else if (i == -1)
                {
                    Messages.InformationMessage("SQL connection failure... Please check the instance name of your sqlexpress with " + Application.StartupPath + " \\sys.txt");
                }
                else if (i == 18493)
                {
                    ChangeConnectionForServer();
                }
                else
                {
                    Messages.InformationMessage("Could not connect to your database... Please check your SQL Configuration");
                }
            }
            catch (Exception)
            {
                //Catche any other exception
            }
            finally
            {

            }
            try
            {
                PrintWorks.frmDBConnection.connectionString = sqlcon.ConnectionString;
            }
            catch
            {
                //sql Error
            }
        }
        /// <summary>
        /// SQL Express may take time to start up due to AutoClose Behaviour of SQLEXPRESS
        /// 
        /// </summary>
        private void Reconnect()
        {

            try
            {
                Thread.Sleep(30000);
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();

                }
            }
            catch (Exception)
            {

                Messages.InformationMessage("Your SQL Server is starting up... Please close and re-open the application");

            }
        }

        /// <summary>
        /// Cheanges the connection string to support SQLServer version instead of SQLExpress
        /// </summary>
        private void ChangeConnectionForServer()
        {
            sqlcon = new SqlConnection(@"Data Source=" + strServer + ";AttachDbFilename=" + path + ";Integrated Security=True;Connect Timeout=120;");
        }
    }
}
