using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using HalconDotNet;
using AutoMachineBLL;
using AutoMachineModel;
using AutoMachineDAL;

namespace AutoMachine
{
    public partial class UI_Form : Form
    {
        //业务逻辑层
        CutMachineBLL bll = new AutoMachineBLL.CutMachineBLL();
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //本框架采用三层架构,UI层,业务逻辑层,数据访问层和在三层之间数据传输的实体层.UI层和数据访问层为所有项目共用,新项目只需按业务逻辑层重构即可实现快速开发.//
        //调用方向:UI--->BLL-->DAL  ;数据返回方向:DAL-->BLL-->UI;                                                                                             //
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public UI_Form()
        {
            InitializeComponent();
            //系统参数初始化
            SystemParameter_Init();

            //实体层的主UI初始化
            ModelLayer_MainUI_Init();

            //实体层的程式制作UI初始化
            ModelLayer_ProgrameMakeUI_Init();

            //UI层主UI初始化
            UiLayer_UI_Init();

            //业务逻辑层初始化
            bll.BLL_Init();

        }
        //UI初始化
        //系统参数初始化
        public void SystemParameter_Init()
        {
            //true:软触发;false:硬触发
            Model.m_bTriggerMode = 0;
            //true:存图;false:不存图
            Model.SaveImageFlag = false;
            //设备项目名称
            Model.m_szAutoMachineName = "CutMachine";
            //应用程序路径
            Model.m_szAppPath = Directory.GetCurrentDirectory();
            //系统文件路径
            Model.m_szSystemIniPath = Directory.GetCurrentDirectory() + "/Setting.ini";

            bll.BLL_GetDalLayer().IniFile.WriteString(Model.m_szAutoMachineName + "_AccessControl", "AccessEnable", "NO", Model.m_szSystemIniPath);

            Model.ExposureTimeAbs = this.ExposureTime_numericUpDown.Value.ToString();

            this.bnStopTest.Enabled = false;

        }

        //实体层的主UI初始化
        public void ModelLayer_MainUI_Init()
        {
            Model.MainUI_Camera_WindowID = 0;
            Model.UiLog_textBox = this.UiLog_textBox;
            Model.VideoWindow_pictureBox = this.VideoWindow_pictureBox;
        }

        //实体层的程式制作UI初始化
        public void ModelLayer_ProgrameMakeUI_Init()
        {
            //Model.MatchScore_textBox = this.MatchScore_textBox;









        }

        //UI层UI初始化
        public void UiLayer_UI_Init()
        {
            //调整分辨率
            AutoAdjustFormSize();
            bll.BLL_DisplayLog("自动调整分辨率完成");
            bll.BLL_DisplayLog("动画加载完成");
            //加载公司信息
            LoadCompanyInformation();
            bll.BLL_DisplayLog("加载公司信息");
            //创建视频显示主窗口
            CreateMainImageWindow();
            bll.BLL_DisplayLog("创建主窗口");

            //允许跨线程访问界面控件
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            HOperatorSet.SetSystem("use_window_thread", "true");

            //开启定时器刷行UI
            this.timer.Interval = 500;
            this.timer.Enabled = true;

        }

        //分辨率类
        public class AutoReSizeForm
        {
            public static void SetFormSize(Control fm, float SW, float SH, float FontSize)
            {

                fm.Location = new Point((int)(fm.Location.X * SW), (int)(fm.Location.Y * SH));
                fm.Size = new Size((int)(fm.Size.Width * SW), (int)(fm.Size.Height * SH));
                fm.Font = new Font(fm.Font.Name, fm.Font.Size * FontSize, fm.Font.Style, fm.Font.Unit, fm.Font.GdiCharSet, fm.Font.GdiVerticalFont);
                if (fm.Controls.Count != 0)
                {
                    SetControlSize(fm, SW, SH, FontSize);
                }
            }
            
