using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMachineModel;

namespace AutoMachineDAL
{
    public class DAL
    {
        
        //模块操作操作
        public INIClass             IniFile;

        //TXT文件操作
        public TxTClass             TxtFile;

        //XML文件操作
        public XmlClass             XmlFile;

        //Excel文件操作
        public ExcelClass           ExcelFile;

        //文件夹操作
        public FolderClass          FolderObject;

        //数据库
        //  public AccessDatabaseClass AccessDatabaseObject;

        //相机操作
        public CameraAbstractClass  CameraObject;

        //图像处理类
        public ImageProcess         ImageProcessObject;

        //运动控制
        public MotionAbstractClass  MotionObject;

        public DAL()
        {

            IniFile                 =   new INIClass();
            
            TxtFile                 =   new TxTClass();

            XmlFile                 =   new XmlClass();

            ExcelFile               =   new ExcelClass();

            FolderObject            =   new FolderClass();

            ImageProcessObject      =   new ImageProcess();

            CameraObject            =   new Hikvision_AreaCamera_Gige();

            MotionObject            =   new LeadShineMotionCard();

            //AccessDatabaseObject    =   new AccessDatabaseClass();
        }
        



    }
}
