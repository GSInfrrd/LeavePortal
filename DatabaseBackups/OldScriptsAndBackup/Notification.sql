ALTER DATABASE LeaveManagementSystem SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE ;

USE [LeaveManagementSystem]
GO

/****** Object:  Table [dbo].[Notification]    Script Date: 10-01-2017 16:32:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Notification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RefEmployeeId] [int] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[RefNotificationType] [int] NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetails_Notification] FOREIGN KEY([RefEmployeeId])
REFERENCES [dbo].[EmployeeDetails] ([Id])
GO

ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_EmployeeDetails_Notification]
GO

ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_MasterDataValue_Notification] FOREIGN KEY([RefNotificationType])
REFERENCES [dbo].[MasterDataValue] ([Id])
GO

ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_MasterDataValue_Notification]
GO







insert into [LeaveManagementSystem].[dbo].[MasterDataType]
  ([Type]) values('NotificationType')




  insert into [LeaveManagementSystem].[dbo].[MasterDataValue]([RefMasterType]
      ,[Value]) values(7,'LeaveNotification')