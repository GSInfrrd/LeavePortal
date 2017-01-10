USE [LeaveManagementSystem]
GO

/****** Object:  Table [dbo].[ResourceRequestDetail]    Script Date: 1/10/2017 12:23:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ResourceRequestDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RequestFromId] [int] NOT NULL,
	[RequestToId] [int] NOT NULL,
	[ResourceRequestTitle] [varchar](250) NULL,
	[NumberRequestedResources] [int] NOT NULL,
	[Skills] [varchar](250) NULL,
	[Ticket] [varchar](250) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[Status] [varchar](250) NULL,
 CONSTRAINT [PK_ResourceRequestDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


