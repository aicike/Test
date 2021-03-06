﻿-----------------------------[Lookup]--------------------------
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
------------------------------[Menu]--------------------------------------------
CREATE UNIQUE INDEX IX_Menu_Token ON Menu (Token)
SET IDENTITY_INSERT [dbo].[Menu] ON
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (1,'Token_Home', 0, N'首页', N'首页', NULL, NULL, NULL, 1, NULL,1,1)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (2,'Token_Manage', 0, N'管理', N'管理', NULL, NULL, NULL, 2, NULL,0,1)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (3,'Token_Product', 0, N'产品', N'产品', NULL, NULL, NULL, 3, NULL,0,1)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (4,'Token_Account', 0, N'账号', N'账号', NULL, NULL, NULL, 4, NULL,0,1)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (5,'Token_Project', 0, N'项目', N'项目', NULL, NULL, NULL, 5, NULL,0,1)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (6,'Token_News', 0, N'调查 活动 资讯', N'调查 活动 资讯', NULL, NULL, NULL, 6, NULL,0,1)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (7,'Token_Set', 0, N'设置', N'设置', NULL, NULL,NULL, 7, NULL,0,1)
--2级											
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (8,'Token_User', 0, N'用户管理', N'用户管理', NULL, N'UserManage', N'Index', 1, 2,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (9,'Token_Member', 0, N'会员管理', N'会员管理', NULL, N'VipInfo', N'Index', 2, 2,0,2)
--
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (10,'Token_Product_M', 0, N'产品管理', N'产品管理', NULL, N'Product', N'Index', 1,3,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (11,'Token_Product_T', 0, N'类别管理', N'类别管理', NULL, N'Classify', N'Index', 2, 3,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (12,'Token_Product_O1', 0, N'订单管理', N'订单管理(微商城)', NULL, N'MicroOrder', N'Index', 3, 3,0,2)
--
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (13,'Token_Account_M', 0, N'账号管理', N'账号管理', NULL, N'Account', N'Index', 1, 4,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (14,'Token_Account_R', 0, N'角色管理', N'角色管理', NULL, N'Character', N'Index', 2, 4,0,2)
--
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (15,'Token_Project_I', 0, N'售楼部信息', N'售楼部信息', NULL, N'BasisSet', N'Index', 1,5,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (16,'Token_Project_M', 0, N'项目管理', N'项目管理', NULL, N'HousesMange', N'Index', 2, 5,0,2)
--
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (17,'Token_News_U', 0, N'用户端资讯', N'用户端资讯', NULL, N'AppAdvertorial', N'Index', 1,6,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (18,'Token_News_Su', 0, N'调查问卷', N'调查问卷', NULL, N'SurveyMain', N'IndexMain', 2, 6,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (19,'Token_News_A', 0, N'活动', N'活动', NULL, N'ActivityInfo', N'Index', 3, 6,0,2)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (20,'Token_News_3D', 0, N'360度全景图', N'360度全景图', NULL, N'Panorama', N'Index', 4, 6,0,2)
--
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (21,'Token_Account_I', 0, N'账号信息', N'账号信息', NULL, N'Set', N'Index', 1, 7,0,2)
-----------3级-------------
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (22,'Token_User_M', 0, N'用户管理', N'用户管理', NULL, N'UserManage', N'Index', 1, 8,0,3)
--
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (23,'Token_Member_I', 0, N'会员信息', N'会员信息', NULL, N'VipInfo', N'Index', 1, 9,0,3)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (24,'Token_Member_C', 0, N'卡片管理', N'卡片管理', NULL, N'CardInfo', N'Index', 2, 9,0,3)
--
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (25,'Token_House_I', 0, N'单元管理', N'单元管理', NULL, N'HouseInfo', N'Index', 1, 16,0,3)
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (26,'Token_House_T', 0, N'户型管理', N'户型管理', NULL, N'HouseType', N'Index', 2, 16,0,3)
----------4级--------------
INSERT [dbo].[Menu] ([ID],[Token], [SystemStatus], [Name],[ShowName], [Area], [Controller], [Action], [Order], [ParentMenuID],[IsAppMenu],[Level]) VALUES (27,'Token_House_M', 0, N'房屋管理', N'房屋管理', NULL, N'HouseInfoDetail', N'Index', 1, 26,0,3)
--
SET IDENTITY_INSERT [dbo].[Menu] OFF

-----------------------------[MenuOption]--------------------------
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,23,'会员列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,23,'绑定会员','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,23,'修改会员','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,23,'会员充值','Recharge',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,24,'卡片列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,24,'前缀录入','AddPrefix',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,24,'添加卡片','Add',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,24,'删除卡片','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,24,'冻结卡片','SetStatus',5)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,10,'产品列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,10,'添加产品','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,10,'修改产品','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,10,'删除产品','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,11,'分类列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,11,'添加分类','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,11,'修改分类','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,11,'删除分类','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,12,'订单列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,12,'取消订单','Cancel',2)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,13,'查看账号列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,13,'添加账号','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,13,'修改账号','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,13,'删除账号','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,13,'启用/禁用账号','SetAccountStatus',5)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,13,'重置密码','ResetPwd',6)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,14,'角色列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,14,'添加角色','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,14,'修改角色','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,14,'删除角色','Delete',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,14,'权限设置','Power',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'项目列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'查看项目详细信息','Select',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'添加项目','Add',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'修改项目','Edit',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,16,'删除项目','Delete',5)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,25,'单元列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,25,'查看单元详细信息','Select',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,25,'添加单元','Add',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,25,'修改单元','Edit',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,25,'删除单元','Delete',5)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,26,'户型列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,26,'查看户型详细信息','Select',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,26,'添加户型','Add',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,26,'修改户型','Edit',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,26,'删除户型','Delete',5)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,27,'房屋列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,27,'查看房屋详细信息','Select',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,27,'添加房屋','Add',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,27,'修改房屋','Edit',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,27,'删除房屋','Delete',5)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,17,'用户端资讯','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,17,'添加资讯','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,17,'修改资讯','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,17,'删除资讯','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,18,'调查列表','IndexMain',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,18,'创建调查','AddMain',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,18,'修改调查','EditMain',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,18,'删除调查','DeleteMain',4)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,18,'调查问题列表','IndexTrouble',5)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,18,'录入调查问题','AddTrouble',6)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,18,'修改调查问题','EditTrouble',7)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,18,'删除调查问题','DeleteTrouble',8)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,19,'查看活动','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,19,'新建活动','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,19,'修改活动','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,19,'删除活动','Delete',4)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,15,'基础设置','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,21,'账号信息','Index',1)

INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,20,'列表','Index',1)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,20,'添加','Add',2)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,20,'修改','Edit',3)
INSERT INTO dbo.MenuOption (SystemStatus, MenuID,Name,ACTION,[Order] ) VALUES  ( 0,20,'删除','Delete',4)

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
