use LeaveManagementSystem
go

Alter Table Employeedetails
Add RefEmployeeType int not null default (select Id from MasterDataValue where Value like 'Experienced')