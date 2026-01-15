Create Proc USP_ChangeEmpId
(
	@Old_EmpId nvarchar(50),
	@New_EmpId nvarchar(50),
	@BranchCode nvarchar(50),
	@MSG VARCHAR(500)='' OUTPUT 
)
AS
BEGIN
 IF Exists(Select * from EmpployeeOfficialDetails where EmpId=@New_EmpId and BranchCode=@BranchCode)    
 BEGIN     
  Set @MSG='D'     
 END
 ELSE
 BEGIN
	 IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'EmpployeeOfficialDetails')    
	 BEGIN    
		Update EmpployeeOfficialDetails Set EmpId=@New_EmpId Where EmpId=@Old_EmpId and BranchCode=@BranchCode    
	 END
	 IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'EmpGeneralDetail')    
	 BEGIN    
		Update EmpGeneralDetail Set EmpId=@New_EmpId Where EmpId=@Old_EmpId and BranchCode=@BranchCode    
	 END
	 IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'EmpDocuments')    
	 BEGIN    
		Update EmpDocuments Set EmpId=@New_EmpId Where EmpId=@Old_EmpId and BranchCode=@BranchCode    
	 END
	 IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'EmpEmployeeDetails')    
	 BEGIN    
		Update EmpEmployeeDetails Set EmpId=@New_EmpId Where EmpId=@Old_EmpId and BranchCode=@BranchCode    
	 END
	 IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'EmployeeAttendanceDayWise')    
	 BEGIN    
		Update EmployeeAttendanceDayWise Set EmpId=@New_EmpId Where EmpId=@Old_EmpId and BranchCode=@BranchCode    
	 END
	 IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = N'dbo'  AND TABLE_NAME = N'EmpPreviousEmployment')    
	 BEGIN    
		Update EmpPreviousEmployment Set EmpId=@New_EmpId Where EmpId=@Old_EmpId and BranchCode=@BranchCode    
	 END
	 IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES T
				Inner Join INFORMATION_SCHEMA.COLUMNS C on C.TABLE_NAME=T.TABLE_NAME
				WHERE T.TABLE_SCHEMA = N'dbo'  
				AND T.TABLE_NAME = N'ClassTeacherMaster' AND C.COLUMN_NAME='EmpId')    
	 BEGIN    
		Declare @sql nvarchar(max)='Update ClassTeacherMaster Set EmpId='''+@New_EmpId+''' Where EmpId='''+@Old_EmpId+''' and BranchCode='''+@BranchCode+''''    
		exec (@sql)
	 END
	 set @MSG='S'
 END
END