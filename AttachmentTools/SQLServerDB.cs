using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace AttTools
{
    public class SQLServerDB
    {
        //连接数据库  
        public SqlConnection sqlCon;
        //连接数据库字符串  
        public String strSql = @"Data Source=1.0.0.84;Initial Catalog=servicedesk;Persist Security Info=True;User ID=sa;Password=bjYB20090116@capInfo";

        //默认构造函数  
        public SQLServerDB()
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection();
                sqlCon.ConnectionString = strSql;
                sqlCon.Open();
            }
        }

        public void Close()
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                //sqlCon.Dispose();  
            }
        }
    }
}
