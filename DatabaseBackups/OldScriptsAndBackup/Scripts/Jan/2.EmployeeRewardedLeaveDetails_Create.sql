USE [LeaveManagementSystem]
GO

/****** Object:  Table [dbo].[EmployeeRewardedLeaveDetails]    Script Date: 1/11/2017 3:14:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmployeeRewardedLeaveDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RefEmployeeId] [int] NOT NULL,
	[RefProjectId] [int] NOT NULL,
	[RewardedBy] [int] NOT NULL,
	[LeaveCount] [int] NOT NULL,
	[RewardedDate] [date] NOT NULL,
 CONSTRAINT [PK_EmployeeRewardedLeaveDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EmployeeRewardedLeaveDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeRewardedLeaveDetails_EmployeeDetails] FOREIGN KEY([RefEmployeeId])
REFERENCES [dbo].[EmployeeDetails] ([Id])
GO

ALTER TABLE [dbo].[EmployeeRewardedLeaveDetails] CHECK CONSTRAINT [FK_EmployeeRewardedLeaveDetails_EmployeeDetails]
GO

ALTER TABLE [dbo].[EmployeeRewardedLeaveDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeRewardedLeaveDetails_EmployeeDetails1] FOREIGN KEY([RewardedBy])
REFERENCES [dbo].[EmployeeDetails] ([Id])
GO

ALTER TABLE [dbo].[EmployeeRewardedLeaveDetails] CHECK CONSTRAINT [FK_EmployeeRewardedLeaveDetails_EmployeeDetails1]
GO

ALTER TABLE [dbo].[EmployeeRewardedLeaveDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeRewardedLeaveDetails_ProjectMaster] FOREIGN KEY([RefProjectId])
REFERENCES [dbo].[ProjectMaster] ([Id])
GO

ALTER TABLE [dbo].[EmployeeRewardedLeaveDetails] CHECK CONSTRAINT [FK_EmployeeRewardedLeaveDetails_ProjectMaster]
GO


