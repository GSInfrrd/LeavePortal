USE [LeaveManagementSystem]
GO

/****** Object:  Table [dbo].[EmployeeLeaveTransactionHistory]    Script Date: 23-01-2017 12:11:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmployeeLeaveTransactionHistory](
	[PID] [bigint] IDENTITY(1,1) NOT NULL,
	[Id] [bigint] NOT NULL,
	[RefEmployeeId] [int] NOT NULL,
	[FromDate] [datetime2](7) NOT NULL,
	[ToDate] [datetime2](7) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[RefStatus] [int] NOT NULL,
	[NumberOfWorkingDays] [float] NOT NULL,
	[RefLeaveType] [int] NOT NULL,
	[EmployeeComment] [nvarchar](max) NULL,
	[ManagerComment] [nvarchar](max) NULL,
	[ModifiedDate] [datetime2](7) NULL,
	[RefCreatedBy] [int] NULL,
	[RefModifiedBy] [int] NULL,
 CONSTRAINT [PK_EmployeeLeaveTransactionHistory] PRIMARY KEY CLUSTERED 
(
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory] ADD  CONSTRAINT [DF_EmployeeLeaveTransactionHistory_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetails_EmployeeLeaveTransactionHistory] FOREIGN KEY([RefEmployeeId])
REFERENCES [dbo].[EmployeeDetails] ([Id])
GO

ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory] CHECK CONSTRAINT [FK_EmployeeDetails_EmployeeLeaveTransactionHistory]
GO

ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetailsCreatedById_EmployeeLeaveTransactionHistory] FOREIGN KEY([RefCreatedBy])
REFERENCES [dbo].[EmployeeDetails] ([Id])
GO

ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory] CHECK CONSTRAINT [FK_EmployeeDetailsCreatedById_EmployeeLeaveTransactionHistory]
GO

ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetilasModifiedById_EmployeeLeaveTransactionHistory] FOREIGN KEY([RefModifiedBy])
REFERENCES [dbo].[EmployeeDetails] ([Id])
GO

ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory] CHECK CONSTRAINT [FK_EmployeeDetilasModifiedById_EmployeeLeaveTransactionHistory]
GO

ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_MasterDataValue_EmployeeLeaveTransactionHistory] FOREIGN KEY([RefLeaveType])
REFERENCES [dbo].[MasterDataValue] ([Id])
GO

ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory] CHECK CONSTRAINT [FK_MasterDataValue_EmployeeLeaveTransactionHistory]
GO

ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_MasterDataValueStatus_EmployeeLeaveTransactionHistory] FOREIGN KEY([RefStatus])
REFERENCES [dbo].[MasterDataValue] ([Id])
GO

ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory] CHECK CONSTRAINT [FK_MasterDataValueStatus_EmployeeLeaveTransactionHistory]
GO


