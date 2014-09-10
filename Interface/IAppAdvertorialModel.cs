using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;

namespace Interface
{
    public interface IAppAdvertorialModel : IBaseModel<AppAdvertorial>
    {
        IQueryable<AppAdvertorial> GetList(int AccountMainID, int AdverTorialType, int EnumAdverClass);

          /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="AID"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        AppAdvertorial GetInfo(int AID, int AMID);

        Result AddAppAdvertorial(AppAdvertorial appadvertorial,int w, int h, int x1, int y1, int tw, int th);

        Result DelAppAdvertorial(int ID, int AdverTorialType);

        Result EditAppAdvertorial(AppAdvertorial appadvertorial, int w, int h, int x1, int y1, int tw, int th);

        int EditAppAdvertorialStick(int ID, int isok, int accoutMainID, int Sort, int AdverTorialType, int EnumAdverClass);

        int EditAppAdvertorialSort(int ID, int AccountMainID, int Sort, int type, int AdverTorialType, int EnumAdverClass);

        /// <summary>
        /// 更改阅读次数
        /// </summary>
        /// <param name="id"></param>
        void BrowseCntADD(int id);


        /// <summary>
        /// 更改活动 修改资讯信息
        /// </summary>
        /// <param name="activityInfo"></param>
        /// <param name="imageUrl"> 默认图片地址</param>
        /// <param name="minimageUrl">默认缩略图地址</param>
        /// <returns></returns>
        Result Editadvert_ByActivityInfo(ActivityInfo activityInfo, string imageUrl, string minimageUrl);


        /// <summary>
        /// 更改调查 修改资讯信息
        /// </summary>
        /// <param name="surveymain"></param>
        /// <param name="imageUrl"> 默认图片地址</param>
        /// <param name="minimageUrl">默认缩略图地址</param>
        /// <returns></returns>
        Result Editadvert_BySurveyMain(SurveyMain surveymain, string imageUrl, string minimageUrl);


         /// <summary>
        /// 删除活动与调查时 删除资讯
        /// </summary>
        /// <param name="EnumAdverURLType"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        Result DelAppadvertorial_byUrlType(int EnumAdverURLType, int ID);



        /// <summary>
        /// 查询是否已经生产资讯
        /// </summary>
        /// <param name="id">活动 或调查 ID</param>
        /// <param name="client">客户端类型 EnumAdvertorialUType </param>
        /// <param name="URLType">咨询url类型 EnumAdverURLType </param>
        /// <returns></returns>
        bool CKAppadverBy_clientAndID(int id, int client, int URLType);

         /// <summary>
        /// 获取生成的资讯信息
        /// </summary>
        /// <param name="id">活动 或调查 ID</param>
        /// <param name="client">咨询url类型 EnumAdverURLType </param>
        /// <returns></returns>
        AppAdvertorial GetAppadverBy_clientAndID(int id, int EnumAdverURLType);
    }
}
