-----1.服务器×××上的MSDTC不可用解决办法------
a. 在windows控制面版-->管理工具-->服务-->Distributed Transaction Coordinator-->属性-->启动
b.在CMD下运行"net start msdtc"开启服务后正常。


-----2.无法加载Interop.Scripting-----------
IIS设置程序池-->高级设置-->启用32位应用程序-->ture

-----3.更新数据库-------------------------
update-database

-----4.生成数据库迁移文件-----------------
add-migrations Name