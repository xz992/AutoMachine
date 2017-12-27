using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using HalconDotNet;

namespace AutoMachineDAL
{
    public class ImageProcess
    {

        //图像锐化1
        public HObject ImageSharpness1(int Width, int Height, System.Drawing.Bitmap MyBitmap)
        {
            HObject Image;
            //24位图像锐化
            System.Drawing.Imaging.BitmapData BitmapData = MyBitmap.LockBits(new Rectangle(0, 0, Width, Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            unsafe
            {
                //图像数据排列BGR
                byte* Inptr = (byte*)(BitmapData.Scan0.ToPointer());
                byte[] R_OutBuffer = new byte[Width * Height];
                byte[] G_OutBuffer = new byte[Width * Height];
                byte[] B_OutBuffer = new byte[Width * Height];
                fixed (byte* R_Outptr = R_OutBuffer, G_Outptr = G_OutBuffer, B_Outptr = B_OutBuffer)
                {
                    //拉普拉斯模板
                    int[] Laplacian = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };

                    for (int i = 1; i < Width - 1; i++)
                    {
                        for (int j = 1; j < Height - 1; j++)
                        {
                            int r = 0, g = 0, b = 0;
                            int Index = 0;

                            for (int col = -1; col <= 1; col++)
                            {
                                for (int row = -1; row <= 1; row++)
                                {
                                    int off = ((j + row) * (Width) + (i + col)) * 3;
                                    b += Inptr[off + 0] * Laplacian[Index];
                                    g += Inptr[off + 1] * Laplacian[Index];
                                    r += Inptr[off + 2] * Laplacian[Index];

                                    Index++;
                                }
                            }

                            //处理颜色值溢出
                            r = r > 255 ? 255 : r;
                            r = r < 0 ? 0 : r;
                            g = g > 255 ? 255 : g;
                            g = g < 0 ? 0 : g;
                            b = b > 255 ? 255 : b;
                            b = b < 0 ? 0 : b;

                            int off2 = (j * Width + i);
                            B_OutBuffer[off2 + 0] = (byte)b;
                            G_OutBuffer[off2 + 0] = (byte)g;
                            R_OutBuffer[off2 + 0] = (byte)r;


                        }

                    }
                    MyBitmap.UnlockBits(BitmapData);

                    HOperatorSet.GenImage3(out Image, "byte", Width, Height, new IntPtr(R_Outptr), new IntPtr(G_Outptr), new IntPtr(B_Outptr));

                    return Image;

                }
            }

        }//图像锐化1
        
        //图像锐化2
        public HObject ImageSharpness2(int Width, int Height, ref byte[] InBuffer)
        {
            HObject Image;

            //24位图像锐化
            unsafe
            {
                //图像数据排列BGR
                byte[] R_OutBuffer = new byte[Width * Height];
                byte[] G_OutBuffer = new byte[Width * Height];
                byte[] B_OutBuffer = new byte[Width * Height];
                fixed (byte* R_Outptr = R_OutBuffer, G_Outptr = G_OutBuffer, B_Outptr = B_OutBuffer, Inptr = InBuffer)
                {
                    //拉普拉斯模板
                    int[] Laplacian = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };

                    for (int i = 1; i < Width - 1; i++)
                    {
                        for (int j = 1; j < Height - 1; j++)
                        {
                            int r = 0, g = 0, b = 0;
                            int Index = 0;

                            for (int col = -1; col <= 1; col++)
                            {
                                for (int row = -1; row <= 1; row++)
                                {
                                    int off = ((j + row) * (Width) + (i + col)) * 3;
                                    b += Inptr[off + 0] * Laplacian[Index];
                                    g += Inptr[off + 1] * Laplacian[Index];
                                    r += Inptr[off + 2] * Laplacian[Index];

                                    Index++;
                                }
                            }

                            //处理颜色值溢出
                            r = r > 255 ? 255 : r;
                            r = r < 0 ? 0 : r;
                            g = g > 255 ? 255 : g;
                            g = g < 0 ? 0 : g;
                            b = b > 255 ? 255 : b;
                            b = b < 0 ? 0 : b;

                            int off2 = (j * Width + i);
                            B_OutBuffer[off2 + 0] = (byte)b;
                            G_OutBuffer[off2 + 0] = (byte)g;
                            R_OutBuffer[off2 + 0] = (byte)r;


                        }

                    }

                    HOperatorSet.GenImage3(out Image, "byte", Width, Height, new IntPtr(R_Outptr), new IntPtr(G_Outptr), new IntPtr(B_Outptr));

                    return Image;

                }
            }

        }//图像锐化2


