using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interface;
using Poco;
using Injection;
using Poco.WebAPI_Poco;
using System.Text;
using Poco.Enum;
using Business;

namespace Web.Controllers
{
    public class WebRequest_AutoMessageController : Controller
    {
        /// <summary>
        /// 获取首次引导信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetFirstAutoMessageList(int accountMainID)
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            var list = autoMessage_KeywordModel.GetFirstAutoMessage(accountMainID);
            Result result = new Result();
            List<App_AutoMessage> appList = new List<App_AutoMessage>();
            foreach (var item in list)
            {
                appList.Add(new App_AutoMessage
                {
                    ID = item.ID,
                    RuleName = item.RuleName
                });
            }
            result.Entity = appList;
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 首次安装APP回复
        /// </summary>
        /// <returns></returns>
        public string GetInitialMsg(int accountMainID)
        {
            var autoMessageAddModel = Factory.Get<IAutoMessage_AddModel>(SystemConst.IOC_Model.AutoMessage_AddModel);
            var entity = autoMessageAddModel.Get(accountMainID);
            string msg = entity.Content ?? "";
            return msg;
        }

        /// <summary>
        /// 根据编号返回答案
        /// </summary>
        [HttpPost]
        public string GetAutoMessageByID(int autoMessageID, int accountMainID, int userID)
        {
            try
            {
                SaveQuestion(accountMainID, userID, autoMessageID, null);
            }
            catch (Exception ex) { }
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            Result result = new Result();
            try
            {
                var obj = autoMessage_KeywordModel.Get(autoMessageID);
                var otherReply = obj.KeywordAutoMessages.OrderBy(a => a.Order);
                result.Entity = App_AutoMessageReplyContentModel.GetReplayList(otherReply.ToList());
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                throw;
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }


        /// <summary>
        /// 根据关键字返回答案
        /// </summary>
        [HttpPost]
        public string GetAutoMessageByKey(int accountMainID, int userID, string key)
        {
            try
            {
                SaveQuestion(accountMainID, userID, null, key);
            }
            catch (Exception ex) { }
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            Result result = new Result();
            try
            {
                var list = autoMessage_KeywordModel.GetAutoMessageByKey(accountMainID, key);
                List<App_AutoMessage> messageList = new List<App_AutoMessage>();
                if (list.Count == 0)
                {
                    result.EntityType = "App_AutoMessage";
                    return Newtonsoft.Json.JsonConvert.SerializeObject(result);
                }

                if (list.Count > 1)
                {
                    //多条，推送问题
                    foreach (var messageKey in list)
                    {
                        messageList.Add(new App_AutoMessage
                        {
                            ID = messageKey.ID,
                            RuleName = messageKey.RuleName
                        });
                    }
                    result.EntityType = "App_AutoMessage";
                    result.Entity = messageList;
                    return Newtonsoft.Json.JsonConvert.SerializeObject(result);
                }
                else
                {
                    //单条，推送答案
                    var replay = list.FirstOrDefault();
                    var otherReply = replay.KeywordAutoMessages.OrderBy(a => a.Order);
                    result.EntityType = "App_AutoMessageReplyContent";
                    result.Entity = App_AutoMessageReplyContentModel.GetReplayList(otherReply.ToList());
                    return Newtonsoft.Json.JsonConvert.SerializeObject(result);

                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// Andriod设置推送消息已读
        /// </summary>
        public void SetPushStatusToRead(int pushID, int enumClientUserType, string clientID)
        {
            var pushDetailModel = Factory.Get<IPushMsgDetailModel>(SystemConst.IOC_Model.PushMsgDetailModel);
            pushDetailModel.SetPushStatusToRead(pushID, (EnumClientUserType)enumClientUserType, clientID);
        }

        /// <summary>
        /// Android获取推送消息
        /// </summary>
        public string GetNotRecivePushMessage(int accountMainID, int enumClientUserType, string clientID)
        {
            var pushDetailModel = Factory.Get<IPushMsgDetailModel>(SystemConst.IOC_Model.PushMsgDetailModel);
            var pushDetailList = pushDetailModel.GetNotRecivePushMessage(accountMainID, (EnumClientUserType)enumClientUserType, clientID);//未读推送消息
            Result result = new Result();
            List<App_AutoMessageReplyContent> pushMessage = new List<App_AutoMessageReplyContent>();//封装的消息
            List<KeywordAutoMessage> list = new List<KeywordAutoMessage>();//具体消息

            var msgType_Text = LookupFactory.GetLookupOptionByToken(EnumMessageType.Text);
            var msgType_Image = LookupFactory.GetLookupOptionByToken(EnumMessageType.Image);
            var msgType_Voice = LookupFactory.GetLookupOptionByToken(EnumMessageType.Voice);
            var msgType_Video = LookupFactory.GetLookupOptionByToken(EnumMessageType.Video);
            var msgType_ImageText = LookupFactory.GetLookupOptionByToken(EnumMessageType.ImageText);

            if (pushDetailList != null && pushDetailList.Count > 0)
            {
                string dataFormat = "yyyy-MM-dd hh:mm:ss";
                foreach (var item in pushDetailList)
                {
                    switch (item.PushMsg.EnumMessageType)
                    {
                        case (int)EnumMessageType.Text:
                            list.Add(new KeywordAutoMessage()
                            {
                                ID = 0,
                                EnumMessageType = msgType_Text,
                                TextReply = item.PushMsg.Text,
                                SendTime = item.PushMsg.PushTime.ToString(dataFormat)
                            });
                            break;
                        case (int)EnumMessageType.Image:
                            list.Add(new KeywordAutoMessage()
                            {
                                EnumMessageType = msgType_Image,
                                MessageID = item.PushMsg.LibraryID.Value,
                                SendTime = item.PushMsg.PushTime.ToString(dataFormat)
                            });
                            break;
                        case (int)EnumMessageType.Voice:
                            list.Add(new KeywordAutoMessage()
                            {
                                EnumMessageType = msgType_Voice,
                                MessageID = item.PushMsg.LibraryID.Value,
                                SendTime = item.PushMsg.PushTime.ToString(dataFormat)
                            });
                            break;
                        case (int)EnumMessageType.Video:
                            list.Add(new KeywordAutoMessage()
                            {
                                EnumMessageType = msgType_Video,
                                MessageID = item.PushMsg.LibraryID.Value,
                                SendTime = item.PushMsg.PushTime.ToString(dataFormat)
                            });
                            break;
                        case (int)EnumMessageType.ImageText:
                            list.Add(new KeywordAutoMessage()
                            {
                                EnumMessageType = msgType_ImageText,
                                MessageID = item.PushMsg.LibraryID.Value,
                                SendTime = item.PushMsg.PushTime.ToString(dataFormat)
                            });
                            break;
                    }
                }
                pushMessage = App_AutoMessageReplyContentModel.GetReplayList(list);
            }
            result.Entity = pushMessage;
            return Newtonsoft.Json.JsonConvert.SerializeObject(result); ;
        }

        private void SaveQuestion(int accountMainID, int userID, int? autoMessageID, string question)
        {
            var model = Factory.Get<IAutoMessage_UserModel>(SystemConst.IOC_Model.AutoMessage_UserModel);
            AutoMessage_User am = new AutoMessage_User();
            am.UserID = userID;
            am.SendTime = DateTime.Now;
            am.AccountMainID = accountMainID;
            if (autoMessageID.HasValue)
            {
                am.AutoMessage_KeywordID = autoMessageID;
            }
            if (string.IsNullOrEmpty(question) == false)
            {
                am.Question = question;
            }
            model.Add(am);
        }
    }
}
