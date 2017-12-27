using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace AutoMachineDAL
{
    public  class TxTClass
    {

        public string  TxtFilePath= Directory.GetCurrentDirectory() + "/log.txt";
        public void WriteTxt(string TxtContent,string  TxtPath)
        {

            FileStream fs=new FileStream(TxtPath,FileMode.Append);
            StreamWriter sw=new StreamWriter(fs);
            sw.WriteLine(TxtContent);
            sw.Close();
            fs.Close();

        }


        public void DisplayLog(string LogContent, System.Windows.Forms.TextBox DisplayLog_TextBox)
        {
            Monitor.Enter(this);//锁定，保持同步

            string TotalLogContent = DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒   ") + LogContent;
            DisplayLog_TextBox.AppendText(TotalLogContent);

            //string LogPath = Directory.GetCurrentDirectory() + "/log/" + DateTime.Now.ToString("yyyy年MM月dd日") + ".txt";
            //WriteTxt(TotalLogContent, LogPath);

            Monitor.Exit(this);//取消锁定

        }

        public void ClearLog(System.Windows.Forms.TextBox DisplayLog_TextBox)
        {

            DisplayLog_TextBox.Clear();

        }



        public void UiDisplayLog(string LogContent, System.Windows.Forms.TextBox DisplayLog_TextBox)
        {
            string TotalLogContent = LogContent;
            DisplayLog_TextBox.AppendText(TotalLogContent);

            string LogPath = Directory.GetCurrentDirectory() + "/log/" + DateTime.Now.ToString("yyyy年MM月dd日") + ".txt";
            WriteTxt(TotalLogContent, LogPath);
        }

        public void DebugOutput(string Mssage)
        {
            Console.WriteLine(Mssage);
        }
    }
}
