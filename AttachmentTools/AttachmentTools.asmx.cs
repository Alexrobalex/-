using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.IO;


namespace AttTools
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class AttachmentTools : System.Web.Services.WebService
    {
        AttTools.Action.SQLServerAction uAction = new AttTools.Action.SQLServerAction();
        [WebMethod]
        public string Select(string AttachmentID)
        {
            return uAction.Select(AttachmentID);
        }

        [WebMethod]
        public DataSet Index(string REQCODE, string DT_S, String DT_E)
        {
            return uAction.Index(REQCODE, DT_S, DT_E);
        }

        ///
        /// 上传文件
        ///
        /// 文件的byte[]
        /// 上传文件的路径
        /// 上传文件名字
        ///

        public bool UploadFile(byte[] fs, string path, string fileName)
        {
            bool flag = false;
            try
            {
                //获取上传案例图片路径
                //path = Server.MapPath(path);
                path = @"D\Attachment";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //定义并实例化一个内存流，以存放提交上来的字节数组。
                MemoryStream m = new MemoryStream(fs);            //定义实际文件对象，保存上载的文件。

                FileStream f = new FileStream(path + "\\" + fileName, FileMode.Create);  //把内存里的数据写入物理文件
                m.WriteTo(f);
                m.Close();
                f.Close();
                f = null;
                m = null;
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        [WebMethod]
        public string Save(string REQCODE, byte[] fs, string path, string Attachment_Name, string Attachment_Name_new, string Attachment_Type, string Attachment_Desc, string Proprogress)
        {
            string rst = "0";
            if (path == "")
            {
                path = @"\SD_Attachment\" + REQCODE.ToString();
            }
            //string tmpRootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            //path = tmpRootDir + path.Replace(@"/", @"\"); //转换成绝对路径 
            path = Server.MapPath(path);
            if (Proprogress == "0")//定义新名称
            {
                Attachment_Name_new = REQCODE + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                rst = Attachment_Name_new;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            string Flagstr = string.Empty;
            try
            {
                switch (Proprogress)
                {
                    case "0":
                        Flagstr = TransFile(fs, path, Attachment_Name_new, true);
                        //rst = Flagstr;
                        break;
                    case "1":
                        Flagstr = TransFile(fs, path, Attachment_Name_new, false);
                        rst = Flagstr;
                        break;
                    case "9":
                        //Flagstr = TransFile(fs, path, Attachment_Name_new, false);
                        if (uAction.Save(REQCODE, Attachment_Name, Attachment_Name_new, Attachment_Type, Attachment_Desc))
                        {
                            Flagstr = "0";
                        }
                        break;
                }
                //if (Flagstr != "0" || Flagstr != "-1")
                // {                    
                //     rst = "-1";
                // }
            }
            catch (Exception ex)
            {
                rst = "-1";
            }
            return rst;
        }


        public byte[] DownloadFile(string Attachment_ID)
        {
            string str = uAction.GetAttachment(Attachment_ID);
            string[] sArray = str.Split('|');
            string REQCODE = sArray[0];
            string Attachment_Name = sArray[1];
            string Attachment_Name_new = sArray[2];

            string path = @"\Attachment\" + REQCODE;

            string strFilePath = Attachment_Name_new;
            FileStream fs = null;
            string CurrentUploadFolderPath = HttpContext.Current.Server.MapPath(path);
            string CurrentUploadFilePath = CurrentUploadFolderPath + "\\" + strFilePath;
            if (File.Exists(CurrentUploadFilePath))
            {
                try
                {
                    ///打开现有文件以进行读取。
                    fs = File.OpenRead(CurrentUploadFilePath);
                    int b1;
                    System.IO.MemoryStream tempStream = new System.IO.MemoryStream();
                    while ((b1 = fs.ReadByte()) != -1)
                    {
                        tempStream.WriteByte(((byte)b1));
                    }
                    return tempStream.ToArray();
                }
                catch (Exception ex)
                {
                    return new byte[0];
                }
                finally
                {
                    fs.Close();
                }
            }
            else
            {
                return new byte[0];
            }

        }

        [WebMethod]
        public string GetDateTime()
        {
            return uAction.GetDateTime();
        }

        [WebMethod]
        public bool Delete(string Attachment_ID)
        {
            return uAction.Delete(Attachment_ID);
        }


        /**/
        /// <summary>
        /// UpDownFile 的摘要说明
        /// </summary>

        //将Stream流转换为byte数组的方法。
        //PS：原本想把这个方法也当做WebMethod的，因为客户端在上传文件时也要调用该方法，后来发现Stream类型的不能通过WebService传输。。。：（
        public byte[] ConvertStreamToByteBuffer(Stream s)
        {
            MemoryStream ms = new MemoryStream();
            int b;
            while ((b = s.ReadByte()) != -1)
            {
                ms.WriteByte((byte)b);
            }
            return ms.ToArray();
        }

        //上传文件至WebService所在服务器的方法，这里为了操作方法，文件都保存在UpDownFile服务所在文件夹下的File目录中

        public bool Up(byte[] data, string filename)
        {
            try
            {
                FileStream fs = File.Create(Server.MapPath("image\\") + filename);

                fs.Write(data, 0, data.Length);
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //下载WebService所在服务器上的文件的方法        
        public byte[] Down(string filename)
        {
            string filepath = Server.MapPath("File\\") + filename;
            if (File.Exists(filepath))
            {
                try
                {
                    FileStream s = File.OpenRead(filepath);
                    return ConvertStreamToByteBuffer(s);
                }
                catch
                {
                    return new byte[0];
                }
            }
            else
            {
                return new byte[0];
            }
        }


        public string TransFile(byte[] fileBt, string path, string fileName, bool ifCreate)
        {
            string rst = "0";
            if (fileBt.Length == 0)
                return rst;
            string filePath = path;
            FileStream fstream;
            if (ifCreate)
            {
                fstream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                fstream.Close();
                return rst;
            }
            else
            {
                fstream = new FileStream(filePath + "\\" + fileName, FileMode.Append);
                try
                {
                    fstream.Write(fileBt, 0, fileBt.Length);  //二进制转换成文件          
                    rst = fstream.Length.ToString();
                }
                catch (Exception ex)
                {
                    //抛出异常信息
                    rst = ex.ToString();
                }
                finally
                {
                    fstream.Close();
                }
                return rst;
            }
        }

        [WebMethod(Description = "获取文件信息")]
        public string GetFileInfo(string Attachment_ID)
        {
            string str = uAction.GetAttachment(Attachment_ID);
            string[] sArray = str.Split('|');
            string REQCODE = sArray[0];
            string Attachment_Name = sArray[1];
            string Attachment_Name_new = sArray[2];
            string path = @"\SD_Attachment\" + REQCODE;
            path = HttpContext.Current.Server.MapPath(path);
            string strFilePath = Attachment_Name_new;
            FileInfo fi = new FileInfo(path + "\\" + strFilePath);
            return fi.Length.ToString();
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="pFilePath">文件路径</param>
        /// <param name="pFileName">文件名称</param>
        /// <param name="PosOffset">文件偏移位置</param>
        /// <param name="BlockLength">下载长度</param>
        /// <returns></returns>
        [WebMethod]
        public byte[] DownFile(string Attachment_ID, long PosOffset, int BlockLength)
        {
            string str = uAction.GetAttachment(Attachment_ID);
            string[] sArray = str.Split('|');
            string REQCODE = sArray[0];
            string Attachment_Name = sArray[1];
            string Attachment_Name_new = sArray[2];
            string path = @"\SD_Attachment\" + REQCODE;
            path = HttpContext.Current.Server.MapPath(path);

            string strFilePath = path + "\\" + Attachment_Name_new;
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

        [WebMethod]
        public string GetREQUESTS(string REQCODE)
        {
            return uAction.GetREQUESTS(REQCODE);
        }

        [WebMethod]
        public string GetAttachmentNum()
        {
            return uAction.GetAttachmentNum();
        }

    }
}