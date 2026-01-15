Create  Proc [dbo].[USP_GuardianLoginCredentialsTemplate]
(
	@BranchCode int,
	@SessionName nvarchar(20),
	@SrNo varchar(20)
)
AS
BEGIN
	Select FirstName StudentName,glp.UserName Username,glp.Password Password,FatherContactNo ContactNo
	from AllStudentRecord_UDF(@SessionName,@BranchCode) cfd
	Inner Join GuardianLoginandPassword glp on glp.Srno=cfd.SrNo
	Where cfd.SessionName=@SessionName and cfd.BranchCode=@BranchCode and cfd.SrNo=@SrNo

	DECLARE @Parametername VARCHAR(8000)=null
	DECLARE @Template VARCHAR(8000)=null

	Select @Template=st.Template,@Parametername=COALESCE(@Parametername + ',', '') + Parametername FROM dbo.SMSTemplatePapameres stp
	inner Join dbo.SMSTemplate st on st.pagename=stp.pagename and st.BranchCode=stp.BranchCode and stp.TemplateFor=st.TemplateFor
	Where st.pagename='GuardianLoginCredentials' and stp.BranchCode=@BranchCode
	Order by OrderNo

	Select @Template Template,@Parametername Parametername
END

