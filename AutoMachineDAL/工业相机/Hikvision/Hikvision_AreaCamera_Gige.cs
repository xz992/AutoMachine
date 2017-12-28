using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MvCamCtrl.NET;
using HalconDotNet;
using AutoMachineModel;

namespace AutoMachineDAL
{
    public class Hikvision_AreaCamera_Gige : CameraAbstractClass
    {

        MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList;
        bool m_CameraOpened;

        private uint iWidth;
        private uint iHeigth;
        UInt32 m_nBufSizeForDriver;
        byte[] m_pBufForDriver;
        UInt32 m_nBufSizeForSaveImage;
        byte[] m_pBufForSaveImage;
        byte[] m_pDataForRed;
        byte[] m_pDataForGreen;
        byte[] m_pDataForBlue;

        int m_nDataLenForRed = 0;
        int m_nDataLenForGreen = 0;
        int m_nDataLenForBlue = 0;

        MyCamera.cbOutputExdelegate ImageCallback;

        //打开相机
        public override void OpenCamera()
        {
            int nRet = -1;
            #region FindCamera
            if (!Model.m_bCameraFound)
            {
                nRet = CameraOperator.EnumDevices(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
                if (0 != nRet)
                {
                    MessageBox.Show("枚举设备失败!");
                    Model.m_bCameraFound = false;
                    return;
                }
                Model.m_bCameraFound = true;
            }
            #endregion

            #region OpenCamera

            if (!Model.m_bCameraOpened)
            {
                //ch:获取选择的设备信息 | en:Get selected device information
                MyCamera.MV_CC_DEVICE_INFO device =
                    (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[0], //打开第一个相机
                                                                       typeof(MyCamera.MV_CC_DEVICE_INFO));
                // ch:打开设备 | en:Open device
                nRet = m_pOperator.Open(ref device);
                if (MyCamera.MV_OK != nRet)
                {
                    MessageBox.Show("设备打开失败!");
                    return;
                }
                m_pOperator.SetEnumValue("AcquisitionMode", 2);// 工作在连续模式
                m_pOperator.SetEnumValue("TriggerMode", Model.m_bTriggerMode);    //  连续拍摄模式
                Model.m_bCameraOpened = true;
            }
            #endregion

            #region StartGrabbing

            if (!Model.m_bGrabbing)
            {
                m_pOperator.GetIntValue("WidthMax", ref iWidth);
                m_pOperator.GetIntValue("HeightMax", ref iHeigth);

                m_nBufSizeForDriver = iWidth * iHeigth * 3;
                m_pBufForDriver = new byte[iWidth * iHeigth * 3];

                m_nBufSizeForSaveImage = iWidth * iHeigth * 3 * 3 + 2048;
                m_pBufForSaveImage = new byte[iWidth * iHeigth * 3 * 3 + 2048];

                m_pDataForRed = new byte[iWidth * iHeigth];
                m_pDataForGreen = new byte[iWidth * iHeigth];
                m_pDataForBlue = new byte[iWidth * iHeigth];

                // ch:注册回调函数 | en:Register image callback
                ImageCallback = new MyCamera.cbOutputExdelegate(GrabImage);
                nRet = m_pOperator.RegisterImageCallBackForRGB(ImageCallback, IntPtr.Zero);

                if (CameraOperator.CO_OK != nRet)
                {
                    MessageBox.Show("注册回调失败!");
                    return;
                }
                // ch:开启抓图 | en:start grab
                nRet = m_pOperator.StartGrabbing();
                if (MyCamera.MV_OK != nRet)
                {
                    MessageBox.Show("开始取流失败！");
                    return;
                }
                Model.m_bGrabbing = true;
            }
            #endregion
        }

        public override void StopGrabbing()
        {
            int nRet = -1;
            // ch:停止抓图 || en:Stop grab image
            if (Model.m_bGrabbing)
            {
                nRet = m_pOperator.StopGrabbing();
                if (nRet != CameraOperator.CO_OK)
                {
                    MessageBox.Show("停止取流失败！");
                    return;
                }
                Model.m_bGrabbing = false;
            }
        }

        public override void CloseCamera()
        {
            int nRet = -1;
            nRet = m_pOperator.Close();
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("相机关闭失败!");
                return;
            }
            Model.m_bCameraFound = false;
            Model.m_bCameraOpened = false;
            Model.m_bGrabbing = false;

        }


        //连续采集图像
        public override void ContinuousAcquisiton()
        {
            m_pOperator.SetEnumValue("AcquisitionMode", 2);// 0:SingleFrame , 1:MultiFrame, 2:Continuous   
            m_pOperator.SetEnumValue("m_bTriggerMode", 0);    // 0：Off 1：On
            /* TriggerSource
            0:Line0
            1:Line1
            2:Line2
            3.Line3
            4:Counter0
            7:Software
            8:FrequencyConverter
            */
            m_pOperator.SetEnumValue("TriggerSource", 0);
        }

