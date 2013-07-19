-----------------------------Role--------------------------
INSERT INTO dbo.Role ( SystemStatus, NAME,IsCanDelete ) VALUES  ( 0,'管理员',0)
INSERT INTO dbo.Role ( SystemStatus, Name,IsCanDelete  ) VALUES  ( 0,'销售经理',1)
INSERT INTO dbo.Role ( SystemStatus, NAME,ParentRoleID,IsCanDelete  ) VALUES  ( 0,'销售代表',1,1)

-----------------------------SystemUserMenu--------------------------
SET IDENTITY_INSERT [dbo].SystemUserMenu ON
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 1,0,'首页','System','SystemUserHome','Index',1,NULL)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 2,0,'售楼部管理','System','AccountMainManage','Index',2,NULL)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 3,0,'售楼部角色管理','System','Role','Index',3,NULL)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 4,0,'系统账号管理','System','SystemUser','Index',4,NULL)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 5,0,'系统角色管理','System','SystemUserRole','Index',5,NULL)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 6,0,'设置','System','Set','Index',6,NULL)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 7,0,'售楼部账号管理','System','AccountManage','Index',1,2)
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

INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,6,'设置','Index',1)

INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,7,'列表','Index',1)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,7,'添加','AddAccount',2)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,7,'修改','EditAccount',3)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,7,'删除','DeleteAccount',4)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,7,'启用/禁用','SetAccountStatus',5)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,7,'详细','View',6)


-----------------------------SystemUserRole--------------------------
SET IDENTITY_INSERT [dbo].SystemUserRole ON
INSERT INTO dbo.SystemUserRole ( [ID],SystemStatus, Name ) VALUES  ( 1,0,'超级管理员')
INSERT INTO dbo.SystemUserRole ( [ID],SystemStatus, NAME,ParentSystemUserRoleID ) VALUES  ( 2,0,'业务员',1)
SET IDENTITY_INSERT [dbo].SystemUserRole OFF

-----------------------------SystemUserRole_SystemUserMenu--------------------------
SET IDENTITY_INSERT [dbo].[SystemUserRole_SystemUserMenu] ON
INSERT [dbo].[SystemUserRole_SystemUserMenu] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuID]) VALUES (1, 0, 2, 1)
INSERT [dbo].[SystemUserRole_SystemUserMenu] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuID]) VALUES (2, 0, 2, 2)
INSERT [dbo].[SystemUserRole_SystemUserMenu] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuID]) VALUES (3, 0, 2, 7)
SET IDENTITY_INSERT [dbo].[SystemUserRole_SystemUserMenu] OFF

-----------------------------SystemUserRole_Option--------------------------
SET IDENTITY_INSERT [dbo].[SystemUserRole_Option] ON
INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (1, 0, 2, 1)
INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (2, 0, 2, 2)
INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (3, 0, 2, 3)
INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (4, 0, 2, 4)
INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (5, 0, 2, 5)
INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (6, 0, 2, 6)
INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (7, 0, 2, 7)
INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (8, 0, 2, 22)
INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (9, 0, 2, 23)
INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (10, 0, 2, 24)
INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (11, 0, 2, 25)
INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (12, 0, 2, 26)
INSERT [dbo].[SystemUserRole_Option] ([ID], [SystemStatus], [SystemUserRoleID], [SystemUserMenuOptionID]) VALUES (13, 0, 2, 27)
SET IDENTITY_INSERT [dbo].[SystemUserRole_Option] OFF

-----------------------------[Lookup]--------------------------
SET IDENTITY_INSERT [dbo].[Lookup] ON
INSERT [dbo].[Lookup] ([ID], [SystemStatus], [Token], [Value]) VALUES (1,0, N'EnumAccountStatus', N'账号状态')
INSERT [dbo].[Lookup] ([ID], [SystemStatus], [Token], [Value]) VALUES (2,0, N'EnumMessageType', N'消息类型')
INSERT [dbo].[Lookup] ([ID], [SystemStatus], [Token], [Value]) VALUES (3,0, N'EnumClientSystemType', N'系统类型')
INSERT [dbo].[Lookup] ([ID], [SystemStatus], [Token], [Value]) VALUES (4,0, N'EnumClientUserType', N'客户端账号类型')
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
SET IDENTITY_INSERT [dbo].[LookupOption] OFF

-----------------------------[SystemUser]--------------------------
INSERT [dbo].[SystemUser] ([SystemStatus], [Name], [Email], [LoginPwd], [Phone], [AccountStatusID],[SystemUserRoleID]) VALUES (0, N'武双琦', N'176534021@qq.com', N'8ZjHBEPwxeqwCrxssgd2cQ==', N'13474137783', 1,1)
INSERT [dbo].[SystemUser] ([SystemStatus], [Name], [Email], [LoginPwd], [Phone], [AccountStatusID],[SystemUserRoleID]) VALUES (0, N'业务员1', N'd537@163.com', N'8ZjHBEPwxeqwCrxssgd2cQ==', N'13474137783', 1,2)

