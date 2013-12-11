------默认约束---------------------------------
alter table[User] add default (getdate()) for CreateDate 

alter table[Account_User] add default (getdate()) for CreateDate 

-----------------------------Role--------------------------
--INSERT INTO dbo.Role ( SystemStatus, NAME,IsCanDelete,Token ) VALUES  ( 0,'管理员',0,'AccountAdmin')
--INSERT INTO dbo.Role ( SystemStatus, Name,IsCanDelete,IsCanFindByUser  ) VALUES  ( 0,'销售经理',1,1)
--INSERT INTO dbo.Role ( SystemStatus, NAME,ParentRoleID,IsCanDelete,IsCanFindByUser  ) VALUES  ( 0,'销售代表',1,1,1)

-----------------------------SystemUserMenu--------------------------
SET IDENTITY_INSERT [dbo].SystemUserMenu ON
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 1,0,'首页','System','SystemUserHome','Index',1,NULL)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 2,0,'项目管理','System','AccountMainManage','Index',2,NULL)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 3,0,'角色管理','System','Role','Index',1,2)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 4,0,'系统账号管理','System','SystemUser','Index',3,NULL)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 5,0,'系统角色管理','System','SystemUserRole','Index',4,NULL)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 6,0,'账号管理','System','AccountManage','Index',2,2)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 7,0,'报表管理','System','Report','Index',3,2)
--INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 7,0,'设置','System','SystemSet','Index',6,NULL)
SET IDENTITY_INSERT [dbo].SystemUserMenu OFF

-----------------------------SystemUserMenuOption--------------------------
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,1,'首页','Index',1)

INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'列表','Index',1)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'添加','AddAccountMain',2)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'修改','EditAccountMain',3)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'删除','DeleteAccountMain',4)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'启用/禁用','SetAccountMainStatus',5)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'详细','ViewAccountMain',6)

INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,3,'列表','Index',1)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,3,'添加','Add',2)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,3,'修改','Edit',3)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,3,'删除','Delete',4)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,3,'分配权限','Permission',5)

INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'列表','Index',1)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'添加','Add',2)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'修改','Edit',3)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'删除','Delete',4)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'启用/禁用','SetStatus',5)

INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,5,'列表','Index',1)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,5,'添加','Add',2)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,5,'修改','Edit',3)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,5,'删除','Delete',4)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,5,'启用/禁用','SetStatus',5)


INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,6,'列表','Index',1)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,6,'添加','AddAccount',2)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,6,'修改','EditAccount',3)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,6,'删除','DeleteAccount',4)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,6,'启用/禁用','SetAccountStatus',5)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,6,'详细','View',6)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,6,'重置密码','ResetPwd',7)

INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,7,'报表设置','Index',1)

--INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,7,'设置','Index',1)


-----------------------------SystemUserRole--------------------------
SET IDENTITY_INSERT [dbo].SystemUserRole ON
INSERT INTO dbo.SystemUserRole ( [ID],SystemStatus, Name ) VALUES  ( 1,0,'超级管理员')
--INSERT INTO dbo.SystemUserRole ( [ID],SystemStatus, NAME,ParentSystemUserRoleID ) VALUES  ( 2,0,'业务员',1)
SET IDENTITY_INSERT [dbo].SystemUserRole OFF

-----------------------------SystemUserRole_SystemUserMenu--------------------------
--SET IDENTITY_INSERT [dbo].[SystemUserRole_SystemUserMenu] ON
--INSERT [dbo].[SystemUserRole_SystemUserMenu] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuID]) VALUES (1, 0, 2, 1)
--INSERT [dbo].[SystemUserRole_SystemUserMenu] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuID]) VALUES (2, 0, 2, 2)
--INSERT [dbo].[SystemUserRole_SystemUserMenu] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuID]) VALUES (3, 0, 2, 6)
--SET IDENTITY_INSERT [dbo].[SystemUserRole_SystemUserMenu] OFF

