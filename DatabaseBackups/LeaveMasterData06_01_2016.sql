USE [LeaveManagementSystem]
GO
SET IDENTITY_INSERT [dbo].[LeaveMaster] ON 

GO
INSERT [dbo].[LeaveMaster] ([Id], [RefLeaveType], [Description], [Count], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy]) VALUES (3, 25, N'CompOff', NULL, CAST(N'2016-12-12 00:00:00.0000000' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [dbo].[LeaveMaster] ([Id], [RefLeaveType], [Description], [Count], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy]) VALUES (4, 26, N'Advance leaves', 20, CAST(N'2017-01-04 15:54:13.6570000' AS DateTime2), NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[LeaveMaster] OFF
GO
