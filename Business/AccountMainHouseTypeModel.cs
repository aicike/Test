using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using System.Web;
using System.IO;
using System.Drawing;
using Common;

namespace Business
{
    public class AccountMainHouseTypeModel : BaseModel<AccountMainHouseType>, IAccountMainHouseTypeModel
    {
        public IQueryable<AccountMainHouseType> GetList(int AccountMainHouseID)
        {
            return List(true).Where(a => a.AccountMainHousesID == AccountMainHouseID);
        }

        [Transaction]
        public Result AddInfo(AccountMainHouseType HouseType, int accountMainID, System.Web.HttpPostedFileBase HouseImagePath)
        {
            var result = base.Add(HouseType);
            if (result.HasError == false && HouseImagePath != null)
            { 
                try
                {
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + HouseImagePath.FileName.GetFileSuffix();
                    var path = string.Format(SystemConst.Business.PathBase, accountMainID);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imagePath = string.Format("{0}\\{1}", path, imageName);
                    var imagePath2 = HttpContext.Current.Server.MapPath(imagePath);

                    int dataLengthToRead = (int)HouseImagePath.InputStream.Length;//获取下载的文件总大小
                    byte[] buffer = new byte[dataLengthToRead];


                    int r = HouseImagePath.InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
                    Stream tream = new MemoryStream(buffer);
                    Image img = Image.FromStream(tream);


                    Tool.SuperGetPicThumbnail(img, imagePath2, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);


                    HouseType.HouseTypeImagePath = imagePath;

                    result = Edit(HouseType);
                }
                catch (Exception ex)
                {
                    result.Error = ex.Message;
                }
            }

            return result;
        }

        [Transaction]
        public Result AddInfo(AccountMainHouseType HouseType, int accountMainID)
        {
            var result = base.Add(HouseType);
            if (result.HasError == false && string.IsNullOrEmpty(HouseType.HouseTypeImagePath) == false)
            {
                try
                {
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + HouseType.HouseTypeImagePath.GetFileSuffix();
                    var path = string.Format(SystemConst.Business.PathBase, accountMainID);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imagePath = string.Format("{0}\\{1}", path, imageName);
                    var imagePath2 = HttpContext.Current.Server.MapPath(imagePath);

                    var lsImgPath = HouseType.HouseTypeImagePath;
                    var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);



                    Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath2, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);


                    HouseType.HouseTypeImagePath = imagePath;

                    result = Edit(HouseType);
                }
                catch (Exception ex)
                {
                    result.Error = ex.Message;
                }
            }

            return result;
        }





        public Result EditInfo(AccountMainHouseType HouseType, int accountMainID, HttpPostedFileBase HouseImagePath)
        {
            var result = base.Edit(HouseType);
            if (result.HasError == false && HouseImagePath != null)
            {
                try
                {
                    //删除原文件
                    var file = HttpContext.Current.Server.MapPath(HouseType.HouseTypeImagePath);
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                    //上传新文件
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + HouseImagePath.FileName.GetFileSuffix();
                    var path = string.Format(SystemConst.Business.PathBase, accountMainID);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imagePath = string.Format("{0}\\{1}", path, imageName);
                    var imagePath2 = HttpContext.Current.Server.MapPath(imagePath);

                    int dataLengthToRead = (int)HouseImagePath.InputStream.Length;//获取下载的文件总大小
                    byte[] buffer = new byte[dataLengthToRead];


                    int r = HouseImagePath.InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
                    Stream tream = new MemoryStream(buffer);
                    Image img = Image.FromStream(tream);


                    Tool.SuperGetPicThumbnail(img, imagePath2, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);


                    HouseType.HouseTypeImagePath = imagePath;

                    result = Edit(HouseType);
                }
                catch (Exception ex)
                {
                    result.Error = ex.Message;
                }
            }
            return result;
        }


        public Result EditInfo(AccountMainHouseType HouseType, int accountMainID)
        {
            var YHouser = this.Get(HouseType.ID);
            var result = base.Edit(HouseType);
            if (result.HasError == false && HouseType.HouseTypeImagePath != YHouser.HouseTypeImagePath)
            {
                try
                {
                    //删除原文件
                    var file = HttpContext.Current.Server.MapPath(YHouser.HouseTypeImagePath);
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                    //上传新文件
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + HouseType.HouseTypeImagePath.GetFileSuffix();
                    var path = string.Format(SystemConst.Business.PathBase, accountMainID);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imagePath = string.Format("{0}\\{1}", path, imageName);
                    var imagePath2 = HttpContext.Current.Server.MapPath(imagePath);


                    var lsImgPath = HouseType.HouseTypeImagePath;
                    var lsImaFilePath = HttpContext.Current.Server.MapPath(lsImgPath);


                    Tool.SuperGetPicThumbnail(lsImaFilePath, imagePath2, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);


                    HouseType.HouseTypeImagePath = imagePath;

                    result = Edit(HouseType);
                }
                catch (Exception ex)
                {
                    result.Error = ex.Message;
                }
            }
            return result;
        }



        public Result DeleteInfo(int id)
        {
            AccountMainHouseType HouseType =base.Get(id);

            var result = base.Delete(id);
            if (result.HasError == false)
            {
                try
                {
                    //删除原文件
                    var file = HttpContext.Current.Server.MapPath(HouseType.HouseTypeImagePath);
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }
            return result;
        }

        //删除所有数据 级联删除
        [Transaction]
        public Result DelteAll(int HousesTypeID)
        {
            Result result = new Result();
            AccountMainHouseType HouseType = base.Get(HousesTypeID);
            string sql = string.Format(@"delete dbo.AccountMainHouseInfoDetail where AccountMainHouseTypeID={0} 
                           delete dbo.AccountMainHouseType where ID = {0}", HousesTypeID);
            try
            {
                result.Entity = base.SqlExecute(sql);
                try
                {
                    
                    //删除原文件
                    var file = HttpContext.Current.Server.MapPath(HouseType.HouseTypeImagePath);
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
                catch (Exception ex)
                {

                }

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Error = ex.Message;
            }
            return result;
        }
    }
}