-----------------------------SystemUserRole_Option--------------------------
--SET IDENTITY_INSERT [dbo].[SystemUserRole_Option] ON
--INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (1, 0, 2, 1)
--INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (2, 0, 2, 2)
--INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (3, 0, 2, 3)
--INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (4, 0, 2, 4)
--INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (5, 0, 2, 5)
--INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (6, 0, 2, 6)
--INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (7, 0, 2, 22)
--INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (8, 0, 2, 23)
--INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (9, 0, 2, 24)
--INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (10, 0, 2, 25)
--INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (11, 0, 2, 26)
--INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (12, 0, 2, 27)
--SET IDENTITY_INSERT [dbo].[SystemUserRole_Option] OFF

-----------------------------[Lookup]--------------------------
SET IDENTITY_INSERT [dbo].[Lookup] ON
INSERT [dbo].[Lookup] ([ID], [SystemStatus], [Token], [Value]) VALUES (1,0, N'EnumAccountStatus', N'账号状态')
INSERT [dbo].[Lookup] ([ID], [SystemStatus], [Token], [Value]) VALUES (2,0, N'EnumMessageType', N'消息类型')
INSERT [dbo].[Lookup] ([ID], [SystemStatus], [Token], [Value]) VALUES (3,0, N'EnumClientSystemType', N'系统类型')
INSERT [dbo].[Lookup] ([ID], [SystemStatus], [Token], [Value]) VALUES (4,0, N'EnumClientUserType', N'客户端账号类型')
INSERT [dbo].[Lookup] ([ID], [SystemStatus], [Token], [Value]) VALUES (5,0, N'EnumSoldState', N'房屋售出状态')
INSERT [dbo].[Lookup] ([ID], [SystemStatus], [Token], [Value]) VALUES (6,0, N'EnumBuildingType', N'建筑类别')
INSERT [dbo].[Lookup] ([ID], [SystemStatus], [Token], [Value]) VALUES (7,0, N'EnumDecoration', N'装修情况')
SET IDENTITY_INSERT [dbo].[Lookup] OFF

-----------------------------[LookupOption]--------------------------
SET IDENTITY_INSERT [dbo].[LookupOption] ON
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (1, 0, 1, N'Enabled', N'启用')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (2, 0, 1, N'Disabled', N'禁用')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (3, 0, 2, N'Text', N'文本')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (4, 0, 2, N'Image', N'图片')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (5, 0, 2, N'Video', N'视频')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (6, 0, 2, N'Voice', N'音频')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (7, 0, 2, N'ImageText', N'图文')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (8, 0, 2, N'TextReply', N'自动回复文本')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (9, 0, 3, N'IOS', N'IOS')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (10, 0, 3, N'Android', N'Android')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (11, 0, 3, N'WindowsPhone', N'WindowsPhone')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (12, 0, 3, N'Windows', N'Windows')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (13, 0, 3, N'Other', N'Other')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (14, 0, 4, N'Platform', N'Platform')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (15, 0, 4, N'Account', N'Account')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (16, 0, 4, N'User', N'User')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (17, 0, 5, N'Reserv', N'预留')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (18, 0, 5, N'HasSold', N'已售出')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (19, 0, 5, N'Scheduled', N'已预定')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (20, 0, 5, N'Unsolde', N'未售出')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (21, 0, 6, N'Bottom', N'底层')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (22, 0, 6, N'MultiStorey', N'多层')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (23, 0, 6, N'SmallHighEise', N'小高层')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (24, 0, 6, N'HighLevel', N'高层')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (25, 0, 6, N'SuperHighRise', N'超高层')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (26, 0, 6, N'Compound', N'复式')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (27, 0, 6, N'Villa', N'洋房')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (28, 0, 6, N'GardenVilla', N'花园洋房')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (29, 0, 6, N'Apartment', N'公寓')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (30, 0, 6, N'CAR', N'商住两用')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (31, 0, 6, N'VillaVilla', N'别墅独栋')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (32, 0, 6, N'VillaPlatoon', N'别墅联排')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (33, 0, 6, N'VillaShuangpin', N'别墅双拼')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (34, 0, 6, N'VillaSpell', N'别墅叠拼')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (35, 0, 7, N'water', N'清水')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (36, 0, 7, N'blank', N'毛坯')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (37, 0, 7, N'paperback', N'简装')
INSERT [dbo].[LookupOption] ([ID], [SystemStatus], [LookupID], [Token], [Value]) VALUES (38, 0, 7, N'hardcover', N'精装')





SET IDENTITY_INSERT [dbo].[LookupOption] OFF

