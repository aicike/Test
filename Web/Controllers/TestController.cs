using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;
using System.IO;
using agsXMPP;
using agsXMPP.protocol.client;
using System.Threading;
using System.Threading.Tasks;
using Poco;
using Poco.Enum;

namespace Web.Controllers
{

    public class TestController : Controller
    {
        //
        // GET: /Test/


        public ActionResult Index()
        {
            return View();
        }

        public void QrCode()
        {
            QrCodeModel model = new QrCodeModel();


            MemoryStream ms = model.Get_Android_DownloadUrl("PH.00001.1");
            if (null != ms)
            {
                Response.BinaryWrite(ms.ToArray());
            }
        }

        public ActionResult Download()
        {
            var rrr = Request;
            string path = Server.MapPath("~/Download/BuyAHouse.apk");
            //application/vnd.android.package-archive
            return File(path, "application/vnd.android.package-archive");
        }

        public ActionResult GetEndDate()
        {
            CommonModel cm = new CommonModel();
            DateTime dt = Convert.ToDateTime("2013-11-05");
            ViewBag.aa = cm.GetEndDate(dt, 6, 1, 0);
            return View();
        }

        [HttpGet]
        public ActionResult TextSMS()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TextSMS(string txtPhone, string txtContent)
        {
            SMS_Model model = new SMS_Model();
            model.SendSMS(txtPhone, txtContent);
            return View();
        }

        public ActionResult YLDl()
        {

            return View();
        }
        [HttpPost]
        public ActionResult YLdengluWeb(int cnt)
        {
            //DateTime dt1 = DateTime.Now;
            //for (int i = 0; i < cnt; i++)
            //{
            //    TreedCon();
            //}
            //string dt2 = (DateTime.Now - dt1).ToString();
            return JavaScript("alert('11')");
        }
        [HttpPost]
        public ActionResult YLdenglu()
        {
            //TreedCon();


            return JavaScript("alert('OK')");
        }
        [HttpPost]
        public ActionResult YLfaxiaoxi(int cnt)
        {
            //DateTime dt1 = DateTime.Now;
            //for (int i = 0; i < cnt; i++)
            //{
            //    SendMessage();
            //}
            //string dt2 = (DateTime.Now - dt1).ToString();
            return JavaScript("alert('212')");
        }

        public void SendMessage()
        {
          
            if (Connection != null)
            {

                agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message();
                msg.Type = MessageType.chat;
                msg.From = new Jid("u1", "localhost", "resource");
                msg.To = new Jid("s1", "localhost", "u1");

                NewsProtocol np = new NewsProtocol();
                np.MSD = ((int)EnumMessageSendDirection.User_Account).ToString(); //->购房  售楼
                np.MT = "1";  //消息
                np.EID = ((int)EnumMessageType.Text).ToString(); //消息类型
                np.SID = "1";//会话ID
                np.Sendtime = DateTime.Now.ToString(SystemConst.Business.TimeFomatFull);//发送时间
                //文本

                msg.Body = "你好吗 ABCDEFG";


                msg.AddChild(np);
                Connection.Send(msg);

            }
            else
            {
            }
        }

        static XmppClientConnection Connection ;

        //登陆XMPP
        public void TreedCon()
        {

            System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(() =>
                 {
                     Connection = new XmppClientConnection();
                     //登陆方法
                     Connection.OnLogin += new ObjectHandler((object sender) =>
                     {

                         Connection.Show = ShowType.chat;
                         Connection.SendMyPresence();
                     }
                     );
                     //Connection.OnMessage += new XmppClientConnection.MessageHandler(con_OnMessage);
                     //出息消息
                     Connection.Username = "u1";
                     Connection.Server = "127.0.0.1";
                     Connection.Port = 5222;
                     Connection.Resource = "aasdf";

                     Thread mythread = new Thread(connect);
                     mythread.Start();
                     mythread.IsBackground = true;





                 });


            t.Start();

        }

        public void connect()
        {
            Connection.Open();
        }

        public ActionResult JqueryMobileDome()
        {

            return View();
        }


    }
}
