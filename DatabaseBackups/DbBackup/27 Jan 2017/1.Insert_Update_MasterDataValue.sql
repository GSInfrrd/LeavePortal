Use LeaveManagementSystem
Go

Update MasterDataValue
set Value='Senior Software Engineer' where Id=1
go

INSERT INTO [dbo].[MasterDataValue] ([Id], [RefMasterType], [Value]) VALUES (4, 1, N'CEO')
INSERT INTO [dbo].[MasterDataValue] ([Id], [RefMasterType], [Value]) VALUES (5, 1, N'Team Lead')
INSERT INTO [dbo].[MasterDataValue] ([Id], [RefMasterType], [Value]) VALUES (6, 1, N'Software Engineer')
INSERT INTO [dbo].[MasterDataValue] ([Id], [RefMasterType], [Value]) VALUES (7, 1, N'Quality Analyst')
go