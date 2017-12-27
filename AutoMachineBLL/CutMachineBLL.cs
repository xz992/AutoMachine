using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AutoMachineDAL;
using AutoMachineModel;
using HalconDotNet;

namespace AutoMachineBLL
{
    public class CutMachineBLL
    {
        private AutoMachineDAL.DAL CutMachineDal;                                 //数据访问层
       
        public CutMachineBLL()
        {
            CutMachineDal = new AutoMachineDAL.DAL();
        }

        public void BLL_Init()
        {
                 CutMachineDal.MotionObject.InitMotionCard();
        }

        public void BLL_LoadProjectInfo()
        {

            string ReadContent;
            ReadContent = CutMachineDal.IniFile.ReadString("Proram", "MatchScore", "0", Model.m_szProjectIniPath);
            Model.MatchScore_textBox.Text = ReadContent;
        }

        public void BLL_SaveProjectInfo()
        {
            string WriteString = Model.MatchScore_textBox.Text;
            CutMachineDal.IniFile.WriteString("Proram", "MatchScore", WriteString, Model.m_szProjectIniPath);

            MessageBox.Show("Save Success");
        }

        public void BLL_Play()
        {

            CutMachineDal.CameraObject.OpenCamera();
  
            if (Model.CameraFound == false)
                return;
         
                //打开相机
                if (!Model.TriggerMode)
                {
                        
                    CutMachineDal.TxtFile.DisplayLog("打开相机,当前工作模式为连续采集\n", Model.UiLog_textBox);
                    
                    
                    Thread.Sleep(200);
               
                }
      

        }

        public void BLL_Test()
        {

        }

        public void BLL_Stop()
        {
   
               
                CutMachineDal.CameraObject.CloseCamera();
                 CutMachineDal.TxtFile.DisplayLog("停止采集图像\n", Model.UiLog_textBox);


        }

        public void BLL_Exit()
        {
       

                CutMachineDal.CameraObject.CloseCamera();

                
        
                CutMachineDal.TxtFile.DisplayLog("停止采集图像\n", Model.UiLog_textBox);


            CutMachineDal.MotionObject.CloseMotionCard();

        }

        public void BLL_DisplayLog(string Message)
        {
            CutMachineDal.TxtFile.DisplayLog(Message+"\n", Model.UiLog_textBox);
        }

        public DAL  BLL_GetDalLayer()
        {
            return CutMachineDal;
        }

        public void CreateMainImageWindow()
        {

            //主界面视频显示区
            if (Model.MainUI_Camera_WindowID != 0)
            {
                HOperatorSet.CloseWindow(Model.MainUI_Camera_WindowID);
            }

            long m_lWindowRow = 0;
            long m_lWindowColumn = 0;
            HTuple Farther_windowHandle = Model.VideoWindow_pictureBox.Handle;
            HOperatorSet.SetWindowAttr("background_color", "blue");
            HOperatorSet.OpenWindow(m_lWindowRow, m_lWindowColumn, (HTuple)Model.VideoWindow_pictureBox.Width, (HTuple)Model.VideoWindow_pictureBox.Height, Farther_windowHandle, "visible", "", out Model.MainUI_Camera_WindowID);
            HOperatorSet.SetPart(Model.MainUI_Camera_WindowID, 0, 0, Model.VideoWindow_pictureBox.Height + int.Parse(Model.WindowsHeightOffset), Model.VideoWindow_pictureBox.Width + int.Parse(Model.WindowsWidthOffset));
                
        }
     
       
    }//类结束

}//命名空间结束
