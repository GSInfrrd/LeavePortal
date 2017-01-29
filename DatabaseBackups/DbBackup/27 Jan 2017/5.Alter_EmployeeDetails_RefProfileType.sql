Use LeaveManagementSystem
Go

Alter Table EmployeeDetails
Add RefProfileType int not null Default 202

ALTER TABLE [dbo].[EmployeeDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetails_MasterDataValue3] FOREIGN KEY([RefProfileType])
REFERENCES [dbo].[MasterDataValue] ([Id])
GO

ALTER TABLE [dbo].[EmployeeDetails] CHECK CONSTRAINT [FK_EmployeeDetails_MasterDataValue3]
GO
