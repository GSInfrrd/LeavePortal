USE [LeaveManagementSystem]
GO

/****** Object:  Table [dbo].[EmailTemplateMaster]    Script Date: 24-01-2017 11:46:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmailTemplateMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Template] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_EmailTemplateMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


USE [LeaveManagementSystem]
GO

/****** Object:  Table [dbo].[EmailTemplateMapping]    Script Date: 24-01-2017 11:47:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmailTemplateMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ActionName] [nvarchar](max) NOT NULL,
	[RefTemplateId] [int] NOT NULL,
 CONSTRAINT [PK_EmailTemplateMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[EmailTemplateMapping]  WITH CHECK ADD  CONSTRAINT [FK_EmailTemplateMaster_EmailTemplateMapping] FOREIGN KEY([RefTemplateId])
REFERENCES [dbo].[EmailTemplateMaster] ([Id])
GO

ALTER TABLE [dbo].[EmailTemplateMapping] CHECK CONSTRAINT [FK_EmailTemplateMaster_EmailTemplateMapping]
GO


/****** Script for SelectTopNRows command from SSMS  ******/
use [LeaveManagementSystem]

  

  insert into [LeaveManagementSystem].[dbo].[EmailTemplateMaster] values('\App_Data\EmailTemplates\LeaveApplied.cshtml')
  insert into [LeaveManagementSystem].[dbo].[EmailTemplateMaster] values('\App_Data\EmailTemplates\ApproveLeave.cshtml')
  insert into [LeaveManagementSystem].[dbo].[EmailTemplateMaster] values('\App_Data\EmailTemplates\RejectLeave.cshtml')
  insert into [LeaveManagementSystem].[dbo].[EmailTemplateMaster] values('\App_Data\EmailTemplates\ReassignLeave.cshtml')
  insert into [LeaveManagementSystem].[dbo].[EmailTemplateMaster] values('\App_Data\EmailTemplates\AddResourceRequest.cshtml')
  insert into [LeaveManagementSystem].[dbo].[EmailTemplateMaster] values('\App_Data\EmailTemplates\ResourceRequestUpdate.cshtml')


  insert into [LeaveManagementSystem].[dbo].[EmailTemplateMapping] values('ApplyLeave',1)
  insert into [LeaveManagementSystem].[dbo].[EmailTemplateMapping] values('ApproveLeave',2)
  insert into [LeaveManagementSystem].[dbo].[EmailTemplateMapping] values('RejectLeave',3)
  insert into [LeaveManagementSystem].[dbo].[EmailTemplateMapping] values('ReassignLeave',4)
  insert into [LeaveManagementSystem].[dbo].[EmailTemplateMapping] values('AddResourceRequest',5)
  insert into [LeaveManagementSystem].[dbo].[EmailTemplateMapping] values('ResourceRequestUpdate',6)