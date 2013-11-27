using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web;

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
                bmp.Save(SavePath, bmp.RawFormat);
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
                bmp.Save(SavePath, bmp.RawFormat);
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
                bmp.Save(SavePath, bmp.RawFormat);
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

        /// <summary>
        /// 裁剪，压缩方法 传地址
        /// </summary>
        /// <param name="imgSrc">原始图片</param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="flag">压缩比例，一般设置在70-100之间</param>
        /// <param name="thumbWidth">缩略图宽度 无 传0</param>
        /// <param name="thumbHeight">缩略图高度 无 传0</param>
        /// <param name="sm">抗锯齿</param>
        /// <param name="cq">影像合成</param>
        /// <param name="im">图片质量</param>
        /// <returns></returns>
        public static bool SuperGetPicThumbnail(string imgSrc, string SavePath, int flag, int thumbWidth, int thumbHeight, SmoothingMode sm = SmoothingMode.HighQuality, CompositingQuality cq = CompositingQuality.HighQuality, InterpolationMode im = InterpolationMode.High)
        {
            if (thumbWidth == 0 && thumbHeight == 0)
            {
                return false;
            }
            Image image = null;
            Bitmap bmp = null;
            System.Drawing.Graphics gr = null;
            EncoderParameter eParam = null;
            EncoderParameters ep = null;
            try
            {
                image = System.Drawing.Image.FromFile(imgSrc); //利用Image对象装载源图像
                //接着创建一个System.Drawing.Bitmap对象，并设置你希望的缩略图的宽度和高度。
                int srcWidth = image.Width;
                int srcHeight = image.Height;
                if (thumbWidth == 0 && thumbHeight == 0)
                {
                    thumbWidth = srcWidth;
                    thumbHeight = srcHeight;
                }
                else if (thumbWidth != 0 && thumbHeight != 0)
                {
                    if (srcWidth < thumbWidth)
                    {
                        thumbWidth = srcWidth;
                        thumbHeight = srcHeight;
                    }
                    else if (srcHeight < thumbHeight)
                    {
                        thumbWidth = srcWidth;
                        thumbHeight = srcHeight;
                    }
                }
                else
                {
                    if (thumbWidth != 0)
                    {
                        if (srcWidth < thumbWidth)
                        {
                            thumbWidth = srcWidth;
                            thumbHeight = srcHeight;
                        }
                        else
                        {
                            thumbHeight = Convert.ToInt32((srcHeight * 1.0 / srcWidth) * thumbWidth);
                        }
                    }
                    else
                    {
                        if (srcHeight < thumbHeight)
                        {
                            thumbWidth = srcWidth;
                            thumbHeight = srcHeight;
                        }
                        else
                        {
                            thumbWidth = Convert.ToInt32((srcWidth * 1.0 / srcHeight) * thumbHeight);
                        }

                    }
                }
               

                bmp = new Bitmap(thumbWidth, thumbHeight);
                //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。
                gr = System.Drawing.Graphics.FromImage(bmp);
                //设置 System.Drawing.Graphics对象的SmoothingMode属性为HighQuality
                gr.SmoothingMode = sm;
                //下面这个也设成高质量
                gr.CompositingQuality = cq;
                //下面这个设成High
                gr.InterpolationMode = im;
                //把原始图像绘制成上面所设置宽高的缩小图
                System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
                gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
                ImageFormat tFormat = image.RawFormat;
                //以下代码为保存图片时，设置压缩质量 
                ep = new EncoderParameters();
                long[] qy = new long[1];
                qy[0] = flag;//设置压缩的比例1-100 
                eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
                ep.Param[0] = eParam;
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
                    bmp.Save(SavePath, jpegICIinfo, ep);//dFile是压缩后的新路径 
                }
                else
                {
                    bmp.Save(SavePath, tFormat);
                }
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
                if (ep != null)
                {
                    ep.Dispose();
                }
                if (eParam != null)
                {
                    eParam.Dispose();
                }
                if (gr != null)
                {
                    gr.Dispose();
                }
                if (image != null)
                {
                    image.Dispose();
                }
            }
        }




        /// <summary>
        /// 裁剪，压缩方法 传Image对象
        /// </summary>
        /// <param name="imgSrc">原始图片</param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="flag">压缩比例，一般设置在70-100之间</param>
        /// <param name="thumbWidth">缩略图宽度 无 传0</param>
        /// <param name="thumbHeight">缩略图高度 无 传0</param>
        /// <param name="sm">抗锯齿</param>
        /// <param name="cq">影像合成</param>
        /// <param name="im">图片质量</param>
        /// <returns></returns>
        public static bool SuperGetPicThumbnail(Image image, string SavePath, int flag, int thumbWidth, int thumbHeight, SmoothingMode sm = SmoothingMode.HighQuality, CompositingQuality cq = CompositingQuality.HighQuality, InterpolationMode im = InterpolationMode.High)
        {

            Bitmap bmp = null;
            System.Drawing.Graphics gr = null;
            EncoderParameter eParam = null;
            EncoderParameters ep = null;
            try
            {
                //接着创建一个System.Drawing.Bitmap对象，并设置你希望的缩略图的宽度和高度。
                int srcWidth = image.Width;
                int srcHeight = image.Height;
                if (thumbWidth == 0 && thumbHeight == 0)
                {
                    thumbWidth = srcWidth;
                    thumbHeight = srcHeight;
                }
                else if (thumbWidth != 0 && thumbHeight != 0)
                {
                    if (srcWidth < thumbWidth)
                    {
                        thumbWidth = srcWidth;
                        thumbHeight = srcHeight;
                    }
                    else if (srcHeight < thumbHeight)
                    {
                        thumbWidth = srcWidth;
                        thumbHeight = srcHeight;
                    }
                }
                else
                {
                    if (thumbWidth != 0)
                    {
                        if (srcWidth < thumbWidth)
                        {
                            thumbWidth = srcWidth;
                            thumbHeight = srcHeight;
                        }
                        else
                        {
                            thumbHeight = Convert.ToInt32((srcHeight * 1.0 / srcWidth) * thumbWidth);
                        }
                    }
                    else
                    {
                        if (srcHeight < thumbHeight)
                        {
                            thumbWidth = srcWidth;
                            thumbHeight = srcHeight;
                        }
                        else
                        {
                            thumbWidth = Convert.ToInt32((srcWidth * 1.0 / srcHeight) * thumbHeight);
                        }

                    }
                }
               

                bmp = new Bitmap(thumbWidth, thumbHeight);
                //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。
                gr = System.Drawing.Graphics.FromImage(bmp);
                //设置 System.Drawing.Graphics对象的SmoothingMode属性为HighQuality
                gr.SmoothingMode = sm;
                //下面这个也设成高质量
                gr.CompositingQuality = cq;
                //下面这个设成High
                gr.InterpolationMode = im;
                //把原始图像绘制成上面所设置宽高的缩小图
                System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
                gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
                ImageFormat tFormat = image.RawFormat;
                //以下代码为保存图片时，设置压缩质量 
                ep = new EncoderParameters();
                long[] qy = new long[1];
                qy[0] = flag;//设置压缩的比例1-100 
                eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
                ep.Param[0] = eParam;
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
                    bmp.Save(SavePath, jpegICIinfo, ep);//dFile是压缩后的新路径 
                }
                else
                {
                    bmp.Save(SavePath, tFormat);
                }
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
                if (ep != null)
                {
                    ep.Dispose();
                }
                if (eParam != null)
                {
                    eParam.Dispose();
                }
                if (gr != null)
                {
                    gr.Dispose();
                }
                if (image != null)
                {
                    image.Dispose();
                }
            }
        }



        /// <summary>
        /// 按比例截图，裁剪，压缩方法 传Image对象
        /// </summary>
        /// <param name="imgSrc">原始图片</param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="flag">压缩比例，一般设置在70-100之间</param>
        /// <param name="w">当前截图宽度</param>
        /// <param name="h">当前截图高度</param>
        /// <param name="x1">当前截图x坐标</param>
        /// <param name="y1">当前截图y坐标</param>
        /// <param name="tw">当前截图显示宽度</param>
        /// <param name="th">当前截图显示高度</param>
        /// <param name="sm">抗锯齿</param>
        /// <param name="cq">影像合成</param>
        /// <param name="im">图片质量</param>
        /// <returns></returns>
        public static bool SuperGetPicThumbnailJT(string imgSrc, string SavePath, int flag, int w, int h, int x1, int y1, int tw, int th, SmoothingMode sm = SmoothingMode.HighQuality, CompositingQuality cq = CompositingQuality.HighQuality, InterpolationMode im = InterpolationMode.High)
        {
            Image image = System.Drawing.Image.FromFile(imgSrc);
            Bitmap sourceBitmap = new Bitmap(image);
            EncoderParameter eParam = null;
            EncoderParameters ep = null;
            try
            {
                //接着创建一个System.Drawing.Bitmap对象，并设置你希望的缩略图的宽度和高度。
                int srcWidth = image.Width;
                int srcHeight = image.Height;


                int ToWidth = w;
                int ToHeight = h;
                int ToX1 = x1;
                int ToY1 = y1;


                int YW = sourceBitmap.Width;
                int YH = sourceBitmap.Height;

                if (YH != th)
                {
                    double ratio = double.Parse(YH.ToString()) / double.Parse(th.ToString());
                    //ratio = Math.Round(ratio, 2);
                    ToWidth = (int)(ToWidth * ratio);
                    ToHeight = (int)(ToHeight * ratio);
                    ToX1 = (int)(ToX1 * ratio);
                    ToY1 = (int)(ToY1 * ratio);
                }

                Bitmap resultBitmap = new Bitmap(ToWidth, ToHeight);

                using (Graphics g = Graphics.FromImage(resultBitmap))
                {
                    g.SmoothingMode = sm;
                    //下面这个也设成高质量
                    g.CompositingQuality = cq;
                    //下面这个设成High
                    g.InterpolationMode = im;
                    Rectangle resultRectangle = new Rectangle(0, 0, ToWidth, ToHeight);
                    Rectangle sourceRectangle = new Rectangle(0 + ToX1, 0 + ToY1, ToWidth, ToHeight);
                    g.DrawImage(sourceBitmap, resultRectangle, sourceRectangle, GraphicsUnit.Pixel);
                }


                //resultBitmap.Save(SavePath, sourceBitmap.RawFormat);



                ImageFormat tFormat = image.RawFormat;
                //以下代码为保存图片时，设置压缩质量 
                ep = new EncoderParameters();
                long[] qy = new long[1];
                qy[0] = flag;//设置压缩的比例1-100 
                eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
                ep.Param[0] = eParam;
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
                    resultBitmap.Save(SavePath, jpegICIinfo, ep);//dFile是压缩后的新路径 
                }
                else
                {
                    resultBitmap.Save(SavePath, tFormat);
                }
                resultBitmap.Dispose();
                sourceBitmap.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                if (ep != null)
                {
                    ep.Dispose();
                }
                if (eParam != null)
                {
                    eParam.Dispose();
                }
                if (image != null)
                {
                    image.Dispose();
                }
            }
        }




        /// <summary>
        /// 按比例截图，裁剪，压缩方法 传Image对象
        /// </summary>
        /// <param name="imgSrc">原始图片</param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="flag">压缩比例，一般设置在70-100之间</param>
        /// <param name="w">当前截图宽度</param>
        /// <param name="h">当前截图高度</param>
        /// <param name="x1">当前截图x坐标</param>
        /// <param name="y1">当前截图y坐标</param>
        /// <param name="tw">当前截图显示宽度</param>
        /// <param name="th">当前截图显示高度</param>
        /// <param name="sm">抗锯齿</param>
        /// <param name="cq">影像合成</param>
        /// <param name="im">图片质量</param>
        /// <returns></returns>
        public static bool SuperGetPicThumbnailJT(Image image, string SavePath, int flag, int w, int h, int x1, int y1, int tw, int th, SmoothingMode sm = SmoothingMode.HighQuality, CompositingQuality cq = CompositingQuality.HighQuality, InterpolationMode im = InterpolationMode.High)
        {
            Bitmap sourceBitmap = new Bitmap(image);
            EncoderParameter eParam = null;
            EncoderParameters ep = null;
            try
            {
                //接着创建一个System.Drawing.Bitmap对象，并设置你希望的缩略图的宽度和高度。
                int srcWidth = image.Width;
                int srcHeight = image.Height;


                int ToWidth = w;
                int ToHeight = h;
                int ToX1 = x1;
                int ToY1 = y1;

               
                int YW = sourceBitmap.Width;
                int YH = sourceBitmap.Height;

                if (YH != th)
                {
                    double ratio = double.Parse(YH.ToString()) / double.Parse(th.ToString());
                    //ratio = Math.Round(ratio, 2);
                    ToWidth = (int)(ToWidth * ratio);
                    ToHeight = (int)(ToHeight * ratio);
                    ToX1 = (int)(ToX1 * ratio);
                    ToY1 = (int)(ToY1 * ratio);
                }

                Bitmap resultBitmap = new Bitmap(ToWidth, ToHeight);

                using (Graphics g = Graphics.FromImage(resultBitmap))
                {
                    g.SmoothingMode = sm;
                    //下面这个也设成高质量
                    g.CompositingQuality = cq;
                    //下面这个设成High
                    g.InterpolationMode = im;
                    Rectangle resultRectangle = new Rectangle(0, 0, ToWidth, ToHeight);
                    Rectangle sourceRectangle = new Rectangle(0 + ToX1, 0 + ToY1, ToWidth, ToHeight);
                    g.DrawImage(sourceBitmap, resultRectangle, sourceRectangle, GraphicsUnit.Pixel);
                }


                //resultBitmap.Save(SavePath, sourceBitmap.RawFormat);
                


                ImageFormat tFormat = image.RawFormat;
                //以下代码为保存图片时，设置压缩质量 
                ep = new EncoderParameters();
                long[] qy = new long[1];
                qy[0] = flag;//设置压缩的比例1-100 
                eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
                ep.Param[0] = eParam;
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
                    resultBitmap.Save(SavePath, jpegICIinfo, ep);//dFile是压缩后的新路径 
                }
                else
                {
                    resultBitmap.Save(SavePath, tFormat);
                }
                resultBitmap.Dispose();
                sourceBitmap.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
              
                if (ep != null)
                {
                    ep.Dispose();
                }
                if (eParam != null)
                {
                    eParam.Dispose();
                }
                if (image != null)
                {
                    image.Dispose();
                }
            }
        }






        /// <summary>
        /// 获取平台临时文件夹位置
        /// </summary>
        /// <returns></returns>
        public static string GetTemporaryPath()
        {
            string Path = "/File/Temporary/"+DateTime.Now.ToString("yyyy-MM-dd");
            //string TempPath = HttpContext.Current.Server.MapPath(Path);
            if (Directory.Exists(Path) == false)
            {
                Directory.CreateDirectory(Path);
            }
            return Path;
        }

    }
}
