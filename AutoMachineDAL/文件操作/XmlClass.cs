using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace AutoMachineDAL
{
    public  class XmlClass
    {


        //Listview控件内容写入到XML中
        public void ListViewWriteXml(ListView ListViewObject,string XmlFilePath)
        {
            try
            {

                DataTable dataTable = new DataTable("TableName");
                for (int i = 0; i < ListViewObject.Columns.Count; i++)
                {
                    dataTable.Columns.Add(ListViewObject.Columns[i].Text);

                }

                for (int i = 0; i < ListViewObject.Items.Count; i++)
                {

                    DataRow dataRow = dataTable.NewRow();
                    for (int j = 0; j < ListViewObject.Columns.Count; j++)
                    {
                        dataRow[j] = ListViewObject.Items[i].SubItems[j].Text;
                    }
                    dataTable.Rows.Add(dataRow);
                }
                dataTable.WriteXml(XmlFilePath);

                dataTable.Dispose();


            }
            catch
            {

            }

        }

        //从XML文件读取数据到ListView控件中
        public void XmlReadListView(string XmlFileName, ListView ListViewObject)
        {

            try
            {
                if (!File.Exists(XmlFileName))
                {
                    MessageBox.Show("XML文件不存在");
                    return;
                }

                DataSet dataSet = new DataSet();
                dataSet.ReadXml(XmlFileName);
                DataTable dataTable = dataSet.Tables[0];
                ListViewObject.Columns.Clear();
                ListViewObject.Items.Clear();
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    ListViewObject.Columns.Add(dataTable.Columns[i].ColumnName);
                    ListViewObject.Columns[i].Width = 100;
                }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    ListViewItem listViewItem = new ListViewItem(dataTable.Rows[i][0].ToString());
                    for (int j = 1; j < dataTable.Columns.Count; j++)
                    {
                        listViewItem.SubItems.Add(dataTable.Rows[i][j].ToString());
                    }
                    ListViewObject.Items.Add(listViewItem);
                }
                dataTable.Dispose();
                dataSet.Dispose();

            }
            catch
            {

            }


        }

          //删除节点内容  
        public void DeleteXml(string XmlFileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlFileName);

            XmlNodeList xnl = xmlDoc.SelectSingleNode("DocumentElement").ChildNodes; //查找节点  

            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;

                xe.RemoveAll();  

            }
            xmlDoc.Save(XmlFileName);
        }

        //从XML文件读取数据到dataGridView控件中
        public void XmlReadDataGridView(string XmlFileName, string TableName, System.Windows.Forms.DataGridView dataGridView)
        {
            try
            {
                if (!File.Exists(XmlFileName))
                {
                    MessageBox.Show("XML文件不存在");
                    return;
                }
                DataSet myDataSet = new DataSet();
                myDataSet.ReadXml(XmlFileName);
                dataGridView.DataSource = myDataSet.Tables[TableName];

            }
            catch
            {

            }

        }

        //创建XML文件
        public void CreateXml(string XmlFileName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                //创建类型声明节点  
                XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
                xmlDoc.AppendChild(node);
                //创建根节点  
                XmlNode root = xmlDoc.CreateElement("root");
                xmlDoc.AppendChild(root);
                try
                {
                    xmlDoc.Save(XmlFileName);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);

                }

            }
            catch
            {
                
            }
            


        }

    }
}
