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
    }
}