-----------------------------[AccountMain]--------------------------
SET IDENTITY_INSERT [dbo].[AccountMain] ON
INSERT [dbo].[AccountMain] ([ID], [SystemStatus], [Name],[HostName], [ProvinceID], [CityID], [DistrictID], [Phone], [LogoImageThumbnailPath], [LogoImagePath], [AccountStatusID], [FileLimit], [AccountMainExpandInfoID], [SystemUserID], [CreateTime]) VALUES (1, 0, N'天朗蓝湖树',N'TLLHS', 27, 288, 2513, N'13474137783', N'~\File\1\Base\20130712164726_80_logo.jpg', N'~\File\1\Base\20130712164726_logo.jpg', 1, 2, 0, 1, CAST(0x0000A1F90114B090 AS DateTime))
SET IDENTITY_INSERT [dbo].[AccountMain] OFF

-----------------------------[AccountMainExpandInfo]--------------------------
INSERT [dbo].[AccountMainExpandInfo] ([ID], [SystemStatus], [AccountMainID], [ProductAddress], [SaleAddress], [OpeningDate], [CheckInDate], [CompletedDate], [BuildCompany], [Investor], [Description], [ProjectSupport], [TrafficInfo], [BuildingMaterials], [FloorInfo], [StallInfo], [OccupyArea], [BuildingArea], [ProjectSchedule], [PropertyRight], [HouseholdsCount]) VALUES (1, 0, 0, N'科技路西口', N'科技路西口', CAST(0x0000A1F900000000 AS DateTime), CAST(0x0000A1FF00000000 AS DateTime), CAST(0x0000A20100000000 AS DateTime), N'天朗', N'天朗', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 70, NULL)

-----------------------------[Account]--------------------------
SET IDENTITY_INSERT [dbo].[Account] ON
INSERT [dbo].[Account] ([ID], [SystemStatus], [Name], [LoginPwd], [Phone], [HeadImagePath], [Email], [RoleID], [AccountStatusID], [IsActivated]) VALUES (1, 0, N'张三', N'8ZjHBEPwxeqwCrxssgd2cQ==', N'1111', N'~\File\1\Account\张三_20130712165054_80_QQ截图20130712095702.jpg', N'zhangsan@163.com', 1, 1, 1)
INSERT [dbo].[Account] ([ID], [SystemStatus], [Name], [LoginPwd], [Phone], [HeadImagePath], [Email], [RoleID], [AccountStatusID], [IsActivated]) VALUES (2, 0, N'李四（销售经理）', N'8ZjHBEPwxeqwCrxssgd2cQ==', N'123456', N'~\File\1\Account\李四（销售经理）_20130712165157_80_logo2.jpg', N'lisi@163.com', 2, 1, 1)
SET IDENTITY_INSERT [dbo].[Account] OFF

-----------------------------[Account_AccountMain]--------------------------
SET IDENTITY_INSERT [dbo].[Account_AccountMain] ON
INSERT [dbo].[Account_AccountMain] ([ID], [SystemStatus], [AccountID], [AccountMainID]) VALUES (1, 0, 1, 1)
INSERT [dbo].[Account_AccountMain] ([ID], [SystemStatus], [AccountID], [AccountMainID]) VALUES (2, 0, 2, 1)
SET IDENTITY_INSERT [dbo].[Account_AccountMain] OFF

------------------------------[Menu]--------------------------------------------
INSERT INTO dbo.[Group](SystemStatus ,GroupName ,AccountID ,AccountMainID ,IsDefaultGroup ,IsCanDelete)VALUES  ( 0 ,N'未分组' ,1 ,1 ,1 ,0)
INSERT INTO dbo.[Group](SystemStatus ,GroupName ,AccountID ,AccountMainID ,IsDefaultGroup ,IsCanDelete)VALUES  ( 0 ,N'未分组' ,2 ,1 ,1 ,0)

------------------------------[Menu]--------------------------------------------
SET IDENTITY_INSERT [dbo].[Menu] ON
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (1, 0, N'首页', NULL, N'Home', N'Index', 1, NULL)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (2, 0, N'用户管理', NULL, N'UserManage', N'Index', 2, NULL)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (3, 0, N'消息管理', NULL, N'Message', N'Index', 3, NULL)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (4, 0, N'素材管理', NULL, N'Library', N'Text', 4, NULL)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (5, 0, N'账号管理', NULL, N'AccountMange', N'Index', 5, NULL)
INSERT [dbo].[Menu] ([ID], [SystemStatus], [Name], [Area], [Controller], [Action], [Order], [ParentMenuID]) VALUES (6, 0, N'设置', NULL, N'Set', N'Index', 6, NULL)
SET IDENTITY_INSERT [dbo].[Menu] OFF

