using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SDEtools.Action
{
   public class Update
    {
       SQLServerDB db = new SQLServerDB();
       /// <summary>  
       /// 查询版本信息  
       /// </summary>   
       /// <returns>Version</returns>  
       public string Getversion()
       {
           string strSql = string.Empty;   
           strSql = "SELECT Version FROM  Version  ";

           SqlCommand sqlCmd = new SqlCommand(strSql, db.sqlCon);
           SqlDataReader sqlReader = sqlCmd.ExecuteReader();
           if (sqlReader.Read())
           {
               return sqlReader[0].ToString();
           }
           return "";
       }

    }
}
