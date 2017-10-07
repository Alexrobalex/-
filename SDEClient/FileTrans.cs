using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SDEClient
{
    public abstract class FileTrans
    {      

        public static byte[] ConvertStreamToByteBuffer(Stream s)
        {
            MemoryStream ms = new MemoryStream();
            int b;
            while ((b = s.ReadByte()) != -1)
            {
                ms.WriteByte((byte)b);
            }
            return ms.ToArray();
        }


        /// <summary>
        /// 获取文件二进制块
        /// </summary>
        /// <paramname="path"></param>
        /// <paramname="byteIndex"></param>
        /// <paramname="length"></param>
        ///<returns></returns>
        public static byte[] GetFileBloc(long byteIndex, long length, string FileName)
        {
            //string path =System.Configuration.ConfigurationSettings.AppSettings["path"].ToString();
            //string path = @"d:\1.exe";
            FileStream stream = new FileInfo(FileName).OpenRead();
            stream.Position = byteIndex;
            byte[] Filebuffer = new byte[length];
            stream.Read(Filebuffer, 0, Convert.ToInt32(length));
            return Filebuffer;
        }
        public static byte[] ChangeFileToByte(string path)
        {
            FileStream stream = new FileInfo(path).OpenRead();
            byte[] Filebuffer = new byte[stream.Length];
            stream.Read(Filebuffer, 0, Convert.ToInt32(stream.Length));
            return Filebuffer;
        }
        public static byte[] ConvertToBinary(long index, bool ifEnd, long fileLenght,string FileName)
        {
            //20971520

            byte[] bysFile;//临时二进制数组，最大20M
            //index = buffer.Length / 20971520;
            //index += buffer.Length % 20971520 == 0 ? 0 : 1;
            //byte[] bys;//临时二进制数组，最大20M
            if (ifEnd == false)
            {

                bysFile = GetFileBloc(20971520 * index, 20971520, FileName);

            }
            else
            {

                bysFile = GetFileBloc(20971520 * index, fileLenght - 20971520 * (index), FileName);

            }
            Console.WriteLine("The length of the current buffer is " + bysFile.Length);
            return bysFile;

        }
        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <paramname="path"></param>
        ///<returns></returns>
        public static long LengthOfFile(string path)
        {
            FileInfo fi = new FileInfo(path);
            return fi.Length;

        }
        public static byte[] ConvertToBinary(string Path)
        {
            FileStream stream = new FileInfo(Path).OpenRead();
            byte[] buffer = new byte[stream.Length];
            //Console.WriteLine("The lenght of the file is" + buffer.Length);
            stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
            return buffer;
        }
        
    }
}
