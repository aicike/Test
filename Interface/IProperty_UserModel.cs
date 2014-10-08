﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IProperty_UserModel : IBaseModel<Property_User>
    {
        IQueryable<Property_User> GetListByAccountMainID(int accountMainID);

        //Result AddList(List<Property_User_Temp> userList);

        Result DeleteByPropertyHouseID(int propertyHouseID);


        /// <summary>
        /// 根据房号获取物业登记的业主信息
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        List<Property_User> GetUserPhoneByRoomNum(int amid, string roomNum);

        /// <summary>
        /// 根据业主电话获取相关信息
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        List<Property_User> GetHouseByUserPhone(int amid, string userPhone);

        /// <summary>
        /// 根据房屋ID获取业主信息
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        List<Property_User> GetHouseByPropertyHouseID(int phid, int accountMainID);

        /// <summary>
        /// 修改业主所关联的账号信息
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="amid"></param>
        /// <param name="userLoginInfoID"></param>
        /// <returns></returns>
        Result EditUserLoginInfoID(int amid, int userLoginInfoID,Property_User pu);

        /// <summary>
        /// 根据电话获取用户信息
        /// </summary>
        /// <param name="Phone"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        Property_User getHoust_ByPhone(string Phone, int AMID);
    }
}
