using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QuickCoding
{
    class Logs
    {
        public string showInfo(string msg){
            return "信息   " +DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"   "+msg;
        }
        public string showError(string msg)
        {
            return "错误   " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "   " + msg;
        }
        public string showWarn(string msg)
        {
            return "警告   " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "   " + msg;
        }
        public void writeLogFile(string input)
        {
            string fname = Directory.GetCurrentDirectory() + "\\LogFile.txt";
            FileInfo finfo = new FileInfo(fname);
            if (!finfo.Exists)
            {
                FileStream fs;
                fs = File.Create(fname);
                fs.Close();
                finfo = new FileInfo(fname);
            }
            if (finfo.Length > 1024 * 1024 * 10)
            {
                File.Move(Directory.GetCurrentDirectory() + "\\LogFile.txt", Directory.GetCurrentDirectory() + DateTime.Now.TimeOfDay + "\\LogFile.txt");
                //删除该文件
                finfo.Delete();
            }
            using (FileStream fs = finfo.OpenWrite())
            {
                StreamWriter w = new StreamWriter(fs);
                w.BaseStream.Seek(0, SeekOrigin.End);
                w.Write("\n\rLog Entry : ");
                w.Write("{0} {1} \n\r", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                w.Write(input + "\n\r");
                w.Write("------------------------------------\n\r");
                w.Flush();
                w.Close();
            }

        }
    }
}
