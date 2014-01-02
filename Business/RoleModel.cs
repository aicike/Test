using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Poco.Enum;
using Injection.Transaction;

namespace Business
{
    public class RoleModel : BaseModel<Role>, IRoleModel
    {
        public int GetRoleIscandelete(int AcountMainID)
        {
            return List().Where(a => a.AccountMainID == AcountMainID && a.IsCanDelete == false).FirstOrDefault().ID;
        }

        public List<Role> GetRoleList(int? accountMainID)
        {
            List<Role> list = List().Where(a=> a.IsCanDelete&&a.AccountMainID==accountMainID).ToList();
            if (accountMainID != null && accountMainID.HasValue)
            {
                foreach (var item in list)
                {
                    item.Account_Roles = item.Account_Roles.Where(a => a.Account.Account_AccountMains.Any(b => b.AccountMainID == accountMainID.Value && b.SystemStatus == (int)EnumSystemStatus.Active)).ToList();
                }

            }
            return list;
        }

        public List<Role> GetRoleListNoaID(int accountMainID, int AccountID)
        {
            List<Role> list = List().Where(a => a.IsCanDelete&&a.AccountMainID==accountMainID).ToList();

            foreach (var item in list)
            {
                item.Account_Roles = item.Account_Roles.Where(a => a.Account.Account_AccountMains.Any(b => b.AccountMainID == accountMainID && b.AccountID != AccountID && b.SystemStatus == (int)EnumSystemStatus.Active)).ToList();
            }


            return list;
        }

        public List<Role> GetRoleListAll(int? accountMainID)
        {
            List<Role> list = List().Where(a=>a.AccountMainID == accountMainID).ToList();
            if (accountMainID != null && accountMainID.HasValue)
            {
                foreach (var item in list)
                {
                    item.Account_Roles = item.Account_Roles.Where(a => a.Account.Account_AccountMains.Any(b => b.AccountMainID == accountMainID.Value && b.SystemStatus == (int)EnumSystemStatus.Active)).ToList();
                }

            }
            return list;
        }

        public new IQueryable<Role> List()
        {
            return base.List().OrderBy(a => a.ID);
        }

        public IQueryable<Role> GetListByAMID(int AccountMainID)
        {
            return base.List().Where(a => a.AccountMainID == AccountMainID).OrderBy(a => a.ID);
        }

        public IQueryable<Role> GetListNoAdmin(int AccountMainID)
        {
            return base.List().Where(a => a.AccountMainID == AccountMainID&&a.IsCanDelete==true).OrderBy(a => a.ID);
        }

        [Transaction]
        public new Result Delete(int id)
        {
            //判断是否有用户使用了该角色
            Result result = new Result();
            var role = Get(id);
            if (!role.IsCanDelete)
            {
                result.Error = "\"管理员\"是系统初始化数据，无法删除。";
                return result;
            }
            if (role.Account_Roles.Any(a => a.SystemStatus == (int)EnumSystemStatus.Active))
            {
                result.Error = "该角色已经被使用，无法删除。";
                return result;
            }
            result = base.Delete(id);
            return result;
        }


        public Result IsCanFindByUser(int id, bool value)
        {
            var entity = Get(id);
            entity.IsCanFindByUser = value;
            return base.Edit(entity);
        }

        public Result DelteByIDAndAMID(int id, int AccountMainID)
        {
            Result result = new Result();
            var list = List().Where(a => a.ID == id && a.AccountMainID == AccountMainID);
            if (list.Count() > 0)
            {
                var role = Get(id);
                if (!role.IsCanDelete)
                {
                    result.Error = "\"管理员\"是系统初始化数据，无法删除。";
                    return result;
                }
                if (role.Account_Roles.Any(a => a.SystemStatus == (int)EnumSystemStatus.Active))
                {
                    result.Error = "该角色已经被使用，无法删除。";
                    return result;
                }
                result = base.CompleteDelete(id);
                return result;
            }
            else
            {
                result.Error = "删除数据有误。";
                return result;
            }
        }

    }
}