-----------------------------[SystemUser]--------------------------
INSERT [dbo].[SystemUser] ([SystemStatus], [Name], [Email], [LoginPwd], [Phone], [AccountStatusID],[SystemUserRoleID]) VALUES (0, N'武双琦', N'176534021@qq.com', N'8ZjHBEPwxeqwCrxssgd2cQ==', N'13474137783', 1,1)
--INSERT [dbo].[SystemUser] ([SystemStatus], [Name], [Email], [LoginPwd], [Phone], [AccountStatusID],[SystemUserRoleID]) VALUES (0, N'业务员1', N'd537@163.com', N'8ZjHBEPwxeqwCrxssgd2cQ==', N'13474137783', 1,2)

-----------------------------[AccountMain]--------------------------
--SET IDENTITY_INSERT [dbo].[AccountMain] ON
--INSERT [dbo].[AccountMain] ([ID], [SystemStatus], [Name],[HostName], [ProvinceID], [CityID], [DistrictID], [Phone], [LogoImageThumbnailPath], [LogoImagePath], [AccountStatusID], [FileLimit],  [SystemUserID], [CreateTime],[SaleAddress]) VALUES (1, 0, N'天朗蓝湖树',N'TLLHS', 27, 288, 2513, N'13474137783', N'~/File/1/Base/20130712164726_80_logo.jpg', N'~/File/1/Base/20130712164726_logo.jpg', 1, 2, 1, CAST(0x0000A1F90114B090 AS DateTime),'科技路西口')
--SET IDENTITY_INSERT [dbo].[AccountMain] OFF


-----------------------------[Account]--------------------------
--SET IDENTITY_INSERT [dbo].[Account] ON
--INSERT [dbo].[Account] ([ID], [SystemStatus], [Name], [LoginPwd], [Phone], [HeadImagePath], [Email], [RoleID], [AccountStatusID], [IsActivated]) VALUES (1, 0, N'王五', N'8ZjHBEPwxeqwCrxssgd2cQ==', N'1111', N'~/File/1/Account/20130712165054_80_QQ20130712095702.jpg', N'wangwu@163.com', 1, 1, 1)
--INSERT [dbo].[Account] ([ID], [SystemStatus], [Name], [LoginPwd], [Phone], [HeadImagePath], [Email], [RoleID], [AccountStatusID], [IsActivated]) VALUES (2, 0, N'张三', N'8ZjHBEPwxeqwCrxssgd2cQ==', N'1111', N'~/File/1/Account/20130712165054_80_QQ20130712095702.jpg', N'zhangsan@163.com', 3, 1, 1)
--INSERT [dbo].[Account] ([ID], [SystemStatus], [Name], [LoginPwd], [Phone], [HeadImagePath], [Email], [RoleID], [AccountStatusID], [IsActivated]) VALUES (3, 0, N'李四（销售经理）', N'8ZjHBEPwxeqwCrxssgd2cQ==', N'123456', N'~/File/1/Account/20130712165157_80_logo2.jpg', N'lisi@163.com', 2, 1, 1)
--SET IDENTITY_INSERT [dbo].[Account] OFF

-----------------------------[Account_AccountMain]--------------------------
--SET IDENTITY_INSERT [dbo].[Account_AccountMain] ON
--INSERT [dbo].[Account_AccountMain] ([ID], [SystemStatus], [AccountID], [AccountMainID]) VALUES (1, 0, 1, 1)
--INSERT [dbo].[Account_AccountMain] ([ID], [SystemStatus], [AccountID], [AccountMainID]) VALUES (2, 0, 2, 1)
--INSERT [dbo].[Account_AccountMain] ([ID], [SystemStatus], [AccountID], [AccountMainID]) VALUES (3, 0, 3, 1)
--SET IDENTITY_INSERT [dbo].[Account_AccountMain] OFF

