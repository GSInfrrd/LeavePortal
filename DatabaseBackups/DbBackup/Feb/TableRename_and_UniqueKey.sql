use LeaveManagementSystem
sp_rename '[LeaveManagementSystem].[dbo].[EmployeeLeaveTransactionHistory]', 'WorkflowHistory';

ALTER TABLE [LeaveManagementSystem].[dbo].[UserAccount]
ADD UNIQUE (UserName)