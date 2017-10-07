using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SDEtools.Action
{
    
    public class AttTools
    {
        SQLServerDB db = new SQLServerDB();

        /// <summary>  
        /// 查询附件信息  
        /// </summary>  
        /// <param name="REQCODE"></param>  
        /// <param name="Attachment_Name"></param>  
        /// <returns></returns>  
        public string Select(string AttachmentID)
        {
            string strSql = string.Empty;

            //strSql = "SELECT   AttachmentID, REQCODE, Attachment_Name, Attachment_Type, Attachment_Desc, Path, Status "+
            //               "FROM      Attachment "+
            //                "WHERE   (REQCODE = '" + REQCODE + "') ";

            strSql = "SELECT     REQUESTS.CUSTCODE, HOSP_BASEINFO.ALIAS, REQUESTS.REQDESCRIPTION, CONVERT(varchar, REQUESTS.INCOMINGDATE, 120) AS Expr1,  " +
                     "REQUESTS.ACCEPTOR, Attachment.Attachment_Desc " +
                     "FROM         REQUESTS INNER JOIN " +
                     "HOSP_BASEINFO ON REQUESTS.CUSTCODE = HOSP_BASEINFO.HOSPCODE INNER JOIN " +
                     "Attachment ON REQUESTS.REQCODE = Attachment.REQCODE " +
                     "WHERE   (Attachment.AttachmentID = " + AttachmentID + ")  ";


            SqlCommand sqlCmd = new SqlCommand(strSql, db.sqlCon);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return sqlReader[0].ToString() + "|" + sqlReader[1].ToString() + "|" + sqlReader[2].ToString() + "|" + sqlReader[3].ToString() + "|" + sqlReader[4].ToString() + "|" + sqlReader[5].ToString();
            }
            return "";
        }

        public DataSet Index(string REQCODE, string DT_S, string DT_E)
        {
            //构建传出DT
            DataTable DT = new DataTable();
            DT.Columns.Add("ID");
            DT.Columns.Add("故障单号");
            DT.Columns.Add("附件名称");
            //DT.Columns.Add("附件类型");
            DT.Columns.Add("附件描述");
            DT.Columns.Add("上传时间");
            //DT.Columns.Add("附件路径");
            //DT.Columns.Add("附件状态");  

            //构建传出DS
            DataSet DS = new DataSet();
            string strSql = string.Empty;
            if (REQCODE != "")
            {
                strSql = "SELECT   AttachmentID , REQCODE , Attachment_Name, Attachment_Type, Attachment_Desc ,Upload_DateTime " +
                                    "FROM      Attachment " +
                                    "WHERE   (REQCODE = '" + REQCODE + "') and (Status = '0' )";
            }
            else if (DT_S != "" && DT_E != "")
            {
                strSql = "SELECT   AttachmentID , REQCODE , Attachment_Name, Attachment_Type, Attachment_Desc ,Upload_DateTime " +
                                   "FROM      Attachment " +
                                   "WHERE   (Upload_DateTime between '" + DT_S + "' and '" + DT_E + "') and (Status = '0' )";
            }

            SqlCommand sqlCmd = new SqlCommand(strSql, db.sqlCon);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                DataRow dr = DT.NewRow();
                dr["ID"] = sqlReader[0].ToString();
                dr["故障单号"] = sqlReader[1].ToString();
                dr["附件名称"] = sqlReader[2].ToString();
                //dr["附件类型"] = sqlReader[3].ToString();
                dr["附件描述"] = sqlReader[4].ToString();
                dr["上传时间"] = sqlReader[5].ToString();
                //dr["附件路径"] = sqlReader[5].ToString();
                //dr["附件状态"] = sqlReader[6].ToString();
                DT.Rows.Add(dr);
            }
            DS.Tables.Add(DT);
            return DS;
        }

        public bool Save(string REQCODE, string Attachment_Name, string Attachment_Name_new, string Attachment_Type, string Attachment_Desc)
        {
            string strSql = string.Empty;
            strSql = "insert into Attachment " +
                     "( REQCODE, Attachment_Name,Attachment_Name_new, Attachment_Type, Attachment_Desc, Upload_DateTime,Status) " +
                     "values ('" + REQCODE + "', '" + Attachment_Name + "', '" + Attachment_Name_new + "', '" + Attachment_Type + "', '" + Attachment_Desc + "', CONVERT (varchar, GETDATE(), 120) , '0')";

            SqlCommand sqlCmd = new SqlCommand(strSql, db.sqlCon);
            sqlCmd.ExecuteNonQuery();
            sqlCmd.Dispose();
            return true;
        }

        public string GetDateTime()
        {
            string strSql = "select CONVERT(VARCHAR(10),GETDATE(),120)  as today";
            SqlCommand sqlCmd = new SqlCommand(strSql, db.sqlCon);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return sqlReader[0].ToString();
            }
            return "";
        }

        public string GetAttachment(string Attachment_ID)
        {
            string strSql = string.Empty;
            strSql = "SELECT   AttachmentID, REQCODE, Attachment_Name, Attachment_Name_new, Attachment_Type, Attachment_Desc, Path, " +
                "Upload_DateTime, Status " +
                "FROM      Attachment " +
                "WHERE   (AttachmentID = '" + Attachment_ID + "')";
            SqlCommand sqlCmd = new SqlCommand(strSql, db.sqlCon);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return sqlReader[1].ToString() + "|" + sqlReader[2].ToString() + "|" + sqlReader[3].ToString();
            }
            return "";
        }

        public bool Delete(string Attachment_ID)
        {
            string strSql = string.Empty;
            strSql = "UPDATE  Attachment SET   Status = N'1' WHERE (AttachmentID = " + Attachment_ID + ")";

            SqlCommand sqlCmd = new SqlCommand(strSql, db.sqlCon);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            return true;
        }

        public string GetREQUESTS(string REQUESTS)
        {
            string strSql = string.Empty;
            strSql = "SELECT     REQUESTS.CUSTCODE, HOSP_BASEINFO.ALIAS, REQUESTS.REQDESCRIPTION, CONVERT(varchar, REQUESTS.INCOMINGDATE, 120) AS Expr1,  " +
                     "REQUESTS.ACCEPTOR, REQUESTS.REQCODE " +
                     "FROM         REQUESTS INNER JOIN " +
                     "HOSP_BASEINFO ON REQUESTS.CUSTCODE = HOSP_BASEINFO.HOSPCODE " +
                     "WHERE     (REQUESTS.REQCODE = '" + REQUESTS + "') ";

            SqlCommand sqlCmd = new SqlCommand(strSql, db.sqlCon);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return sqlReader[0].ToString() + "|" + sqlReader[1].ToString() + "|" + sqlReader[2].ToString() + "|" + sqlReader[3].ToString() + "|" + sqlReader[4].ToString() + "|" + sqlReader[5].ToString();
            }
            return "";
        }

        public string GetAttachmentNum()
        {
            string strSql = string.Empty;
            strSql = "declare @T nvarchar(10) " +
                    "declare @T1 nvarchar(10) " +
                    "set @T= (SELECT   CAST(COUNT(*) AS varchar(10)) AS Expr1 FROM      Attachment WHERE   (Status = N'0')) " +
                    "set @T1= (SELECT   CAST(COUNT(*) AS varchar(10)) AS Expr1 FROM      Attachment ) " +
                    "select @T+'/'+@T1";

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
