﻿------默认约束---------------------------------
alter table[User] add default (getdate()) for CreateDate 

alter table[Account_User] add default (getdate()) for CreateDate 

-----------------------------Role--------------------------
--INSERT INTO dbo.Role ( SystemStatus, NAME,IsCanDelete,Token ) VALUES  ( 0,'管理员',0,'AccountAdmin')
--INSERT INTO dbo.Role ( SystemStatus, Name,IsCanDelete,IsCanFindByUser  ) VALUES  ( 0,'销售经理',1,1)
--INSERT INTO dbo.Role ( SystemStatus, NAME,ParentRoleID,IsCanDelete,IsCanFindByUser  ) VALUES  ( 0,'销售代表',1,1,1)

-----------------------------SystemUserMenu--------------------------
SET IDENTITY_INSERT [dbo].SystemUserMenu ON

--1级菜单---
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 1,0,'首页','System','SystemUserHome','Index',1,NULL)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 2,0,'物业管理','System','AccountMainManage','Index',2,NULL)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 3,0,'商家管理','Merchant','Merchant','Index',3,NULL)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 4,0,'平台信息发布','System','Information','Index',4,NULL)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 5,0,'系统管理','System','SystemUser','Index',5,NULL)
--2级菜单---
--物业
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 6,0,'物业管理','System','AccountMainManage','Index',1,2)
--商家
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 7,0,'商家管理','Manager','Merchant','Index',1,3)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 8,0,'周边外卖','Manager','M_TakeOut','Index',2,3)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 9,0,'商家促销','Manager','Merchant','Index',3,3)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 10,0,'家教','Manager','Tutor','Index',4,3)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 11,0,'干洗服务','Manager','DryCleaning','Index',5,3)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 12,0,'教育培训','Manager','EducationTrain','Index',6,3)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 13,0,'宠物医院','Manager','PetHospital','Index',7,3)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 14,0,'家电维修','Manager','Merchant','Index',8,3)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 15,0,'家政服务','Manager','Domestic','Index',9,3)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 16,0,'开锁换锁','Manager','Unlock','Index',10,3)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 17,0,'搬家','Manager','Move','Index',11,3)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 18,0,'管道疏通','Manager','PipelineDredge','Index',12,3)
--平台
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 19,0,'每日食谱','System','Recipes','Index',1,4)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 20,0,'生活技巧','System','LifeSkill','Index',2,4)
--系统
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 21,0,'系统角色管理','System','SystemUserRole','Index',1,5)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 22,0,'系统账号管理','System','SystemUser','Index',2,5)
INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 23,0,'问题反馈','System','Feedback','Index',3,5)

--三级菜单
--物业
--INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 8,0,'角色管理','System','Role','Index',1,6)
--INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 9,0,'账号管理','System','AccountManage','Index',2,6)
--INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 10,0,'报表管理','System','Report','Index',3,6)
--商家
--INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 11,0,'账号管理','Merchant','Account','Index',1,7)
--INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 12,0,'报表管理','Merchant','Report','Index',2,7)

--INSERT INTO dbo.SystemUserMenu ( [ID], SystemStatus, NAME,Area,Controller,Action,[Order],ParentMenuID ) VALUES  ( 8,0,'问题反馈','System'','Feedback','Index',5,NULL)
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
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,8,'问题反馈','Index',1)
--INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,7,'设置','Index',1)

INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,9,'列表','Index',1)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,9,'添加','Add',2)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,9,'修改','Edit',3)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,9,'删除','Delete',4)

INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,10,'列表','Index',1)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,10,'添加','Add',2)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,10,'修改','Edit',3)
INSERT INTO dbo.SystemUserMenuOption (SystemStatus, SystemUserMenuID,Name,ACTION,[Order] ) VALUES  ( 0,10,'删除','Delete',4)