------------------------------[Group]--------------------------------------------
--INSERT INTO dbo.[Group](SystemStatus ,GroupName ,AccountID ,AccountMainID ,IsDefaultGroup ,IsCanDelete)VALUES  ( 0 ,N'未分组' ,1 ,1 ,1 ,0)
--INSERT INTO dbo.[Group](SystemStatus ,GroupName ,AccountID ,AccountMainID ,IsDefaultGroup ,IsCanDelete)VALUES  ( 0 ,N'黑名单' ,1 ,1 ,0 ,0)
--INSERT INTO dbo.[Group](SystemStatus ,GroupName ,AccountID ,AccountMainID ,IsDefaultGroup ,IsCanDelete)VALUES  ( 0 ,N'未分组' ,2 ,1 ,1 ,0)
--INSERT INTO dbo.[Group](SystemStatus ,GroupName ,AccountID ,AccountMainID ,IsDefaultGroup ,IsCanDelete)VALUES  ( 0 ,N'黑名单' ,2 ,1 ,0 ,0)

------------------------------[Menu]--------------------------------------------
SET IDENTITY_INSERT [dbo].[Menu] ON
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (1, 0, N'首页', NULL, N'Home', N'Index', 1, NULL)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (2, 0, N'用户管理', NULL, N'UserManage', N'Index', 2, NULL)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (3, 0, N'消息管理', NULL, N'Message', N'Index', 3, NULL)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (4, 0, N'素材管理', NULL, N'LibraryImage', N'Index', 4, NULL)

INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (5, 0, N'图片素材', NULL, N'LibraryImage', N'Index', 1, 4)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (6, 0, N'语音素材', NULL, N'LibraryVoice', N'Index', 2, 4)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (7, 0, N'视频素材', NULL, N'LibraryVideo', N'Index', 3, 4)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (8, 0, N'图文素材', NULL, N'LibraryImageText', N'Index', 4, 4)

INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (9, 0, N'账号管理', NULL, N'Account', N'Index', 5, NULL)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (10, 0, N'项目管理', NULL, N'HousesMange', N'Index', 6, NULL)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (11, 0, N'设置', NULL, N'Set', N'Index', 7, NULL)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (12, 0, N'单元管理', NULL, N'HouseInfo', N'Index', 1, 10)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (13, 0, N'房屋管理', NULL, N'HouseInfoDetail', N'Index', 2, 10)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (14, 0, N'户型管理', NULL, N'HouseType', N'Index', 3, 10)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (15, 0, N'安装APP自动回复', NULL, N'InstallAppReply', N'Index', 2, 11)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (16, 0, N'关键词自动回复', NULL, N'KeywordMessage', N'Index', 3, 11)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (17, 0, N'即时消息', NULL, N'InstantMes', N'Index', 1, 2)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (18, 0, N'聊天窗口', NULL, N'SingleChat', N'Index', 2, 2)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (19, 0, N'销售与客户管理', NULL, N'SalesMessage', N'Index', 3, 2)

INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (20, 0, N'基础设置', NULL, N'BasisSet', N'Index', 1, 11)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (21, 0, N'App动态软文', NULL, N'AppAdvertorial', N'Index', 4, 11)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (22, 0, N'App等待画面', NULL, N'AppWaitImg', N'Index', 5, 11)


INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (23, 0, N'产品管理', NULL, N'Product', N'Index', 4, NULL)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (24, 0, N'产品管理', NULL, N'Product', N'Index', 1, 23)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (25, 0, N'类别管理', NULL, N'Classify', N'Index', 2, 23)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (26, 0, N'订单类型', NULL, N'OrderMType', N'Index', 3, 23)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (27, 0, N'节假日管理', NULL, N'Holiday', N'Index', 4, 23)

INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (28, 0, N'订单管理', NULL, N'Order', N'Index', 4, NULL)

INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (29, 0, N'任务通知管理', NULL, N'Task', N'Index', 5, NULL)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (30, 0, N'我的任务通知', NULL, N'MyTask', N'Index', 1, 29)

INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (31, 0, N'角色管理', NULL, N'Character', N'Index', 5, NULL)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (32, 0, N'会员管理', NULL, N'VipMessage', N'Index', 5, NULL)

SET IDENTITY_INSERT [dbo].[Menu] OFF

-----------------------------[MenuOption]--------------------------
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,1,'查看首页','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'用户列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'新建用户分组','AddGroup',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'编辑用户分组','EditGroup',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'删除用户分组','DeleteGroup',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'更改用户分组','ChangeGroup',5)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'修改用户信息','EditUser',6)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'查看用户信息','ViewUser',7)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,3,'新建消息','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,3,'发送消息','SendText',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,3,'已发送','History',3)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,3,'删除发送消息','Delete',4)