            private static void SetControlSize(Control InitC, float SW, float SH, float FontSize)
            {

                foreach (Control c in InitC.Controls)
                {
                    c.Location = new Point((int)(c.Location.X * SW), (int)(c.Location.Y * SH));
                    c.Size = new Size((int)(c.Size.Width * SW), (int)(c.Size.Height * SH));
                    c.Font = new Font(c.Font.Name, c.Font.Size * FontSize, c.Font.Style, c.Font.Unit, c.Font.GdiCharSet, c.Font.GdiVerticalFont);
                    if (c.Controls.Count != 0)
                    {
                        SetControlSize(c, SW, SH, FontSize);
                    }
                }
            }

        }

        //自动调整窗口大小
        public void AutoAdjustFormSize()
        {
            Screen screen = Screen.PrimaryScreen;
            int ScreenWidth = screen.Bounds.Width;
            int ScreenHeight = screen.Bounds.Height;

            if (ScreenWidth == 1920 && ScreenHeight == 1080)
            {
                AutoReSizeForm.SetFormSize(this, 1.18F, 1.34F, 1.05F);
            }

            if (ScreenWidth == 1680 && ScreenHeight == 1050)
            {
                AutoReSizeForm.SetFormSize(this, 1.06F, 1.25F, 1.05F);
            }

            if (ScreenWidth == 1600 && ScreenHeight == 1024)
            {
                AutoReSizeForm.SetFormSize(this, 0.98F, 1.28F, 1.05F);
            }

            if (ScreenWidth == 1600 && ScreenHeight == 900)
            {
                AutoReSizeForm.SetFormSize(this, 0.98F, 1.1F, 1.05F);
            }

            if (ScreenWidth == 1440 && ScreenHeight == 900)
            {
                AutoReSizeForm.SetFormSize(this, 0.88F, 1.1F, 1.05F);
            }

            if (ScreenWidth == 1366 && ScreenHeight == 768)
            {
                AutoReSizeForm.SetFormSize(this, 0.84F, 0.9F, 1.05F);
            }

            if (ScreenWidth == 1360 && ScreenHeight == 768)
            {
                AutoReSizeForm.SetFormSize(this, 0.84F, 0.9F, 1.05F);
            }

            if (ScreenWidth == 1280 && ScreenHeight == 1024)
            {
                AutoReSizeForm.SetFormSize(this, 0.79F, 1.25F, 1.05F);
            }

            if (ScreenWidth == 1280 && ScreenHeight == 960)
            {
                AutoReSizeForm.SetFormSize(this, 0.78F, 1.18F, 1.05F);
            }

            if (ScreenWidth == 1280 && ScreenHeight == 800)
            {
                AutoReSizeForm.SetFormSize(this, 0.78F, 0.96F, 1.05F);
            }

            if (ScreenWidth == 1280 && ScreenHeight == 768)
            {
                AutoReSizeForm.SetFormSize(this, 0.78F, 0.92F, 1.05F);
            }

            if (ScreenWidth == 1280 && ScreenHeight == 720)
            {
                AutoReSizeForm.SetFormSize(this, 0.78F, 0.85F, 1.05F);
            }

            if (ScreenWidth == 1280 && ScreenHeight == 600)
            {
                AutoReSizeForm.SetFormSize(this, 0.78F, 0.7F, 1.05F);
            }

            if (ScreenWidth == 1152 && ScreenHeight == 864)
            {
                AutoReSizeForm.SetFormSize(this, 0.70F, 1.05F, 1.05F);
            }

            if (ScreenWidth == 1024 && ScreenHeight == 768)
            {
                AutoReSizeForm.SetFormSize(this, 0.63F, 0.92F, 1.05F);
            }

            if (ScreenWidth == 800 && ScreenHeight == 600)
            {
                AutoReSizeForm.SetFormSize(this, 0.5F, 0.68F, 1.05F);
            }

        }
        //加载状态栏信息
        public void LoadCompanyInformation()
        {
            CompanyName_toolStripStatusLabel.Text = "公司名称:  浙江大学台州研究院机电研究所";
            CustomerName_toolStripStatusLabel.Text = "当前用户:  客户";
            CurrentDate_toolStripStatusLabel.Text = "当前日期:" + System.DateTime.Now.ToString("yyyy年MM月dd日");
        }
        //主界面
        #region
        //视频播放
        private void PlayVideo(object sender, EventArgs e)
        {
            bll.BLL_Play();

            if (Model.m_bGrabbing)
            {
                this.bnPlayVideo.Enabled = false;
                this.bnStopTest.Enabled = true;
            }
      
        }

