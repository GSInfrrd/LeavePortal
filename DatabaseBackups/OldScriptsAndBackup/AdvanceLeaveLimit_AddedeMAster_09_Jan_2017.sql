USE [LeaveManagementSystem]
GO
SET IDENTITY_INSERT [dbo].[MasterDataType] ON 

GO

INSERT [dbo].[MasterDataType] ([Id], [Type]) VALUES (7, N'AdvanceLeaveLimit')
GO
SET IDENTITY_INSERT [dbo].[MasterDataType] OFF
GO
SET IDENTITY_INSERT [dbo].[MasterDataValue] ON 

GO
INSERT [dbo].[MasterDataValue] ([Id], [RefMasterType], [Value]) VALUES (28, 7, N'6')
Go
SET IDENTITY_INSERT [dbo].[MasterDataValue] OFF
GO
