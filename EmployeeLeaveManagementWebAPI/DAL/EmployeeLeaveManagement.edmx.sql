
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/04/2017 15:51:55
-- Generated from EDMX file: D:\GIT\EmployeeLeaveManagementWebAPI\DAL\EmployeeLeaveManagement.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LeaveManagementSystem];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ConsolidatedEmployeeLeaveDetails_EmployeeDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConsolidatedEmployeeLeaveDetails] DROP CONSTRAINT [FK_ConsolidatedEmployeeLeaveDetails_EmployeeDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpDetails_EmployeeContactDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeContactDetails] DROP CONSTRAINT [FK_EmpDetails_EmployeeContactDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeDetails_EmployeeLeaveTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeLeaveTransaction] DROP CONSTRAINT [FK_EmployeeDetails_EmployeeLeaveTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeDetails_EmployeeLeaveTransactionHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory] DROP CONSTRAINT [FK_EmployeeDetails_EmployeeLeaveTransactionHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeDetails_EmployeeProjectDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeProjectDetail] DROP CONSTRAINT [FK_EmployeeDetails_EmployeeProjectDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeDetails_MasterDataValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeDetails] DROP CONSTRAINT [FK_EmployeeDetails_MasterDataValue];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeDetails_Notification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notification] DROP CONSTRAINT [FK_EmployeeDetails_Notification];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeDetails_UserAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserAccount] DROP CONSTRAINT [FK_EmployeeDetails_UserAccount];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeDetailsId_Workflow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Workflow] DROP CONSTRAINT [FK_EmployeeDetailsId_Workflow];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeEducationDetails_EmployeeDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeEducationDetails] DROP CONSTRAINT [FK_EmployeeEducationDetails_EmployeeDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeExperienceDetails_EmployeeDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeExperienceDetails] DROP CONSTRAINT [FK_EmployeeExperienceDetails_EmployeeDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeLeaveTransaction_Workflow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Workflow] DROP CONSTRAINT [FK_EmployeeLeaveTransaction_Workflow];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeSkills_EmployeeDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeSkills] DROP CONSTRAINT [FK_EmployeeSkills_EmployeeDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterDataType_MasterDataValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MasterDataValue] DROP CONSTRAINT [FK_MasterDataType_MasterDataValue];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterDataValue_EmployeeContactDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeContactDetails] DROP CONSTRAINT [FK_MasterDataValue_EmployeeContactDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterDataValue_EmployeeLeaveTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeLeaveTransaction] DROP CONSTRAINT [FK_MasterDataValue_EmployeeLeaveTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterDataValue_EmployeeLeaveTransactionHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory] DROP CONSTRAINT [FK_MasterDataValue_EmployeeLeaveTransactionHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterDataValue_EmployeeProjectDetail1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeProjectDetail] DROP CONSTRAINT [FK_MasterDataValue_EmployeeProjectDetail1];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterDataValue_LeaveMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LeaveMaster] DROP CONSTRAINT [FK_MasterDataValue_LeaveMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterDataValueStatus_EmployeeLeaveTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeLeaveTransaction] DROP CONSTRAINT [FK_MasterDataValueStatus_EmployeeLeaveTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterDataValueStatus_EmployeeLeaveTransactionHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory] DROP CONSTRAINT [FK_MasterDataValueStatus_EmployeeLeaveTransactionHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterDataValueStatus_Workflow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Workflow] DROP CONSTRAINT [FK_MasterDataValueStatus_Workflow];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Announcements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Announcements];
GO
IF OBJECT_ID(N'[dbo].[ConsolidatedEmployeeLeaveDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConsolidatedEmployeeLeaveDetails];
GO
IF OBJECT_ID(N'[dbo].[Contacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contacts];
GO
IF OBJECT_ID(N'[dbo].[EmployeeContactDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeContactDetails];
GO
IF OBJECT_ID(N'[dbo].[EmployeeDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeDetails];
GO
IF OBJECT_ID(N'[dbo].[EmployeeEducationDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeEducationDetails];
GO
IF OBJECT_ID(N'[dbo].[EmployeeExperienceDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeExperienceDetails];
GO
IF OBJECT_ID(N'[dbo].[EmployeeLeaveTransaction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeLeaveTransaction];
GO
IF OBJECT_ID(N'[dbo].[EmployeeLeaveTransactionHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeLeaveTransactionHistory];
GO
IF OBJECT_ID(N'[dbo].[EmployeeProjectDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeProjectDetail];
GO
IF OBJECT_ID(N'[dbo].[EmployeeSkills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeSkills];
GO
IF OBJECT_ID(N'[dbo].[Holidays]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Holidays];
GO
IF OBJECT_ID(N'[dbo].[LeaveMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LeaveMaster];
GO
IF OBJECT_ID(N'[dbo].[MasterDataType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MasterDataType];
GO
IF OBJECT_ID(N'[dbo].[MasterDataValue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MasterDataValue];
GO
IF OBJECT_ID(N'[dbo].[Notification]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notification];
GO
IF OBJECT_ID(N'[dbo].[UserAccount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAccount];
GO
IF OBJECT_ID(N'[dbo].[Workflow]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Workflow];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'EmployeeContactDetails'
CREATE TABLE [dbo].[EmployeeContactDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RefContactType] int  NOT NULL,
    [ContactDetails] nvarchar(max)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL,
    [RefEmpId] int  NOT NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ModifiedBy] nvarchar(50)  NULL
);
GO

-- Creating table 'EmployeeDetails'
CREATE TABLE [dbo].[EmployeeDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RefRoleId] int  NOT NULL,
    [DateOfJoining] datetime  NULL,
    [ManagerId] int  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL,
    [Experience] int  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ModifiedBy] nvarchar(50)  NULL,
    [EmpNumber] nvarchar(50)  NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [DateOfBirth] datetime  NOT NULL,
    [City] varchar(50)  NULL,
    [Country] varchar(50)  NULL,
    [LastName] varchar(50)  NULL,
    [PhoneNumber] varchar(20)  NULL,
    [ImagePath] varchar(250)  NULL,
    [Bio] varchar(250)  NULL,
    [RefHierarchyLevel] int  NULL
);
GO

-- Creating table 'EmployeeProjectDetails'
CREATE TABLE [dbo].[EmployeeProjectDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RefEmployeeId] int  NOT NULL,
    [RefProjectId] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ModifiedBy] nvarchar(50)  NULL
);
GO

-- Creating table 'Holidays'
CREATE TABLE [dbo].[Holidays] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Date] datetime  NULL,
    [Description] nvarchar(max)  NULL,
    [Year] bigint  NOT NULL,
    [CreatedDate] datetime  NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ModifiedBy] nvarchar(50)  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'LeaveMasters'
CREATE TABLE [dbo].[LeaveMasters] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RefLeaveType] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Count] int  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ModifiedBy] nvarchar(50)  NULL
);
GO

-- Creating table 'MasterDataTypes'
CREATE TABLE [dbo].[MasterDataTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'MasterDataValues'
CREATE TABLE [dbo].[MasterDataValues] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RefMasterType] int  NOT NULL,
    [Value] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'UserAccounts'
CREATE TABLE [dbo].[UserAccounts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [Lastlogin] datetime  NULL,
    [RefEmployeeId] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ModifiedBy] nvarchar(50)  NULL
);
GO

-- Creating table 'Workflows'
CREATE TABLE [dbo].[Workflows] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [RefLeaveTransactionId] bigint  NOT NULL,
    [RefApproverId] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL,
    [RefStatus] int  NOT NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ModifiedBy] nvarchar(50)  NULL,
    [ManagerComments] nvarchar(max)  NULL
);
GO

-- Creating table 'Announcements'
CREATE TABLE [dbo].[Announcements] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(250)  NULL,
    [CarouselContent] varchar(max)  NULL,
    [ImagePath] varchar(max)  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'EmployeeEducationDetails'
CREATE TABLE [dbo].[EmployeeEducationDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RefEmployeeId] int  NOT NULL,
    [Institution] varchar(250)  NOT NULL,
    [Degree] varchar(100)  NOT NULL,
    [FromDate] datetime  NOT NULL,
    [ToDate] datetime  NOT NULL
);
GO

-- Creating table 'EmployeeExperienceDetails'
CREATE TABLE [dbo].[EmployeeExperienceDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RefEmployeeId] int  NOT NULL,
    [CompanyName] varchar(250)  NOT NULL,
    [Role] varchar(250)  NOT NULL,
    [FromDate] datetime  NOT NULL,
    [ToDate] datetime  NOT NULL
);
GO

