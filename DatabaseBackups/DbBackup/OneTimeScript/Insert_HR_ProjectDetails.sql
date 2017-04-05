Use LeaveManagementSystem
Go

SET IDENTITY_INSERT [dbo].[ProjectMaster] ON
INSERT INTO [dbo].[ProjectMaster] ([Id], [ProjectName], [Description], [IsActive], [StartDate], [EndDate], [RefManagerId], [Technology], [ProjectLogo], [IsBench]) VALUES (1, N'HR', N'Human Resource Management', 1, N'2016-12-01', NULL, 2,N'Excel',NULL,0)
INSERT INTO [dbo].[ProjectMaster] ([Id], [ProjectName], [Description], [IsActive], [StartDate], [EndDate], [RefManagerId], [Technology], [ProjectLogo], [IsBench]) VALUES (2, N'Talent Pool', N'Employees not assigned to any project', 1, N'2016-12-01', NULL, 2, N'All', NULL, 1)
SET IDENTITY_INSERT [dbo].[ProjectMaster] OFF

SET IDENTITY_INSERT [dbo].[EmployeeProjectDetail] ON
INSERT INTO [dbo].[EmployeeProjectDetail] ([Id], [RefEmployeeId], [RefProjectId], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [IsActive], [StartDate], [EndDate]) VALUES (1,2, 1, N'2017-01-24 14:49:49', NULL, NULL, NULL, 1, N'2017-01-24', NULL)
INSERT INTO [dbo].[EmployeeProjectDetail] ([Id], [RefEmployeeId], [RefProjectId], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [IsActive], [StartDate], [EndDate]) VALUES (2,1, 1, N'2017-01-24 14:49:49', NULL, NULL, NULL, 1, N'2017-01-24', NULL)
SET IDENTITY_INSERT [dbo].[EmployeeProjectDetail] OFF

ALTER DATABASE LeaveManagementSystem SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE ;
