using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace SDEtools.Action
{
    public class RemTools
    {
        SQLServerDB db = new SQLServerDB();
        public DataSet GetEngineer()
        {
            //构建传出DT
            DataTable DT = new DataTable();
            DT.Columns.Add("EngineerID");
            DT.Columns.Add("EngineerName");
            
            //构建传出DS
            DataSet DS = new DataSet();            

            string strSql = string.Empty;
            strSql="SELECT   OPERID_SOFTPHONEID.OPER_ID, OPERID_SOFTPHONEID.SOFTPHONE_ID, OPERATOR.Oper_Name "+
                    "FROM      OPERID_SOFTPHONEID INNER JOIN "+
                    "OPERATOR ON OPERID_SOFTPHONEID.OPER_ID = OPERATOR.Oper_ID "+
                    "WHERE   (OPERID_SOFTPHONEID.SOFTPHONE_ID >= '1171') AND (OPERID_SOFTPHONEID.SOFTPHONE_ID <= '1190') "+
                    "ORDER BY OPERID_SOFTPHONEID.SOFTPHONE_ID ";
           

            SqlCommand sqlCmd = new SqlCommand(strSql, db.sqlCon);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                DataRow dr = DT.NewRow();
                dr["EngineerID"] = sqlReader[0].ToString();
                dr["EngineerName"] = sqlReader[2].ToString();               
                DT.Rows.Add(dr);
            }
            DS.Tables.Add(DT);
            sqlCmd.Dispose();
            return DS;
        }
        public DataSet GetRemindList(string EngineerName,string Date_S,string Date_E)
        {
            //构建传出DT
            DataTable DT = new DataTable();
            DT.Columns.Add("序号");
            DT.Columns.Add("故障单号");
            DT.Columns.Add("子单数量");
            DT.Columns.Add("问题简述");
            DT.Columns.Add("医院编码");
            DT.Columns.Add("医院名称");
            DT.Columns.Add("受理时间");
            DT.Columns.Add("受理人");            
            DT.Columns.Add("处理时间");
            DT.Columns.Add("责任工程师");
            DT.Columns.Add("L2状态");
            DT.Columns.Add("L2完成时间");
            DT.Columns.Add("现场状态");
            DT.Columns.Add("现场完成时间");
            DT.Columns.Add("L1状态");
            DT.Columns.Add("L1完成时间");
            DT.Columns.Add("子单未完成数量");
            DT.Columns[0].DataType = typeof(int);
            DT.Columns[8].DataType = typeof(Double);
            DT.Columns[11].DataType = typeof(Double);
            DT.Columns[13].DataType = typeof(Double);
            DT.Columns[15].DataType = typeof(Double);
            
            //构建传出DS
            DataSet DS = new DataSet();            

            string strSql = string.Empty;
            if (EngineerName=="")
            {
                //strSql = "SELECT   REQUESTS.REQCODE, REQUESTS.REQCOUNT, REQUESTS.REQDESCRIPTION, REQUESTS.CUSTCODE,  "+
                //         "CUSTOMERS.ALIAS, CONVERT(varchar(100), REQUESTS.INCOMINGDATE, 20) AS INCOMINGDATE,  "+
                //         "REQUESTS.ACCEPTOR, CAST(ROUND(DATEDIFF(s, REQUESTS.INCOMINGDATE, GETDATE()) * 1.0 / 3600, 2)  "+
                //         "AS NUMERIC(10, 2)) AS DisposeTime, REQUESTS.ACCEPTOR_RESPONSIBILITY_NAME,  "+
                //         "View_DISPOSALS_MAXCOMP.DISPOSESTATUS, (CASE WHEN View_DISPOSALS_MAXCOMP.sdcode IS NOT NULL  "+
                //         "THEN '1' ELSE ISNULL(CAST(ROUND(DATEDIFF(s, View_DISPOSALS_MAXCOMP.COMPLETEDATE, GETDATE())  "+
                //         "* 1.0 / 3600, 2) AS NUMERIC(10, 2)), 0) END) AS L2Compdate, View_FIELDWORKS_MAXCOMP.FIELDDISPOSESTATUS,  "+
                //         "isnull(View_FIELDWORKS_MAXCOMP.FieldCompdate,0) as FieldCompdate, View_DISPOSALSL1_MAXCOMP.DISPOSEL1STATUS,  " +
                //         "isnull(View_DISPOSALSL1_MAXCOMP.L1Compdate,0) as L1Compdate, CASE WHEN View_disposals_maxcomp.DISPOSESTATUS IS NULL AND  " + 
                //         "View_fieldworks_maxcomp.fielddisposestatus IS NULL AND View_disposalsl1_maxcomp.DISPOSEL1STATUS IS NULL  "+
                //         "THEN NULL ELSE (CASE WHEN View_DISPOSALS_MAXCOMP.DISPOSESTATUS <> '04' THEN COUNT(*) ELSE '0' END)  "+
                //         "+ (CASE WHEN View_fieldworks_maxcomp.fielddisposestatus <> '04' THEN COUNT(*) ELSE '0' END)  "+
                //         "+ (CASE WHEN View_disposalsl1_maxcomp.DISPOSEL1STATUS <> '04' THEN COUNT(*) ELSE '0' END)  "+
                //         "END AS Unfinished "+
                //         "FROM      REQUESTS INNER JOIN "+
                //         "CUSTOMERS ON REQUESTS.CUSTCODE = CUSTOMERS.CODE LEFT OUTER JOIN "+
                //         "View_DISPOSALSL1_MAXCOMP ON  "+
                //         "REQUESTS.REQCODE = View_DISPOSALSL1_MAXCOMP.REQCODE LEFT OUTER JOIN " +
                //         "View_DISPOSALS_MAXCOMP ON REQUESTS.REQCODE = View_DISPOSALS_MAXCOMP.REQCODE LEFT OUTER JOIN "+
                //         "View_FIELDWORKS_MAXCOMP ON REQUESTS.REQCODE = View_FIELDWORKS_MAXCOMP.REQCODE "+
                //         "GROUP BY REQUESTS.REQCODE, REQUESTS.REQCOUNT, View_DISPOSALS_MAXCOMP.DISPOSESTATUS,  "+
                //         "View_FIELDWORKS_MAXCOMP.FIELDDISPOSESTATUS, View_DISPOSALSL1_MAXCOMP.DISPOSEL1STATUS,  "+
                //         "REQUESTS.REQUESTSTATUS, CUSTOMERS.CUSTOMERCLASS, REQUESTS.REQDESCRIPTION,  "+
                //         "REQUESTS.CUSTCODE, CUSTOMERS.ALIAS, REQUESTS.INCOMINGDATE, REQUESTS.ACCEPTOR,  "+
                //         "REQUESTS.ACCEPTOR_RESPONSIBILITY_NAME, View_DISPOSALS_MAXCOMP.L2Compdate,  "+
                //         "View_FIELDWORKS_MAXCOMP.FieldCompdate, View_DISPOSALSL1_MAXCOMP.L1Compdate,  "+
                //         "View_DISPOSALS_MAXCOMP.SDCODE, View_DISPOSALS_MAXCOMP.COMPLETEDATE "+
                //         "HAVING   (REQUESTS.REQUESTSTATUS = '01') AND (CUSTOMERS.CUSTOMERCLASS = '01') "+
                //         "ORDER BY INCOMINGDATE";

                strSql = "SELECT   REQUESTS.REQCODE, REQUESTS.REQCOUNT, REQUESTS.REQDESCRIPTION, REQUESTS.CUSTCODE,  " +
                         "CUSTOMERS.ALIAS, CONVERT(varchar(100), REQUESTS.INCOMINGDATE, 20) AS INCOMINGDATE,  " +
                         "REQUESTS.ACCEPTOR, CAST(ROUND(DATEDIFF(s, REQUESTS.INCOMINGDATE, GETDATE()) * 1.0 / 3600, 2)  " +
                         "AS NUMERIC(10, 2)) AS DisposeTime, REQUESTS.ACCEPTOR_RESPONSIBILITY_NAME,  " +
                         "View_DISPOSALS_MAXCOMP.DISPOSESTATUS, (CASE WHEN View_DISPOSALS_MAXCOMP.sdcode IS NOT NULL  " +
                         "THEN '1' ELSE ISNULL(CAST(ROUND(DATEDIFF(s, View_DISPOSALS_MAXCOMP.COMPLETEDATE, GETDATE())  " +
                         "* 1.0 / 3600, 2) AS NUMERIC(10, 2)), 0) END) AS L2Compdate, View_FIELDWORKS_MAXCOMP.FIELDDISPOSESTATUS,  " +
                         "isnull(View_FIELDWORKS_MAXCOMP.FieldCompdate,0) as FieldCompdate, View_DISPOSALSL1_MAXCOMP.DISPOSEL1STATUS,  " +
                         "isnull(View_DISPOSALSL1_MAXCOMP.L1Compdate,0) as L1Compdate, view_DISP_FIELD_Unfinished.Unfinished AS NUM " +
                         "FROM         REQUESTS INNER JOIN " +
                         "CUSTOMERS ON REQUESTS.CUSTCODE = CUSTOMERS.CODE LEFT OUTER JOIN "+
                         "view_DISP_FIELD_Unfinished ON REQUESTS.REQCODE = view_DISP_FIELD_Unfinished.REQCODE LEFT OUTER JOIN "+
                         "View_DISPOSALSL1_MAXCOMP ON REQUESTS.REQCODE = View_DISPOSALSL1_MAXCOMP.REQCODE LEFT OUTER JOIN "+
                         "View_DISPOSALS_MAXCOMP ON REQUESTS.REQCODE = View_DISPOSALS_MAXCOMP.REQCODE LEFT OUTER JOIN "+
                         "View_FIELDWORKS_MAXCOMP ON REQUESTS.REQCODE = View_FIELDWORKS_MAXCOMP.REQCODE "+
                         "GROUP BY REQUESTS.REQCODE, REQUESTS.REQCOUNT, View_DISPOSALS_MAXCOMP.DISPOSESTATUS, "+
                         "View_FIELDWORKS_MAXCOMP.FIELDDISPOSESTATUS, View_DISPOSALSL1_MAXCOMP.DISPOSEL1STATUS, REQUESTS.REQUESTSTATUS, "+
                         "CUSTOMERS.CUSTOMERCLASS, REQUESTS.REQDESCRIPTION, REQUESTS.CUSTCODE, CUSTOMERS.ALIAS, REQUESTS.INCOMINGDATE, "+
                         "REQUESTS.ACCEPTOR, REQUESTS.ACCEPTOR_RESPONSIBILITY_NAME, View_DISPOSALS_MAXCOMP.L2Compdate, "+
                         "View_FIELDWORKS_MAXCOMP.FieldCompdate, View_DISPOSALSL1_MAXCOMP.L1Compdate, View_DISPOSALS_MAXCOMP.SDCODE, "+
                         "View_DISPOSALS_MAXCOMP.COMPLETEDATE, view_DISP_FIELD_Unfinished.Unfinished "+
                         "HAVING      (REQUESTS.REQUESTSTATUS = '01') AND (CUSTOMERS.CUSTOMERCLASS = '01') "+
                         "ORDER BY INCOMINGDATE";          
            }
            else
            {
                strSql = "SELECT   REQUESTS.REQCODE, REQUESTS.REQCOUNT, REQUESTS.REQDESCRIPTION, REQUESTS.CUSTCODE,  " +
                         "CUSTOMERS.ALIAS, CONVERT(varchar(100), REQUESTS.INCOMINGDATE, 20) AS INCOMINGDATE,  " +
                         "REQUESTS.ACCEPTOR, CAST(ROUND(DATEDIFF(s, REQUESTS.INCOMINGDATE, GETDATE()) * 1.0 / 3600, 2)  " +
                         "AS NUMERIC(10, 2)) AS DisposeTime, REQUESTS.ACCEPTOR_RESPONSIBILITY_NAME,  " +
                         "View_DISPOSALS_MAXCOMP.DISPOSESTATUS, (CASE WHEN View_DISPOSALS_MAXCOMP.sdcode IS NOT NULL  " +
                         "THEN '1' ELSE ISNULL(CAST(ROUND(DATEDIFF(s, View_DISPOSALS_MAXCOMP.COMPLETEDATE, GETDATE())  " +
                         "* 1.0 / 3600, 2) AS NUMERIC(10, 2)), 0) END) AS L2Compdate, View_FIELDWORKS_MAXCOMP.FIELDDISPOSESTATUS,  " +
                         "isnull(View_FIELDWORKS_MAXCOMP.FieldCompdate,0) as FieldCompdate, View_DISPOSALSL1_MAXCOMP.DISPOSEL1STATUS,  " +
                         "isnull(View_DISPOSALSL1_MAXCOMP.L1Compdate,0) as L1Compdate, view_DISP_FIELD_Unfinished.Unfinished AS NUM " +
                         "FROM         REQUESTS INNER JOIN " +
                         "CUSTOMERS ON REQUESTS.CUSTCODE = CUSTOMERS.CODE LEFT OUTER JOIN " +
                         "view_DISP_FIELD_Unfinished ON REQUESTS.REQCODE = view_DISP_FIELD_Unfinished.REQCODE LEFT OUTER JOIN " +
                         "View_DISPOSALSL1_MAXCOMP ON REQUESTS.REQCODE = View_DISPOSALSL1_MAXCOMP.REQCODE LEFT OUTER JOIN " +
                         "View_DISPOSALS_MAXCOMP ON REQUESTS.REQCODE = View_DISPOSALS_MAXCOMP.REQCODE LEFT OUTER JOIN " +
                         "View_FIELDWORKS_MAXCOMP ON REQUESTS.REQCODE = View_FIELDWORKS_MAXCOMP.REQCODE " +
                         "GROUP BY REQUESTS.REQCODE, REQUESTS.REQCOUNT, View_DISPOSALS_MAXCOMP.DISPOSESTATUS, " +
                         "View_FIELDWORKS_MAXCOMP.FIELDDISPOSESTATUS, View_DISPOSALSL1_MAXCOMP.DISPOSEL1STATUS, REQUESTS.REQUESTSTATUS, " +
                         "CUSTOMERS.CUSTOMERCLASS, REQUESTS.REQDESCRIPTION, REQUESTS.CUSTCODE, CUSTOMERS.ALIAS, REQUESTS.INCOMINGDATE,  " +
                         "REQUESTS.ACCEPTOR, REQUESTS.ACCEPTOR_RESPONSIBILITY_NAME, View_DISPOSALS_MAXCOMP.L2Compdate, " +
                         "View_FIELDWORKS_MAXCOMP.FieldCompdate, View_DISPOSALSL1_MAXCOMP.L1Compdate, View_DISPOSALS_MAXCOMP.SDCODE, " +
                         "View_DISPOSALS_MAXCOMP.COMPLETEDATE, view_DISP_FIELD_Unfinished.Unfinished " +
                         "HAVING      (REQUESTS.REQUESTSTATUS = '01') AND (CUSTOMERS.CUSTOMERCLASS = '01') " +
                         "AND (REQUESTS.ACCEPTOR_RESPONSIBILITY_NAME = '" + EngineerName + "') " +
                         "ORDER BY INCOMINGDATE";   
                                            
            }
            
            SqlCommand sqlCmd = new SqlCommand(strSql, db.sqlCon);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            int i = 0;
            while (sqlReader.Read())
            {
                i ++;
                DataRow dr = DT.NewRow();
                dr["序号"] = i.ToString();
                dr["故障单号"] = sqlReader[0].ToString();
                dr["子单数量"] = sqlReader[1].ToString();
                dr["问题简述"] = sqlReader[2].ToString();
                dr["医院编码"] = sqlReader[3].ToString();
                dr["医院名称"] = sqlReader[4].ToString();
                dr["受理时间"] = sqlReader[5].ToString();
                dr["受理人"] = sqlReader[6].ToString();
                dr["处理时间"] = Convert.ToDouble(sqlReader[7].ToString());
                dr["责任工程师"] = sqlReader[8].ToString();
                dr["L2状态"] = sqlReader[9].ToString();
                dr["L2完成时间"] = Convert.ToDouble(sqlReader[10].ToString());
                dr["现场状态"] = sqlReader[11].ToString();
                dr["现场完成时间"] = Convert.ToDouble(sqlReader[12].ToString());
                dr["L1状态"] = sqlReader[13].ToString();
                dr["L1完成时间"] = Convert.ToDouble(sqlReader[14].ToString());
                dr["子单未完成数量"] = sqlReader[15].ToString();
                DT.Rows.Add(dr);
            }
            DS.Tables.Add(DT);
            sqlCmd.Dispose();
            return DS;
        }

        public DataSet GetTestHosp()
        {
            //构建传出DT
            DataTable DT = new DataTable();
            DT.Columns.Add("序号");
            DT.Columns.Add("医院编码");
            DT.Columns.Add("医院名称");
            DT.Columns.Add("医院级别");
            DT.Columns.Add("医院类型");            
            DT.Columns.Add("所在区县");
            DT.Columns.Add("分中心名称");
            DT.Columns.Add("分中心编码");

            //构建传出DS
            DataSet DS = new DataSet();

            string strSql = string.Empty;
            strSql =  "SELECT     hosp.HOSPCODE, hosp.CAPTION, hosplevel.CAPTION AS 医院级别, hosptype.CAPTION AS 医院类型, county_1.CAPTION AS countyname,  "+
                      "range.UnitName, range.Unit "+
                      "FROM         (SELECT     HOSP_BASEINFO.HOSPCODE, HOSP_BASEINFO.CAPTION, HOSP_BASEINFO.HOSPITALLEVEL, HOSP_BASEINFO.HOSPITALTYPE,  "+
                      "HOSP_BASEINFO.COUNTY "+
                      "FROM          HOSP_BASEINFO LEFT OUTER JOIN "+
                      "UNIT_CUSTOMER ON HOSP_BASEINFO.HOSPCODE = UNIT_CUSTOMER.CUSTCODE "+
                      "WHERE      (HOSP_BASEINFO.HOSPCODE LIKE '999%') AND (NOT (HOSP_BASEINFO.HOSPCODE = '99999999')) AND  "+
                      "(UNIT_CUSTOMER.UNITCODE IS NULL) AND (HOSP_BASEINFO.COUNTY <> '0000')) AS hosp INNER JOIN "+
                      "(SELECT     TYPE, CODE, CAPTION, USABLE "+
                      "FROM          DICTS AS county "+
                      "WHERE      (TYPE = 'county')) AS county_1 ON hosp.COUNTY = county_1.CODE INNER JOIN "+
                      "(SELECT     TYPE, CODE, CAPTION, USABLE "+
                      "FROM          DICTS "+
                      "WHERE      (TYPE = 'customerLevel')) AS hosplevel ON hosp.HOSPITALLEVEL = hosplevel.CODE INNER JOIN "+
                      "(SELECT     TYPE, CODE, CAPTION, USABLE "+
                      "FROM          DICTS AS DICTS_1 "+
                      "WHERE      (TYPE = 'customertype')) AS hosptype ON hosp.HOSPITALTYPE = hosptype.CODE INNER JOIN "+
                      "(SELECT     Unit, UnitName, Range, Name " +
                      "FROM          UNIT_RANGE) AS range ON hosp.COUNTY = range.Range"; 


            SqlCommand sqlCmd = new SqlCommand(strSql, db.sqlCon);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            int i = 0;
            while (sqlReader.Read())
            {
                i++;
                DataRow dr = DT.NewRow();
                dr["序号"] = i.ToString();
                dr["医院编码"] = sqlReader[0].ToString();
                dr["医院名称"] = sqlReader[1].ToString();
                dr["医院级别"] = sqlReader[2].ToString();
                dr["医院类型"] = sqlReader[3].ToString();
                dr["所在区县"] = sqlReader[4].ToString();
                dr["分中心名称"] = sqlReader[5].ToString();
                dr["分中心编码"] = sqlReader[6].ToString();
                DT.Rows.Add(dr);
            }
            DS.Tables.Add(DT);
            sqlCmd.Dispose();
            return DS;
        }

        public bool SetUnitRange(string Unit,string Hosp)
        {
            bool flag = false;
            //检查是否存在，如果存在update，不存在insert
            string strSql = string.Empty;
            try
            {
                //if (IsExist(Hosp))
                //{
                //    strSql = "UPDATE  UNIT_CUSTOMER SET   UNITCODE = '" + Unit + "' WHERE   (CUSTCODE = '" + Hosp + "')";
                //    SqlCommand sqlCmd = new SqlCommand(strSql, db.sqlCon);
                //    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                //    sqlCmd.Dispose();
                //}
                //else
                //{
                //    strSql = "INSERT INTO UNIT_CUSTOMER (UNITCODE, CUSTCODE) VALUES   ('" + Unit + "','" + Hosp + "')";
                //    SqlCommand sqlCmd = new SqlCommand(strSql, db.sqlCon);
                //    sqlCmd.ExecuteNonQuery();
                //    sqlCmd.Dispose();
                //}
                //flag = true;
                strSql = "IF EXISTS(SELECT   UNITCODE, CUSTCODE FROM  UNIT_CUSTOMER WHERE   (CUSTCODE = '" + Hosp + "'))  "+
                        "BEGIN "+
                        "UPDATE  UNIT_CUSTOMER SET   UNITCODE = '" + Unit + "' WHERE   (CUSTCODE = '" + Hosp + "') "+
                        "END "+
                        "ELSE "+
                        "BEGIN "+
                        "INSERT INTO UNIT_CUSTOMER (UNITCODE, CUSTCODE) VALUES   ('" + Unit + "','" + Hosp + "') " +
                        "END";
                using (SqlCommand sqlCmd = new SqlCommand(strSql, db.sqlCon))
                {
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();                
                }                
                flag = true;
            }
            catch
            {
                flag = false;
            }
            return flag;           
        }
        public bool IsExist(string Hosp)
        {
            bool flag = false;
            //检查是否存在，如果存在update，不存在insert
            string strSql = string.Empty;
            strSql = "SELECT   UNITCODE, CUSTCODE FROM  UNIT_CUSTOMER WHERE   (CUSTCODE = '" + Hosp + "')";
            SqlCommand sqlCmd = new SqlCommand(strSql, db.sqlCon);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            
            if (sqlReader.Read() && sqlReader[0].ToString() != "")
            {
                //存在，update                
                flag = true;
            }
            else
            {
                //不存在，insert                
                flag = false;
            }
            sqlReader.Close();            
            return flag;           
        }

        public DataSet GetWorkloadGroup()
        {
            //构建传出DT
            DataTable DT = new DataTable();
            DT.Columns.Add("GroupID");
            DT.Columns.Add("GroupName");

            //构建传出DS
            DataSet DS = new DataSet();

            DataRow dr = DT.NewRow();
            dr["GroupID"] = "L1-1";
            dr["GroupName"] = "L1服务一组";
            DT.Rows.Add(dr);
            dr = DT.NewRow();
            dr["GroupID"] = "L2-1";
            dr["GroupName"] = "技术支持一组";
            DT.Rows.Add(dr);
            
            DS.Tables.Add(DT);            
            return DS;
        }

        public DataSet GetWorkload(string GroupID, string DT_S, string DT_E)
        {
            //构建传出DT
            DataTable DT = new DataTable();
            DT.Columns.Add("序号");
            DT.Columns.Add("员工号");
            DT.Columns.Add("员工姓名");
            DT.Columns.Add("未完成记录单数");
            DT.Columns.Add("未完成处理单数");
            
            DT.Columns[0].DataType = typeof(int);
            DT.Columns[3].DataType = typeof(int);
            DT.Columns[4].DataType = typeof(int);
            

            //构建传出DS
            DataSet DS = new DataSet();

            string strSql = string.Empty;
            if (GroupID == "L1-1")
            {
                strSql = "SELECT   Oper_ID, Oper_Name, SUM(RecordNotCompNum) AS RecordNotCompNum, SUM(DisposeNotCompNum)  "+
                         "AS DisposeNotCompNum "+
                         "FROM      (SELECT   b.Oper_ID, b.Oper_Name, COUNT(a.ACCEPTOR_RESPONSIBILITY) AS RecordNotCompNum,  "+
                         "0 AS DisposeNotCompNum "+
                         "FROM      REQUESTS AS a INNER JOIN "+
                         "OPERATOR AS b ON a.ACCEPTOR_RESPONSIBILITY = b.Oper_ID "+
                         "WHERE   (a.REQUESTSTATUS <> '04') AND (INCOMINGDATE BETWEEN '" + DT_S + "  00:00:00' AND '" + DT_E + "  23:59:59') " +
                         "AND (b.Oper_ID IN "+
                         "(SELECT   OPERATOR.Oper_ID "+
                         "FROM      SERVICE_GROUP INNER JOIN "+
                         "SERVICE_GROUP_MEMBER ON  "+
                         "SERVICE_GROUP.GROUP_CODE = SERVICE_GROUP_MEMBER.GROUP_CODE INNER JOIN "+
                         "OPERID_SOFTPHONEID ON  "+
                         "SERVICE_GROUP_MEMBER.OPERATOR_ID = OPERID_SOFTPHONEID.OPER_ID INNER JOIN "+
                         "OPERATOR ON OPERID_SOFTPHONEID.OPER_ID = OPERATOR.Oper_ID "+
                         "WHERE   (SERVICE_GROUP.GROUP_CODE = N'L1-1') AND  "+
                         "(OPERID_SOFTPHONEID.SOFTPHONE_ID >= '1171') AND  "+
                         "(OPERID_SOFTPHONEID.SOFTPHONE_ID <= '1190'))) "+
                         "GROUP BY b.Oper_ID, b.Oper_Name "+
                         "UNION "+
                         "SELECT   b.Oper_ID, b.Oper_Name, 0 AS RecordNotCompNum, COUNT(a.ACCEPTOR) AS DisposeNotCompNum "+
                         "FROM      DISPOSALS AS a INNER JOIN "+
                         "OPERATOR AS b ON a.ACCEPTOR = b.Oper_ID "+
                         "WHERE   (a.DISPOSESTATUS = '02' OR "+
                         "a.DISPOSESTATUS = '03') AND (DISPATCHDATE BETWEEN '" + DT_S + "  00:00:00' AND '" + DT_E + "  23:59:59') " +
                         "AND (b.Oper_ID IN "+
                         "(SELECT   OPERATOR_1.Oper_ID "+
                         "FROM      SERVICE_GROUP AS SERVICE_GROUP_2 INNER JOIN "+
                         "SERVICE_GROUP_MEMBER AS SERVICE_GROUP_MEMBER_2 ON  "+
                         "SERVICE_GROUP_2.GROUP_CODE = SERVICE_GROUP_MEMBER_2.GROUP_CODE INNER JOIN "+
                         "OPERID_SOFTPHONEID AS OPERID_SOFTPHONEID_1 ON  "+
                         "SERVICE_GROUP_MEMBER_2.OPERATOR_ID = OPERID_SOFTPHONEID_1.OPER_ID INNER JOIN "+
                         "OPERATOR AS OPERATOR_1 ON  "+
                         "OPERID_SOFTPHONEID_1.OPER_ID = OPERATOR_1.Oper_ID "+
                         "WHERE   (SERVICE_GROUP_2.GROUP_CODE = N'L1-1') AND  "+
                         "(OPERID_SOFTPHONEID_1.SOFTPHONE_ID >= '1171') AND  "+
                         "(OPERID_SOFTPHONEID_1.SOFTPHONE_ID <= '1190'))) "+
                         "GROUP BY b.Oper_ID, b.Oper_Name "+
                         "UNION "+
                         "SELECT   b.Oper_ID, b.Oper_Name, 0 AS RecordNotCompNum, COUNT(a.WORKACCEPTOR)  "+
                         "AS DisposeNotCompNum "+
                         "FROM      FIELDWORKS AS a INNER JOIN "+
                         "OPERATOR AS b ON a.WORKACCEPTOR = b.Oper_ID "+
                         "WHERE   (a.FIELDDISPOSESTATUS = '02' OR "+
                         "a.FIELDDISPOSESTATUS = '03') AND (GENERATEDATE BETWEEN '" + DT_S + "  00:00:00' AND '" + DT_E + "  23:59:59') " +
                         "AND (b.Oper_ID IN "+
                         "(SELECT   OPERATOR_2.Oper_ID "+
                         "FROM      SERVICE_GROUP AS SERVICE_GROUP_1 INNER JOIN "+
                         "SERVICE_GROUP_MEMBER AS SERVICE_GROUP_MEMBER_1 ON  "+
                         "SERVICE_GROUP_1.GROUP_CODE = SERVICE_GROUP_MEMBER_1.GROUP_CODE INNER JOIN "+
                         "OPERID_SOFTPHONEID AS OPERID_SOFTPHONEID_2 ON  "+
                         "SERVICE_GROUP_MEMBER_1.OPERATOR_ID = OPERID_SOFTPHONEID_2.OPER_ID INNER JOIN "+
                         "OPERATOR AS OPERATOR_2 ON  "+
                         "OPERID_SOFTPHONEID_2.OPER_ID = OPERATOR_2.Oper_ID "+
                         "WHERE   (SERVICE_GROUP_1.GROUP_CODE = N'L1-1') AND  "+
                         "(OPERID_SOFTPHONEID_2.SOFTPHONE_ID >= '1171') AND  "+
                         "(OPERID_SOFTPHONEID_2.SOFTPHONE_ID <= '1190'))) "+
                         "GROUP BY b.Oper_ID, b.Oper_Name) AS t1 " +
                         "GROUP BY Oper_ID, Oper_Name"; 
            }
            else if (GroupID == "L2-1")                
            {
                strSql = "SELECT   Oper_ID, Oper_Name, SUM(RecordNotCompNum) AS RecordNotCompNum, SUM(DisposeNotCompNum)  "+
                         "AS DisposeNotCompNum "+
                         "FROM      (SELECT   b.Oper_ID, b.Oper_Name, COUNT(a.ACCEPTOR_RESPONSIBILITY) AS RecordNotCompNum,  "+
                         "0 AS DisposeNotCompNum "+
                         "FROM      REQUESTS AS a INNER JOIN "+
                         "OPERATOR AS b ON a.ACCEPTOR_RESPONSIBILITY = b.Oper_ID "+
                         "WHERE   (a.REQUESTSTATUS <> '04') AND (INCOMINGDATE BETWEEN '" + DT_S + "  00:00:00' AND '" + DT_E + "  23:59:59') "+
                         "AND (b.Oper_ID IN "+
                         "(SELECT   OPERATOR.Oper_ID " +
                         "FROM      SERVICE_GROUP_MEMBER INNER JOIN "+
                         "SERVICE_GROUP ON SERVICE_GROUP_MEMBER.GROUP_CODE = SERVICE_GROUP.GROUP_CODE INNER JOIN "+
                         "OPERATOR ON SERVICE_GROUP_MEMBER.OPERATOR_ID = OPERATOR.Oper_ID "+
                         "WHERE   (SERVICE_GROUP.GROUP_CODE = 'L2-1') )) "+
                         "GROUP BY b.Oper_ID, b.Oper_Name "+
                         "UNION "+
                         "SELECT   b.Oper_ID, b.Oper_Name, 0 AS RecordNotCompNum, COUNT(a.ACCEPTOR) AS DisposeNotCompNum "+
                         "FROM      DISPOSALS AS a INNER JOIN "+
                         "OPERATOR AS b ON a.ACCEPTOR = b.Oper_ID "+
                         "WHERE   (a.DISPOSESTATUS = '02' OR "+
                         "a.DISPOSESTATUS = '03') AND (DISPATCHDATE BETWEEN '" + DT_S + "  00:00:00' AND '" + DT_E + "  23:59:59') "+
                         "AND (b.Oper_ID IN "+
                         "(SELECT   OPERATOR.Oper_ID " +
                         "FROM      SERVICE_GROUP_MEMBER INNER JOIN " +
                         "SERVICE_GROUP ON SERVICE_GROUP_MEMBER.GROUP_CODE = SERVICE_GROUP.GROUP_CODE INNER JOIN " +
                         "OPERATOR ON SERVICE_GROUP_MEMBER.OPERATOR_ID = OPERATOR.Oper_ID " +
                         "WHERE   (SERVICE_GROUP.GROUP_CODE = 'L2-1') )) " +
                         "GROUP BY b.Oper_ID, b.Oper_Name "+
                         "UNION "+
                         "SELECT   b.Oper_ID, b.Oper_Name, 0 AS RecordNotCompNum, COUNT(a.WORKACCEPTOR)  "+
                         "AS DisposeNotCompNum "+
                         "FROM      FIELDWORKS AS a INNER JOIN "+
                         "OPERATOR AS b ON a.WORKACCEPTOR = b.Oper_ID "+
                         "WHERE   (a.FIELDDISPOSESTATUS = '02' OR "+
                         "a.FIELDDISPOSESTATUS = '03') AND (GENERATEDATE BETWEEN '" + DT_S + "  00:00:00' AND '" + DT_E + "  23:59:59') "+
                         "AND (b.Oper_ID IN  "+
                         "(SELECT   OPERATOR.Oper_ID " +
                         "FROM      SERVICE_GROUP_MEMBER INNER JOIN " +
                         "SERVICE_GROUP ON SERVICE_GROUP_MEMBER.GROUP_CODE = SERVICE_GROUP.GROUP_CODE INNER JOIN " +
                         "OPERATOR ON SERVICE_GROUP_MEMBER.OPERATOR_ID = OPERATOR.Oper_ID " +
                         "WHERE   (SERVICE_GROUP.GROUP_CODE = 'L2-1') )) " +
                         "GROUP BY b.Oper_ID, b.Oper_Name) AS t1 "+
                         "GROUP BY Oper_ID, Oper_Name"; 
            }

            SqlCommand sqlCmd = new SqlCommand(strSql, db.sqlCon);
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            int i = 0;
            while (sqlReader.Read())
            {
                i++;
                DataRow dr = DT.NewRow();
                dr["序号"] = i.ToString();
                dr["员工号"] = sqlReader[0].ToString();
                dr["员工姓名"] = sqlReader[1].ToString();
                dr["未完成记录单数"] = sqlReader[2].ToString();
                dr["未完成处理单数"] = sqlReader[3].ToString();                
                DT.Rows.Add(dr);
            }
            DS.Tables.Add(DT);
            sqlCmd.Dispose();
            return DS;
        }
    }
}