--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 1,4,'文本','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 1,4,'添加文本','Add',2)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 1,4,'修改文本','Edit',3)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'删除文本','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,5,'查看图片','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,5,'上传图片','Upload',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,5,'修改标题','ReName',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,5,'删除图片','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,6,'查看语音','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,6,'上传语音','Upload',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,6,'修改标题','ReName',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,6,'删除语音','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,7,'查看视频','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,7,'上传视频','Upload',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,7,'修改标题','ReName',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,7,'删除视频','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,8,'查看图文','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,8,'新建单图文','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,8,'新建多图文','MoreAdd',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,8,'修改图文','Edit',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,8,'删除图文','Delete',5)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,9,'查看账号列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,9,'添加账号','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,9,'修改账号','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,9,'删除账号','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,9,'启用/禁用账号','SetAccountStatus',5)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,9,'重置密码','ResetPwd',6)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,10,'项目列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,10,'查看项目详细信息','Select',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,10,'添加项目','Add',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,10,'修改项目','Edit',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,10,'删除项目','Delete',5)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,11,'个人信息','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,12,'单元列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,12,'查看单元详细信息','Select',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,12,'添加单元','Add',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,12,'修改单元','Edit',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,12,'删除单元','Delete',5)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,13,'房屋列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,13,'查看房屋详细信息','Select',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,13,'添加房屋','Add',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,13,'修改房屋','Edit',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,13,'删除房屋','Delete',5)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,14,'户型列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,14,'查看户型详细信息','Select',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,14,'添加户型','Add',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,14,'修改户型','Edit',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,14,'删除户型','Delete',5)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,15,'安装APP自动回复','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,15,'保存安装APP自动回复','AddOrUpd',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,15,'删除安装APP自动回复','Delete',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'关键词自动回复','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'保存关键词自动回复','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'修改关键词自动回复','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'删除关键词自动回复','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,17,'即时消息列表','index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,18,'聊天窗口','index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,19,'销售与客户关系管理列表','index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,19,'销售与客户聊天记录','SelectAtoUmes',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,20,'基础设置','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'App动态软文','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'添加软文','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'修改软文','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'删除软文','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,22,'App等待画面','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,24,'产品列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,24,'添加产品','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,24,'修改产品','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,24,'删除产品','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,25,'分类列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,25,'添加分类','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,25,'修改分类','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,25,'删除分类','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,26,'订单类型列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,26,'添加订单类型','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,26,'修改订单类型','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,26,'删除订单类型','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,27,'节假日列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,27,'添加节假日','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,27,'修改节假日','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,27,'删除节假日','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,28,'订单列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,28,'取消订单','Cancel',2)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,29,'任务列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,29,'指派任务','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,29,'修改指派任务','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,29,'删除指派任务','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,30,'我的任务','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,30,'任务详细','Detail',2)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,31,'角色列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,31,'添加角色','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,31,'修改角色','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,31,'删除角色','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,31,'权限设置','Power',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,32,'会员列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,32,'绑定会员','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,32,'修改会员','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,32,'删除会员','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,32,'会员充值','Recharge',5)



-----------------------------RoleMenu--------------------------
--SET IDENTITY_INSERT [dbo].[RoleMenu] ON
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (1, 0, 1, 1)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (2, 0, 1, 2)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (3, 0, 1, 3)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (4, 0, 1, 4)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (5, 0, 1, 5)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (6, 0, 1, 6)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (7, 0, 1, 7)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (8, 0, 1, 8)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (9, 0, 1, 9)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (10, 0, 1, 10)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (11, 0, 1, 11)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (12, 0, 1, 12)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (13, 0, 1, 13)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (14, 0, 1, 14)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (15, 0, 1, 15)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (16, 0, 1, 16)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (17, 0, 1, 17)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (18, 0, 1, 18)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (19, 0, 1, 19)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (20, 0, 1, 20)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (21, 0, 3, 1)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (22, 0, 3, 2)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (23, 0, 3, 19)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (24, 0, 3, 18)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (25, 0, 3, 17)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (26, 0, 3, 3)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (27, 0, 3, 5)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (28, 0, 3, 8)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (29, 0, 3, 7)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (30, 0, 3, 6)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (31, 0, 3, 9)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (32, 0, 3, 10)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (33, 0, 3, 14)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (34, 0, 3, 13)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (35, 0, 3, 12)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (36, 0, 3, 11)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (37, 0, 3, 20)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (38, 0, 3, 16)
--INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (39, 0, 3, 15)
--SET IDENTITY_INSERT [dbo].[RoleMenu] OFF

