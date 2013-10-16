using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Common
{
    public static class Tool
    {
        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="imgSrc">原图路径</param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="thumbWidth">宽像素</param>
        public static bool Thumbnail(string imgSrc, string SavePath, int thumbWidth)
        {
            Image image = null;
            Bitmap bmp = null;
            try
            {
                image = System.Drawing.Image.FromFile(imgSrc); //利用Image对象装载源图像
                //接着创建一个System.Drawing.Bitmap对象，并设置你希望的缩略图的宽度和高度。
                int srcWidth = image.Width;
                int srcHeight = image.Height;
                if (srcWidth < thumbWidth)
                {
                    return false;
                }
                int thumbHeight = Convert.ToInt32((srcHeight * 1.0 / srcWidth) * thumbWidth);
                bmp = new Bitmap(thumbWidth, thumbHeight);
                //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。
                System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
                //设置 System.Drawing.Graphics对象的SmoothingMode属性为HighQuality
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //下面这个也设成高质量
                gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //下面这个设成High
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //把原始图像绘制成上面所设置宽高的缩小图
                System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
                gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
                bmp.Save(SavePath);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                }
                if (image != null)
                {
                    image.Dispose();
                }
            }
        }

        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="imgSrc">原图路径</param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="thumbHeight">高像素</param>
        public static bool Thumbnail(string imgSrc, int thumbHeight, string SavePath)
        {
            Image image = null;
            Bitmap bmp = null;
            try
            {
                image = System.Drawing.Image.FromFile(imgSrc); //利用Image对象装载源图像
                //接着创建一个System.Drawing.Bitmap对象，并设置你希望的缩略图的宽度和高度。
                int srcWidth = image.Width;
                int srcHeight = image.Height;
                if (srcHeight < thumbHeight)
                {
                    return false;
                }
                int thumbWidth = Convert.ToInt32((srcWidth * 1.0 / srcHeight) * thumbHeight);
                bmp = new Bitmap(thumbWidth, thumbHeight);
                //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。
                System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
                //设置 System.Drawing.Graphics对象的SmoothingMode属性为HighQuality
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //下面这个也设成高质量
                gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //下面这个设成High
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //把原始图像绘制成上面所设置宽高的缩小图
                System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
                gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
                bmp.Save(SavePath);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                }
                if (image != null)
                {
                    image.Dispose();
                }
            }
        }

        /// <summary>
        /// 创建缩略图 置顶宽高
        /// </summary>
        /// <param name="imgSrc">原图路径</param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="thumbWidth">宽像素 0</param>
        public static bool Thumbnail(string imgSrc, string SavePath, int thumbWidth, int thumbHeight)
        {
            Image image = null;
            Bitmap bmp = null;
            try
            {
                image = System.Drawing.Image.FromFile(imgSrc); //利用Image对象装载源图像
                //接着创建一个System.Drawing.Bitmap对象，并设置你希望的缩略图的宽度和高度。
                int srcWidth = image.Width;
                int srcHeight = image.Height;
                if (srcWidth < thumbWidth)
                {
                    return false;
                }
                //int thumbHeight = Convert.ToInt32((srcHeight * 1.0 / srcWidth) * thumbWidth);
                bmp = new Bitmap(thumbWidth, thumbHeight);
                //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。
                System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
                //设置 System.Drawing.Graphics对象的SmoothingMode属性为HighQuality
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //下面这个也设成高质量
                gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //下面这个设成High
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //把原始图像绘制成上面所设置宽高的缩小图
                System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
                gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
                bmp.Save(SavePath);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                }
                if (image != null)
                {
                    image.Dispose();
                }
            }
        }


        /// <summary> 
        /// 图片压缩 
        /// </summary> 
        /// <param name="sFile"></param> 
        /// <param name="outPath"></param> 
        /// <param name="flag"></param> 
        /// <returns></returns> 
        public static bool GetPicThumbnail(string sFile, string outPath, int flag)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            //以下代码为保存图片时，设置压缩质量 
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100 
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    iSource.Save(outPath, jpegICIinfo, ep);//dFile是压缩后的新路径 
                }
                else
                {
                    iSource.Save(outPath, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                iSource.Dispose();
            }
        }
    }
}