        public override void EnableSoftTrigger()
        { }
        public override void EnableHardTrigger()
        { }



        private void GrabImage(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            HTuple SecondsStart;
            HTuple SecondsEnd;
            HTuple SecondsPassed;

            HOperatorSet.CountSeconds(out SecondsStart);

            if (pData != null)
            {
                Marshal.Copy(pData, m_pBufForSaveImage, 0, (int)(iWidth * iHeigth * 3));

                HObject Himage = new HObject();

                UInt32 nSupWidth = (pFrameInfo.nWidth + (UInt32)3) & 0xfffffffc;//宽度补齐为4的倍数
                Int32 nLength = (Int32)pFrameInfo.nWidth * (Int32)pFrameInfo.nHeight;

                RellocBuf(m_pDataForRed, m_nDataLenForRed, nLength);
                RellocBuf(m_pDataForGreen, m_nDataLenForGreen, nLength);
                RellocBuf(m_pDataForBlue, m_nDataLenForBlue, nLength);

                for (int nRow = 0; nRow < pFrameInfo.nHeight; nRow++)
                {
                    for (int col = 0; col < pFrameInfo.nWidth; col++)
                    {
                        m_pDataForRed[nRow * nSupWidth + col] = m_pBufForSaveImage[nRow * pFrameInfo.nWidth * 3 + (3 * col)];
                        m_pDataForGreen[nRow * nSupWidth + col] = m_pBufForSaveImage[nRow * pFrameInfo.nWidth * 3 + (3 * col + 1)];
                        m_pDataForBlue[nRow * nSupWidth + col] = m_pBufForSaveImage[nRow * pFrameInfo.nWidth * 3 + (3 * col + 2)];
                    }
                }

                IntPtr RedPtr = BytesToIntptr(m_pDataForRed);
                IntPtr GreenPtr = BytesToIntptr(m_pDataForGreen);
                IntPtr BluePtr = BytesToIntptr(m_pDataForBlue);
                try
                {
                    HOperatorSet.GenImage3(out Himage, (HTuple)"byte", pFrameInfo.nWidth, pFrameInfo.nHeight,
                                        (new HTuple(RedPtr)), (new HTuple(GreenPtr)), (new HTuple(BluePtr)));
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                // ch: 显示 || display
                HTuple hImageWidth = 0;
                HTuple hImageHeight = 0;
                HTuple point = null;
                HTuple type = null;

                try
                {
                    HOperatorSet.GetImagePointer1(Himage, out point, out type, out hImageWidth, out hImageHeight);//.ch: 得到图像的宽高和指针 || en: Get the width and height of the image
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show(ex.ToString());
                    return;
                }
                try
                {
                    HOperatorSet.SetPart(Model.MainUI_Camera_WindowID, 0, 0, hImageHeight, hImageWidth);// ch: 使图像显示适应窗口大小 || en: Make the image adapt the window size
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show(ex.ToString());
                    return;
                }
                if (true)
                {
                    if (Model.MainUI_Camera_WindowID == null)
                    {
                        return;
                    }
                    try
                    {
                        HOperatorSet.ClearWindow(Model.MainUI_Camera_WindowID);
                        HOperatorSet.DispObj(Himage, Model.MainUI_Camera_WindowID);// ch 显示 || en: display

                        Himage.Dispose();

                        Marshal.FreeHGlobal(RedPtr);// ch 释放空间 || en: release space
                        Marshal.FreeHGlobal(GreenPtr);// ch 释放空间 || en: release space
                        Marshal.FreeHGlobal(BluePtr);// ch 释放空间 || en: release space
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }

            }

            HOperatorSet.CountSeconds(out SecondsEnd);
            HOperatorSet.TupleSub(SecondsEnd, SecondsStart, out SecondsPassed);
            HOperatorSet.SetColor(Model.MainUI_Camera_WindowID, "red");
            HOperatorSet.SetTposition(Model.MainUI_Camera_WindowID, 400, 400); //在图像上显示Index
            HOperatorSet.WriteString(Model.MainUI_Camera_WindowID, SecondsPassed.ToString());

            return;
        }


        private void RellocBuf(byte[] pBuffer, int nBufSize, int nDstBufSize)
        {
            if (null == pBuffer)
            {
                pBuffer = new byte[nDstBufSize];
                if (null == pBuffer)
                {
                    return;
                }

                nBufSize = nDstBufSize;
            }
            else
            {
                if (nBufSize != nDstBufSize)
                {
                    pBuffer = new byte[nDstBufSize]; ;
                    if (null == pBuffer)
                    {
                        return;
                    }
                    nBufSize = nDstBufSize;
                }
            }
            return;
        }

        public static IntPtr BytesToIntptr(byte[] bytes)
        {
            int size = bytes.Length;
            IntPtr buffer = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, buffer, size);
            return buffer;
        }

    }




}
