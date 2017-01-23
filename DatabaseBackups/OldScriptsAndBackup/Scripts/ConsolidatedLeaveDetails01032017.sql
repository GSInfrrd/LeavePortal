USE [LeaveManagementSystem]
GO

/****** Object:  Table [dbo].[ConsolidatedEmployeeLeaveDetails]    Script Date: 1/3/2017 12:29:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConsolidatedEmployeeLeaveDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RefEmployeeId] [int] NOT NULL,
	[EarnedLeavesCount] [int] NULL,
	[AppliedLeavesCount] [int] NULL,
	[WorkFromHomeCount] [int] NULL,
	[LossofPayCount] [int] NULL,
	[CreatedDate] [date] NOT NULL,
 CONSTRAINT [PK_ConsolidatedEmployeeLeaveDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConsolidatedEmployeeLeaveDetails]  WITH CHECK ADD  CONSTRAINT [FK_ConsolidatedEmployeeLeaveDetails_EmployeeDetails] FOREIGN KEY([RefEmployeeId])
REFERENCES [dbo].[EmployeeDetails] ([Id])
GO

ALTER TABLE [dbo].[ConsolidatedEmployeeLeaveDetails] CHECK CONSTRAINT [FK_ConsolidatedEmployeeLeaveDetails_EmployeeDetails]
GO


