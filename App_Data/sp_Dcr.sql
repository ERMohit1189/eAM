-- EXEC sp_DatewiseCollection 'student','13-May-2021', null,null,null,null, 1
ALTER procedure [dbo].[sp_Dcr]
(
  @action nvarchar(50)='',
  @DepositDate datetime=null,
  @PaymentMode nvarchar(50)=null,
  @Status nvarchar(50)=NULL,
  @Statuss nvarchar(50)=NULL,
  @SessionName  nvarchar(50)=null,
  @BranchCode int=null,
  @IsExcludeOtherFee bit=null,
  @SrNo nvarchar(50)=null,
  @ReceiptNo nvarchar(50)=null,
  @LoginName nvarchar(50)=null
)

as

begin

	declare @SessionName2 nvarchar(50)
	select top(1) @SessionName2=SessionName from SessionMaster where convert(date, @DepositDate) between convert(date, FromDate) and convert(date, ToDate)

	Set @LoginName = ISNULL(@LoginName,'')

	if @action='students'

	begin

	select * from (

		select distinct T1.SrNo, Name, CombineClassName, ReceiptNo, Status, Mode, asr.SessionName from AllStudentRecord_UDF('', @BranchCode) asr

		inner join 

		(select * from (

		select distinct SrNo, ReceiptNo, receiptStatus Status, ModeOfPayment as Mode, SessionName from CompositFeeDeposit where convert(date, DepositDate)=convert(date, @DepositDate) 

		and BranchCode=@BranchCode and SessionName=isnull(null,SessionName) and ModeOfPayment=ISNULL(@PaymentMode, ModeOfPayment) and receiptStatus=ISNULL(@Statuss, receiptStatus)
		
		and @LoginName= IIF(@LoginName='','',LoginName)
		
		union all

		select distinct SrNo, Receipt_no as ReceiptNo, PaymentSatus as status, Mode as Mode, SessionName from OtherFeeDeposit 

		where convert(date, DepositDate)=convert(date, @DepositDate) and BranchCode=@BranchCode and SessionName=isnull(@SessionName,SessionName) 

		and Mode=ISNULL(@PaymentMode, Mode) and PaymentSatus=ISNULL(@Statuss, PaymentSatus) and @LoginName= IIF(@LoginName='','',LoginName)
		

		union all

		select distinct SrNo, RecieptNo as ReceiptNo, Status, MOP as Mode, SessionName from TCCollection where convert(date, AdmissionFromDate)=convert(date, @DepositDate) 

		and BranchCode=@BranchCode and SessionName=isnull(null,SessionName) and MOP=ISNULL(@PaymentMode, MOP) 
		
		and Status=ISNULL(@Statuss, Status) and @LoginName= IIF(@LoginName='','',LoginName)
		

		union all

		select distinct SrNo, RecieptNo as ReceiptNo, Status, MOP as Mode, SessionName from CCCollection where convert(date, CCissuedate)=convert(date, @DepositDate) 

		and BranchCode=@BranchCode and SessionName=isnull(null,SessionName) and MOP=ISNULL(@PaymentMode, MOP) 
		
		and Status=ISNULL(@Status, Status) and @LoginName= IIF(@LoginName='','',LoginName)
		

		union all

		select distinct SrNo, Receipt_no as ReceiptNo, PaymentSatus, Mode as Mode, SessionName from UniformFeeDeposit where convert(date, DepositDate)=convert(date, @DepositDate) 

		and BranchCode=@BranchCode and SessionName=isnull(null,SessionName) and Mode=ISNULL(@PaymentMode, Mode) and PaymentSatus=ISNULL(@Statuss, PaymentSatus)

		and @LoginName= IIF(@LoginName='','',LoginName)
		

		union all

		select distinct SrNo, Receipt_no as ReceiptNo, PaymentSatus, Mode as Mode, SessionName from OtherFeeDeposit where convert(date, DepositDate)=convert(date, @DepositDate) 

		and BranchCode=@BranchCode and SessionName=isnull(null,SessionName) and Mode=ISNULL(@PaymentMode, Mode) 
		
		and PaymentSatus=ISNULL(@Statuss, PaymentSatus) and @LoginName= IIF(@LoginName='','',LoginName)
		

		union all

		select distinct bi.srno, Receiptno as ReceiptNo, Status, MOD as Mode, f.SessionName from ReturnBookFine f 
		
		inner join BookIssueReturn bi on bi.id=f.BIRid and bi.branchcode=f.branchcode where convert(date, returndate)=convert(date, @DepositDate) 

		and bi.BranchCode=@BranchCode and bi.SessionName=isnull(null,bi.SessionName) 
		
		and MOD=ISNULL(@PaymentMode, MOD) and Status=ISNULL(@Statuss, Status) and @LoginName= IIF(@LoginName='','',f.LoginName)

		union all

		select distinct SrNo, Receipt_no as ReceiptNo, Status, MOP as Mode, SessionName from Other_fee_collection_1 where convert(date, RecordDate)=convert(date, @DepositDate) 

		and BranchCode=@BranchCode and SessionName=isnull(null,SessionName) and MOP=ISNULL(@PaymentMode, MOP) 
		
		and Status=ISNULL(@Statuss, Status) and @LoginName= IIF(@LoginName='','',LoginName)
		

	)T)T1 on T1.SrNo=asr.SrNo and T1.SessionName=asr.SessionName 

	union all

	select distinct FatherContactNo+'_adm' as SrNo, StudentName+' '+MiddleName+' '+lastname+' ('+FatherContactNo+')'  as Name, Class CombineClassName, RecieptNo as ReceiptNo, Status, MOP as Mode, SessionName from AdmissionFormCollection where convert(date, AdmissionFromDate)=convert(date, @DepositDate) 

	and BranchCode=@BranchCode and SessionName=isnull(null,SessionName) and MOP=ISNULL(@PaymentMode, MOP) and Status=ISNULL(@Statuss, Status)
	

	) Ts order by ReceiptNo asc

	end

	if @action='details'

	begin
		declare @ClassId int, @CardId int, @TypeOFAdmision nvarchar(10), @BranchId int, @Medium nvarchar(10), @TypeOfEducation nvarchar(10)
		select @ClassId=ClassId, @CardId=CardId, @TypeOFAdmision=TypeOFAdmision, @BranchId=BranchId, @Medium=Medium, 
		@TypeOfEducation=case when TypeofEducation='R' then 'Regular' else 'Private' end from AllStudentRecord_UDF(@SessionName, @BranchCode) where SrNo=@SrNo

		---------------Tuition Fee Start--------------------


		declare @FeeInstallments as table (MonthNames Nvarchar(50), MonthId int)
		declare @FeeAmount  as table (HeadName Nvarchar(50), Amount decimal(10, 2))
		
		
		insert into @FeeInstallments	
		select [MonthName], MonthId from MonthMaster mm
		inner join CompositFeeDeposit co on co.InstallmentId=mm.MonthId and co.SessionName=mm.SessionName and co.BranchCode=mm.BranchCode
		where ClassId=@ClassId and mm.SessionName=isnull(@SessionName,mm.SessionName) and mm.BranchCode=@BranchCode and typeofAdd=@TypeOfEducation
		 and ModeOfPayment=ISNULL(@PaymentMode, ModeOfPayment) and receiptStatus=ISNULL(@Status, receiptStatus) and SrNo=@SrNo 
		 and co.InstallmentId <> 0 and ReceiptNo=@ReceiptNo and convert(date, DepositDate)=convert(date, @DepositDate) and CardType=@CardId and ForMonth=1
		insert into @FeeInstallments	
		select distinct 'Arrear', 0 from CompositFeeDeposit where convert(date, DepositDate)=convert(date, @DepositDate) 
		and BranchCode=@BranchCode and SessionName=isnull(@SessionName,SessionName) and ModeOfPayment=ISNULL(@PaymentMode, ModeOfPayment) and receiptStatus=ISNULL(@Status, receiptStatus) and SrNo=@SrNo 
		and ReceiptNo=@ReceiptNo and InstallmentId=0

			 insert into @FeeAmount
			 select  t1.HeadName, Amount from (
				 select distinct 'Arrear' HeadName, sum(fc.PaidAmount) Amount, 0 FeeHeadId from CompositFeeDeposit  fc
				 where convert(date, DepositDate)=convert(date, @DepositDate) and fc.BranchCode=@BranchCode and SessionName=isnull(@SessionName,SessionName) 
				 and ModeOfPayment=ISNULL(@PaymentMode, ModeOfPayment) and receiptStatus=ISNULL(@Status, receiptStatus) and SrNo=@SrNo 
				 and fc.InstallmentId = 0 and ReceiptNo=@ReceiptNo and convert(date, DepositDate)=convert(date, @DepositDate)
				 union all
				 select distinct FeeHead HeadName, sum(fc.PaidAmount) Amount, FeeHeadId from CompositFeeDeposit fc 
				 inner join FeeHeadMaster fm on fm.Id=fc.FeeHeadId and fm.BranchCode=fc.BranchCode 
				 where convert(date, DepositDate)=convert(date, @DepositDate) and fc.BranchCode=@BranchCode and SessionName=isnull(@SessionName,SessionName) 
				 and ModeOfPayment=ISNULL(@PaymentMode, ModeOfPayment) and receiptStatus=ISNULL(@Status, receiptStatus) and SrNo=@SrNo 
				 and fc.InstallmentId <> 0 and ReceiptNo=@ReceiptNo and convert(date, DepositDate)=convert(date, @DepositDate)
				 and ReceiptNo=@ReceiptNo and FeeType not in ('Fine (Late Fee)','Cheque Bounce Charge') group by FeeHead,FeeHeadId
				 union all
				 select distinct FeeHead HeadName, sum(fc.PaidAmount) Amount, FeeHeadId from CompositFeeDeposit  fc
				 inner join FeeHeadMaster fm on fm.Id=fc.FeeHeadId and fm.BranchCode=fc.BranchCode 
				 where convert(date, DepositDate)=convert(date, @DepositDate) and fc.BranchCode=@BranchCode and SessionName=isnull(@SessionName,SessionName) 
				 and ModeOfPayment=ISNULL(@PaymentMode, ModeOfPayment) and receiptStatus=ISNULL(@Status, receiptStatus) and SrNo=@SrNo 
				 and fc.InstallmentId <> 0 and ReceiptNo=@ReceiptNo and convert(date, DepositDate)=convert(date, @DepositDate)
				 and ReceiptNo=@ReceiptNo and fm.FeeType in ('Fine (Late Fee)','Cheque Bounce Charge') group by FeeHead,FeeHeadId
			 ) T1
			 left join FeeHeadMaster fm on fm.Id=t1.FeeHeadId and fm.BranchCode=@BranchCode 
			 order by Priority asc


		---------------Tuition Fee End------------------------





		---------------TC Fee Start-------------------------------

		if(select count(*) from TCCollection where convert(date, AdmissionFromDate)=convert(date, @DepositDate) and BranchCode=@BranchCode 

		and SessionName=isnull(@SessionName,SessionName) and MOP=ISNULL(@PaymentMode, MOP) and Status=ISNULL(@Status, Status) and RecieptNo=@ReceiptNo and SrNo=@SrNo)>0

		begin

			insert into @FeeAmount

			select 'TC Fee', sum(ReceivedAmount) from TCCollection where convert(date, AdmissionFromDate)=convert(date, @DepositDate) and BranchCode=@BranchCode 

			and SessionName=isnull(@SessionName,SessionName) and MOP=ISNULL(@PaymentMode, MOP) and Status=ISNULL(@Status, Status) and RecieptNo=@ReceiptNo and SrNo=@SrNo

			

		end

		---------------TC Fee End---------------------------------



		---------------CC Fee Start-------------------------------

		if(select count(*) from CCCollection where convert(date, CCIssueDate)=convert(date, @DepositDate) and BranchCode=@BranchCode 

			and SessionName=isnull(@SessionName,SessionName) and MOP=ISNULL(@PaymentMode, MOP) and Status=ISNULL(@Status, Status) and RecieptNo=@ReceiptNo and SrNo=@SrNo 
			)>0

		begin

			insert into @FeeAmount

			select 'CC Fee', sum(ReceivedAmount) from CCCollection where convert(date, CCIssueDate)=convert(date, @DepositDate) and BranchCode=@BranchCode 

			and SessionName=isnull(@SessionName,SessionName) and MOP=ISNULL(@PaymentMode, MOP) and Status=ISNULL(@Status, Status) and RecieptNo=@ReceiptNo and SrNo=@SrNo

			

		end

		---------------CC Fee End---------------------------------



		---------------AdmissionForm Fee Start--------------------

		if(select count(*) from AdmissionFormCollection where convert(date, AdmissionFromDate)=convert(date, @DepositDate) and BranchCode=@BranchCode 

			and SessionName=isnull(@SessionName,SessionName) and MOP=ISNULL(@PaymentMode, MOP) and Status=ISNULL(@Status, Status) and RecieptNo=@ReceiptNo and FatherContactNo=@SrNo 

			--and Status<>'Cancelled'
			)>0

		begin

			insert into @FeeAmount

			select 'Admission Form Fee', sum(Amount) from AdmissionFormCollection where convert(date, AdmissionFromDate)=convert(date, @DepositDate) and BranchCode=@BranchCode 

			and SessionName=isnull(@SessionName,SessionName) and MOP=ISNULL(@PaymentMode, MOP) and Status=ISNULL(@Status, Status) and RecieptNo=@ReceiptNo and FatherContactNo=@SrNo 

			--and Status<>'Cancelled'

		end

		---------------AdmissionForm Fee End----------------------



		---------------UniformFeeDeposit Fee Start--------------------

		if(select count(*) from UniformFeeDeposit where convert(date, DepositDate)=convert(date, @DepositDate) and BranchCode=@BranchCode 

			and SessionName=isnull(@SessionName,SessionName) and Mode=ISNULL(@PaymentMode, Mode) and PaymentSatus=ISNULL(@Status, PaymentSatus) and Receipt_no=@ReceiptNo and SrNo=@SrNo

			)>0

		begin

			insert into @FeeAmount

			select 'Product Fee', sum(PaidAmt) from UniformFeeDeposit where convert(date, DepositDate)=convert(date, @DepositDate) 

			and BranchCode=@BranchCode 

			and SessionName=isnull(@SessionName,SessionName) and Mode=ISNULL(@PaymentMode, Mode) and PaymentSatus=ISNULL(@Status, PaymentSatus) and Receipt_no=@ReceiptNo and SrNo=@SrNo

			

		end

		---------------UniformFeeDeposit Fee End----------------------

		---------------OtherFeeDeposit Fee Start--------------------

		if @IsExcludeOtherFee=0

		begin

			if(select count(*) from OtherFeeDeposit where convert(date, DepositDate)=convert(date, @DepositDate) and BranchCode=@BranchCode 

				and SessionName=isnull(@SessionName,SessionName) and Mode=ISNULL(@PaymentMode, Mode) and PaymentSatus=ISNULL(@Status, PaymentSatus) and Receipt_no=@ReceiptNo and SrNo=@SrNo

				)>0

			begin

				insert into @FeeAmount

				select 'Other Fee', sum(PaidAmt) from OtherFeeDeposit where convert(date, DepositDate)=convert(date, @DepositDate) 

				and BranchCode=@BranchCode 

				and SessionName=isnull(@SessionName,SessionName) and Mode=ISNULL(@PaymentMode, Mode) and PaymentSatus=ISNULL(@Status, PaymentSatus) and Receipt_no=@ReceiptNo and SrNo=@SrNo

				

			end

		end

		---------------OtherFeeDeposit Fee End----------------------

		

		---------------Library Fee Start--------------------



		if(select count(*) from ReturnBookFine f inner join BookIssueReturn bi on bi.id=f.BIRid where convert(date, returndate)=convert(date, @DepositDate) and bi.branchcode=@BranchCode 

			and bi.sessionname=isnull(@SessionName,bi.SessionName) and Mod=ISNULL(@PaymentMode, Mod) and Status=ISNULL(@Status, Status) and Receiptno=@ReceiptNo and SrNo=@SrNo

			)>0

		begin

			insert into @FeeAmount

			select 'Library Fee', sum(FineAmount) from ReturnBookFine f inner join BookIssueReturn bi on bi.id=f.BIRid where convert(date, returndate)=convert(date, @DepositDate) 

			and bi.BranchCode=@BranchCode 

			and bi.SessionName=isnull(@SessionName,bi.SessionName) and Mod=ISNULL(@PaymentMode, Mod) and Status=ISNULL(@Status, Status) and Receiptno=@ReceiptNo and SrNo=@SrNo

			 

		end

		---------------Library Fee End----------------------

		



		---------------Additional Fee Start--------------------

		if(select count(*) from Other_fee_collection_1 where convert(date, FeeDepositeDate)=convert(date, @DepositDate) and BranchCode=@BranchCode 

			and SessionName=isnull(@SessionName,SessionName) and MOP=ISNULL(@PaymentMode, MOP) and Status=ISNULL(@Status, Status) and Receipt_no=@ReceiptNo and SrNo=@SrNo

			)>0

		begin

			insert into @FeeAmount

			select 'Additional Fee', sum(ReceivedAmount) from Other_fee_collection_1 where convert(date, FeeDepositeDate)=convert(date, @DepositDate) 

			and BranchCode=@BranchCode 

			and SessionName=isnull(@SessionName,SessionName) and MOP=ISNULL(@PaymentMode, MOP) and Status=ISNULL(@Status, Status) and Receipt_no=@ReceiptNo and SrNo=@SrNo

			

		end

		---------------Additional Fee End----------------------



		



		---------------Fine, Discount, Paid Fee Start-------------

		declare @FineDiscountPaid as table(Fine decimal(10, 2), Discount decimal(10, 2), Paid decimal(10, 2))

		insert into @FineDiscountPaid

		select sum(fine) as fine, sum(Discount) as Discount, sum(PaidAmount) as PaidAmount from (

			select 0 as fine, 0 Discount,

			sum(isnull(PaidAmount, 0.00)) as PaidAmount 

			from CompositFeeDeposit where convert(date, DepositDate)=convert(date, @DepositDate) and BranchCode=@BranchCode and SessionName=isnull(@SessionName,SessionName) 

			and ModeOfPayment=ISNULL(@PaymentMode, ModeOfPayment) and receiptStatus=ISNULL(@Status, receiptStatus) and ReceiptNo=@ReceiptNo 
			
			
			union all



			select sum(isnull(BounceCharges, 0)) as fine, sum(isnull(Concession, 0)) Discount, 

			sum(isnull(convert(decimal(10, 2),ReceivedAmount), 0.00)) as PaidAmount 

			from TCCollection where convert(date, AdmissionFromDate)=convert(date, @DepositDate) and BranchCode=@BranchCode and SessionName=isnull(@SessionName,SessionName) 

			and MOP=ISNULL(@PaymentMode, MOP) and Status=ISNULL(@Status, Status) and RecieptNo=@ReceiptNo 
			

			union all

			select sum(isnull(BounceCharges, 0)) as fine, sum(isnull(Concession, 0)) Discount, 

			sum(isnull(convert(decimal(10, 2),ReceivedAmount), 0.00)) as PaidAmount 

			from CCCollection where convert(date, CCissuedate)=convert(date, @DepositDate) and BranchCode=@BranchCode and SessionName=isnull(@SessionName,SessionName) 

			and MOP=ISNULL(@PaymentMode, MOP) and Status=ISNULL(@Status, Status) and RecieptNo=@ReceiptNo 
			

			union all

			select sum(isnull(BounceCharges, 0)) as fine, sum(isnull(Concession, 0)) Discount, 

			sum(isnull(convert(decimal(10, 2),ReceivedAmount), 0.00)) as PaidAmount 

			from AdmissionFormCollection where convert(date, AdmissionFromDate)=convert(date, @DepositDate) and BranchCode=@BranchCode and SessionName=isnull(@SessionName,SessionName) 

			and MOP=ISNULL(@PaymentMode, MOP) and Status=ISNULL(@Status, Status) and RecieptNo=@ReceiptNo 
			

			union all

			select sum(isnull(BounceCharge, 0)) as fine, sum(isnull(Concession, 0)) Discount, 

			sum(isnull(convert(decimal(10, 2),FineAmount), 0.00)) as PaidAmount 

			from ReturnBookFine f inner join BookIssueReturn bi on bi.id=f.BIRid where convert(date, returndate)=convert(date, @DepositDate) and bi.BranchCode=@BranchCode and bi.SessionName=isnull(@SessionName,bi.SessionName) 

			and MOD=ISNULL(@PaymentMode, MOD) and Status=ISNULL(@Status, Status) and Receiptno=@ReceiptNo 
			

			union all

			select sum(isnull(BounceCharge, 0)) as fine, sum(isnull(Discount, 0)) Discount, 

			sum(isnull(convert(decimal(10, 2),PaidAmt),0.00)) as PaidAmount 

			from UniformFeeDeposit where convert(date, DepositDate)=convert(date, @DepositDate) and BranchCode=@BranchCode and SessionName=isnull(@SessionName,SessionName) 

			and Mode=ISNULL(@PaymentMode, Mode) and PaymentSatus=ISNULL(@Status, PaymentSatus) and Receipt_no=@ReceiptNo 
			

			union all



			select sum(isnull(BounceCharges, 0)) as fine, sum(isnull(Discount, 0)) Discount, 

			sum(isnull(convert(decimal(10, 2),PaidAmt), 0.00)) as PaidAmount from OtherFeeDeposit 

			where convert(date, DepositDate)=convert(date, @DepositDate) and BranchCode=@BranchCode and SessionName=isnull(@SessionName,SessionName) and Mode=ISNULL(@PaymentMode, Mode) 

			and PaymentSatus=ISNULL(@Status, PaymentSatus) and Receipt_no=@ReceiptNo 
			 and @IsExcludeOtherFee=0

			union all

			select sum(isnull(BounceCharges, 0)) as fine, sum(isnull(Concession, 0)) Discount, 

			sum(isnull(convert(decimal(10, 2),ReceivedAmount), 0.00)) as PaidAmount 

			from Other_fee_collection_1 where convert(date, FeeDepositeDate)=convert(date, @DepositDate) and BranchCode=@BranchCode and SessionName=isnull(@SessionName,SessionName) 

			and MOP=ISNULL(@PaymentMode, MOP) and Status=ISNULL(@Status, Status) and Receipt_no=@ReceiptNo 
			

		)T



		---------------Fine, Discount, Paid Fee End---------------
		declare @FeeInstallmentss as table(MonthNames nvarchar(100), MonthId int)
		insert into @FeeInstallmentss
		select distinct MonthNames, MonthId from @FeeInstallments 
		select distinct * from @FeeInstallments where MonthNames<>'Arrear' order by MonthId asc

		select distinct HeadName, sum(Amount) Amount from @FeeAmount group by HeadName

		select Paid from @FineDiscountPaid

		select * from @FeeInstallments where MonthNames='Arrear' order by MonthId asc


	end

end


