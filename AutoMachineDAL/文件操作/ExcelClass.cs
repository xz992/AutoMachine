using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Microsoft.Office.Interop.Excel;
namespace AutoMachineDAL
{
    public class ExcelClass
    {
        //向Excel写入数据
        public  bool WriteXls(string filename)
        {
            //启动Excel应用程序
            Microsoft.Office.Interop.Excel.Application xls = new Microsoft.Office.Interop.Excel.Application();
            _Workbook book = xls.Workbooks.Add(Missing.Value); //创建一张表，一张表可以包含多个sheet

            //如果表已经存在，可以用下面的命令打开
            //_Workbook book = xls.Workbooks.Open(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            _Worksheet sheet;//定义sheet变量
            xls.Visible = false;//设置Excel后台运行
            xls.DisplayAlerts = false;//设置不显示确认修改提示

            for (int i = 1; i < 4; i++)//循环创建并写入数据到sheet
            {
                try
                {
                    sheet = (_Worksheet)book.Worksheets.get_Item(i);//获得第i个sheet，准备写入
                }
                catch (Exception ex)//不存在就增加一个sheet
                {
                    sheet = (_Worksheet)book.Worksheets.Add(Missing.Value, book.Worksheets[book.Sheets.Count], 1, Missing.Value);
                }
                sheet.Name = "第" + i.ToString() + "页";//设置当前sheet的Name
                for (int row = 1; row < 20; row++)//循环设置每个单元格的值
                {
                    for (int offset = 1; offset < 10; offset++)
                        sheet.Cells[row, offset] = "( " + row.ToString() + "," + offset.ToString() + " )";
                }
            }
            //将表另存为
            book.SaveAs(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //如果表已经存在，直接用下面的命令保存即可
            //book.Save();

            book.Close(false, Missing.Value, Missing.Value);//关闭打开的表
            xls.Quit();//Excel程序退出
            //sheet,book,xls设置为null，防止内存泄露
            sheet = null;
            book = null;
            xls = null;
            GC.Collect();//系统回收资源
            return true;
        }


        //从Excel读取数据
        public  Array ReadXls(string filename, int index)//读取第index个sheet的数据
        {
            //启动Excel应用程序
            Microsoft.Office.Interop.Excel.Application xls = new Microsoft.Office.Interop.Excel.Application();
            //打开filename表
            _Workbook book = xls.Workbooks.Open(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            _Worksheet sheet;//定义sheet变量
            xls.Visible = false;//设置Excel后台运行
            xls.DisplayAlerts = false;//设置不显示确认修改提示

            try
            {
                sheet = (_Worksheet)book.Worksheets.get_Item(index);//获得第index个sheet，准备读取
            }
            catch (Exception ex)//不存在就退出
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            Console.WriteLine(sheet.Name);
            int row = sheet.UsedRange.Rows.Count;//获取不为空的行数
            int col = sheet.UsedRange.Columns.Count;//获取不为空的列数
            Array value = (Array)sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[row, col]).Cells.Value2;//获得区域数据赋值给Array数组，方便读取

            book.Save();//保存
            book.Close(false, Missing.Value, Missing.Value);//关闭打开的表
            xls.Quit();//Excel程序退出
            //sheet,book,xls设置为null，防止内存泄露
            sheet = null;
            book = null;
            xls = null;
            GC.Collect();//系统回收资源
            return value;
        }


    }
}