        //停止采集
        private void Stop(object sender, EventArgs e)
        {
            bll.BLL_Stop();

            if (!Model.m_bGrabbing)
            {
                this.bnPlayVideo.Enabled = true;
                this.bnStopTest.Enabled = false;
            }


        }

        //软件退出
        private void Exit(object sender, EventArgs e)
        {
            bll.BLL_GetDalLayer().IniFile.WriteString(Model.m_szAutoMachineName + "_AccessControl", "AccessEnable", "NO", Directory.GetCurrentDirectory() + "/Setting.ini");
            bll.BLL_Exit();
            //关闭定时器
            this.timer.Enabled = false;
            this.Close();
        }
 
        //创建主界面窗口
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
        
            HOperatorSet.SetWindowAttr("background_color", "black");
            HOperatorSet.OpenWindow(m_lWindowRow, m_lWindowColumn, (HTuple)Model.VideoWindow_pictureBox.Width, (HTuple)Model.VideoWindow_pictureBox.Height, Farther_windowHandle, "visible", "", out Model.MainUI_Camera_WindowID);
            HOperatorSet.SetPart(Model.MainUI_Camera_WindowID, 
                                                            0, 
                                                            0, 
                                Model.VideoWindow_pictureBox.Height , 
                                Model.VideoWindow_pictureBox.Width 
                                );

        }

        //动态更新UI
        private void DynamicUI(object sender, EventArgs e)
        {

            string ReadContent = bll.BLL_GetDalLayer().IniFile.ReadString(Model.m_szAutoMachineName + "_AccessControl", "AccessEnable", "", Directory.GetCurrentDirectory() + "/Setting.ini");
   
            if (Model.m_ProgramStatus == "2")//打开
            {

                this.ProductProject_textBox.Text = Model.m_szProjectName;

                bll.BLL_LoadProjectInfo();

                if (Model.m_szProjectDatabasePath == string.Empty)
                {
                    MessageBox.Show("加载程式ROI坐标失败,请先打开机种!");
                    return;
                }

            }

            if (Model.m_ProgramStatus == "3")//修改
            {

                this.ProductProject_textBox.Text = Model.m_szProjectName;

                bll.BLL_LoadProjectInfo();

                if (Model.m_szProjectDatabasePath == string.Empty)
                {
                    MessageBox.Show("加载程式ROI坐标失败,请先打开机种!");
                    return;
                }

            }


            Model.m_ProgramStatus = "-1";


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            label_position.Text = "状态：" + Convert.ToString(Dmc1000_Dll.d1000_get_axis_status(Model.XCH));
            label_position.Text = label_position.Text + "," + Convert.ToString(Dmc1000_Dll.d1000_get_axis_status(Model.YCH));
            label_position.Text = label_position.Text + "," + Convert.ToString(Dmc1000_Dll.d1000_get_axis_status(Model.ZCH));
            label_position.Text = label_position.Text + "," + Convert.ToString(Dmc1000_Dll.d1000_get_axis_status(Model.UCH));
            
            label_position.Text = label_position.Text + " 位置：" + Convert.ToString(Dmc1000_Dll.d1000_get_command_pos(Model.XCH));
            label_position.Text = label_position.Text + "," + Convert.ToString(Dmc1000_Dll.d1000_get_command_pos(Model.YCH));
            label_position.Text = label_position.Text + "," + Convert.ToString(Dmc1000_Dll.d1000_get_command_pos(Model.ZCH));
            label_position.Text = label_position.Text + "," + Convert.ToString(Dmc1000_Dll.d1000_get_command_pos(Model.UCH));

            label_input.Text =    "IN1：" + Convert.ToString(Dmc1000_Dll.d1000_in_bit(1)) + 
                                ", IN2：" + Convert.ToString(Dmc1000_Dll.d1000_in_bit(2)) + 
                                ", IN3：" + Convert.ToString(Dmc1000_Dll.d1000_in_bit(3)) + 
                                ", IN4：" + Convert.ToString(Dmc1000_Dll.d1000_in_bit(4));

            
            //光标处的图像坐标
            Point LocalMousePosition = new Point();
            Point ZoomLocalMousePosition = new Point();
            LocalMousePosition = VideoWindow_pictureBox.PointToClient(Cursor.Position);
            //  toolStripStatusLabel1.Text = "X=" + LocalMousePosition.X + "," + "Y=" + LocalMousePosition.Y;
            ZoomLocalMousePosition = TranslateZoomMousePosition(LocalMousePosition);
            ImgPosition_toolStripStatusLabel.Text = "图像坐标: ( " + ZoomLocalMousePosition.Y + " , " + ZoomLocalMousePosition.X + " )";
        }

