using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using System.Web;
using Common;

namespace Business
{
    public class PanoramaModel : BaseModel<Panorama>, IPanoramaModel
    {
        public IQueryable<Panorama> GetByAccountMainID(int accountMainID)
        {
            return base.List().Where(a => a.AccountMainID == accountMainID);
        }

        public new Result Add(Panorama panorama)
        {
            //保存封面图片
            CommonModel com = new CommonModel();
            //fullImage
            if (!string.IsNullOrEmpty(panorama.FullImage))
            {
                var LastName = com.CreateRandom("", 5) + panorama.FullImage.GetFileSuffix();
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}{1}", string.Format(SystemConst.Business.PathFileLibrary, panorama.AccountMainID), imageName);
                var savePath = HttpContext.Current.Server.MapPath(imagePath);
                var oldImaFilePath_fullImage = HttpContext.Current.Server.MapPath(panorama.FullImage);
                var image = System.Drawing.Image.FromFile(oldImaFilePath_fullImage);
                image.Save(savePath);
                image.Dispose();
                panorama.FullImage = imagePath;
            }
            //左
            if (!string.IsNullOrEmpty(panorama.ImageLeft))
            {
                var LastName = com.CreateRandom("", 5) + panorama.ImageLeft.GetFileSuffix();
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}{1}", string.Format(SystemConst.Business.PathFileLibrary, panorama.AccountMainID), imageName);
                var savePath = HttpContext.Current.Server.MapPath(imagePath);
                var oldImaFilePath_ImageLeft = HttpContext.Current.Server.MapPath(panorama.ImageLeft);
                var image = System.Drawing.Image.FromFile(oldImaFilePath_ImageLeft);
                image.Save(savePath);
                image.Dispose();
                panorama.ImageLeft = imagePath;
            }
            //右
            if (!string.IsNullOrEmpty(panorama.ImageRight))
            {
                var LastName = com.CreateRandom("", 5) + panorama.ImageRight.GetFileSuffix();
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}{1}", string.Format(SystemConst.Business.PathFileLibrary, panorama.AccountMainID), imageName);
                var savePath = HttpContext.Current.Server.MapPath(imagePath);
                var oldImaFilePath_ImageRight = HttpContext.Current.Server.MapPath(panorama.ImageRight);
                var image = System.Drawing.Image.FromFile(oldImaFilePath_ImageRight);
                image.Save(savePath);
                image.Dispose();
                panorama.ImageRight = imagePath;
            }
            //上
            if (!string.IsNullOrEmpty(panorama.ImageTop))
            {
                var LastName = com.CreateRandom("", 5) + panorama.ImageTop.GetFileSuffix();
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}{1}", string.Format(SystemConst.Business.PathFileLibrary, panorama.AccountMainID), imageName);
                var savePath = HttpContext.Current.Server.MapPath(imagePath);
                var oldImaFilePath_ImageTop = HttpContext.Current.Server.MapPath(panorama.ImageTop);
                var image = System.Drawing.Image.FromFile(oldImaFilePath_ImageTop);
                image.Save(savePath);
                image.Dispose();
                panorama.ImageTop = imagePath;
            }
            //下
            if (!string.IsNullOrEmpty(panorama.ImageBottom))
            {
                var LastName = com.CreateRandom("", 5) + panorama.ImageBottom.GetFileSuffix();
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}{1}", string.Format(SystemConst.Business.PathFileLibrary, panorama.AccountMainID), imageName);
                var savePath = HttpContext.Current.Server.MapPath(imagePath);
                var oldImaFilePath_ImageBottom = HttpContext.Current.Server.MapPath(panorama.ImageBottom);
                var image = System.Drawing.Image.FromFile(oldImaFilePath_ImageBottom);
                image.Save(savePath);
                image.Dispose();
                panorama.ImageBottom = imagePath;
            }
            //前
            if (!string.IsNullOrEmpty(panorama.ImageFront))
            {
                var LastName = com.CreateRandom("", 5) + panorama.ImageFront.GetFileSuffix();
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}{1}", string.Format(SystemConst.Business.PathFileLibrary, panorama.AccountMainID), imageName);
                var savePath = HttpContext.Current.Server.MapPath(imagePath);
                var oldImaFilePath_ImageFront = HttpContext.Current.Server.MapPath(panorama.ImageFront);
                var image = System.Drawing.Image.FromFile(oldImaFilePath_ImageFront);
                image.Save(savePath);
                image.Dispose();
                panorama.ImageFront = imagePath;
            }
            //后
            if (!string.IsNullOrEmpty(panorama.ImageBottom))
            {
                var LastName = com.CreateRandom("", 5) + panorama.ImageBottom.GetFileSuffix();
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var imageName = string.Format("{0}_{1}", token, LastName);
                var imagePath = string.Format("{0}{1}", string.Format(SystemConst.Business.PathFileLibrary, panorama.AccountMainID), imageName);
                var savePath = HttpContext.Current.Server.MapPath(imagePath);
                var oldImaFilePath_ImageBottom = HttpContext.Current.Server.MapPath(panorama.ImageBottom);
                var image = System.Drawing.Image.FromFile(oldImaFilePath_ImageBottom);
                image.Save(savePath);
                image.Dispose();
                panorama.ImageBottom = imagePath;
            }
            return base.Add(panorama);
        }
    }
}
