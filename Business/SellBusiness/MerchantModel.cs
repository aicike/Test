using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Common;
using System.Web;
using System.IO;
using System.Drawing;

namespace Business
{
    public class MerchantModel : BaseModel<Merchant>, IMerchantModel
    {
        /// <summary>
        /// 商户登陆
        /// </summary>
        /// <param name="Phone"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        public Result Login(string Phone, string PWD)
        {
            Result result = new Result();
            PWD = DESEncrypt.Encrypt(PWD);

            var merchant = List().Where(a => a.Phone.Equals(Phone, StringComparison.OrdinalIgnoreCase) && a.LoginPwd.Equals(PWD)).FirstOrDefault();
            if (merchant == null)
            {
                result.Error = "用户名或密码错误";
                result.HasError = true;
            }
            else
            {
                Merchant entity = new Merchant();
                entity.ID = merchant.ID;
                entity.SystemStatus = merchant.SystemStatus;
                entity.Name = merchant.Name;
                entity.Email = merchant.Email;
                entity.LoginPwd = merchant.LoginPwd;
                entity.LogoImagePath = merchant.LogoImagePath;
                entity.Phone = merchant.Phone;
                entity.Address = merchant.Address;
                HttpContext.Current.Session[SystemConst.Session.LoginMerchant] = entity;
            }
       
            return result;
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="merchant"></param>
        /// <returns></returns>
        public Result EditInfo(Merchant merchant, System.Web.HttpPostedFileBase LogoImagePathFile)
        {
            Result result = new Result();
            var Merchantys = this.Get(merchant.ID);
            if (LogoImagePathFile != null)
            {
                if (Merchantys.LogoImagePath.Substring(Merchantys.LogoImagePath.LastIndexOf('/')) != LogoImagePathFile.FileName)
                {
                    CommonModel com = new CommonModel();
                    var LastName = com.CreateRandom("", 5) + LogoImagePathFile.FileName.GetFileSuffix();
                    var path = string.Format(SystemConst.Business.MerchantFile, merchant.ID);
                    var PathFile = HttpContext.Current.Server.MapPath(path);
                    if (!Directory.Exists(PathFile))//判断目录是否存在
                    {
                        Directory.CreateDirectory(PathFile);
                    }
                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var imageName = string.Format("{0}_{1}", token, LastName);
                    var imageName2 = string.Format("{0}_M_{1}", token, LastName);
                    var imagePath = string.Format("{0}\\{1}", PathFile, imageName);
                    var imagePath2 = string.Format("{0}\\{1}", PathFile, imageName2);


                    int dataLengthToRead = (int)LogoImagePathFile.InputStream.Length;//获取下载的文件总大小
                    byte[] buffer = new byte[dataLengthToRead];


                    int r = LogoImagePathFile.InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
                    Stream tream = new MemoryStream(buffer);
                    Image img = Image.FromStream(tream);

                    Tool.SuperGetPicThumbnail(img, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                    merchant.LogoImagePath = path + imageName;
                    Tool.SuperGetPicThumbnail(imagePath, imagePath2, 70, 0, 58, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                    merchant.LogoShow = path + imageName2;

                }
            }
            merchant.LoginPwd = DESEncrypt.Encrypt(merchant.LoginPwd);
            merchant.LoginPwdPage = DESEncrypt.Encrypt(merchant.LoginPwdPage);
            result = base.Edit(merchant);
            return result;
        }

    }
}
