using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Common;
using System.Web;
using System.IO;
using System.Drawing;
using Injection;

namespace Business
{
    public class Lottery_dishModel : BaseModel<Lottery_dish>, ILottery_dishModel
    {
        public IQueryable<Lottery_dish> List(int accountMainID)
        {
            return List(true).Where(a => a.AccountMainID == accountMainID);
        }

        //public Result Add(Lottery_dish lottery_dish)
        //{

        //    return null;

        //}

        [Transaction]
        public Result Add(Lottery_dish entity, List<Lottery_dish_detail> items, System.Web.HttpFileCollection files)
        {
            //添加基本信息
            Result result = base.Add(entity);
            if (result.HasError == true)
            {
                return result;
            }
            //上传奖品图片
            var path = string.Format(SystemConst.Business.PathBase, entity.AccountMainID);
            var token = DateTime.Now.ToString("yyyyMMddHHmmss");
            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                if (file.FileName.Length > 0)
                {
                    var imageName = string.Format("{0}_{1}", token, file.FileName);
                    var imageshowPath = string.Format("{0}/{1}", path, imageName);
                    items[i].Image = imageshowPath;
                    UploadTemp(file, HttpContext.Current.Server.MapPath(imageshowPath));
                }
            }
            var detailModel = Factory.Get<ILottery_dish_detailModel>(SystemConst.IOC_Model.Lottery_dish_detailModel);
            foreach (var item in items)
            {
                item.Lottery_dishID = entity.ID;
                result = detailModel.Add(item);
                if (result.HasError)
                {
                    return result;
                }
            }
            return result;
        }

        public void UploadTemp(HttpPostedFile file, string savePath)
        {
            int dataLengthToRead = (int)file.InputStream.Length;//获取下载的文件总大小
            byte[] buffer = new byte[dataLengthToRead];

            int r = file.InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
            Stream tream = new MemoryStream(buffer);
            Image img = Image.FromStream(tream);
            Tool.SuperGetPicThumbnail(img, savePath, 70, 100, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
        }

        public Result Edit(Lottery_dish entity, List<Lottery_dish_detail> items, HttpFileCollection files)
        {
            //添加基本信息
            Result result = base.Edit(entity);
            if (result.HasError == true)
            {
                return result;
            }
            //上传奖品图片
            var path = string.Format(SystemConst.Business.PathBase, entity.AccountMainID);
            var token = DateTime.Now.ToString("yyyyMMddHHmmss");
            for (int i = 0; i < files.Count; i++)
            {
                if (items[i].IsNewImg)
                {
                    var file = files[i];
                    var imageName = string.Format("{0}_{1}", token, file.FileName);
                    var imageshowPath = string.Format("{0}/{1}", path, imageName);
                    items[i].Image = imageshowPath;
                    UploadTemp(file, HttpContext.Current.Server.MapPath(imageshowPath));
                }
            }
            //删除原奖品项
            var detailModel = Factory.Get<ILottery_dish_detailModel>(SystemConst.IOC_Model.Lottery_dish_detailModel);
            result = detailModel.DeleteByLottery_dishID(entity.ID);
            if (result.HasError == true)
            {
                return result;
            }
            foreach (var item in items)
            {
                item.Lottery_dishID = entity.ID;
                result = detailModel.Add(item);
                if (result.HasError)
                {
                    return result;
                }
            }
            return result;
        }
    }
}
