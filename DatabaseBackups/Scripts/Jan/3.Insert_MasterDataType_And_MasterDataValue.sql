Use LeaveManagementSystem
go
declare @identity1 int=0;
insert into [LeaveManagementSystem].[dbo].[MasterDataType]
  ([Type]) values('EmployeeType')


  SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];  
  
set @identity1= @@IDENTITY;  


  insert into [LeaveManagementSystem].[dbo].[MasterDataValue]([RefMasterType]
      ,[Value]) values(@identity1,'Fresher')
	

	    insert into [LeaveManagementSystem].[dbo].[MasterDataValue]([RefMasterType]
      ,[Value]) values(@identity1,'Experienced')


