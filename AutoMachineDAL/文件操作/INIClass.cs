using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;





namespace AutoMachineDAL
{
    public  class INIClass
    {

 
        //声明读写INI文件的API函数  
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath); 

        public  string  IniFilePath=Directory.GetCurrentDirectory() + "/Setting.ini";
        /// <summary>
        /// 创建INI文件
        /// </summary>
        /// <param name="AFileName"></param>
        public void  IniFiles(string AFileName)  
        {  
            // 判断文件是否存在  
            FileInfo fileInfo = new FileInfo(AFileName);  
            //Todo:搞清枚举的用法  
            if ((!fileInfo.Exists))  
            { //|| (FileAttributes.Directory in fileInfo.Attributes))  
                //文件不存在，建立文件  
                System.IO.StreamWriter sw = new System.IO.StreamWriter(AFileName, false, System.Text.Encoding.Default);  
                try  
                {  
                    sw.Write("文件创建成功");  
                    sw.Close();  
                }  
                catch  
                {  
                    MessageBox.Show("Ini文件不存在");  
                }  
            }  
            //必须是完全路径，不能是相对路径  
            //FileName = fileInfo.FullName;  
        }



        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Value"></param>

        public void WriteString(string Section, string Ident, string Value, string FileName)
        {
            if (!WritePrivateProfileString(Section, Ident, Value, FileName))
            {

                MessageBox.Show("写Ini文件出错");
            }
        }




        /// <summary>
        /// 读INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public string ReadString(string Section, string Ident, string Default, string FileName)
        {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), FileName);
            //必须设定0（系统默认的代码页）的编码方式，否则无法支持中文  
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            s = s.Substring(0, bufLen);
            return s.Trim();
        }




        /// <summary>
        /// 读整数
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Default"></param>
        /// <returns></returns>

        public int ReadInteger(string Section, string Ident, int Default, string FileName)
        {
            string intStr = ReadString(Section, Ident, Convert.ToString(Default), FileName);
            try
            {
                return Convert.ToInt32(intStr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return Default;
            }
        }

        /// <summary>
        /// 写整数
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Value"></param>

        public void WriteInteger(string Section, string Ident, int Value, string FileName)
        {
            WriteString(Section, Ident, Value.ToString(),FileName);
        }




        /// <summary>
        /// 读布尔值
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Default"></param>
        /// <returns></returns>

        public bool ReadBool(string Section, string Ident, bool Default, string FileName)
        {
            try
            {
                return Convert.ToBoolean(ReadString(Section, Ident, Convert.ToString(Default),FileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return Default;
            }
        }

        /// <summary>
        /// 写布尔值
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Value"></param>
        /// <param name="FileName"></param>
 
        public void WriteBool(string Section, string Ident, bool Value, string FileName)
        {
            WriteString(Section, Ident, Convert.ToString(Value),FileName);
        }


        /// <summary>
        /// 读double类型数据
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Default"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public double ReadDouble(string Section, string Ident, double Default, string FileName)
        {
            try
            {
                return Convert.ToDouble(ReadString(Section, Ident, Convert.ToString(Default), FileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return Default;
            }
        }


        /// <summary>
        /// 写double类型数据
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Value"></param>
        /// <param name="FileName"></param>
        public void WriteDouble(string Section, string Ident, double Value, string FileName)
        {
            WriteString(Section, Ident, Convert.ToString(Value), FileName);
        }


        /// <summary>
        /// 从Ini文件中，将指定的Section名称中的所有Ident添加到列表中
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Idents"></param>

        public void ReadSection(string Section, StringCollection Idents, string FileName)
        {
            Byte[] Buffer = new Byte[16384];
            //Idents.Clear();  

            int bufLen = GetPrivateProfileString(Section, null, null, Buffer, Buffer.GetUpperBound(0), FileName);
            //对Section进行解析  
            GetStringsFromBuffer(Buffer, bufLen, Idents);
        }

        private void GetStringsFromBuffer(Byte[] Buffer, int bufLen, StringCollection Strings)
        {
            Strings.Clear();
            if (bufLen != 0)
            {
                int start = 0;
                for (int i = 0; i < bufLen; i++)
                {
                    if ((Buffer[i] == 0) && ((i - start) > 0))
                    {
                        String s = Encoding.GetEncoding(0).GetString(Buffer, start, i - start);
                        Strings.Add(s);
                        start = i + 1;
                    }
                }
            }
        }

        /// <summary>
        /// 从Ini文件中，读取所有的Sections的名称 
        /// </summary>
        /// <param name="SectionList"></param>
        /// <param name="FileName"></param>
 
        public void ReadSections(StringCollection SectionList, string FileName)
        {
            //Note:必须得用Bytes来实现，StringBuilder只能取到第一个Section  
            byte[] Buffer = new byte[65535];
            int bufLen = 0;
            bufLen = GetPrivateProfileString(null, null, null, Buffer,Buffer.GetUpperBound(0), FileName);
            GetStringsFromBuffer(Buffer, bufLen, SectionList);
        }

        /// <summary>
        /// 清除某个Section 
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="FileName"></param>
 
        public void EraseSection(string Section, string FileName)
        {
            if (!WritePrivateProfileString(Section, null, null, FileName))
            {
                MessageBox.Show("无法清除Ini文件中的Section");
            }
        }


        /// <summary>
        /// 删除某个Section下的键 
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>

        public void DeleteKey(string Section, string Ident, string FileName)
        {
            WritePrivateProfileString(Section, Ident, null, FileName);
        }



        /// <summary>
        /// 检查某个Section下的某个键值是否存在
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <returns></returns>
        public bool ValueExists(string Section, string Ident, string FileName)
        {
            StringCollection Idents = new StringCollection();
            ReadSection(Section, Idents,FileName);
            return Idents.IndexOf(Ident) > -1;
        }



    }
}
