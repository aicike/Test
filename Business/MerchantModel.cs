using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Common;
using System.Web;

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

    }
}
