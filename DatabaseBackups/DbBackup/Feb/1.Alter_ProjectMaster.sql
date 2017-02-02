Use LeaveManagementSystem
Go

Alter Table ProjectMaster
Add IsBench bit not null default 0
go

--SET IDENTITY_INSERT [dbo].[ProjectMaster] ON
--INSERT INTO [dbo].[ProjectMaster] ([Id], [ProjectName], [Description], [IsActive], [StartDate], [EndDate], [RefManagerId], [Technology], [ProjectLogo], [IsBench]) VALUES (4, N'Talent Pool', N'Employees not assigned to any project', 1, N'2016-12-01', NULL, 2, N'All', NULL, 1)
--SET IDENTITY_INSERT [dbo].[ProjectMaster] OFF