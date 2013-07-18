using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Poco;
using Interface;
using Injection;
using Poco.Enum;

namespace Web.Controllers
{
    public class WebAPI_UserController : ApiController
    {
        public IList<AccountMain> GetAllProducts()
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            return accountMainModel.List().ToList().Select(a => new AccountMain() { Name = a.Name }).ToList();
        }

        public User PostLogin(User user)
        {
            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var result = userModel.Login(user.Email, user.LoginPwd);
            user = result.Entity as User;
            User entity = new User();
            entity.ID = user.ID;
            return entity;
        }

        //// GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}