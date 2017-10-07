using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.IO;

namespace SDEtools
{
    /// <summary>
    /// UPDATE 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://SDETools/UPDATE/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class UPDATE : System.Web.Services.WebService
    {
        SDEtools.Action.Update uAction = new SDEtools.Action.Update();
        [WebMethod(Description = "取得版本号")]
        public string GetVersion(string File_Name)
        {
            //return uAction.Getversion ();

            string path = @"\SDETools\Update";
            path = HttpContext.Current.Server.MapPath(path);
            //string File_Name = "服务平台增强工具.exe";
            System.Diagnostics.FileVersionInfo fileinfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(path + "\\" + File_Name);
            return fileinfo.FileVersion ;
        }
        [WebMethod(Description = "获取文件信息")]
        public string GetFileInfo(string File_Name)
        {
            string path = @"\SDETools\Update";
            path = HttpContext.Current.Server.MapPath(path );
            FileInfo fi = new FileInfo(path + "\\" + File_Name);
            return fi.Length.ToString();
        }

        [WebMethod(Description = "下载对应附件")]
        public byte[] DownFile(string File_name, long PosOffset, int BlockLength)
        {
            string path = @"\SDETools\Update";
            path = HttpContext.Current.Server.MapPath(path);

            string strFilePath = path + "\\" + File_name;
            byte[] rbt;

            FileStream FileStream = null;

            try
            {
                if (BlockLength > 20 * 1024 * 1024) throw new Exception("下载文件分块大小大于16KB");

                if (false == File.Exists(strFilePath)) throw new Exception(string.Format("失败 ：未能找到 {0} 文件！", strFilePath));

                string strBase64 = string.Empty;

                using (FileStream = File.Open(strFilePath, FileMode.Open, FileAccess.Read))
                {
                    FileStream.Seek(PosOffset, SeekOrigin.Begin);

                    rbt = new byte[BlockLength];

                    int readLength = FileStream.Read(rbt, 0, rbt.Length);
                    strBase64 = Convert.ToBase64String(rbt);
                    return rbt;
                }
            }
            catch (Exception ex)
            {
                return new byte[0];
            }
            finally
            {
                FileStream.Close();
                FileStream = null;
            }

        }
    }
}