-----------------------------RoleOption--------------------------
--SET IDENTITY_INSERT [dbo].[RoleOption] ON
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (1, 0, 1, 1)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (2, 0, 1, 2)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (3, 0, 1, 3)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (4, 0, 1, 4)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (5, 0, 1, 5)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (6, 0, 1, 6)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (7, 0, 1, 7)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (8, 0, 1, 8)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (9, 0, 1, 9)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (10, 0, 1, 10)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (11, 0, 1, 11)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (12, 0, 1, 12)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (13, 0, 1, 13)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (14, 0, 1, 14)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (15, 0, 1, 15)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (16, 0, 1, 16)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (17, 0, 1, 17)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (18, 0, 1, 18)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (19, 0, 1, 19)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (20, 0, 1, 20)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (21, 0, 1, 21)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (22, 0, 1, 22)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (23, 0, 1, 23)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (24, 0, 1, 24)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (25, 0, 1, 25)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (26, 0, 1, 26)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (27, 0, 1, 27)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (28, 0, 1, 28)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (29, 0, 1, 29)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (30, 0, 1, 30)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (31, 0, 1, 31)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (32, 0, 1, 32)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (33, 0, 1, 33)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (34, 0, 1, 34)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (35, 0, 1, 35)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (36, 0, 1, 36)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (37, 0, 1, 37)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (38, 0, 1, 38)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (39, 0, 1, 39)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (40, 0, 1, 40)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (41, 0, 1, 41)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (42, 0, 1, 42)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (43, 0, 1, 43)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (44, 0, 1, 44)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (45, 0, 1, 45)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (46, 0, 1, 46)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (47, 0, 1, 47)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (48, 0, 1, 48)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (49, 0, 1, 49)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (50, 0, 1, 50)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (51, 0, 1, 51)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (52, 0, 1, 52)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (53, 0, 1, 53)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (54, 0, 1, 54)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (55, 0, 1, 55)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (56, 0, 1, 56)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (57, 0, 1, 57)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (58, 0, 1, 58)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (59, 0, 1, 59)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (60, 0, 1, 60)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (61, 0, 1, 61)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (62, 0, 1, 62)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (63, 0, 1, 63)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (64, 0, 1, 64)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (65, 0, 1, 65)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (66, 0, 1, 66)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (67, 0, 1, 67)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (68, 0, 1, 68)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (69, 0, 1, 69)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (70, 0, 1, 70)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (71, 0, 3, 1)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (72, 0, 3, 2)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (73, 0, 3, 3)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (74, 0, 3, 4)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (75, 0, 3, 5)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (76, 0, 3, 6)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (77, 0, 3, 7)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (78, 0, 3, 8)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (79, 0, 3, 68)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (80, 0, 3, 69)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (81, 0, 3, 67)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (82, 0, 3, 66)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (83, 0, 3, 9)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (84, 0, 3, 10)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (85, 0, 3, 11)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (86, 0, 3, 16)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (87, 0, 3, 17)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (88, 0, 3, 18)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (89, 0, 3, 19)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (90, 0, 3, 28)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (91, 0, 3, 29)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (92, 0, 3, 30)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (93, 0, 3, 31)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (94, 0, 3, 32)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (95, 0, 3, 24)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (96, 0, 3, 25)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (97, 0, 3, 26)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (98, 0, 3, 27)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (99, 0, 3, 20)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (100, 0, 3, 21)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (101, 0, 3, 22)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (102, 0, 3, 23)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (103, 0, 3, 33)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (104, 0, 3, 34)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (105, 0, 3, 35)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (106, 0, 3, 36)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (107, 0, 3, 37)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (108, 0, 3, 38)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (109, 0, 3, 39)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (110, 0, 3, 40)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (111, 0, 3, 41)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (112, 0, 3, 42)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (113, 0, 3, 54)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (114, 0, 3, 55)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (115, 0, 3, 56)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (116, 0, 3, 57)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (117, 0, 3, 58)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (118, 0, 3, 49)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (119, 0, 3, 50)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (120, 0, 3, 51)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (121, 0, 3, 52)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (122, 0, 3, 53)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (123, 0, 3, 44)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (124, 0, 3, 45)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (125, 0, 3, 46)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (126, 0, 3, 47)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (127, 0, 3, 48)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (128, 0, 3, 43)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (129, 0, 3, 70)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (130, 0, 3, 62)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (131, 0, 3, 63)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (132, 0, 3, 64)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (133, 0, 3, 65)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (134, 0, 3, 59)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (135, 0, 3, 60)
--INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (136, 0, 3, 61)
--SET IDENTITY_INSERT [dbo].[RoleOption] OFF