        //光标处的图像坐标，有缩放情形
        private Point TranslateZoomMousePosition(Point coordinates)
        {
            HObject Image = Model.RawImage;
            HTuple hv_Width, hv_Height;
            HTuple Width = VideoWindow_pictureBox.Width;
            HTuple Height = VideoWindow_pictureBox.Height;
            // test to make sure our image is not null
            if (Image == null) return coordinates;
            // Make sure our control width and height are not 0 and our 
            // image width and height are not 0
            
            

            HOperatorSet.GetImageSize(Model.RawImage, out hv_Width, out hv_Height);
            if (Width == 0 || Height == 0 || hv_Width == 0 || hv_Height == 0) return coordinates;
            // This is the one that gets a little tricky. Essentially, need to check 
            // the aspect ratio of the image to the aspect ratio of the control
            // to determine how it is being rendered
            HTuple imageAspect = hv_Width.D / hv_Height.D;

            HTuple controlAspect = Width.D / Height.D;
            HTuple newX = coordinates.X;
            HTuple newY = coordinates.Y;
            if (imageAspect > controlAspect)
            {
                // This means that we are limited by width, 
                // meaning the image fills up the entire control from left to right
                HTuple ratioWidth = hv_Width.D / Width.D;
                newX *= ratioWidth.D;
                HTuple scale = Width.D / hv_Width.D;
                HTuple displayHeight = scale.D * hv_Height.D;
                HTuple diffHeight = Height.D - displayHeight.D;
                diffHeight.D /= 2;
                newY -= diffHeight.D;
                // newY /= scale;
                newY = newY.D / scale.D;
                ////指定图片的显示部分
                //                Model.topleft_x = diffHeight.D;
                //                Model.topleft_y = 0;
                //                Model.bottomright_x = diffHeight.D + displayHeight;
                //                Model.bottomright_y = Width.D;

            }
            else
            {
                // This means that we are limited by height, 
                // meaning the image fills up the entire control from top to bottom
                HTuple ratioHeight = hv_Height.D / Height.D;
                newY *= ratioHeight;
                HTuple scale = Height.D / hv_Height.D;
                HTuple displayWidth = scale * hv_Width.D;
                HTuple diffWidth = Width - displayWidth.D;
                diffWidth.D /= 2;
                newX -= diffWidth.D;
                //newX /= scale;
                newX = newX / scale.D;

                //Model.topleft_x = 0;
                //Model.topleft_y = diffWidth;
                //Model.bottomright_x = Height;
                //Model.bottomright_y = diffWidth + displayWidth;
            }
            int i_temp, j_temp;
            i_temp = (int)newX.D;
            j_temp = (int)newY.D;
            return new Point(i_temp, j_temp);
        }



        //程序制作TAB保存
        private void ProgramMakeSave(object sender, EventArgs e)
        {
            bll.BLL_SaveProjectInfo();
        }
        #endregion

