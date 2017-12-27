using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DALSA.SaperaLT.SapClassBasic;
using DALSA.SaperaLT.Examples.NET.Utils;
using HalconDotNet;
using AutoMachineModel;
using AutoMachineDAL;

namespace AutoMachineDAL
{
    public class Dasla_LinerCamera_CameraLink_Winform:CameraAbstractClass
    {

        private             MyAcquisitionParams acqParams           = null;
        private             SapAcquisition      m_Acquisition       =null;
        private             SapAcqToBuf         m_Xfer              =null;
        private             SapLocation         m_ServerLocation    =null;

static  private             SapBuffer           m_Buffers;
static  private             int                 AcquisionImageCount;
static  private             bool                firstFrame;
static  private             double              start;
static  private             double              end;
static  private             double              duration;



        public Dasla_LinerCamera_CameraLink_Winform()
        {

            HOperatorSet.GenEmptyObj(out ho_Rectangle);

            HOperatorSet.GenEmptyObj(out ho_ImageReduced);

            HOperatorSet.GenEmptyObj(out ho_TotalImage);

            HOperatorSet.GenEmptyObj(out ho_TiledImage);

            HOperatorSet.GenEmptyObj(out ho_Region);

            HOperatorSet.GenEmptyObj(out ho_CopyImage);

            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);

            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);

            for (int i = 0; i < 4; i++)
            {
                Model.RawImageSequence[i] = null;

            }


