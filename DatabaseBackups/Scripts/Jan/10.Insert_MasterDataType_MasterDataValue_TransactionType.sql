Use LeaveManagementSystem
go
SET IDENTITY_INSERT [dbo].[MasterDataType] ON
INSERT INTO [dbo].[MasterDataType] ([Id], [Type]) VALUES (1016, N'TransactionType')
SET IDENTITY_INSERT [dbo].[MasterDataType] OFF

SET IDENTITY_INSERT [dbo].[MasterDataValue] ON
INSERT INTO [dbo].[MasterDataValue] ([Id], [RefMasterType], [Value]) VALUES (2052, 1016, N'Debit')
INSERT INTO [dbo].[MasterDataValue] ([Id], [RefMasterType], [Value]) VALUES (2053, 1016, N'Credit')
SET IDENTITY_INSERT [dbo].[MasterDataValue] OFF