        //图像锐化3
        public HObject ImageSharpness3(int ImageWidth, int ImageHeight, ref byte[] ImageInPutBuffer, ref byte[] ImageOutBuffer)
        {
            HObject Image;

            //8位图像锐化

            //拉普拉斯模板
            int[] Laplacian = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };

            for (int i = 1; i < ImageWidth - 1; i++)
            {
                for (int j = 1; j < ImageHeight - 1; j++)
                {
                    int y = 0, Index = 0;

                    for (int col = -1; col <= 1; col++)
                    {
                        for (int row = -1; row <= 1; row++)
                        {
                            int off = ((j + row) * (ImageWidth) + (i + col));
                            y += ImageInPutBuffer[off] * Laplacian[Index];
                            Index++;
                        }
                    }

                    //处理颜色值溢出
                    y = y > 255 ? 255 : y;
                    y = y < 0 ? 0 : y;


                    int off2 = (j * ImageWidth + i);
                    ImageOutBuffer[off2 + 0] = (byte)y;

                }

            }

            //byte数组转IntPtr
            unsafe
            {
                void* TempPoint;
                IntPtr ptr;
                fixed (byte* P = ImageOutBuffer)
                {
                    TempPoint = (void*)P;
                    ptr = new IntPtr(TempPoint);
                }

                HOperatorSet.GenImage1(out Image, "byte", ImageWidth, ImageHeight, ptr);
                //HOperatorSet.GenImage3(out Image, "byte", Width, Height, new IntPtr(R_Outptr), new IntPtr(G_Outptr), new IntPtr(B_Outptr));

            }


            return Image;

        }//图像锐化3


        //图像锐化3
        public HObject ImageSharpness4(int ImageWidth, int ImageHeight,int n, ref byte[] ImageInPutBuffer, ref byte[] ImageOutBuffer)
        {
            HObject Image;

            //8位图像锐化

            //根据参数n来调节对比度，n越大，对比越强烈
            int Low = n;
            int High = 255 - n;
            float Grad = ((float)(High - Low)) / 255;

            for (int i = 1; i < ImageHeight - 1; i++)
            {
                for (int j = 1; j < ImageWidth - 1; j++)
                {
                    int off = (i * ImageWidth + j);

                   //数据很小，设置为0
                    if (ImageInPutBuffer[off] <= Low)
                        ImageOutBuffer[off] = 0;
                    //中间数据，进行对比增强处理
                    else if ((Low < ImageInPutBuffer[off]) && (ImageInPutBuffer[off] < High))
                        ImageOutBuffer[off] = (byte)((ImageInPutBuffer[off] - Low) / Grad);
                    //数据很大，设置为255
                    else
                        ImageOutBuffer[off] = 255;

                }

            }

            //byte数组转IntPtr
            unsafe
            {
                void* TempPoint;
                IntPtr ptr;
                fixed (byte* P = ImageOutBuffer)
                {
                    TempPoint = (void*)P;
                    ptr = new IntPtr(TempPoint);
                }

                HOperatorSet.GenImage1(out Image, "byte", ImageWidth, ImageHeight, ptr);
                //HOperatorSet.GenImage3(out Image, "byte", Width, Height, new IntPtr(R_Outptr), new IntPtr(G_Outptr), new IntPtr(B_Outptr));

            }


            return Image;

        }//图像锐化3

