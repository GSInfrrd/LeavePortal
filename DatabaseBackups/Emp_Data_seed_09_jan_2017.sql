USE [LeaveManagementSystem]
GO
SET IDENTITY_INSERT [dbo].[EmployeeLeaveMaster] ON 

GO
INSERT [dbo].[EmployeeLeaveMaster] ([Id], [RefEmployeeId], [AppliedLeavesCount], [WorkFromHomeCount], [LossofPayCount], [CreatedDate], [LeaveBalance], [AdvancedLeaveCount], [RewardedLeaveCount], [ModifiiedDate], [ModifiedBy]) VALUES (1, 3, 0, 3, 1, CAST(N'2017-12-12' AS Date), 2, 12, 1, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[EmployeeLeaveMaster] OFF
GO
