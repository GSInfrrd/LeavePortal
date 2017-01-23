USE [master]
GO
ALTER DATABASE LeaveManagementSystem SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE ;


GO

USE [LeaveManagementSystem]
GO


INSERT INTO [dbo].[MasterDataValue]
           ([Id]
           ,[RefMasterType]
           ,[Value])
     VALUES
           (185,13, 'Partial Approval')
GO
INSERT INTO [dbo].[MasterDataValue]
           ([Id]
           ,[RefMasterType]
           ,[Value])
     VALUES
           (186,13, 'Requested')
GO


ALTER TABLE [dbo].[EmployeeDetails]  
WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetails_EmployeeManager] FOREIGN KEY([ManagerId])
REFERENCES [dbo].[EmployeeDetails] ([Id])
GO