-- Creating table 'EmployeeSkills'
CREATE TABLE [dbo].[EmployeeSkills] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RefEmployeeId] int  NOT NULL,
    [Skill] nchar(50)  NULL
);
GO

-- Creating table 'WorkFromHomes'
CREATE TABLE [dbo].[WorkFromHomes] (
    [Id] bigint  NOT NULL,
    [RefEmployeeId] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [RefStatus] int  NOT NULL,
    [CreatedBy] int  NULL,
    [ModifiedBy] int  NULL,
    [ModifiedDate] datetime  NULL,
    [RefReason] int  NOT NULL
);
GO

-- Creating table 'ConsolidatedEmployeeLeaveDetails'
CREATE TABLE [dbo].[ConsolidatedEmployeeLeaveDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RefEmployeeId] int  NOT NULL,
    [EarnedLeavesCount] int  NULL,
    [AppliedLeavesCount] int  NULL,
    [WorkFromHomeCount] int  NULL,
    [LossofPayCount] int  NULL,
    [CreatedDate] datetime  NOT NULL
);
GO

-- Creating table 'EmployeeLeaveTransactions'
CREATE TABLE [dbo].[EmployeeLeaveTransactions] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [RefEmployeeId] int  NOT NULL,
    [FromDate] datetime  NOT NULL,
    [ToDate] datetime  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [RefStatus] int  NOT NULL,
    [NumberOfWorkingDays] float  NOT NULL,
    [RefLeaveType] int  NOT NULL,
    [EmployeeComment] nvarchar(max)  NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ModifiedBy] nvarchar(50)  NULL
);
GO