------------------------------[UserLoginInfo]---------------------------------
--SET IDENTITY_INSERT [dbo].[UserLoginInfo] ON
----INSERT [dbo].[UserLoginInfo] ([ID], [SystemStatus], [Name], [Email], [LoginPwd]) VALUES (1, 0, N'wushuangqi', N'd537@163.com', N'8ZjHBEPwxeqwCrxssgd2cQ==')
--SET IDENTITY_INSERT [dbo].[UserLoginInfo] OFF
------------------------------[User]---------------------------------
--SET IDENTITY_INSERT [dbo].[User] ON
----[ID], [SystemStatus], [Name], [Phone], [AccountStatusID], [IdentityCard], [AccountMainID],UserLoginInfoID) VALUES (1, 0, N'武双琦', NULL, 1, NULL, 1,1)
--SET IDENTITY_INSERT [dbo].[User] OFF
------------------------------[ClientInfo]---------------------------------
--SET IDENTITY_INSERT [dbo].[ClientInfo] ON
----INSERT [dbo].[ClientInfo] ([ID], [SystemStatus], [IMEI], [EnumClientSystemTypeID], [SetupTiem], [EnumClientUserTypeID], [Tag],[ClientID], [EntityID]) VALUES (1, 0, NULL, 10, CAST(0x0000A1FE00000000 AS DateTime), 16, NULL,'03b2ac5b2c55619f7c29f87eabff771f', 1)
--SET IDENTITY_INSERT [dbo].[ClientInfo] OFF
------------------------------[Account_User]---------------------------------
--INSERT INTO dbo.Account_User( SystemStatus, AccountID, UserID,GroupID ) VALUES  ( 0,1,1,1)

------------------------------Other---------------------------------
CREATE INDEX IX_HostName ON AccountMain (HostName)

---Insert Function

GO
Create function SetSerialNumber(@prefix varchar(10),@digit int,@AccountMainID int)
returns varchar(100)
as
begin
	declare @predate varchar(12)= convert(varchar(50),getdate(),112)
	declare @Number varchar(100) = UPPER(@prefix)+@predate

	declare @Prefixcnt int = DATALENGTH(@prefix)
	declare @MaxNumber varchar(100) 
	select @MaxNumber =Max(OrderNum) from dbo.[Order] where AccountMainID= @AccountMainID and OrderNum like '%'+@Number+'%'  
	
	declare @lasNum varchar(50)='0'
	if(@MaxNumber is null)
		begin
			declare @i int = 1
			while @i<@digit
				begin
					if(@i=@digit-1)
						begin
							set @lasNum =@lasNum+'1'
						end
					else
						begin
							set @lasNum =@lasNum+'0'
						end
					
					set @i=@i+1
				end
			set @Number = @Number+@lasNum
		end
	else
		begin
			declare @lastNumber int = replace(@MaxNumber,@Number,'')
			set @lastNumber = @lastNumber+1
			declare @l int = 1
			while @l<(@digit-DATALENGTH(convert(varchar(10),@lastNumber)))
				begin
					set @lasNum ='0'+@lasNum
					set @l=@l+1
				end
			set @Number = @Number+@lasNum+convert(varchar(10),@lastNumber)
		end

	return @Number
end

GO




--INSERT VIEW----[View_UserUnreadMessage]----------------------------------------------------------


