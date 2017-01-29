Use LeaveManagementSystem
Go

SET IDENTITY_INSERT [dbo].[ProjectMaster] ON
INSERT INTO [dbo].[ProjectMaster] ([Id], [ProjectName], [Description], [IsActive], [StartDate], [EndDate], [RefManagerId], [Technology]) VALUES (1, N'HR', N'Human Resource Management', 1, N'2016-12-01', NULL, 1,N'Excel')
SET IDENTITY_INSERT [dbo].[ProjectMaster] OFF

SET IDENTITY_INSERT [dbo].[EmployeeProjectDetail] ON
INSERT INTO [dbo].[EmployeeProjectDetail] ([Id], [RefEmployeeId], [RefProjectId], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [IsActive], [StartDate], [EndDate]) VALUES (1,2, 1, N'2017-01-24 14:49:49', NULL, NULL, NULL, 1, N'2017-01-24', NULL)
SET IDENTITY_INSERT [dbo].[EmployeeProjectDetail] OFF