-- Creating table 'EmployeeLeaveTransactionHistories'
CREATE TABLE [dbo].[EmployeeLeaveTransactionHistories] (
    [Id] bigint  NOT NULL,
    [RefEmployeeId] int  NOT NULL,
    [FromDate] datetime  NOT NULL,
    [ToDate] datetime  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [RefStatus] int  NOT NULL,
    [NumberOfWorkingDays] float  NOT NULL,
    [RefLeaveType] int  NOT NULL,
    [EmployeeComment] nvarchar(max)  NULL,
    [ManagerComment] nvarchar(max)  NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ModifiedBy] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Contacts'
CREATE TABLE [dbo].[Contacts] (
    [ContactID] int IDENTITY(1,1) NOT NULL,
    [ContactName] varchar(100)  NOT NULL,
    [ContactNo] varchar(50)  NOT NULL,
    [AddedOn] datetime  NOT NULL
);
GO

-- Creating table 'EmployeeLeaveTransaction1'
CREATE TABLE [dbo].[EmployeeLeaveTransaction1] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [RefEmployeeId] int  NOT NULL,
    [FromDate] datetime  NOT NULL,
    [ToDate] datetime  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [RefStatus] int  NOT NULL,
    [NumberOfWorkingDays] float  NOT NULL,
    [RefLeaveType] int  NOT NULL,
    [EmployeeComment] nvarchar(max)  NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ModifiedBy] nvarchar(50)  NULL
);
GO

-- Creating table 'EmployeeLeaveTransactionHistory1'
CREATE TABLE [dbo].[EmployeeLeaveTransactionHistory1] (
    [Id] bigint  NOT NULL,
    [RefEmployeeId] int  NOT NULL,
    [FromDate] datetime  NOT NULL,
    [ToDate] datetime  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [RefStatus] int  NOT NULL,
    [NumberOfWorkingDays] float  NOT NULL,
    [RefLeaveType] int  NOT NULL,
    [EmployeeComment] nvarchar(max)  NULL,
    [ManagerComment] nvarchar(max)  NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ModifiedBy] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'EmployeeProjectDetail1'
CREATE TABLE [dbo].[EmployeeProjectDetail1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RefEmployeeId] int  NOT NULL,
    [RefProjectId] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ModifiedBy] nvarchar(50)  NULL
);
GO

-- Creating table 'LeaveMaster1'
CREATE TABLE [dbo].[LeaveMaster1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RefLeaveType] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Count] int  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ModifiedBy] nvarchar(50)  NULL
);
GO

-- Creating table 'MasterDataType1'
CREATE TABLE [dbo].[MasterDataType1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'MasterDataValue1'
CREATE TABLE [dbo].[MasterDataValue1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RefMasterType] int  NOT NULL,
    [Value] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Notifications'
CREATE TABLE [dbo].[Notifications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RefEmployeeId] int  NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [Status] int  NOT NULL
);
GO

-- Creating table 'UserAccount1'
CREATE TABLE [dbo].[UserAccount1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [Lastlogin] datetime  NULL,
    [RefEmployeeId] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ModifiedBy] nvarchar(50)  NULL
);
GO

-- Creating table 'Workflow1'
CREATE TABLE [dbo].[Workflow1] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [RefLeaveTransactionId] bigint  NOT NULL,
    [RefApproverId] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NULL,
    [RefStatus] int  NOT NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ModifiedBy] nvarchar(50)  NULL,
    [ManagerComments] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'EmployeeContactDetails'
