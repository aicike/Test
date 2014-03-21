-----1.服务器×××上的MSDTC不可用解决办法------
a. 在windows控制面版-->管理工具-->服务-->Distributed Transaction Coordinator-->属性-->启动
b.在CMD下运行"net start msdtc"开启服务后正常。


-----2.无法加载Interop.Scripting-----------
IIS设置程序池-->高级设置-->启用32位应用程序-->ture

-----3.更新数据库-------------------------
update-database

-----4.生成数据库迁移文件-----------------
add-migrations Name


-----5.聊天 附件存储位置----------------------
WebConfig中UpLodeFile节点值+“所属售楼部ID”+“.Message/”
售楼部+“Account/”+当前售楼账号ID
	图片+“/Image”
	视频+“/Video”
	语音+“/Voice”
	头像+“/HeadImage”

用户+“User/”+当前用户账号ID
	图片+“/Image”
	视频+“/Video”
	语音+“/Voice”
	头像+“/HeadImage”

----6.jquery mobile 主题编辑器
http://themeroller.jquerymobile.com/


----7.短域名需要配置wcf

<bindings>
      <basicHttpBinding>
        <binding name="BasicHttpBinding_IShortURLService" closeTimeout="00:01:00"
          openTimeout="00:01:00" receiveTimeout="00:10:00" sendTimeout="00:01:00"
          allowCookies="false" bypassProxyOnLocal="false" hostNameComparisonMode="StrongWildcard"
          maxBufferSize="65536" maxBufferPoolSize="524288" maxReceivedMessageSize="65536"
          messageEncoding="Text" textEncoding="utf-8" transferMode="Buffered"
          useDefaultWebProxy="true">
          <readerQuotas maxDepth="32" maxStringContentLength="8192" maxArrayLength="16384"
            maxBytesPerRead="4096" maxNameTableCharCount="16384" />
          <security mode="None">
            <transport clientCredentialType="None" proxyCredentialType="None"
              realm="" />
            <message clientCredentialType="UserName" algorithmSuite="Default" />
          </security>
        </binding>
      </basicHttpBinding>
    </bindings>
    <client>
      <endpoint address="http://url.imtimely.com/WCF/ShortURLService.svc"
        binding="basicHttpBinding" bindingConfiguration="BasicHttpBinding_IShortURLService"
        contract="Imtimely_ShortURL.IShortURLService" name="BasicHttpBinding_IShortURLService" />
    </client>