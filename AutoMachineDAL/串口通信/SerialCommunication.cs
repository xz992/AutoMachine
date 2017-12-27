using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace AutoMachineDAL
{
    class SerialCommunication
    {
        public bool  OpenCom(ref SerialPort serialPort)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    serialPort.Open();
                }
                else
                {
                    serialPort.Open();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("串口打开失败!!" + ex.Message);
                return false;
            }

            return true;

        }


        public bool  CloseCom(ref SerialPort serialPort)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("串口关闭失败!!" + ex.Message);
                return false;
            }

            return true;

        }


        public bool  SendData(ref SerialPort serialPort,string SendContent)
        {
            try
            {
                serialPort.WriteLine(SendContent);

            }
            catch (Exception ex)
            {
                MessageBox.Show("串口发送数据失败!!" + ex.Message);
                return false;
            }

            return true;

        }

        public string GetData(ref SerialPort serialPort)
        {
            string strRead = string.Empty;
            try
            {
                strRead = serialPort.ReadExisting();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("串口接收数据失败!!" + ex.Message);
                strRead = "ERROR";
            }

            return strRead;
        }

    }//类结束
}
