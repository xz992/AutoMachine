using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using MvCamCtrl.NET;


namespace AutoMachineModel
{
    public class Model
    {
        //公共部分
        public  static string       m_szProjectName             = "";
        public  static string       m_szCustomerName            = "";
        public  static string       m_szCustomerPath            = "";
        public  static string       m_szProjectPath             = "";
        public  static string       m_szProjectIniName          = "";
        public  static string       m_szProjectIniPath          = "";
        public  static string       m_szProjectXmlName          = "";
        public  static string       m_szProjectXmlPath          = "";
        public  static string       m_szSystemIniName           = "";
        public  static string       m_szSystemIniPath           = "";
        public  static string       m_szTestImagePath           = "";
        public  static string       m_szModelImagePath          = "";
        public  static string       m_szRoiTableName            = "";
        public  static string       m_szMeasureTableName        = "";
        public  static string       m_szProjectDatabaseName     = "";
        public  static string       m_szProjectDatabasePath     = "";
        public  static string       m_szProjectListTableName    = "";
        public  static string       m_szProjectListDatabasePath = "";
        public  static string       m_szAppPath                 = "";
        public  static string       m_szAutoMachineName         = "";






        public static string        WindowsHeightOffset = "";
        public static string        WindowsWidthOffset = "";
        public static string        ImageWidth = "";
        public static string        ImageHeight = "";
        public static string        ExposureTimeAbs = "";
        
        public static string        m_ProgramStatus = "";//程式操作状态 1:新建程式  ;2:打开程式;  3:修改程式;   4:删除程式
        
        public  static HObject      RawImage                    = null;
        public  static HTuple       MainUI_Camera_WindowID      = 0;
        public  static PictureBox   VideoWindow_pictureBox      = new PictureBox();     
        public  static TextBox      UiLog_textBox               = new TextBox();
                
        public  static bool         SaveImageFlag               = true;
        public  static bool         StartTestFlag               = false;


      
        /// <summary>
        /// Camera Related
        /// </summary>

      
        public static bool          CameraFound                 = false;

        public static bool TriggerMode = false;                 //TriggerMode=true触发采集;TriggerMode=flase 连续采集;

        public static bool          TriggerSource = true;       //TriggerSource=true:软件触发;TriggerSource=flase:硬件触发;
        public  static bool         TestMode                    = false;



        public static double dTimeStart                         = 0.0;
        public static double dTimeEnd                           = 0.0;
        public static double dTimeElapsed                       = 0.0;

        
        public  const int XCH = 0;
        public  const int YCH = 1;
        public  const int ZCH = 2;
        public  const int UCH = 3;

        public static int nAxis = 0;

        //线扫测试机
        public  static HObject[]    RawImageSequence            = new HObject[8];
        public  static double[]     AcquisionImageTime          = new double[8];
        public  static int          AccessImageIndex            =  2;
 



        //模切机
        public static TextBox       MatchScore_textBox          = new TextBox();
    }
}