        //图像增强TAB
        //打开离线图像
        private void OpenImageDialog(object sender, EventArgs e)
        {
            this.OpenImageDialog_button.Enabled = false;


            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //VideoWindow_pictureBox.Image = Image.FromFile(this.openFileDialog.FileName);
                HTuple hv_Width = null, hv_Height = null;

                string OfflineImagePath = this.openFileDialog.FileName;

                HOperatorSet.ReadImage(out Model.RawImage, OfflineImagePath);

                HOperatorSet.SetPart(Model.MainUI_Camera_WindowID, 0, 0, this.VideoWindow_pictureBox.Height, this.VideoWindow_pictureBox.Width);

                HOperatorSet.DispObj(Model.RawImage, Model.MainUI_Camera_WindowID);


                HOperatorSet.GetImageSize(Model.RawImage, out hv_Width, out hv_Height);

                Model.ImageWidth = hv_Width[0].I.ToString();

                Model.ImageHeight = hv_Height[0].I.ToString();

         
                this.ImagePath_textBox.Text = this.openFileDialog.FileName;
            }

            this.OpenImageDialog_button.Enabled = true;
        }
        //工业相机TAB
        //相机TAB保存
        private void CameraSave(object sender, EventArgs e)
        {
      

            Model.ExposureTimeAbs = this.ExposureTime_numericUpDown.Value.ToString();

            bll.BLL_GetDalLayer().IniFile.WriteString(Model.m_szAutoMachineName + "_Camera", "ImageWidth", Model.ImageWidth, Model.m_szSystemIniPath);

            bll.BLL_GetDalLayer().IniFile.WriteString(Model.m_szAutoMachineName + "_Camera", "ImageHeight", Model.ImageHeight, Model.m_szSystemIniPath);

            bll.BLL_GetDalLayer().IniFile.WriteString(Model.m_szAutoMachineName + "_Camera", "WindowsWidthOffset", Model.WindowsWidthOffset, Model.m_szSystemIniPath);

            bll.BLL_GetDalLayer().IniFile.WriteString(Model.m_szAutoMachineName + "_Camera", "WindowsHeightOffset", Model.WindowsHeightOffset, Model.m_szSystemIniPath);

            bll.BLL_GetDalLayer().IniFile.WriteString(Model.m_szAutoMachineName + "_Camera", "ExposureTimeAbs", Model.ExposureTimeAbs, Model.m_szSystemIniPath);
            
            MessageBox.Show("保存成功");
        }

        private void StartTest_button_Click(object sender, EventArgs e)
        {

        }

        private void Snap_button_Click(object sender, EventArgs e)
        {
    
        }
        
        #region 运动控制卡手动控制

        private void button_start_Click(object sender, EventArgs e)
        {

            UpdateControl();

            if (Dmc1000_Dll.d1000_check_done(Model.nAxis) == 0) return;//若在运行，则返回

            Dmc1000_Dll.d1000_set_sd(Model.nAxis, checkBoxSD.Checked ? 1 : 0);

            int nDir = checkBoxDir.Checked ? 1 : -1;

            int nStart = Convert.ToInt16(textBoxOSpd.Text);
            int nMSpeed = Convert.ToInt16(textBoxMSpd.Text);
            double nTAcc = Convert.ToDouble(textBoxTAcc.Text);
            int nPulse = Convert.ToInt16(textBoxPulse.Text);

            if (checkBoxPMode.Checked)//脉冲+方向
            {
                Dmc1000_Dll.d1000_set_pls_outmode(Model.nAxis, checkBoxPLog.Checked ? 1 : 0);//1-pulse/dir 模式，脉冲下降沿有效;0-pulse/dir 模式，脉冲上升沿有效
            }
            else//脉冲+脉冲
            {
                Dmc1000_Dll.d1000_set_pls_outmode(Model.nAxis, checkBoxPLog.Checked ? 3 : 2);//3-CW/CCW 模式，脉冲下降沿有效;2-CW/CCW 模式，脉冲上升沿有效
            }

            if (radioButtonM.Checked)//定长运动
            {
                if (radioButtonS.Checked)
                {
                    Dmc1000_Dll.d1000_start_s_move(Model.nAxis, nPulse * nDir, nStart, nMSpeed, nTAcc);//S形
                }
                else if (radioButtonT.Checked)
                {
                    Dmc1000_Dll.d1000_start_t_move(Model.nAxis, nPulse * nDir, nStart, nMSpeed, nTAcc);//T形
                }
            }



        }

        public void UpdateControl()
        {
            checkBoxPMode.Text  = (checkBoxPMode.Checked    ? "脉冲+方向"          : "脉冲+脉冲");
            checkBoxPLog.Text   = (checkBoxPLog.Checked     ? "脉冲下降沿有效"     : "脉冲上升沿有效");
            checkBoxSD.Text     = (checkBoxSD.Checked       ? "减速：使能"         : "减速：禁止");
            checkBoxDir.Text    = (checkBoxDir.Checked      ? "驱动方向：正"       : "驱动方向：负");

            checkBoxOut1.Text = (checkBoxOut1.Checked ? "OUT1：高电平" : "OUT1：低电平");
            Dmc1000_Dll.d1000_out_bit(1, checkBoxOut1.Checked ? 1 : 0);

            checkBoxOut2.Text = (checkBoxOut2.Checked ? "OUT2：高电平" : "OUT2：低电平");
            Dmc1000_Dll.d1000_out_bit(2, checkBoxOut2.Checked ? 1 : 0);

            checkBoxOut3.Text = (checkBoxOut3.Checked ? "OUT3：高电平" : "OUT3：低电平");
            Dmc1000_Dll.d1000_out_bit(3, checkBoxOut3.Checked ? 1 : 0);

            checkBoxOut4.Text = (checkBoxOut4.Checked ? "OUT4：高电平" : "OUT4：低电平");
            Dmc1000_Dll.d1000_out_bit(4, checkBoxOut4.Checked ? 1 : 0);
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
               for( int i=0; i<4; i++)
                Dmc1000_Dll.d1000_set_command_pos(i, 0);
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            if (Dmc1000_Dll.d1000_check_done(Model.XCH) == 0 //在运行
             || Dmc1000_Dll.d1000_check_done(Model.YCH) == 0
             || Dmc1000_Dll.d1000_check_done(Model.ZCH) == 0
             || Dmc1000_Dll.d1000_check_done(Model.UCH) == 0)
            {//当减速时间为0时，为急停效果
                Dmc1000_Dll.d1000_decel_stop(Model.XCH);
                Dmc1000_Dll.d1000_decel_stop(Model.YCH);
                Dmc1000_Dll.d1000_decel_stop(Model.ZCH);
                Dmc1000_Dll.d1000_decel_stop(Model.UCH);
                return;
            }
        }

        private void radioButtonX_CheckedChanged(object sender, EventArgs e)
        {
            Model.nAxis = Model.XCH;
        }

        private void radioButtonY_CheckedChanged(object sender, EventArgs e)
        {
            Model.nAxis = Model.YCH;
        }

        private void radioButtonZ_CheckedChanged(object sender, EventArgs e)
        {
             Model.nAxis = Model.ZCH;
        }

        private void radioButtonU_CheckedChanged(object sender, EventArgs e)
        {
             Model.nAxis = Model.UCH;
        }

        private void checkBoxPMode_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControl();
        }

        private void checkBoxPLog_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControl();
        }

        private void checkBoxSD_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControl();
        }

        private void checkBoxDir_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControl();
        }

        private void checkBoxOut1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControl();
        }

        private void checkBoxOut3_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControl();
        }

        private void checkBoxOut2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControl();
        }

        private void checkBoxOut4_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControl();
        }
        #endregion

        private double[] string2point(string ImgP2)
        {
            //string ImgP2 = this.xy_imag_2.Text;

            string[] ImgP2values = ImgP2.Split(',');

            double[] ImgP2values_double = new double[2];

            //   double[] ImagP = new double[4];
            for (int i = 0; i < ImgP2values.Length; i++)
            {
                ImgP2values[i] = ImgP2values[i].Trim();
                ImgP2values_double[i] = Convert.ToDouble(ImgP2values[i]);

            }
            return ImgP2values_double;
        }
        
        private void Calib_button_Click(object sender, EventArgs e)
        {

          
            
            HTuple hv_x = new HTuple(), hv_y = new HTuple(), hv_x_mm = new HTuple();
            HTuple hv_y_mm = new HTuple(), hv_HomMat2D = new HTuple();

            //count valid pairs  validPairs

            int validPairs1 = 0, validPairs2 = 0, validPairs = 0;
            int min = 9;
            foreach (TextBox tb in this.Cali_tabPage.Controls.OfType<TextBox>())
            {
                if (tb is TextBox && tb.Text == string.Empty && tb.TabIndex < min)
                    min = tb.TabIndex;

            }
            foreach (TextBox tb in this.Cali_tabPage.Controls.OfType<TextBox>())
            {
                if (tb is TextBox && tb.TabIndex == min)
                {
                    if (tb.Text == string.Empty)
                    {
                        validPairs1 = min - 1;
                        break;
                    }
                    else
                    {
                        validPairs1 = min;
                        break;
                    }
                }
            }

            int min2 = 19;
            foreach (TextBox tb in this.Cali_tabPage.Controls.OfType<TextBox>())
            {
                if (tb is TextBox && tb.Text == string.Empty && tb.TabIndex < min2 && tb.TabIndex > 10)
                    min2 = tb.TabIndex;

            }
            foreach (TextBox tb in this.Cali_tabPage.Controls.OfType<TextBox>())
            {
                if (tb is TextBox && tb.TabIndex == min2)
                {
                    if (tb.Text == string.Empty)
                    {
                        validPairs2 = min2 - 1;
                        break;
                    }
                    else
                    {
                        validPairs2 = min2;
                        break;
                    }
                }
            }
            if (validPairs1 - (validPairs2 - 10) > 0)
            {
                validPairs = validPairs2 - 10;
            }
            else
            {
                validPairs = validPairs1;
            }


            foreach (TextBox tb in this.Cali_tabPage.Controls.OfType<TextBox>())
            {
                for (int i = 0; i < validPairs; i++)
                {
                    if (tb.TabIndex == i + 1)
                    {
                        hv_x[i] = string2point(tb.Text)[0];
                        hv_y[i] = string2point(tb.Text)[1];

                        tb.BackColor = Color.Green;
                    }


                    if (tb.TabIndex == i + 1 + 10)
                    {
                        hv_x_mm[i] = string2point(tb.Text)[0];
                        hv_y_mm[i] = string2point(tb.Text)[1];
                        tb.BackColor = Color.Green;
                    }

                }

            }


            HOperatorSet.VectorToSimilarity(hv_x, hv_y, hv_x_mm, hv_y_mm, out hv_HomMat2D);

            MessageBox.Show(hv_HomMat2D.ToString());

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("test");
        }

        private void VideoWindow_pictureBox_Click(object sender, EventArgs e)
        {
            MessageBox.Show("test");
        }

        private void bnGetParam_Click(object sender, EventArgs e)
        {
            float fExposure = 0;
            bll.BLL_GetDalLayer().CameraObject.m_pOperator.GetFloatValue("ExposureTime",ref fExposure);
            tbExposure.Text = fExposure.ToString("F1");

            float fGain = 0;
            bll.BLL_GetDalLayer().CameraObject.m_pOperator.GetFloatValue("Gain", ref fGain);
            tbGain.Text = fGain.ToString("F1");


            float fTriggerDelay = 0;
            bll.BLL_GetDalLayer().CameraObject.m_pOperator.GetFloatValue("TriggerDelay", ref fTriggerDelay);
            tbTriggerdelay.Text = fTriggerDelay.ToString("F1");

        }

        private void Result_listView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
