﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Injection;

namespace Business
{
    public class Property_UserModel : BaseModel<Property_User>, IProperty_UserModel
    {
        public IQueryable<Property_User> GetListByAccountMainID(int accountMainID)
        {
            return base.List(true).Where(a => a.AccountMainID == accountMainID);
        }

        [Transaction]
        public new Result Edit(Property_User pu)
        {
            var result = base.Edit(pu);
            if (result.HasError)
            {
                return result;
            }
            if (pu.Property_House != null)
            {
                var property_HouseModel = Factory.Get<IProperty_HouseModel>(SystemConst.IOC_Model.Property_HouseModel);
                result = property_HouseModel.Edit(pu.Property_House);
            }
            return result;
        }

        [Transaction]
        public Result DeleteByPropertyHouseID(int propertyHouseID)
        {
            Result result = new Result();
            try
            {
                if (propertyHouseID > 0)
                {
                    var userLoginInfoIDs = List().Where(a => a.Property_HouseID == propertyHouseID).Select(a => a.UserLoginInfoID).ToList().ConvertToString(",");
                    string sql = "DELETE dbo.Property_User WHERE Property_HouseID=" + propertyHouseID;
                    int i = base.SqlExecute(sql);
                    if (i > 0 && userLoginInfoIDs.Length > 0)
                    {
                        sql = " UPDATE dbo.UserLoginInfo SET Phone=NULL ,Email =NULL WHERE ID in (" + userLoginInfoIDs + ")";
                        base.SqlExecute(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 根据房号获取物业登记的业主信息
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public List<Property_User> GetUserPhoneByRoomNum(int amid, string roomNum)
        {
            return GetListByAccountMainID(amid).Where(a => a.Property_House.RoomNumber == roomNum).ToList();
        }

        /// <summary>
        /// 根据业主电话获取相关信息
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public List<Property_User> GetHouseByUserPhone(int amid, string userPhone)
        {
            return GetListByAccountMainID(amid).Where(a => a.Phone == userPhone).ToList();
        }

        [Transaction]
        public Result EditUserLoginInfoID(int amid, int userLoginInfoID, Property_User pu)
        {
            Result result = new Result();
            try
            {
                string sql = string.Format("UPDATE dbo.Property_User SET UserLoginInfoID={0},UserName='{1}',Email='{2}' WHERE Phone='{3}' AND AccountMainID={4}", userLoginInfoID,pu.UserName, pu.Email,pu.Phone, amid);
                base.SqlExecute(sql);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }


        public List<Property_User> GetHouseByPropertyHouseID(int phid, int accountMainID)
        {
            return List().Where(a => a.Property_HouseID == phid && a.AccountMainID == accountMainID).ToList();
        }


        //public Result AddList(List<Property_User_Temp> userList)
        //{
        //    var property_HouseModel = Factory.Get<IProperty_HouseModel>(SystemConst.IOC_Model.Property_HouseModel);
        //    foreach (var item in userList)
        //    {
        //        var ph= property_HouseModel.GetByHouseShortNum(item.HouseShortNum);
        //        if (ph != null) {
        //            item.Property_HouseID = ph.ID;
        //        }
        //    }
        //}

        /// <summary>
        /// 根据电话获取用户信息
        /// </summary>
        /// <param name="Phone"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public Property_User getHoust_ByPhone(string Phone, int AMID)
        {
            var item = List().Where(a => a.AccountMainID == AMID && a.Phone==Phone).FirstOrDefault();
            return item;
        }

    }
}