-----------------------------SystemUserRole--------------------------
SET IDENTITY_INSERT [dbo].SystemUserRole ON
INSERT INTO dbo.SystemUserRole ( [ID],SystemStatus, Name ) VALUES  ( 1,0,'超级管理员')
--INSERT INTO dbo.SystemUserRole ( [ID],SystemStatus, NAME,ParentSystemUserRoleID ) VALUES  ( 2,0,'业务员',1)
SET IDENTITY_INSERT [dbo].SystemUserRole OFF


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

------------------------------[Service]-------------------------------------------
SET IDENTITY_INSERT [dbo].[Service] ON
INSERT INTO dbo.Service ( [ID],SystemStatus, NAME, IsIndependentSite) VALUES  (1, 0, N'WEB基本服务',0)
INSERT INTO dbo.Service ( [ID],SystemStatus, Name, IsIndependentSite) VALUES  (2, 1, N'售楼部服务',0)
INSERT INTO dbo.Service ( [ID],SystemStatus, NAME, IsIndependentSite) VALUES  (3, 0, N'微网站服务',1)
INSERT INTO dbo.Service ( [ID],SystemStatus, NAME, IsIndependentSite) VALUES  (4, 1, N'微信集成服务',1)
INSERT INTO dbo.Service ( [ID],SystemStatus, NAME, IsIndependentSite) VALUES  (5, 0, N'物业服务',0)
SET IDENTITY_INSERT [dbo].[Service] OFF


------------------------------[Menu]--------------------------------------------
CREATE UNIQUE INDEX IX_Menu_Token ON Menu (Token)
SET IDENTITY_INSERT [dbo].[Menu] ON

INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level],ServiceID,[Type]) VALUES (1,'Token_WUYE', 0, N'物业管理', N'物业管理（物业）', NULL, NULL,NULL, 1, NULL,0,1,5,1)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level],ServiceID,[Type]) VALUES (2,'Token_News', 0, N'调查活动通知发布', N'调查活动通知发布', NULL, NULL, NULL, 2, NULL,0,1,1,1)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level],ServiceID,[Type]) VALUES (3,'Token_User', 0, N'用户管理', N'用户管理', NULL, NULL,NULL, 3, NULL,0,1,1,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level],ServiceID,[Type]) VALUES (4,'Token_Message', 0, N'消息管理', N'消息管理', NULL, NULL,NULL, 4, NULL,0,1,1,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level],ServiceID,[Type]) VALUES (5,'Token_Library', 0, N'素材管理', N'素材管理', NULL,  NULL,NULL, 5, NULL,0,1,1,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level],ServiceID,[Type]) VALUES (6,'Token_Account', 0, N'系统角色与人员管理', N'系统角色与人员管理', NULL, NULL,NULL, 7, NULL,0,1,1,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level],ServiceID,[Type]) VALUES (7,'Token_AppSet', 0, N'App设置', N'App设置', NULL, NULL, NULL, 8, NULL,0,1,1,3)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level],ServiceID,[Type]) VALUES (8,'Token_Account_I', 0, N'我的账号', N'我的账号', NULL, N'Set', N'Index',9, NULL,0,1,1,3)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level],ServiceID,[Type]) VALUES (43,'Token_Shop', 0, N'商城管理', N'商城管理', NULL, NULL,NULL,6, NULL,0,1,3,2)
--二级
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (9,'Token_Property_I', 0, N'物业信息', N'物业信息', NULL, N'BasisSet', N'Index', 1, 1,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (10,'Token_WUYE_AboutUS', 0, N'关于我们', N'关于我们（物业）', NULL, N'AboutUS', N'Index',10, 1,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (11,'Token_Property_House', 0, N'房屋管理', N'房屋管理（物业）', NULL, N'PropertyHouse', N'Index',2, 1,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (12,'Token_WUYE_Fee', 0, N'物业费管理', N'物业费管理（物业）', NULL, N'PropertyFeeInfo', N'Index',3, 1,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (13,'Token_WUYE_ParkingFee', 0, N'停车费', N'停车费（物业）', NULL, N'ParkingFee', N'Index',4, 1,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (14,'Token_WUYE_Repair', 0, N'报修管理', N'报修管理（物业）', NULL, N'RepairInfo', N'Index',5, 1,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (15,'Token_WUYE_Complain', 0, N'投诉管理', N'投诉管理（物业）', NULL, N'Complaint', N'Index',6, 1,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (16,'Token_WUYE_Repaircharges', 0, N'收费维修', N'收费维修（物业）', NULL, N'Repairchargeso', N'Index',7, 1,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (17,'Token_WUYE_Rental', 0, N'房屋租赁', N'房屋租赁（物业）', NULL, N'RentalHouse', N'Index',8, 1,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (18,'Token_WUYE_ExpressCollection', 0, N'快递代收', N'快递代收（物业）', NULL, N'ExpressCollection', N'Index',9, 1,0,2)

INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (19,'Token_News_U', 0, N'小区通知', N'小区通知', NULL, N'AppAdvertorial', N'Index', 1,2,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (20,'Token_News_S', 1, N'销售资讯', N'销售资讯', NULL, N'AppAdvertorialAccount', N'Index', 2,2,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (21,'Token_News_Su', 0, N'调查问卷', N'调查问卷', NULL, N'SurveyMain', N'IndexMain', 3, 2,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (22,'Token_News_A', 0, N'活动', N'活动', NULL, N'ActivityInfo', N'Index', 4, 2,0,2)

--INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (23,'Token_User_M', 1, N'用户管理', N'用户管理', NULL, N'UserManage', N'Index', 1, 3,0,2)
--INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (25,'Token_User_SM', 1, N'人员与用户管理', N'人员与用户管理', NULL, N'SalesMessage', N'Index', 3, 3,0,2)

INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (23,'Token_Keyword', 0, N'自动回复', N'自动回复', NULL, N'KeywordMessage', N'Index', 1, 4,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (24,'Token_Message_N', 0, N'消息推送', N'消息推送', NULL, N'Message', N'Index', 2, 4,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (25,'Token_Message_H', 0, N'已发送', N'已发送', NULL, N'Message', N'History', 3, 4,0,2)

INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (26,'Token_Library_I', 0, N'图片素材', N'图片素材', NULL, N'LibraryImage', N'Index', 1, 5,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (27,'Token_Library_Vo', 0, N'语音素材', N'语音素材', NULL, N'LibraryVoice', N'Index', 2, 5,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (28,'Token_Library_Vi', 0, N'视频素材', N'视频素材', NULL, N'LibraryVideo', N'Index', 3, 5,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (29,'Token_Library_IT', 0, N'图文素材', N'图文素材', NULL, N'LibraryImageText', N'Index', 4, 5,0,2)

INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (30,'Token_Account_R', 0, N'角色管理', N'角色管理', NULL, N'Character', N'Index', 1, 6,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (31,'Token_Account_M', 0, N'人员管理', N'人员管理', NULL, N'Account', N'Index', 2, 6,0,2)

INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (32,'Token_Waitpage', 0, N'App等待画面', N'App等待画面', NULL, N'AppWaitImg', N'Index',1, 7,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (33,'Token_AutoMessage_Reply', 1, N'被添加时自动回复', N'被添加自动回复', NULL, N'AutoMessageReply', N'Index', 2, 7,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (34,'Token_Payment', 0, N'支付配置', N'支付配置', NULL, N'Payment', N'Index',3, 7,0,2)

INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (35,'Token_Shop_S', 0, N'商城设置', N'商城设置', NULL, N'MicroSiteSet', N'Index', 1, 43,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (36,'Token_Shop_T', 0, N'类别管理', N'类别管理', NULL, N'Classify', N'Index', 2, 43,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (37,'Token_Shop_M', 0, N'产品管理', N'产品管理', NULL, N'Product', N'Index', 3, 43,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (38,'Token_Shop_O1', 0, N'订单管理', N'订单管理(微商城)', NULL, N'MicroOrder', N'Index', 4, 43,0,2)
--三级
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (39,'Token_User_ALL', 0, N'全部用户', N'全部用户', NULL, N'UserManage', N'AllUser', 1, 3,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (40,'Token_User_Msg', 0, N'人工回答', N'人工回答', NULL, N'InstantMes', N'Index', 2, 3,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (41,'Token_Chat', 0, N'聊天窗口', N'聊天窗口', NULL, N'SingleChat', N'Index', 1, 39,0,3)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (42,'Token_Property_User', 0, N'业主信息', N'业主信息', NULL, N'PropertyUser', N'Index',1, 11,0,3)
/**/
/*
--2级
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (11,'Token_Member', 0, N'会员管理', N'会员管理', NULL, N'VipInfo', N'Index', 3, 2,0,2)
--
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (15,'Token_Product_OT', 1, N'订单类型', N'订单类型(卖奶)', NULL, N'OrderMType', N'Index', 5, 3,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (16,'Token_Product_D', 1, N'节假日管理', N'节假日管理(卖奶)', NULL, N'Holiday', N'Index', 6, 3,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (17,'Token_Product_O2', 1, N'订单管理', N'订单管理(卖奶)', NULL, N'Order', N'Index', 7, 3,0,2)
--

INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (20,'Token_Project_M', 0, N'项目管理', N'项目管理', NULL, N'HousesMange', N'Index', 1, 5,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (21,'Token_Project_M2', 0, N'项目和账号管理', N'项目和账号管理', NULL, N'AccountMain', N'Index', 2, 5,0,2)

INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (36,'Token_Member_I', 0, N'会员信息', N'会员信息', NULL, N'VipInfo', N'Index', 1, 11,0,3)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (37,'Token_Member_C', 0, N'卡片管理', N'卡片管理', NULL, N'CardInfo', N'Index', 2, 11,0,3)
--
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (38,'Token_House_I', 0, N'单元管理', N'单元管理', NULL, N'HouseInfo', N'Index', 1, 20,0,3)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (39,'Token_House_T', 0, N'户型管理', N'户型管理', NULL, N'HouseType', N'Index', 2, 20,0,3)
--
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (46,'Token_Project_Report', 1, N'报表管理', N'报表管理', NULL, N'AccountMain', N'Report', 3, 21,0,3)

----------4级--------------
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (47,'Token_House_M', 0, N'房屋管理', N'房屋管理', NULL, N'HouseInfoDetail', N'Index', 1, 38,0,3)
--


-----------App菜单----------无需在web端上显示
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level],ServiceID) VALUES (49,'Token_App_R', 0, N'App权限', N'App权限', NULL, NULL, NULL, 9, NULL,1,1,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (50,'Token_App_U', 0, N'会员管理', N'会员管理', NULL, NULL, NULL, 1, 49,1,1)

------------新增加-----------------
/*抽奖菜单开始*/
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (52,'Token_News_L', 0, N'抽奖活动', N'抽奖活动', NULL, N'LotteryDish', N'Index', 5, 6,0,2)
--抽奖3级菜单
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (53,'Token_News_L_D', 0, N'大转盘', N'大转盘', NULL, N'LotteryDish', N'Index', 1, 52,0,3)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (54,'Token_News_L_E', 0, N'砸金蛋', N'砸金蛋', NULL, N'LotteryEgg', N'Index', 2, 52,0,3)
/*抽奖菜单结束*/
/*物业菜单开始*/
--一级
--二级
*/
/*物业菜单结束*/
/*支付配置*/
SET IDENTITY_INSERT [dbo].[Menu] OFF

-----------------------------[MenuOption]--------------------------
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,35,'会员列表','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,35,'绑定会员','Add',2)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,35,'修改会员','Edit',3)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,35,'会员充值','Recharge',4)

--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,36,'卡片列表','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,36,'前缀录入','AddPrefix',2)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,36,'添加卡片','Add',3)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,36,'修改卡片','Edit',4)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,36,'删除卡片','Delete',5)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,36,'冻结卡片','SetStatus',6)

--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,15,'订单类型列表','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,15,'添加订单类型','Add',2)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,15,'修改订单类型','Edit',3)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,15,'删除订单类型','Delete',4)

--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'节假日列表','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'添加节假日','Add',2)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'修改节假日','Edit',3)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'删除节假日','Delete',4)


--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,17,'订单列表','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,17,'取消订单','Cancel',2)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,31,'人员列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,31,'添加人员','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,31,'修改人员','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,31,'删除人员','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,31,'启用/禁用人员','SetAccountMainStatus',5)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,31,'重置密码','ResetPwd',6)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,30,'角色列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,30,'添加角色','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,30,'修改角色','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,30,'删除角色','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,30,'权限设置','Power',4)

--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,20,'项目列表','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,20,'查看项目详细信息','Select',2)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,20,'添加项目','Add',3)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,20,'修改项目','Edit',4)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,20,'删除项目','Delete',5)

--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'项目列表','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'添加项目','Add',2)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'修改项目','Edit',3)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'删除项目','Delete',4)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'启用/禁用项目','SetStatus',5)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'角色管理','Role',6)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'账号管理','Account',7)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'报表管理','Report',8)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'添加角色','AddRole',9)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'修改角色','EditRole',10)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'删除角色','DeleteRole',11)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'分配角色权限','PowerRole',12)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'添加账号','AddAccount',13)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'修改账号','EditAccount',14)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'删除账号','DeleteAccount',15)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'启用/禁用账号','SetAccountStatus',16)

--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,38,'单元列表','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,38,'查看单元详细信息','Select',2)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,38,'添加单元','Add',3)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,38,'修改单元','Edit',4)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,38,'删除单元','Delete',5)

--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,39,'户型列表','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,39,'查看户型详细信息','Select',2)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,39,'添加户型','Add',3)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,39,'修改户型','Edit',4)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,39,'删除户型','Delete',5)

--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,47,'房屋列表','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,47,'查看房屋详细信息','Select',2)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,47,'添加房屋','Add',3)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,47,'修改房屋','Edit',4)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,47,'删除房屋','Delete',5)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,19,'用户资讯','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,19,'添加资讯','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,19,'修改资讯','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,19,'删除资讯','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,20,'销售端资讯','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,20,'添加资讯','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,20,'修改资讯','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,20,'删除资讯','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'调查列表','IndexMain',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'创建调查','AddMain',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'修改调查','EditMain',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'删除调查','DeleteMain',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'调查问题列表','IndexTrouble',5)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'录入调查问题','AddTrouble',6)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'修改调查问题','EditTrouble',7)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'删除调查问题','DeleteTrouble',8)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,22,'查看活动','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,22,'新建活动','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,22,'修改活动','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,22,'删除活动','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,9,'基础设置','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,23,'关键词自动回复','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,23,'保存关键词自动回复','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,23,'修改关键词自动回复','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,23,'删除关键词自动回复','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,32,'App等待画面','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,8,'我的账号','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,29,'查看图文','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,29,'新建单图文','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,29,'新建多图文','MoreAdd',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,29,'修改图文','Edit',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,29,'删除图文','Delete',5)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,33,'编辑','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,22,'详细信息','ViewUser',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,22,'修改','EditUser',2)

--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,53,'大转盘列表','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,53,'新增','Add',2)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,53,'修改','Edit',3)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,53,'删除','Delete',4)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,53,'启用/禁用','SetStatus',5)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,54,'砸金蛋列表','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,54,'新增','Add',2)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,54,'修改','Edit',3)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,54,'删除','Delete',4)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,54,'启用/禁用','SetStatus',5)


INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,11,'列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,11,'新增','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,11,'编辑','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,11,'删除','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,42,'业主信息','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,42,'新增','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,42,'编辑','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,42,'删除','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,12,'物业费列表','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,14,'报修列表','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,15,'投诉列表','Index',1)

--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,61,'列表','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,61,'新增','Add',2)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,61,'修改','Edit',3)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,61,'删除','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'新增','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'修改','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'删除','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,17,'列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,17,'新增','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,17,'修改','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,17,'删除','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,18,'列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,18,'新增','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,18,'修改','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,18,'删除','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,13,'停车费列表','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,10,'关于我们','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,34,'支付配置','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,23,'用户列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,23,'新建用户分组','AddGroup',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,23,'编辑用户分组','EditGroup',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,23,'删除用户分组','DeleteGroup',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,23,'更改用户分组','ChangeGroup',5)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,23,'修改用户信息','EditUser',6)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,23,'查看用户信息','ViewUser',7)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,40,'列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,41,'详细','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,25,'详细','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,24,'新增推送','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,25,'历史推送','History',1)


INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,26,'查看图片','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,26,'上传图片','Upload',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,26,'修改标题','ReName',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,26,'删除图片','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,27,'查看语音','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,27,'上传语音','Upload',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,27,'修改标题','ReName',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,27,'删除语音','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,28,'查看视频','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,28,'上传视频','Upload',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,28,'修改标题','ReName',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,28,'删除视频','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,39,'列表','AllUser',1)

--商城
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,35,'设置','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,36,'分类列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,36,'添加分类','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,36,'修改分类','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,36,'删除分类','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,37,'产品列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,37,'添加产品','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,37,'修改产品','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,37,'删除产品','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,38,'订单列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,38,'取消订单','Cancel',2)



--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,11,'个人信息','Index',1)

--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,15,'安装APP自动回复','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,15,'保存安装APP自动回复','AddOrUpd',2)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,15,'删除安装APP自动回复','Delete',3)

--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,17,'即时消息列表','index',1)

--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,18,'聊天窗口','index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,19,'销售与客户关系管理列表','index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,19,'销售与客户聊天记录','SelectAtoUmes',2)

--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,29,'任务列表','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,29,'指派任务','Add',2)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,29,'修改指派任务','Edit',3)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,29,'删除指派任务','Delete',4)

--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,30,'我的任务','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,30,'任务详细','Detail',2)



--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,34,'卡片列表','Index',1)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,34,'前缀录入','AddPrefix',2)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,34,'添加卡片','Add',3)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,34,'删除卡片','Delete',4)
--INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,34,'冻结卡片','SetStatus',5)



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

-------------------------------[UserTag]-----------------------------------
SET IDENTITY_INSERT [dbo].[UserTag] ON
INSERT INTO dbo.UserTag (ID,SystemStatus,TagName) VALUES (1,0,'新用户')
INSERT INTO dbo.UserTag (ID,SystemStatus,TagName) VALUES (2,0,'确认户型')
INSERT INTO dbo.UserTag (ID,SystemStatus,TagName) VALUES (3,0,'已预订')
INSERT INTO dbo.UserTag (ID,SystemStatus,TagName) VALUES (4,0,'已成交')
SET IDENTITY_INSERT [dbo].[UserTag] OFF




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

Create function SetSerialNumber_Property(@prefix varchar(10),@digit int,@AccountMainID int)
returns varchar(100)
as
begin
	declare @predate varchar(12)= convert(varchar(50),getdate(),112)
	declare @Number varchar(100) = UPPER(@prefix)+@predate

	declare @Prefixcnt int = DATALENGTH(@prefix)
	declare @MaxNumber varchar(100) 
	select @MaxNumber =Max(OrderNum) from dbo.PropertyOrder where AccountMainID= @AccountMainID and OrderNum like '%'+@Number+'%'  
	
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

