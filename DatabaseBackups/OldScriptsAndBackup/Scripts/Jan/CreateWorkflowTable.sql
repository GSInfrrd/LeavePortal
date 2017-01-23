USE [LeaveManagementSystem]
GO

/****** Object:  Table [dbo].[Workflow]    Script Date: 23-01-2017 12:12:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Workflow](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RefLeaveTransactionId] [bigint] NOT NULL,
	[RefApproverId] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL CONSTRAINT [DF_Workflow_CreatedDate]  DEFAULT (getdate()),
	[ModifiedDate] [datetime2](7) NULL,
	[RefStatus] [int] NOT NULL,
	[RefCreatedBy] [int] NULL,
	[RefModifiedBy] [int] NULL,
	[ManagerComments] [nvarchar](max) NULL,
 CONSTRAINT [PK_Workflow] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Workflow]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetailsCreatedById_Workflow] FOREIGN KEY([RefCreatedBy])
REFERENCES [dbo].[EmployeeDetails] ([Id])
GO

ALTER TABLE [dbo].[Workflow] CHECK CONSTRAINT [FK_EmployeeDetailsCreatedById_Workflow]
GO

ALTER TABLE [dbo].[Workflow]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetailsId_Workflow] FOREIGN KEY([RefApproverId])
REFERENCES [dbo].[EmployeeDetails] ([Id])
GO

ALTER TABLE [dbo].[Workflow] CHECK CONSTRAINT [FK_EmployeeDetailsId_Workflow]
GO

ALTER TABLE [dbo].[Workflow]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetailsModifiedById_Workflow] FOREIGN KEY([RefModifiedBy])
REFERENCES [dbo].[EmployeeDetails] ([Id])
GO

ALTER TABLE [dbo].[Workflow] CHECK CONSTRAINT [FK_EmployeeDetailsModifiedById_Workflow]
GO

ALTER TABLE [dbo].[Workflow]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeLeaveTransaction_Workflow] FOREIGN KEY([RefLeaveTransactionId])
REFERENCES [dbo].[EmployeeLeaveTransaction] ([Id])
GO

ALTER TABLE [dbo].[Workflow] CHECK CONSTRAINT [FK_EmployeeLeaveTransaction_Workflow]
GO

ALTER TABLE [dbo].[Workflow]  WITH CHECK ADD  CONSTRAINT [FK_MasterDataValueStatus_Workflow] FOREIGN KEY([RefStatus])
REFERENCES [dbo].[MasterDataValue] ([Id])
GO

ALTER TABLE [dbo].[Workflow] CHECK CONSTRAINT [FK_MasterDataValueStatus_Workflow]
GO


