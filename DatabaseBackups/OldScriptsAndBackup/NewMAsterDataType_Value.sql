USE [LeaveManagementSystem]
GO
SET IDENTITY_INSERT [dbo].[MasterDataType] ON 

GO

INSERT [dbo].[MasterDataType] ([Id], [Type]) VALUES (6, N'WorkFromHomeReason')
GO
SET IDENTITY_INSERT [dbo].[MasterDataType] OFF
GO
SET IDENTITY_INSERT [dbo].[MasterDataValue] ON 

GO
INSERT [dbo].[MasterDataValue] ([Id], [RefMasterType], [Value]) VALUES (23, 6, N'Maintanace at home')
GO
INSERT [dbo].[MasterDataValue] ([Id], [RefMasterType], [Value]) VALUES (21, 6, N'To avoid Travel')
GO
SET IDENTITY_INSERT [dbo].[MasterDataValue] OFF
GO