-----------------------------[MenuOption]--------------------------
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,1,'首页','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'新建分组','AddGroup',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'编辑分组','EditGroup',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'删除分组','DeleteGroup',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'更改用户分组','ChangeGroup',5)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'修改用户信息','EditUser',6)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,2,'查看用户信息','ViewUser',7)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,3,'新建消息','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,3,'已发送','History',2)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'文本','Text',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'添加文本','AddText',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'修改文本','EditText',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'删除文本','DeleteText',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'图片','Image',5)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'上传图片','AddImage',6)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'修改标题','EditImage',7)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'删除图片','DeleteImage',8)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'语音','Voice',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'上传语音','AddVoice',6)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'修改标题','EditVoice',7)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'删除语音','DeleteVoice',8)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'视频','Video',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'上传视频','AddVideo',6)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'修改标题','EditVideo',7)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'删除视频','DeleteVideo',8)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,4,'图文','ImageText',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,5,'列表','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,6,'设置','Index',1)

-----------------------------RoleMenu--------------------------
SET IDENTITY_INSERT [dbo].[RoleMenu] ON
INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (1, 0, 1, 1)
INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (2, 0, 1, 2)
INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (3, 0, 1, 3)
INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (4, 0, 1, 4)
INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (5, 0, 1, 5)
INSERT [dbo].[RoleMenu] ([ID], [SystemStatus], [RoleID], [MenuID]) VALUES (6, 0, 1, 6)
SET IDENTITY_INSERT [dbo].[RoleMenu] OFF

-----------------------------RoleOption--------------------------
SET IDENTITY_INSERT [dbo].[RoleOption] ON
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (1, 0, 1, 1)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (2, 0, 1, 2)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (3, 0, 1, 3)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (4, 0, 1, 4)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (5, 0, 1, 5)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (6, 0, 1, 6)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (7, 0, 1, 7)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (8, 0, 1, 8)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (9, 0, 1, 9)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (10, 0, 1, 10)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (11, 0, 1, 11)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (12, 0, 1, 12)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (13, 0, 1, 13)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (14, 0, 1, 14)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (15, 0, 1, 15)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (16, 0, 1, 16)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (17, 0, 1, 17)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (18, 0, 1, 18)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (19, 0, 1, 19)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (20, 0, 1, 20)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (21, 0, 1, 21)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (22, 0, 1, 22)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (23, 0, 1, 23)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (24, 0, 1, 24)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (25, 0, 1, 25)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (26, 0, 1, 26)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (27, 0, 1, 27)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (28, 0, 1, 28)
INSERT [dbo].[RoleOption] ([ID], [SystemStatus], [RoleID], [MenuOptionID]) VALUES (28, 0, 1, 29)
SET IDENTITY_INSERT [dbo].[RoleOption] OFF

------------------------------[UserLoginInfo]---------------------------------
SET IDENTITY_INSERT [dbo].[UserLoginInfo] ON
INSERT [dbo].[UserLoginInfo] ([ID], [SystemStatus], [LoginName], [Email], [LoginPwd]) VALUES (1, 0, N'wushuangqi', N'd537@163.com', N'8ZjHBEPwxeqwCrxssgd2cQ==')
SET IDENTITY_INSERT [dbo].[UserLoginInfo] OFF
------------------------------[User]---------------------------------
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([ID], [SystemStatus], [Name], [Email], [LoginPwd], [Address], [Phone], [HeadImagePath], [AccountStatusID], [IdentityCard], [AccountMainID]) VALUES (1, 0, N'武双琦', N'd537@163.com', N'8ZjHBEPwxeqwCrxssgd2cQ==', NULL, NULL, "", 1, NULL, 1)
SET IDENTITY_INSERT [dbo].[User] OFF
------------------------------[ClientInfo]---------------------------------
SET IDENTITY_INSERT [dbo].[ClientInfo] ON
INSERT [dbo].[ClientInfo] ([ID], [SystemStatus], [IMEI], [EnumClientSystemTypeID], [SetupTiem], [EnumClientUserTypeID], [Tag], [EntityID]) VALUES (1, 0, NULL, 10, CAST(0x0000A1FE00000000 AS DateTime), 16, NULL, 1)
SET IDENTITY_INSERT [dbo].[ClientInfo] OFF
------------------------------[Account_User]---------------------------------
INSERT INTO dbo.Account_User( SystemStatus, AccountID, UserID,GroupID ) VALUES  ( 0,1,1,1)

------------------------------Other---------------------------------
CREATE INDEX IX_HostName ON AccountMain (HostName)
