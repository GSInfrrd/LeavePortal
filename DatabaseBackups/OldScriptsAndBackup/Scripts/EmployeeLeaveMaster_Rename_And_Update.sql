USE LeaveManagementSystem
GO

EXEC sp_rename 'ConsolidatedEmployeeLeaveDetails', 'EmployeeLeaveMaster'

Alter Table EmployeeLeaveMaster
Add LeaveBalance int null

Alter Table EmployeeLeaveMaster
Add AdvancedLeaveCount int null

Alter Table EmployeeLeaveMaster
Add RewardedLeaveCount int null

Alter Table EmployeeLeaveMaster
Add ModifiiedDate date null

Alter Table EmployeeLeaveMaster
Add ModifiedBy int null

Alter Table EmployeeLeaveMaster
Drop column EarnedLeavesCount 
