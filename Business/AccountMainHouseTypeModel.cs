using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using System.Web;
using System.IO;

namespace Business
{
    public class AccountMainHouseTypeModel : BaseModel<AccountMainHouseType>, IAccountMainHouseTypeModel
    {
        public IQueryable<AccountMainHouseType> GetList(int AccountMainHouseID)
        {
            return List().Where(a => a.AccountMainHousesID == AccountMainHouseID);
        }

        [Transaction]
        public Result AddInfo(AccountMainHouseType HouseType, int accountMainID, System.Web.HttpPostedFileBase HouseImagePath)
        {
            var result = base.Add(HouseType);
            if (result.HasError == false && HouseImagePath != null)
            {
                try
                {
                    var path = string.Format(SystemConst.Business.PathBase, accountMainID);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var imageName = string.Format("{0}_{1}", token, HouseImagePath.FileName);
                    var imagePath = string.Format("{0}\\{1}", path, imageName);
                    var imagePath2 = HttpContext.Current.Server.MapPath(imagePath);
                    HouseImagePath.SaveAs(imagePath2);
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
                    var path = string.Format(SystemConst.Business.PathBase, accountMainID);
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var imageName = string.Format("{0}_{1}", token, HouseImagePath.FileName);
                    var imagePath = string.Format("{0}\\{1}", path, imageName);
                    var imagePath2 = HttpContext.Current.Server.MapPath(imagePath);
                    HouseImagePath.SaveAs(imagePath2);
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
    }
}