        //图像对比度增强
        public HObject ImageContrastEnhance(double a, double b, int ImageWidth, int ImageHeight, ref byte[] ImageInPutBuffer, ref byte[] ImageOutBuffer)
        {

            HObject Image;

            int temp = 0;
            int Sum = 0;
            int RGB_Y = 0;
            for (int y = 0; y < ImageHeight; y++)
            {
                for (int x = 0; x < ImageWidth; x++)
                {

                    int off = (y * ImageWidth + x);
                    RGB_Y = ImageInPutBuffer[off + 0];


                    Sum = (int)(a * RGB_Y + b);

                    if (Sum > 255)
                    {
                        temp = 255;
                    }
                    else if (Sum < 0)
                    {
                        temp = 0;
                    }
                    else
                    {
                        temp = (byte)Sum;
                    }

                    int off2 = (y * ImageWidth + x);
                    ImageOutBuffer[off2] = (byte)temp;

                }

            }

            //byte数组转IntPtr
            unsafe
            {
                void* TempPoint;
                IntPtr ptr;
                fixed (byte* P = ImageOutBuffer)
                {
                    TempPoint = (void*)P;
                    ptr = new IntPtr(TempPoint);
                }

                HOperatorSet.GenImage1(out Image, "byte", ImageWidth, ImageHeight, ptr);
                //HOperatorSet.GenImage3(out Image, "byte", Width, Height, new IntPtr(R_Outptr), new IntPtr(G_Outptr), new IntPtr(B_Outptr));

            }

            return Image;

        }
        //图像对比度增强


        //24位RGB转8位灰度
        public HObject RgbToGray1(int ImageWidth, int ImageHeight, ref byte[] ImageBuffer)
        {
            HObject Image;

            unsafe
            {
                //图像数据排列BGR
                byte[] RGB_Y_ChanelBuffer = new byte[ImageWidth * ImageHeight];
                fixed (byte* P_RGB_Y_ChanelBuffer = RGB_Y_ChanelBuffer, P_ImageBuffer = ImageBuffer)
                {

                    for (int i = 1; i < ImageHeight - 1; i++)
                    {
                        for (int j = 1; j < ImageWidth - 1; j++)
                        {
                            int Index = (i * ImageWidth + j) * 3;


                            byte RGB_B = P_ImageBuffer[Index + 0];
                            byte RGB_G = P_ImageBuffer[Index + 1];
                            byte RGB_R = P_ImageBuffer[Index + 2];

                            byte Average = (byte)(RGB_R * 0.299 + RGB_G * 0.587 + RGB_B * 0.114);

                            Index = (i * ImageWidth + j);

                            RGB_Y_ChanelBuffer[Index] = (byte)Average;
                        }

                    }

                    HOperatorSet.GenImage1(out Image, "byte", ImageWidth, ImageHeight, new IntPtr(P_RGB_Y_ChanelBuffer));
                    //HOperatorSet.GenImage3(out Image, "byte", Width, Height, new IntPtr(R_Outptr), new IntPtr(G_Outptr), new IntPtr(B_Outptr));

                    return Image;

                }
            }

        }//RBG转灰度


        //24位RGB转8位灰度
        public void RgbToGray2(int ImageWidth, int ImageHeight, ref byte[] ImageInPutBuffer, ref byte[] ImageOutBuffer)
        {

            //图像数据排列BGR
            byte[] RGB_Y_ChanelBuffer = new byte[ImageWidth * ImageHeight];


            for (int i = 1; i < ImageHeight - 1; i++)
            {
                for (int j = 1; j < ImageWidth - 1; j++)
                {
                    int Index = (i * ImageWidth + j) * 3;


                    byte RGB_B = ImageInPutBuffer[Index + 0];
                    byte RGB_G = ImageInPutBuffer[Index + 1];
                    byte RGB_R = ImageInPutBuffer[Index + 2];

                    byte Average = (byte)(RGB_R * 0.299 + RGB_G * 0.587 + RGB_B * 0.114);

                    Index = (i * ImageWidth + j);

                    ImageOutBuffer[Index] = (byte)Average;
                }
            }

        }//RBG转灰度


        //24位RGB转8位灰度
        public HObject RgbToGray3(int ImageWidth, int ImageHeight, ref byte[] ImageInPutBuffer, ref byte[] ImageOutBuffer)
        {

            HObject Image;

            //图像数据排列BGR
            byte[] RGB_Y_ChanelBuffer = new byte[ImageWidth * ImageHeight];


            for (int i = 1; i < ImageHeight - 1; i++)
            {
                for (int j = 1; j < ImageWidth - 1; j++)
                {
                    int Index = (i * ImageWidth + j) * 3;


                    byte RGB_B = ImageInPutBuffer[Index + 0];
                    byte RGB_G = ImageInPutBuffer[Index + 1];
                    byte RGB_R = ImageInPutBuffer[Index + 2];

                    byte Average = (byte)(RGB_R * 0.299 + RGB_G * 0.587 + RGB_B * 0.114);

                    Index = (i * ImageWidth + j);

                    ImageOutBuffer[Index] = (byte)Average;
                }
            }

            //byte数组转IntPtr
            unsafe
            {
                void* TempPoint;
                IntPtr ptr;
                fixed (byte* P = ImageOutBuffer)
                {
                    TempPoint = (void*)P;
                    ptr = new IntPtr(TempPoint);
                }

                HOperatorSet.GenImage1(out Image, "byte", ImageWidth, ImageHeight, ptr);
                //HOperatorSet.GenImage3(out Image, "byte", Width, Height, new IntPtr(R_Outptr), new IntPtr(G_Outptr), new IntPtr(B_Outptr));

            }


            return Image;

        }//RBG转灰度


