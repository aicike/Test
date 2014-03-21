using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ShortURL.WCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IShortURLService”。
    [ServiceContract]
    public interface IShortURLService
    {
        /// <summary>
        /// 获取短域名 返回字符串数组， 0：异常消息 1：短域名
        /// </summary>
        /// <param name="fullUrl"></param>
        /// <returns></returns>
        [OperationContract]
        string [] GetURL(string fullUrl);

        /// <summary>
        /// 删除短域名
        /// </summary>
        /// <param name="shortURL"></param>
        [OperationContract]
        void DeleteShortURL(string shortURL);
    }
}
