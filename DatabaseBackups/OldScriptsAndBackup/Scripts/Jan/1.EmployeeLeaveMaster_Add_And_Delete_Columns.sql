Use LeaveManagementSystem
go

alter table EmployeeLeaveMaster
Drop  column AppliedLeavesCount,WorkfromHomeCount,LossOfPayCount,LeaveBalance,AdvancedleaveCount
go

Alter Table EmployeeLeaveMaster
Add SpentAdvanceLeave int null
go

Alter Table EmployeeLeaveMaster
Add TakenLossOfPay int null
go

Alter Table EmployeeLeaveMaster
Add EarnedCasualLeave int null
go