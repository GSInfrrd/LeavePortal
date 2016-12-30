USE [LeaveManagementSystem]
GO
ALTER TABLE [dbo].[WorkFromHome] DROP CONSTRAINT [FK_WorkFromHome_RefStatus]
GO
ALTER TABLE [dbo].[WorkFromHome] DROP CONSTRAINT [FK_WorkFromHome_RefReason]
GO
ALTER TABLE [dbo].[WorkFromHome] DROP CONSTRAINT [FK_WorkFromHome_refEmpID]
GO
/****** Object:  Table [dbo].[WorkFromHome]    Script Date: 12/30/2016 12:35:39 PM ******/
DROP TABLE [dbo].[WorkFromHome]
GO
/****** Object:  Table [dbo].[WorkFromHome]    Script Date: 12/30/2016 12:35:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkFromHome](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RefEmployeeId] [int] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL CONSTRAINT [DF_WorkFromHome_CreatedDate]  DEFAULT (getdate()),
	[RefStatus] [int] NOT NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime2](7) NULL,
	[RefReason] [int] NOT NULL,
 CONSTRAINT [PK_WorkFromHome] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WorkFromHome]  WITH CHECK ADD  CONSTRAINT [FK_WorkFromHome_refEmpID] FOREIGN KEY([RefEmployeeId])
REFERENCES [dbo].[EmployeeDetails] ([Id])
GO
ALTER TABLE [dbo].[WorkFromHome] CHECK CONSTRAINT [FK_WorkFromHome_refEmpID]
GO
ALTER TABLE [dbo].[WorkFromHome]  WITH CHECK ADD  CONSTRAINT [FK_WorkFromHome_RefReason] FOREIGN KEY([RefReason])
REFERENCES [dbo].[MasterDataValue] ([Id])
GO
ALTER TABLE [dbo].[WorkFromHome] CHECK CONSTRAINT [FK_WorkFromHome_RefReason]
GO
ALTER TABLE [dbo].[WorkFromHome]  WITH CHECK ADD  CONSTRAINT [FK_WorkFromHome_RefStatus] FOREIGN KEY([RefStatus])
REFERENCES [dbo].[MasterDataValue] ([Id])
GO
ALTER TABLE [dbo].[WorkFromHome] CHECK CONSTRAINT [FK_WorkFromHome_RefStatus]
GO
