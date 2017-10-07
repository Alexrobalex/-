using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.IO;

namespace SDEtools
{
    /// <summary>
    /// RemindTools 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://SDETools/RemindTools/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class RemindTools : System.Web.Services.WebService
    {
        SDEtools.Action.RemTools uAction = new SDEtools.Action.RemTools();
        [WebMethod(Description = "取得工程师列表")]
        public DataSet GetEngineer()
        {
            return uAction.GetEngineer();
        }

        [WebMethod(Description = "取得对应工程师的提醒列表")]
        public DataSet GetRemindList(string EngineerName, string Date_S, string Date_E)
        {
            DataSet ds = new DataSet();
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            DataTable dt = new DataTable();
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
            DT.Columns.Add("显示状态");            
            DT.Columns[0].DataType = typeof(int);
            DT.Columns[8].DataType = typeof(Double);

            ds = uAction.GetRemindList(EngineerName, Date_S, Date_E);
            dt= ds.Tables [0];            
            for (int i=0;i<dt.Rows.Count ;i++)
            {
                DataRow DR = DT.NewRow();
                DR[0] = int.Parse(dt.Rows[i][0].ToString());
                DR[1] = dt.Rows[i][1].ToString();
                DR[2] = dt.Rows[i][2].ToString();
                DR[3] = dt.Rows[i][3].ToString();
                DR[4] = dt.Rows[i][4].ToString();
                DR[5] = dt.Rows[i][5].ToString();
                DR[6] = dt.Rows[i][6].ToString();
                DR[7] = dt.Rows[i][7].ToString();
                DR[8] = double.Parse(dt.Rows[i][8].ToString());
                DR[9] = dt.Rows[i][9].ToString();
                if (double.Parse(dt.Rows[i][8].ToString()) < 72)
                {
                    DR[10] = "black";
                }
                if (double.Parse(dt.Rows[i][8].ToString()) >= 72)
                {
                    DR[10] = "red";
                }
                if (dt.Rows[i][10].ToString() == "04" && double.Parse(dt.Rows[i][11].ToString()) > 0 && double.Parse(dt.Rows[i][11].ToString()) <= 24) //L2子单处理
                {
                    DR[10] = "green";
                }
                if (dt.Rows[i][12].ToString() == "04" && double.Parse(dt.Rows[i][13].ToString()) > 0 && double.Parse(dt.Rows[i][13].ToString()) <= 24) //现场子单处理
                {
                    DR[10] = "green";
                }
               if (dt.Rows[i][14].ToString() == "04" && double.Parse(dt.Rows[i][15].ToString()) > 0 && double.Parse(dt.Rows[i][15].ToString()) <= 24) //L1子单处理
                {
                    DR[10] = "green";
                }
                if (dt.Rows[i][16].ToString() == "" )//全部子单处理
                {
                    DR[10] = "blue";
                }
                DT.Rows.Add(DR);
            }
            DS.Tables.Add(DT);
            return DS;
        }
        [WebMethod(Description = "取得当前日期")]
        public string GetDate()
        {
            return DateTime.Now.ToLongDateString().ToString();
        }

        [WebMethod(Description = "取得测试医院分区划片信息")]
        public DataSet GetTestHosp()
        {
            return uAction.GetTestHosp();
        }
        [WebMethod(Description = "提交医院分区划片信息")]
        public bool SetUnitRange(string Unit, string Hosp)
        {
            return uAction.SetUnitRange(Unit,  Hosp);
        }

        [WebMethod(Description = "获取工作量负荷统计工作组")]
        public DataSet GetWorkloadGroup()
        {
            return uAction.GetWorkloadGroup();
        }
        [WebMethod(Description = "获取工作量负荷统计")]
        public DataSet GetWorkload(string GroupID, string DT_S, string DT_E)
        {
            return uAction.GetWorkload(GroupID, DT_S, DT_E);
        }
        [WebMethod(Description = "获取授权")]
        public bool GetAuthorize(string Token)
        {
            bool flag = false;
            if (Token=="1487")
            {
            flag = true;
            }
            return flag;
        }


    }
}
