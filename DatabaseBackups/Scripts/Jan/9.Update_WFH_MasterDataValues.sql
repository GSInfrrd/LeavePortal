Use LeaveManagementSystem
Go

Update [LeaveManagementSystem].[dbo].[MasterDataValue] 
set Value='Personal Work' where Value like '%maintenance at home%'
go
Update [LeaveManagementSystem].[dbo].[MasterDataValue] 
set Value='Commute Issue' where Value like '%To Avoid Travel%'
go

Insert into [LeaveManagementSystem].[dbo].[MasterDataValue] (RefMasterType,Value) values(6,'Appointment with Doctor');
go
Insert into [LeaveManagementSystem].[dbo].[MasterDataValue] (RefMasterType,Value) values(6,'Others');
go