ALTER TABLE [dbo].[EmployeeContactDetails]
ADD CONSTRAINT [PK_EmployeeContactDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeDetails'
ALTER TABLE [dbo].[EmployeeDetails]
ADD CONSTRAINT [PK_EmployeeDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeProjectDetails'
ALTER TABLE [dbo].[EmployeeProjectDetails]
ADD CONSTRAINT [PK_EmployeeProjectDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Holidays'
ALTER TABLE [dbo].[Holidays]
ADD CONSTRAINT [PK_Holidays]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LeaveMasters'
ALTER TABLE [dbo].[LeaveMasters]
ADD CONSTRAINT [PK_LeaveMasters]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MasterDataTypes'
ALTER TABLE [dbo].[MasterDataTypes]
ADD CONSTRAINT [PK_MasterDataTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MasterDataValues'
ALTER TABLE [dbo].[MasterDataValues]
ADD CONSTRAINT [PK_MasterDataValues]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserAccounts'
ALTER TABLE [dbo].[UserAccounts]
ADD CONSTRAINT [PK_UserAccounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Workflows'
ALTER TABLE [dbo].[Workflows]
ADD CONSTRAINT [PK_Workflows]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Announcements'
ALTER TABLE [dbo].[Announcements]
ADD CONSTRAINT [PK_Announcements]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeEducationDetails'
ALTER TABLE [dbo].[EmployeeEducationDetails]
ADD CONSTRAINT [PK_EmployeeEducationDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeExperienceDetails'
ALTER TABLE [dbo].[EmployeeExperienceDetails]
ADD CONSTRAINT [PK_EmployeeExperienceDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeSkills'
ALTER TABLE [dbo].[EmployeeSkills]
ADD CONSTRAINT [PK_EmployeeSkills]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WorkFromHomes'
ALTER TABLE [dbo].[WorkFromHomes]
ADD CONSTRAINT [PK_WorkFromHomes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ConsolidatedEmployeeLeaveDetails'
ALTER TABLE [dbo].[ConsolidatedEmployeeLeaveDetails]
ADD CONSTRAINT [PK_ConsolidatedEmployeeLeaveDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeLeaveTransactions'
ALTER TABLE [dbo].[EmployeeLeaveTransactions]
ADD CONSTRAINT [PK_EmployeeLeaveTransactions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeLeaveTransactionHistories'
ALTER TABLE [dbo].[EmployeeLeaveTransactionHistories]
ADD CONSTRAINT [PK_EmployeeLeaveTransactionHistories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ContactID] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [PK_Contacts]
    PRIMARY KEY CLUSTERED ([ContactID] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeLeaveTransaction1'
ALTER TABLE [dbo].[EmployeeLeaveTransaction1]
ADD CONSTRAINT [PK_EmployeeLeaveTransaction1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeLeaveTransactionHistory1'
ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory1]
ADD CONSTRAINT [PK_EmployeeLeaveTransactionHistory1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeProjectDetail1'
ALTER TABLE [dbo].[EmployeeProjectDetail1]
ADD CONSTRAINT [PK_EmployeeProjectDetail1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LeaveMaster1'
ALTER TABLE [dbo].[LeaveMaster1]
ADD CONSTRAINT [PK_LeaveMaster1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MasterDataType1'
ALTER TABLE [dbo].[MasterDataType1]
ADD CONSTRAINT [PK_MasterDataType1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MasterDataValue1'
ALTER TABLE [dbo].[MasterDataValue1]
ADD CONSTRAINT [PK_MasterDataValue1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [PK_Notifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserAccount1'
ALTER TABLE [dbo].[UserAccount1]
ADD CONSTRAINT [PK_UserAccount1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Workflow1'
ALTER TABLE [dbo].[Workflow1]
ADD CONSTRAINT [PK_Workflow1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [RefEmpId] in table 'EmployeeContactDetails'
ALTER TABLE [dbo].[EmployeeContactDetails]
ADD CONSTRAINT [FK_EmpDetails_EmployeeContactDetails]
    FOREIGN KEY ([RefEmpId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpDetails_EmployeeContactDetails'
CREATE INDEX [IX_FK_EmpDetails_EmployeeContactDetails]
ON [dbo].[EmployeeContactDetails]
    ([RefEmpId]);
GO

-- Creating foreign key on [RefContactType] in table 'EmployeeContactDetails'
ALTER TABLE [dbo].[EmployeeContactDetails]
ADD CONSTRAINT [FK_MasterDataValue_EmployeeContactDetails]
    FOREIGN KEY ([RefContactType])
    REFERENCES [dbo].[MasterDataValues]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValue_EmployeeContactDetails'
CREATE INDEX [IX_FK_MasterDataValue_EmployeeContactDetails]
ON [dbo].[EmployeeContactDetails]
    ([RefContactType]);
GO

-- Creating foreign key on [RefEmployeeId] in table 'EmployeeProjectDetails'
ALTER TABLE [dbo].[EmployeeProjectDetails]
ADD CONSTRAINT [FK_EmployeeDetails_EmployeeProjectDetail]
    FOREIGN KEY ([RefEmployeeId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDetails_EmployeeProjectDetail'
CREATE INDEX [IX_FK_EmployeeDetails_EmployeeProjectDetail]
ON [dbo].[EmployeeProjectDetails]
    ([RefEmployeeId]);
GO

-- Creating foreign key on [RefEmployeeId] in table 'UserAccounts'
ALTER TABLE [dbo].[UserAccounts]
ADD CONSTRAINT [FK_EmployeeDetails_UserAccount]
    FOREIGN KEY ([RefEmployeeId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDetails_UserAccount'
CREATE INDEX [IX_FK_EmployeeDetails_UserAccount]
ON [dbo].[UserAccounts]
    ([RefEmployeeId]);
GO

-- Creating foreign key on [RefApproverId] in table 'Workflows'
ALTER TABLE [dbo].[Workflows]
ADD CONSTRAINT [FK_EmployeeDetailsId_Workflow]
    FOREIGN KEY ([RefApproverId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDetailsId_Workflow'
CREATE INDEX [IX_FK_EmployeeDetailsId_Workflow]
ON [dbo].[Workflows]
    ([RefApproverId]);
GO

-- Creating foreign key on [RefRoleId] in table 'EmployeeDetails'
ALTER TABLE [dbo].[EmployeeDetails]
ADD CONSTRAINT [FK_MasterDataValue_EmployeeDetails]
    FOREIGN KEY ([RefRoleId])
    REFERENCES [dbo].[MasterDataValues]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValue_EmployeeDetails'
CREATE INDEX [IX_FK_MasterDataValue_EmployeeDetails]
ON [dbo].[EmployeeDetails]
    ([RefRoleId]);
GO

-- Creating foreign key on [RefProjectId] in table 'EmployeeProjectDetails'
ALTER TABLE [dbo].[EmployeeProjectDetails]
ADD CONSTRAINT [FK_MasterDataValue_EmployeeProjectDetail1]
    FOREIGN KEY ([RefProjectId])
    REFERENCES [dbo].[MasterDataValues]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValue_EmployeeProjectDetail1'
CREATE INDEX [IX_FK_MasterDataValue_EmployeeProjectDetail1]
ON [dbo].[EmployeeProjectDetails]
    ([RefProjectId]);
GO

-- Creating foreign key on [RefLeaveType] in table 'LeaveMasters'
ALTER TABLE [dbo].[LeaveMasters]
ADD CONSTRAINT [FK_MasterDataValue_LeaveMaster]
    FOREIGN KEY ([RefLeaveType])
    REFERENCES [dbo].[MasterDataValues]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValue_LeaveMaster'
CREATE INDEX [IX_FK_MasterDataValue_LeaveMaster]
ON [dbo].[LeaveMasters]
    ([RefLeaveType]);
GO

-- Creating foreign key on [RefMasterType] in table 'MasterDataValues'
ALTER TABLE [dbo].[MasterDataValues]
ADD CONSTRAINT [FK_MasterDataType_MasterDataValue]
    FOREIGN KEY ([RefMasterType])
    REFERENCES [dbo].[MasterDataTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataType_MasterDataValue'
CREATE INDEX [IX_FK_MasterDataType_MasterDataValue]
ON [dbo].[MasterDataValues]
    ([RefMasterType]);
GO

-- Creating foreign key on [RefStatus] in table 'Workflows'
ALTER TABLE [dbo].[Workflows]
ADD CONSTRAINT [FK_MasterDataValueStatus_Workflow]
    FOREIGN KEY ([RefStatus])
    REFERENCES [dbo].[MasterDataValues]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValueStatus_Workflow'
CREATE INDEX [IX_FK_MasterDataValueStatus_Workflow]
ON [dbo].[Workflows]
    ([RefStatus]);
GO

-- Creating foreign key on [RefEmployeeId] in table 'EmployeeEducationDetails'
ALTER TABLE [dbo].[EmployeeEducationDetails]
ADD CONSTRAINT [FK_EmployeeEducationDetails_EmployeeDetails]
    FOREIGN KEY ([RefEmployeeId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeEducationDetails_EmployeeDetails'
CREATE INDEX [IX_FK_EmployeeEducationDetails_EmployeeDetails]
ON [dbo].[EmployeeEducationDetails]
    ([RefEmployeeId]);
GO

-- Creating foreign key on [RefEmployeeId] in table 'EmployeeExperienceDetails'
ALTER TABLE [dbo].[EmployeeExperienceDetails]
ADD CONSTRAINT [FK_EmployeeExperienceDetails_EmployeeDetails]
    FOREIGN KEY ([RefEmployeeId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeExperienceDetails_EmployeeDetails'
CREATE INDEX [IX_FK_EmployeeExperienceDetails_EmployeeDetails]
ON [dbo].[EmployeeExperienceDetails]
    ([RefEmployeeId]);
GO

-- Creating foreign key on [RefHierarchyLevel] in table 'EmployeeDetails'
ALTER TABLE [dbo].[EmployeeDetails]
ADD CONSTRAINT [FK_EmployeeDetails_MasterDataValue]
    FOREIGN KEY ([RefHierarchyLevel])
    REFERENCES [dbo].[MasterDataValues]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDetails_MasterDataValue'
CREATE INDEX [IX_FK_EmployeeDetails_MasterDataValue]
ON [dbo].[EmployeeDetails]
    ([RefHierarchyLevel]);
GO

-- Creating foreign key on [RefEmployeeId] in table 'EmployeeSkills'
ALTER TABLE [dbo].[EmployeeSkills]
ADD CONSTRAINT [FK_EmployeeSkills_EmployeeDetails]
    FOREIGN KEY ([RefEmployeeId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeSkills_EmployeeDetails'
CREATE INDEX [IX_FK_EmployeeSkills_EmployeeDetails]
ON [dbo].[EmployeeSkills]
    ([RefEmployeeId]);
GO

-- Creating foreign key on [RefEmployeeId] in table 'WorkFromHomes'
ALTER TABLE [dbo].[WorkFromHomes]
ADD CONSTRAINT [FK_WorkFromHome_refEmpID]
    FOREIGN KEY ([RefEmployeeId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WorkFromHome_refEmpID'
CREATE INDEX [IX_FK_WorkFromHome_refEmpID]
ON [dbo].[WorkFromHomes]
    ([RefEmployeeId]);
GO

-- Creating foreign key on [RefStatus] in table 'WorkFromHomes'
ALTER TABLE [dbo].[WorkFromHomes]
ADD CONSTRAINT [FK_WorkFromHome_RefStatus]
    FOREIGN KEY ([RefStatus])
    REFERENCES [dbo].[MasterDataValues]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WorkFromHome_RefStatus'
CREATE INDEX [IX_FK_WorkFromHome_RefStatus]
ON [dbo].[WorkFromHomes]
    ([RefStatus]);
GO

-- Creating foreign key on [RefReason] in table 'WorkFromHomes'
ALTER TABLE [dbo].[WorkFromHomes]
ADD CONSTRAINT [FK_WorkFromHome_RefReason]
    FOREIGN KEY ([RefReason])
    REFERENCES [dbo].[MasterDataValues]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WorkFromHome_RefReason'
CREATE INDEX [IX_FK_WorkFromHome_RefReason]
ON [dbo].[WorkFromHomes]
    ([RefReason]);
GO

-- Creating foreign key on [RefEmployeeId] in table 'ConsolidatedEmployeeLeaveDetails'
ALTER TABLE [dbo].[ConsolidatedEmployeeLeaveDetails]
ADD CONSTRAINT [FK_ConsolidatedEmployeeLeaveDetails_EmployeeDetails]
    FOREIGN KEY ([RefEmployeeId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConsolidatedEmployeeLeaveDetails_EmployeeDetails'
CREATE INDEX [IX_FK_ConsolidatedEmployeeLeaveDetails_EmployeeDetails]
ON [dbo].[ConsolidatedEmployeeLeaveDetails]
    ([RefEmployeeId]);
GO

-- Creating foreign key on [RefEmployeeId] in table 'EmployeeLeaveTransactions'
ALTER TABLE [dbo].[EmployeeLeaveTransactions]
ADD CONSTRAINT [FK_EmployeeDetails_EmployeeLeaveTransaction]
    FOREIGN KEY ([RefEmployeeId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDetails_EmployeeLeaveTransaction'
CREATE INDEX [IX_FK_EmployeeDetails_EmployeeLeaveTransaction]
ON [dbo].[EmployeeLeaveTransactions]
    ([RefEmployeeId]);
GO

-- Creating foreign key on [RefEmployeeId] in table 'EmployeeLeaveTransactionHistories'
ALTER TABLE [dbo].[EmployeeLeaveTransactionHistories]
ADD CONSTRAINT [FK_EmployeeDetails_EmployeeLeaveTransactionHistory]
    FOREIGN KEY ([RefEmployeeId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDetails_EmployeeLeaveTransactionHistory'
CREATE INDEX [IX_FK_EmployeeDetails_EmployeeLeaveTransactionHistory]
ON [dbo].[EmployeeLeaveTransactionHistories]
    ([RefEmployeeId]);
GO

-- Creating foreign key on [RefLeaveTransactionId] in table 'Workflows'
ALTER TABLE [dbo].[Workflows]
ADD CONSTRAINT [FK_EmployeeLeaveTransaction_Workflow]
    FOREIGN KEY ([RefLeaveTransactionId])
    REFERENCES [dbo].[EmployeeLeaveTransactions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeLeaveTransaction_Workflow'
CREATE INDEX [IX_FK_EmployeeLeaveTransaction_Workflow]
ON [dbo].[Workflows]
    ([RefLeaveTransactionId]);
GO

-- Creating foreign key on [RefLeaveType] in table 'EmployeeLeaveTransactions'
ALTER TABLE [dbo].[EmployeeLeaveTransactions]
ADD CONSTRAINT [FK_MasterDataValue_EmployeeLeaveTransaction]
    FOREIGN KEY ([RefLeaveType])
    REFERENCES [dbo].[MasterDataValues]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValue_EmployeeLeaveTransaction'
CREATE INDEX [IX_FK_MasterDataValue_EmployeeLeaveTransaction]
ON [dbo].[EmployeeLeaveTransactions]
    ([RefLeaveType]);
GO

-- Creating foreign key on [RefStatus] in table 'EmployeeLeaveTransactions'
ALTER TABLE [dbo].[EmployeeLeaveTransactions]
ADD CONSTRAINT [FK_MasterDataValueStatus_EmployeeLeaveTransaction]
    FOREIGN KEY ([RefStatus])
    REFERENCES [dbo].[MasterDataValues]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValueStatus_EmployeeLeaveTransaction'
CREATE INDEX [IX_FK_MasterDataValueStatus_EmployeeLeaveTransaction]
ON [dbo].[EmployeeLeaveTransactions]
    ([RefStatus]);
GO

-- Creating foreign key on [RefLeaveType] in table 'EmployeeLeaveTransactionHistories'
ALTER TABLE [dbo].[EmployeeLeaveTransactionHistories]
ADD CONSTRAINT [FK_MasterDataValue_EmployeeLeaveTransactionHistory]
    FOREIGN KEY ([RefLeaveType])
    REFERENCES [dbo].[MasterDataValues]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValue_EmployeeLeaveTransactionHistory'
CREATE INDEX [IX_FK_MasterDataValue_EmployeeLeaveTransactionHistory]
ON [dbo].[EmployeeLeaveTransactionHistories]
    ([RefLeaveType]);
GO

-- Creating foreign key on [RefStatus] in table 'EmployeeLeaveTransactionHistories'
ALTER TABLE [dbo].[EmployeeLeaveTransactionHistories]
ADD CONSTRAINT [FK_MasterDataValueStatus_EmployeeLeaveTransactionHistory]
    FOREIGN KEY ([RefStatus])
    REFERENCES [dbo].[MasterDataValues]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValueStatus_EmployeeLeaveTransactionHistory'
CREATE INDEX [IX_FK_MasterDataValueStatus_EmployeeLeaveTransactionHistory]
ON [dbo].[EmployeeLeaveTransactionHistories]
    ([RefStatus]);
GO

-- Creating foreign key on [RefContactType] in table 'EmployeeContactDetails'
ALTER TABLE [dbo].[EmployeeContactDetails]
ADD CONSTRAINT [FK_MasterDataValue_EmployeeContactDetails1]
    FOREIGN KEY ([RefContactType])
    REFERENCES [dbo].[MasterDataValue1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValue_EmployeeContactDetails1'
CREATE INDEX [IX_FK_MasterDataValue_EmployeeContactDetails1]
ON [dbo].[EmployeeContactDetails]
    ([RefContactType]);
GO

-- Creating foreign key on [RefEmployeeId] in table 'EmployeeLeaveTransaction1'
ALTER TABLE [dbo].[EmployeeLeaveTransaction1]
ADD CONSTRAINT [FK_EmployeeDetails_EmployeeLeaveTransaction1]
    FOREIGN KEY ([RefEmployeeId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDetails_EmployeeLeaveTransaction1'
CREATE INDEX [IX_FK_EmployeeDetails_EmployeeLeaveTransaction1]
ON [dbo].[EmployeeLeaveTransaction1]
    ([RefEmployeeId]);
GO

-- Creating foreign key on [RefEmployeeId] in table 'EmployeeLeaveTransactionHistory1'
ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory1]
ADD CONSTRAINT [FK_EmployeeDetails_EmployeeLeaveTransactionHistory1]
    FOREIGN KEY ([RefEmployeeId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDetails_EmployeeLeaveTransactionHistory1'
CREATE INDEX [IX_FK_EmployeeDetails_EmployeeLeaveTransactionHistory1]
ON [dbo].[EmployeeLeaveTransactionHistory1]
    ([RefEmployeeId]);
GO

-- Creating foreign key on [RefEmployeeId] in table 'EmployeeProjectDetail1'
ALTER TABLE [dbo].[EmployeeProjectDetail1]
ADD CONSTRAINT [FK_EmployeeDetails_EmployeeProjectDetail1]
    FOREIGN KEY ([RefEmployeeId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDetails_EmployeeProjectDetail1'
CREATE INDEX [IX_FK_EmployeeDetails_EmployeeProjectDetail1]
ON [dbo].[EmployeeProjectDetail1]
    ([RefEmployeeId]);
GO

-- Creating foreign key on [RefHierarchyLevel] in table 'EmployeeDetails'
ALTER TABLE [dbo].[EmployeeDetails]
ADD CONSTRAINT [FK_EmployeeDetails_MasterDataValue1]
    FOREIGN KEY ([RefHierarchyLevel])
    REFERENCES [dbo].[MasterDataValue1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDetails_MasterDataValue1'
CREATE INDEX [IX_FK_EmployeeDetails_MasterDataValue1]
ON [dbo].[EmployeeDetails]
    ([RefHierarchyLevel]);
GO

-- Creating foreign key on [RefEmployeeId] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [FK_EmployeeDetails_Notification]
    FOREIGN KEY ([RefEmployeeId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDetails_Notification'
CREATE INDEX [IX_FK_EmployeeDetails_Notification]
ON [dbo].[Notifications]
    ([RefEmployeeId]);
GO

-- Creating foreign key on [RefEmployeeId] in table 'UserAccount1'
ALTER TABLE [dbo].[UserAccount1]
ADD CONSTRAINT [FK_EmployeeDetails_UserAccount1]
    FOREIGN KEY ([RefEmployeeId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDetails_UserAccount1'
CREATE INDEX [IX_FK_EmployeeDetails_UserAccount1]
ON [dbo].[UserAccount1]
    ([RefEmployeeId]);
GO

-- Creating foreign key on [RefApproverId] in table 'Workflow1'
ALTER TABLE [dbo].[Workflow1]
ADD CONSTRAINT [FK_EmployeeDetailsId_Workflow1]
    FOREIGN KEY ([RefApproverId])
    REFERENCES [dbo].[EmployeeDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDetailsId_Workflow1'
CREATE INDEX [IX_FK_EmployeeDetailsId_Workflow1]
ON [dbo].[Workflow1]
    ([RefApproverId]);
GO

-- Creating foreign key on [RefLeaveTransactionId] in table 'Workflow1'
ALTER TABLE [dbo].[Workflow1]
ADD CONSTRAINT [FK_EmployeeLeaveTransaction_Workflow1]
    FOREIGN KEY ([RefLeaveTransactionId])
    REFERENCES [dbo].[EmployeeLeaveTransaction1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeLeaveTransaction_Workflow1'
CREATE INDEX [IX_FK_EmployeeLeaveTransaction_Workflow1]
ON [dbo].[Workflow1]
    ([RefLeaveTransactionId]);
GO

-- Creating foreign key on [RefLeaveType] in table 'EmployeeLeaveTransaction1'
ALTER TABLE [dbo].[EmployeeLeaveTransaction1]
ADD CONSTRAINT [FK_MasterDataValue_EmployeeLeaveTransaction1]
    FOREIGN KEY ([RefLeaveType])
    REFERENCES [dbo].[MasterDataValue1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValue_EmployeeLeaveTransaction1'
CREATE INDEX [IX_FK_MasterDataValue_EmployeeLeaveTransaction1]
ON [dbo].[EmployeeLeaveTransaction1]
    ([RefLeaveType]);
GO

-- Creating foreign key on [RefStatus] in table 'EmployeeLeaveTransaction1'
ALTER TABLE [dbo].[EmployeeLeaveTransaction1]
ADD CONSTRAINT [FK_MasterDataValueStatus_EmployeeLeaveTransaction1]
    FOREIGN KEY ([RefStatus])
    REFERENCES [dbo].[MasterDataValue1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValueStatus_EmployeeLeaveTransaction1'
CREATE INDEX [IX_FK_MasterDataValueStatus_EmployeeLeaveTransaction1]
ON [dbo].[EmployeeLeaveTransaction1]
    ([RefStatus]);
GO

-- Creating foreign key on [RefLeaveType] in table 'EmployeeLeaveTransactionHistory1'
ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory1]
ADD CONSTRAINT [FK_MasterDataValue_EmployeeLeaveTransactionHistory1]
    FOREIGN KEY ([RefLeaveType])
    REFERENCES [dbo].[MasterDataValue1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValue_EmployeeLeaveTransactionHistory1'
CREATE INDEX [IX_FK_MasterDataValue_EmployeeLeaveTransactionHistory1]
ON [dbo].[EmployeeLeaveTransactionHistory1]
    ([RefLeaveType]);
GO

-- Creating foreign key on [RefStatus] in table 'EmployeeLeaveTransactionHistory1'
ALTER TABLE [dbo].[EmployeeLeaveTransactionHistory1]
ADD CONSTRAINT [FK_MasterDataValueStatus_EmployeeLeaveTransactionHistory1]
    FOREIGN KEY ([RefStatus])
    REFERENCES [dbo].[MasterDataValue1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValueStatus_EmployeeLeaveTransactionHistory1'
CREATE INDEX [IX_FK_MasterDataValueStatus_EmployeeLeaveTransactionHistory1]
ON [dbo].[EmployeeLeaveTransactionHistory1]
    ([RefStatus]);
GO

-- Creating foreign key on [RefProjectId] in table 'EmployeeProjectDetail1'
ALTER TABLE [dbo].[EmployeeProjectDetail1]
ADD CONSTRAINT [FK_MasterDataValue_EmployeeProjectDetail11]
    FOREIGN KEY ([RefProjectId])
    REFERENCES [dbo].[MasterDataValue1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValue_EmployeeProjectDetail11'
CREATE INDEX [IX_FK_MasterDataValue_EmployeeProjectDetail11]
ON [dbo].[EmployeeProjectDetail1]
    ([RefProjectId]);
GO

-- Creating foreign key on [RefLeaveType] in table 'LeaveMaster1'
ALTER TABLE [dbo].[LeaveMaster1]
ADD CONSTRAINT [FK_MasterDataValue_LeaveMaster1]
    FOREIGN KEY ([RefLeaveType])
    REFERENCES [dbo].[MasterDataValue1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValue_LeaveMaster1'
CREATE INDEX [IX_FK_MasterDataValue_LeaveMaster1]
ON [dbo].[LeaveMaster1]
    ([RefLeaveType]);
GO

-- Creating foreign key on [RefMasterType] in table 'MasterDataValue1'
ALTER TABLE [dbo].[MasterDataValue1]
ADD CONSTRAINT [FK_MasterDataType_MasterDataValue1]
    FOREIGN KEY ([RefMasterType])
    REFERENCES [dbo].[MasterDataType1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataType_MasterDataValue1'
CREATE INDEX [IX_FK_MasterDataType_MasterDataValue1]
ON [dbo].[MasterDataValue1]
    ([RefMasterType]);
GO

-- Creating foreign key on [RefStatus] in table 'Workflow1'
ALTER TABLE [dbo].[Workflow1]
ADD CONSTRAINT [FK_MasterDataValueStatus_Workflow1]
    FOREIGN KEY ([RefStatus])
    REFERENCES [dbo].[MasterDataValue1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterDataValueStatus_Workflow1'
CREATE INDEX [IX_FK_MasterDataValueStatus_Workflow1]
ON [dbo].[Workflow1]
    ([RefStatus]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------