/****** Object:  View [dbo].[View_UserUnreadMessage]    Script Date: 09/04/2013 10:46:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[View_UserUnreadMessage]
AS
SELECT     CASE WHEN a.fromaccountid <> 0 THEN a.fromaccountid ELSE a.fromuserid END AS FromID, COUNT(*) AS Messagecnt, MAX(SendTime) AS SendTime,
                          (SELECT     ToUserID
                            FROM          dbo.PendingMessages
                            WHERE      (SendTime = MAX(a.SendTime))) AS ToUserID,
                          (SELECT     EnumMessageTypeID
                            FROM          dbo.PendingMessages AS PendingMessages_4
                            WHERE      (SendTime = MAX(a.SendTime))) AS EID,
                          (SELECT     [Content]
                            FROM          dbo.PendingMessages AS PendingMessages_3
                            WHERE      (SendTime = MAX(a.SendTime))) AS Content,
                          (SELECT     MSD
                            FROM          dbo.PendingMessages AS PendingMessages_2
                            WHERE      (SendTime = MAX(a.SendTime))) AS MSD,
                          (SELECT     ConversationID
                            FROM          dbo.PendingMessages AS PendingMessages_1
                            WHERE      (SendTime = MAX(a.SendTime))) AS ConversationID
FROM         dbo.PendingMessages AS a
WHERE     (ToUserID <> 0)
GROUP BY FromAccountID, FromUserID

GO


---INSERT VIEW ----View_AccountUnreadMessage-----------------------------------------------


/****** Object:  View [dbo].[View_AccountUnreadMessage]    Script Date: 09/04/2013 10:46:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[View_AccountUnreadMessage]
AS
SELECT     CASE WHEN a.fromaccountid <> 0 THEN a.fromaccountid ELSE a.fromuserid END AS FromID, COUNT(*) AS Messagecnt, MAX(SendTime) AS SendTime,
                          (SELECT     ToAccountID
                            FROM          dbo.PendingMessages
                            WHERE      (SendTime = MAX(a.SendTime))) AS ToAccountID,
                          (SELECT     EnumMessageTypeID
                            FROM          dbo.PendingMessages AS PendingMessages_4
                            WHERE      (SendTime = MAX(a.SendTime))) AS EID,
                          (SELECT     [Content]
                            FROM          dbo.PendingMessages AS PendingMessages_3
                            WHERE      (SendTime = MAX(a.SendTime))) AS Content,
                          (SELECT     MSD
                            FROM          dbo.PendingMessages AS PendingMessages_2
                            WHERE      (SendTime = MAX(a.SendTime))) AS MSD,
                          (SELECT     ConversationID
                            FROM          dbo.PendingMessages AS PendingMessages_1
                            WHERE      (SendTime = MAX(a.SendTime))) AS ConversationID
FROM         dbo.PendingMessages AS a
WHERE     (ToAccountID <> 0)
GROUP BY FromAccountID, FromUserID

GO


---INSERT VIEW ----[View_AccountMessageList]---------------------------------------------



/****** Object:  View [dbo].[View_AccountMessageList]    Script Date: 09/04/2013 18:07:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[View_AccountMessageList]
AS
SELECT     TOP (100) PERCENT x.FromUserID, x.MData, x.NoSend, x.Tcontent, x.ConversationID, x.EnumMessageTypeID, y.Name AS UMark, z.Name, z.HeadImagePath
FROM         (SELECT     FromUserID, MAX(SendTime) AS MData,
                                                  (SELECT     COUNT(ID) AS Expr1
                                                    FROM          dbo.Message
                                                    WHERE      (IsReceive = 0) AND (ToAccountID <> 0)) AS NoSend,
                                                  (SELECT     TextContent
                                                    FROM          dbo.Message AS Message_3
                                                    WHERE      (ID = MAX(a.ID))) AS Tcontent,
                                                  (SELECT     ConversationID
                                                    FROM          dbo.Message AS Message_2
                                                    WHERE      (ID = MAX(a.ID))) AS ConversationID,
                                                  (SELECT     EnumMessageTypeID
                                                    FROM          dbo.Message AS Message_1
                                                    WHERE      (ID = MAX(a.ID))) AS EnumMessageTypeID
                       FROM          dbo.Message AS a
                       GROUP BY FromUserID) AS x INNER JOIN
                      dbo.[User] AS y ON x.FromUserID = y.ID INNER JOIN
                      dbo.UserLoginInfo AS z ON y.UserLoginInfoID = z.ID
ORDER BY x.MData DESC
