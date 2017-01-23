ALTER TABLE [LeaveManagementSystem].[dbo].[ResourceRequestDetail]
ALTER COLUMN Status int not null
GO

ALTER TABLE [LeaveManagementSystem].[dbo].[ResourceRequestDetail]  WITH CHECK ADD  CONSTRAINT [FK_ResourceRequestDetail_EmployeeDetails] FOREIGN KEY([RequestFromId])
REFERENCES [dbo].[EmployeeDetails] ([Id])
GO

ALTER TABLE [LeaveManagementSystem].[dbo].[ResourceRequestDetail] CHECK CONSTRAINT [FK_ResourceRequestDetail_EmployeeDetails]
GO