        //32位RGB转8位灰度
        public HObject Rgb32ToGray(int ImageWidth, int ImageHeight, ref byte[] ImageInPutBuffer, ref byte[] ImageOutBuffer)
        {

            HObject Image;

            //图像数据排列BGR
            byte[] RGB_Y_ChanelBuffer = new byte[ImageWidth * ImageHeight];


            for (int i = 1; i < ImageHeight - 1; i++)
            {
                for (int j = 1; j < ImageWidth - 1; j++)
                {
                    int Index = (i * ImageWidth + j) * 4;


                    byte RGB_B = ImageInPutBuffer[Index + 0];
                    byte RGB_G = ImageInPutBuffer[Index + 1];
                    byte RGB_R = ImageInPutBuffer[Index + 2];

                    byte Average = (byte)(RGB_R * 0.299 + RGB_G * 0.587 + RGB_B * 0.114);

                    Index = (i * ImageWidth + j);

                    ImageOutBuffer[Index] = (byte)Average;
                }
            }

            //byte数组转IntPtr
            unsafe
            {
                void* TempPoint;
                IntPtr ptr;
                fixed (byte* P = ImageOutBuffer)
                {
                    TempPoint = (void*)P;
                    ptr = new IntPtr(TempPoint);
                }

                HOperatorSet.GenImage1(out Image, "byte", ImageWidth, ImageHeight, ptr);
                //HOperatorSet.GenImage3(out Image, "byte", Width, Height, new IntPtr(R_Outptr), new IntPtr(G_Outptr), new IntPtr(B_Outptr));

            }


            return Image;

        }//RBG转灰度


        //byte[] 转换 Bitmap
        public static Bitmap BytesToBitmap(byte[] Bytes)
        {
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(Bytes);
                return new Bitmap((Image)new Bitmap(stream));
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
        }

 
        //Bitmap转byte[]  
        public static byte[] BitmapToBytes(Bitmap Bitmap) 
        { 
            MemoryStream ms = null; 
            try 
            { 
                ms = new MemoryStream(); 
                Bitmap.Save(ms, Bitmap.RawFormat); 
                byte[] byteImage = new Byte[ms.Length]; 
                byteImage = ms.ToArray(); 
                return byteImage; 
            } 
            catch (ArgumentNullException ex) 
            { 
                throw ex; 
            } 
            finally 
            { 
                ms.Close(); 
            } 
        }


        // byte[]转换成Image
        public static System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null)
                return null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArrayIn))
            {
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                ms.Flush();
                return returnImage;
            }
        }


        //图像缓存数据转化为Hobject
        public HObject ImagePtrToHobject(int ImageWidth, int ImageHeight, ref byte[] ImageBuffer)
        {
            HObject Image;

            unsafe
            {

                fixed (byte* ImageBufferPtr = ImageBuffer)
                {


                    HOperatorSet.GenImage1(out Image, "byte", ImageWidth, ImageHeight, new IntPtr(ImageBufferPtr));
                    //HOperatorSet.GenImage3(out Image, "byte", Width, Height, new IntPtr(R_Outptr), new IntPtr(G_Outptr), new IntPtr(B_Outptr));

                    return Image;

                }
            }

        }


        //图像缓存数据转化为Hobject
        public HObject ImagePtrToHobject(int ImageWidth, int ImageHeight, ref IntPtr ImageBufferPtr)
        {
            HObject Image;

            HOperatorSet.GenImage1(out Image, "byte", ImageWidth, ImageHeight, ImageBufferPtr);

            return Image;

       }

        public static void ImageThreshold(ref HObject hv_Image, out HObject hv_ImageOut)
        {
            HOperatorSet.Threshold(hv_Image, out hv_ImageOut, 0, 100);
        }

    }
}
