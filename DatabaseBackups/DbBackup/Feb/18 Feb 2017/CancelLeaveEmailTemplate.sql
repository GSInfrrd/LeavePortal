/****** Script for SelectTopNRows command from SSMS  ******/


  insert into [LeaveManagementSystem].[dbo].[EmailTemplateMaster] values('\App_Data\EmailTemplates\CancelLeave.cshtml')

  insert into [LeaveManagementSystem].[dbo].[EmailTemplateMapping] values('CancelLeave',9)