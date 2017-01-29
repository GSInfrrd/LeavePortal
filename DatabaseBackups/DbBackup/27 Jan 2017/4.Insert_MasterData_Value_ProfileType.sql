Use LeaveManagementSystem
Go

INSERT INTO [dbo].[MasterDataType] ([Id], [Type]) VALUES (14, N'ProfileType')
Go

INSERT INTO [dbo].[MasterDataValue] ([Id], [RefMasterType], [Value]) VALUES (200, 14, N'CEO')
INSERT INTO [dbo].[MasterDataValue] ([Id], [RefMasterType], [Value]) VALUES (201, 14, N'Admin/HR')
INSERT INTO [dbo].[MasterDataValue] ([Id], [RefMasterType], [Value]) VALUES (202, 14, N'Manager')
INSERT INTO [dbo].[MasterDataValue] ([Id], [RefMasterType], [Value]) VALUES (203, 14, N'Employee')
Go
