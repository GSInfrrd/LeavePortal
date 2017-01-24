Use LeaveManagementSystem
Go

ALTER TABLE [dbo].[EmployeeDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetails_MasterDataValue2] FOREIGN KEY([RefRoleId])
REFERENCES [dbo].[MasterDataValue] ([Id])
GO

ALTER TABLE [dbo].[EmployeeDetails] CHECK CONSTRAINT [FK_EmployeeDetails_MasterDataValue2]
GO