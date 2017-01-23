USE [LeaveManagementSystem]
GO

/****** Object:  Table [dbo].[EmployeeLeaveTransaction]    Script Date: 23-01-2017 12:10:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmployeeLeaveTransaction](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RefEmployeeId] [int] NOT NULL,
	[FromDate] [date] NOT NULL,
	[ToDate] [date] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL CONSTRAINT [DF_EmployeeLeaveTransaction_CreatedDate]  DEFAULT (getdate()),
	[RefStatus] [int] NOT NULL,
	[NumberOfWorkingDays] [float] NOT NULL,
	[RefLeaveType] [int] NOT NULL,
	[EmployeeComment] [nvarchar](max) NULL,
	[ModifiedDate] [datetime2](7) NULL,
	[RefCreatedBy] [int] NULL,
	[RefModifiedBy] [int] NULL,
	[RefTransactionType] [int] NOT NULL DEFAULT ((2052)),
 CONSTRAINT [PK_EmployeeLeaveTransaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[EmployeeLeaveTransaction]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetails_EmployeeLeaveTransaction] FOREIGN KEY([RefEmployeeId])
REFERENCES [dbo].[EmployeeDetails] ([Id])
GO

ALTER TABLE [dbo].[EmployeeLeaveTransaction] CHECK CONSTRAINT [FK_EmployeeDetails_EmployeeLeaveTransaction]
GO

ALTER TABLE [dbo].[EmployeeLeaveTransaction]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetailsCreatedById_EmployeeLeaveTransaction] FOREIGN KEY([RefCreatedBy])
REFERENCES [dbo].[EmployeeDetails] ([Id])
GO

ALTER TABLE [dbo].[EmployeeLeaveTransaction] CHECK CONSTRAINT [FK_EmployeeDetailsCreatedById_EmployeeLeaveTransaction]
GO

ALTER TABLE [dbo].[EmployeeLeaveTransaction]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetialsModifiedById_EmployeeLeaveTransaction] FOREIGN KEY([RefModifiedBy])
REFERENCES [dbo].[EmployeeDetails] ([Id])
GO

ALTER TABLE [dbo].[EmployeeLeaveTransaction] CHECK CONSTRAINT [FK_EmployeeDetialsModifiedById_EmployeeLeaveTransaction]
GO

ALTER TABLE [dbo].[EmployeeLeaveTransaction]  WITH CHECK ADD  CONSTRAINT [FK_MasterDataValue_EmployeeLeaveTransaction] FOREIGN KEY([RefLeaveType])
REFERENCES [dbo].[MasterDataValue] ([Id])
GO

ALTER TABLE [dbo].[EmployeeLeaveTransaction] CHECK CONSTRAINT [FK_MasterDataValue_EmployeeLeaveTransaction]
GO

ALTER TABLE [dbo].[EmployeeLeaveTransaction]  WITH CHECK ADD  CONSTRAINT [FK_MasterDataValueStatus_EmployeeLeaveTransaction] FOREIGN KEY([RefStatus])
REFERENCES [dbo].[MasterDataValue] ([Id])
GO

ALTER TABLE [dbo].[EmployeeLeaveTransaction] CHECK CONSTRAINT [FK_MasterDataValueStatus_EmployeeLeaveTransaction]
GO