            m_Buffers = new SapBuffer();


        }


        /**********************************************软触发模式开始***************************************************************/


        //打开相机
        public  override void OpenCameraSoftTrigger(string DCF_Name)
        {

        }


        //连续采集图像
        public  override void ContinuousAcquisitonSoftTrigger()
        {

        }


        //单张采集图像
        public  override void SnapAcquisitionSoftTrigger(ref byte[] ImageBufferPtr)
        {

        }


        //停止采集图像
        public  override void StopCameraSoftTrigger()
        {

        }


        //关闭相机
        public  override void CloseCameraSoftTrigger()
        {

        }


        /**********************************************软触发模式结束***************************************************************/



        /**********************************************硬触发模式开始***************************************************************/
        private static HObject ho_TotalImage;
        private static HObject ho_TiledImage;
        private static HObject ho_Rectangle;
        private static HObject ho_ConnectedRegions;
        private static HObject ho_SelectedRegions;
        private static HObject ho_ImageReduced;
        private static HObject ho_Region;
        private static HObject ho_CopyImage;
        private static HTuple  hv_Area;
        private static HTuple  hv_Row;
        private static HTuple  hv_Column;
        private static HTuple  hv_TempIndex;
        private static HTuple  hv_ImageIndex;
        private static HTuple  hv_flag;
        private static HTuple  hv_ImageCount;
        private static HTuple  hv_SaveImageCount;


        //打开相机
        public  override void OpenCameraHardTrigger(string DCF_Name)
        {

            if (m_Xfer == null)
            {
                ExampleUtils.configPath = DCF_Name;

                string[] args = null;
                if (!GetOptions(args, acqParams))
                {
                    MessageBox.Show("加载相机CCF文件失败!");
                    return;
                }

                m_ServerLocation = new SapLocation(acqParams.ServerName, acqParams.ResourceIndex);

                CreateNewObjects(m_ServerLocation, acqParams.ConfigFileName);

                if (!CreateObjects())
                {
                    DisposeObjects();
                    return;
                }

            }
            else
            {
                 //相机连接着
            }


        }


        //信号触发采集图像
        public  override void ContinuousAcquisitonHardTrigger()
        {
            m_Xfer.Grab();
        }


        //停止采集图像
        public  override void StopCameraHardTrigger()
        {
            m_Xfer.Freeze();
            m_Xfer.Wait(1000);
            m_Xfer.Abort();

        }


        //关闭相机
        public  override void CloseCameraHardTrigger()
        {
            DestroyObjects();

        }


        //允许硬件触发
        public  override void EnableHardTrigger()
        {
            m_Acquisition.EnableEvent(SapAcquisition.AcqEventType.StartOfFrame);
        }


        //禁止硬件触发
        public  override void DisableHardTrigger()
        {
            m_Acquisition.DisableEvent();
        }


        /**********************************************硬触发模式结束***************************************************************/


        static bool GetOptions(string[] args, MyAcquisitionParams acqParams)
        {

             return ExampleUtils.GetOptionsFromQuestions(acqParams);
        }


        public bool CreateNewObjects(SapLocation ServerLocation, string ConfigFile)
        {

            m_Acquisition = new SapAcquisition(m_ServerLocation, ConfigFile);
            if (SapBuffer.IsBufferTypeSupported(m_ServerLocation, SapBuffer.MemoryType.ScatterGather))
                m_Buffers = new SapBufferWithTrash(2, m_Acquisition, SapBuffer.MemoryType.ScatterGather);
            else
                m_Buffers = new SapBufferWithTrash(2, m_Acquisition, SapBuffer.MemoryType.ScatterGatherPhysical);
            m_Xfer = new SapAcqToBuf(m_Acquisition, m_Buffers);


            //event for view
            m_Xfer.Pairs[0].EventType = SapXferPair.XferEventType.EndOfFrame;
            m_Xfer.XferNotify += new SapXferNotifyHandler(xfer_XferNotify);

            return true;
        }


        private bool CreateObjects()
        {
            // Create acquisition object
            if (m_Acquisition != null )
            {
                if (m_Acquisition.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }
            }
            // Create buffer object
            if (m_Buffers != null )
            {
                if (m_Buffers.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }
                m_Buffers.Clear();
            }
            // Create Xfer object
            if (m_Xfer != null)
            {
                if (m_Xfer.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }
            }
            return true;
        }


        private void DisposeObjects()
        {
            if (m_Xfer != null)
            { m_Xfer.Dispose(); m_Xfer = null; }
            if (m_Buffers != null)
            { m_Buffers.Dispose(); m_Buffers = null; }
            if (m_Acquisition != null)
            { m_Acquisition.Dispose(); m_Acquisition = null; }

        }


        private void DestroyObjects()
        {
            if (m_Xfer != null && m_Xfer.Initialized)
                m_Xfer.Destroy();
            if (m_Buffers != null && m_Buffers.Initialized)
                m_Buffers.Destroy();
            if (m_Acquisition != null && m_Acquisition.Initialized)
                m_Acquisition.Destroy();
        }


        public void xfer_XferNotify(object sender, SapXferNotifyEventArgs argsNotify)
        {
            DateTime StartTime = DateTime.Now;


            //1.计算图像采集时间
            if (firstFrame)
            {
                firstFrame = false;
                start = DateTime.Now.Second + (float)DateTime.Now.Millisecond / 1000;
                return;
            }

            end = DateTime.Now.Second + (float)DateTime.Now.Millisecond / 1000;
            duration = end - start;
            start = end;
            Console.WriteLine("图像采集时间:" + duration.ToString() + "秒\n");
            Model.AcquisionImageTime[AcquisionImageCount] = duration;



            //2.图像缓存转化为halcon图像类型
            int SubImageWidth = 13384;
            int SubImageHeight = 100;//7635


            IntPtr dataAddress = IntPtr.Zero;
            bool Flag = m_Buffers.GetAddress(out dataAddress);
            if (!Flag)
            {
                MessageBox.Show("图像缓存获取失败!");
            }

            AutoMachineDAL.DAL Dal = new AutoMachineDAL.DAL();
            Model.RawImage = Dal.ImageProcessObject.ImagePtrToHobject(SubImageWidth, SubImageHeight, ref dataAddress);
            HOperatorSet.DispObj(Model.RawImage, Model.MainUI_Camera_WindowID);

            if (Model.SaveImageFlag)
            {
                SaveImage(Model.RawImage, true);

            }


            //CombinImage(CommonModuleClass.RawImage, SubImageWidth, SubImageHeight);


            //5.计算图像转换时间
            DateTime EndTime = DateTime.Now;
            TimeSpan ts = EndTime - StartTime;
            double difference = ts.TotalSeconds;  //秒单位
            Console.WriteLine("图像转换时间:" + difference.ToString() + "秒\n");
        }


        public  void CombinImage(HObject RawImage, int SubImageWidth, int SubImageHeight)
        {
            ho_Rectangle.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle, 0, 7436, 97, 7706);


            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(RawImage, ho_Rectangle, out ho_ImageReduced);


            ho_Region.Dispose();
            HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 35, 255);
            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);

            ho_SelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "rectangularity", "and", 0.9, 1);

            HOperatorSet.AreaCenter(ho_SelectedRegions, out hv_Area, out hv_Row, out hv_Column);

            // 判断图像开始位置
            if ((int)((new HTuple(hv_Area.TupleGreaterEqual(50))).TupleAnd(new HTuple(hv_Area.TupleLess(50000)))) != 0)
            {
                if ((int)(new HTuple(((hv_ImageIndex - hv_TempIndex)).TupleNotEqual(1))) != 0)
                {

                    hv_flag = hv_flag + 1;

                }

                hv_TempIndex = hv_ImageIndex.Clone();
            }

            //构建图像队列
            if ((int)((new HTuple(hv_flag.TupleEqual(1))).TupleOr(new HTuple(hv_flag.TupleEqual(2)))) != 0)
            {
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_TotalImage, RawImage, out ExpTmpOutVar_0);
                    ho_TotalImage.Dispose();
                    ho_TotalImage = ExpTmpOutVar_0;
                }
                hv_ImageCount = hv_ImageCount + 1;
            }

            //判断图像结束位置
            HTuple hv_StartRow = new HTuple(), hv_StartColumn = new HTuple();
            HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple(), hv_Row2 = new HTuple(), hv_Column2 = new HTuple();
            HTuple hv_j = new HTuple();

            if ((int)(new HTuple(hv_flag.TupleEqual(2))) != 0)
            {
                hv_flag = 1;

                HTuple end_val459 = hv_ImageCount - 1;
                HTuple step_val459 = 1;
                for (hv_j = 0; hv_j.Continue(end_val459, step_val459); hv_j = hv_j.TupleAdd(step_val459))
                {
                    if (hv_StartRow == null)
                        hv_StartRow = new HTuple();
                    hv_StartRow[hv_j] = SubImageHeight * hv_j;
                    if (hv_StartColumn == null)
                        hv_StartColumn = new HTuple();
                    hv_StartColumn[hv_j] = 0;
                    if (hv_Row1 == null)
                        hv_Row1 = new HTuple();
                    hv_Row1[hv_j] = -1;
                    if (hv_Column1 == null)
                        hv_Column1 = new HTuple();
                    hv_Column1[hv_j] = -1;
                    if (hv_Row2 == null)
                        hv_Row2 = new HTuple();
                    hv_Row2[hv_j] = -1;
                    if (hv_Column2 == null)
                        hv_Column2 = new HTuple();
                    hv_Column2[hv_j] = -1;
                }

                HOperatorSet.TileImagesOffset(ho_TotalImage, out ho_TiledImage, hv_StartRow, hv_StartColumn, hv_Row1, hv_Column1, hv_Row2, hv_Column2, SubImageWidth, SubImageHeight * hv_ImageCount);


                HOperatorSet.CopyImage(ho_TiledImage, out Model.RawImageSequence[AcquisionImageCount]);
                if (AcquisionImageCount == 0)
                {
                    Model.AccessImageIndex = 0;
                }
                else if (AcquisionImageCount == 1)
                {
                    Model.AccessImageIndex = 1;
                }
                else if (AcquisionImageCount == 2)
                {
                    Model.AccessImageIndex = 2;
                }
                else if (AcquisionImageCount == 3)
                {
                    Model.AccessImageIndex = 3;
                }

                //SaveImage(CommonModuleClass.RawImageSequence[AcquisionImageCount], false);




                hv_ImageCount = 1;
                ho_TiledImage.Dispose();
                ho_TotalImage.Dispose();
                HOperatorSet.CopyImage(RawImage, out ho_TotalImage);
                if (ho_CopyImage != null)
                {
                    HOperatorSet.ClearWindow(Model.MainUI_Camera_WindowID);
                    HOperatorSet.DispObj(Model.RawImageSequence[AcquisionImageCount], Model.MainUI_Camera_WindowID);
                }

                AcquisionImageCount++;

                if (AcquisionImageCount == 4)
                {
                    AcquisionImageCount = 0;
                }

            }

            hv_ImageIndex++;

        }


        public  void SaveImage(HObject SaveImage, bool Mode)//Mode=true:小图;Mode=false:大图
        {
            if (Mode)
            {
                string FileName = DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒");
                HOperatorSet.WriteImage(SaveImage, "bmp", 0, Model.m_szTestImagePath + "/" + FileName + ".bmp");
            }
            else
            {
                string FileName = DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒");
                HOperatorSet.WriteImage(ho_TiledImage, "bmp", 0, Model.m_szTestImagePath + "/Large_" + FileName + ".bmp");

            }


        }


    }//类结束
}//命名空间结束
