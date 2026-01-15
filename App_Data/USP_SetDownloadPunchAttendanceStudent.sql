ALTER PROC [dbo].[USP_SetDownloadPunchAttendanceStudent]

AS

BEGIN

    BEGIN TRY		
			Declare @CurrentDate DateTime='07-Oct-2025 05:00:00 PM'
			Declare @GetDateForRecordDate DateTime=@CurrentDate		

			declare @StudentTemp as table

			(

				StudentMachineId int , AttendanceDate DATE, ClassName NVARCHAR(50), SectionName NVARCHAR(50), StEnRCode NVARCHAR(50), 

				Srno NVARCHAR(50), StudentName NVARCHAR(50), FatherName NVARCHAR(50), SessionName NVARCHAR(50), 

				BranchCode int, RecordDate datetime, IsBiometric bit, AttendanceValue NVARCHAR(5)

			);	

			--DECLARE @StartinDate VARCHAR(11) =(SELECT FORMAT(DATEADD(mm, DATEDIFF(mm, 0, dateadd(month, -1, @CurrentDate)), 0), 'dd-MMM-yyyy'));

			DECLARE @StartinDate VARCHAR(11) =Format(dateadd(dd, 0, @CurrentDate),'dd-MMM-yyyy');

			DECLARE @EndDate VARCHAR(11) = (SELECT FORMAT(@CurrentDate, 'dd-MMM-yyyy'));	

			Create Table #Temmp_Table_DownloadedPunchStudent
			(
				Intime date,
				BranchCode int,
				InMachineNo varchar(50),
				OutMachineNo varchar(50)
			)
			Insert into #Temmp_Table_DownloadedPunchStudent(Intime,BranchCode,InMachineNo,OutMachineNo)
			Select Distinct Convert(date,Intime),BranchCode,InMachineNo,OutMachineNo from DownloadedPunchStudent

			declare @BranchCodeTable as table (BranchCode1 int, MachineNo int, punchdate date)

			insert into @BranchCodeTable

			select distinct BranchCode, MachineNo, date from (

			select distinct eof.BranchCode, eof.MachineNo, t1.date 

			from PunchMachineConfigurationStudent eof with(nolock)

			left join #Temmp_Table_DownloadedPunchStudent dp with(nolock) on dp.BranchCode=eof.BranchCode and (eof.MachineNo=InMachineNo or eof.MachineNo=OutMachineNo)

			inner join 

			(

				SELECT FORMAT(DATEADD(DAY, Number, @StartinDate), 'dd-MMM-yyyy') date 

				FROM master..spt_values with(nolock) WHERE type = 'P' AND DATEADD(DAY, Number, @StartinDate) <= @EndDate

			) T1 on t1.date=isnull(Intime, @CurrentDate)

			union

			select distinct eof.BranchCode, eof.MachineNo, convert(date, @CurrentDate) date from PunchMachineConfigurationStudent eof with(nolock)  

			)T2

			Create Table #Temmp_Table_DownloadedPunchStudent1
			(
				StudentMachineId int,
				IsAttendance bit,
				Intime datetime,
				BranchCode int,
				InMachineNo varchar(50),
				OutMachineNo varchar(50)
			)

			while (select count(*) from @BranchCodeTable)>0

			begin

				declare @branchcodeprm int declare @machinnoparam int declare @punchdateParam date

				select top(1) @branchcodeprm=BranchCode1, @machinnoparam=MachineNo, @punchdateParam=punchdate from @BranchCodeTable

				Truncate Table #Temmp_Table_DownloadedPunchStudent1
				
				Insert into #Temmp_Table_DownloadedPunchStudent1(StudentMachineId,IsAttendance,Intime,BranchCode,InMachineNo,OutMachineNo)
				Select Distinct StudentMachineId,IsAttendance,Intime,BranchCode,InMachineNo,OutMachineNo from DownloadedPunchStudent Where 
				ISNULL(IsAttendance,1)=1 and CONVERT(DATE, Intime)=@punchdateParam and (InMachineNo=@machinnoparam or OutMachineNo=@machinnoparam)

				INSERT INTO @StudentTemp
				SELECT distinct StudentMachineId, AttendanceDate, ClassName, SectionName, StEnRCode, Srno, StudentName, 
				FatherName,SessionName, BranchCode, RecordDate, IsBiometric,
				(
				  CASE WHEN AttendenceIn = 'A' THEN 'A' else
				  CASE WHEN AttendenceIn = 'P' AND isnull(AttendenceOut, '')='A' THEN 'P' else 
				  CASE WHEN AttendenceIn = 'P' AND isnull(AttendenceOut, '')='P' THEN 'P' else 
				  CASE WHEN AttendenceIn = 'Lt' AND isnull(AttendenceOut, '')='A' THEN 'Lt' else 
				  CASE WHEN AttendenceIn = 'Lt' AND isnull(AttendenceOut, '')='P' THEN 'Lt' else 
				  'A' END end end end end
				) as AttendanceValue

				FROM

				(
					SELECT distinct asr.CardNo StudentMachineId,  @punchdateParam AttendanceDate, asr.ClassName, asr.SectionName, 
					asr.StEnRCode,  asr.Srno, asr.Name StudentName, asr.FatherName, asr.SessionName, asr.BranchCode, @GetDateForRecordDate as RecordDate, 1 IsBiometric,				
					CASE WHEN CONVERT(TIME(0), dp.Intime) > DATEADD(MINUTE, convert(int, sft.GraceIn), convert(time(0), sft.ShiftIn)) THEN 'LT'
					else CASE WHEN CONVERT(TIME(0), dp.Intime) between convert(time(0), sft.ShiftIn) and 
					DATEADD(MINUTE, convert(int, sft.GraceIn), convert(time(0), sft.ShiftIn)) THEN 'Lt'
					else CASE WHEN CONVERT(TIME(0), dp.Intime)<=convert(time(0), sft.ShiftIn) THEN 'P'
				    end end end AttendenceIn,
					case when isnull(dp.Intime, '')='' then 'A' else 'P' end AttendenceOut
					FROM  AllStudentRecord_UDF('',@branchcodeprm) asr 
					inner JOIN StudentShiftMaster sft with(nolock) ON sft.BranchCode=asr.BranchCode
					inner JOIN dbo.StudentShiftMapping esm with(nolock) ON sft.ID=esm.shiftId and esm.SrNo = asr.SrNo 
					and esm.BranchCode=asr.BranchCode and esm.SessionName=asr.SessionName
					inner join #Temmp_Table_DownloadedPunchStudent1 dp with(nolock) ON  dp.StudentMachineId = asr.CardNo and dp.BranchCode=asr.BranchCode
					where asr.BranchCode=@branchcodeprm and asr.Withdrwal is null

					UNION 

					SELECT distinct asr.CardNo StudentMachineId,  @punchdateParam AttendanceDate, asr.ClassName, asr.SectionName, 
					asr.StEnRCode,  asr.Srno, asr.Name StudentName, asr.FatherName, asr.SessionName, asr.BranchCode, @GetDateForRecordDate as RecordDate, 1 IsBiometric,
					'A' AttendenceIn,
					'A' AttendenceOut
					FROM  AllStudentRecord_UDF('',@branchcodeprm) asr
					inner JOIN StudentShiftMaster sft with(nolock) ON sft.BranchCode=asr.BranchCode 
					inner JOIN dbo.StudentShiftMapping esm with(nolock) ON sft.ID=esm.shiftId and esm.SrNo = asr.SrNo 
					and esm.BranchCode=asr.BranchCode and esm.SessionName=asr.SessionName
					left join AttendanceDetailsDateWise addw on  addw.BranchCode=@branchcodeprm and CONVERT(DATE, AttendanceDate)=@punchdateParam
					and asr.SrNo=addw.SrNo and addw.AttendanceValue='A'
					left join dbo.DownloadedPunchStudent dp on  dp.BranchCode=@branchcodeprm and CONVERT(DATE, Intime)=@punchdateParam
					and asr.CardNo=dp.StudentMachineId
					where convert(time(0), sft.AbsenOn)<convert(time(0), @CurrentDate) and ISNULL(asr.CardNo,'')<>''
					and dp.StudentMachineId is null and addw.SrNo is null and asr.Withdrwal is null 
					and DateName(weekday,@punchdateParam)<>'Sunday'
					)T1

				delete from @BranchCodeTable where BranchCode1=@branchcodeprm and MachineNo=@machinnoparam and punchdate=@punchdateParam
				--select *from @StudentTemp
			end

			DECLARE @StudentMachineId VARCHAR(10), @AttendanceDate VARCHAR(20), @ClassName VARCHAR(20), @SectionName  VARCHAR(20), 
			@StEnRCode VARCHAR(50),  @Srno VARCHAR(20), @StudentName VARCHAR(50), @FatherName VARCHAR(50), 
			@AttendanceValue VARCHAR(30), @SessionName VARCHAR(20), @BranchCode INT,@RecordDate datetime, @IsBiometric BIT;	

			DECLARE db_cursor CURSOR FOR

			SELECT StudentMachineId, AttendanceDate, ClassName, SectionName, StEnRCode, Srno, StudentName, FatherName, AttendanceValue,
			SessionName, BranchCode, RecordDate, IsBiometric
			FROM @StudentTemp;

			OPEN db_cursor;

			FETCH NEXT FROM db_cursor
			INTO @StudentMachineId, @AttendanceDate, @ClassName, @SectionName, @StEnRCode, @Srno, @StudentName, 
			@FatherName, @AttendanceValue,@SessionName, @BranchCode, @RecordDate, @IsBiometric;

			WHILE @@FETCH_STATUS = 0
			BEGIN

				Create Table #Temmp_Table_DownloadedPunchStudent2
				(
					StudentMachineId int,
					IsAttendance bit,
					Outtime datetime,
					Intime datetime,
					BranchCode int
				)

				Insert into #Temmp_Table_DownloadedPunchStudent2(StudentMachineId,IsAttendance,Outtime,Intime,BranchCode)
				Select StudentMachineId,IsAttendance,Outtime,Intime,BranchCode from dbo.DownloadedPunchStudent with(nolock)

				WHERE StudentMachineId = @StudentMachineId  and BranchCode=@BranchCode AND CONVERT(DATE, Intime) = CONVERT(DATE, @AttendanceDate) 
				declare @IsAttendance bit=0

				select @IsAttendance=(case when convert(nvarchar(50), Outtime)='' and Convert(date,Intime) = Convert(date,@CurrentDate) then 1 else 
				case when convert(nvarchar(50), Outtime)<>'' and Convert(date,Intime) < Convert(date,@CurrentDate) then 0 else 0 end end)
				from #Temmp_Table_DownloadedPunchStudent2 
				WHERE StudentMachineId = @StudentMachineId  and BranchCode=@BranchCode AND CONVERT(DATE, Intime) = CONVERT(DATE, @AttendanceDate) 
			
				Declare @TodayAbsentMark bit
				Set @TodayAbsentMark=(Select Top 1 ISNULL(StCountAtt,1) from PlannerMaster Where Convert(date,@CurrentDate) between fromdate and ToDate 
				and ISNULL(StCountAtt,1)=0)
				
				Set @TodayAbsentMark=ISNULL(@TodayAbsentMark,1)
				--Select  @TodayAbsentMark
				IF NOT EXISTS (
					SELECT 1 FROM dbo.AttendanceDetailsDateWise with(nolock) WHERE Srno = @Srno AND 
					Convert(date,AttendanceDate) = Convert(date,@AttendanceDate) and BranchCode=@BranchCode
				)

				BEGIN

					IF(@TodayAbsentMark=0)
					BEGIN
						IF(@AttendanceValue<>'A')
						BEGIN
							INSERT INTO AttendanceDetailsDateWise
							(
								StudentMachineId, CategoryWise, AttendanceMonth, AttendanceDate, ClassName, SectionName, StEnRCode, 
								Srno, StudentName, FatherName, AttendanceValue, SessionName, BranchCode, RecordDate, IsBiometric
							)
							VALUES
							(
								@StudentMachineId, N'Date Wise', FORMAT(CONVERT(DATE, @AttendanceDate), 'MMM'), @AttendanceDate,
								@ClassName, @SectionName, @StEnRCode, @Srno, @StudentName, @FatherName, @AttendanceValue,
								@SessionName, @BranchCode, @RecordDate, 1
							)
						END
					END
					ELSE
					BEGIN					
						INSERT INTO AttendanceDetailsDateWise
						(
							StudentMachineId, CategoryWise, AttendanceMonth, AttendanceDate, ClassName, SectionName, StEnRCode, 
							Srno, StudentName, FatherName, AttendanceValue, SessionName, BranchCode, RecordDate, IsBiometric
						)
						VALUES
						(
							@StudentMachineId, N'Date Wise', FORMAT(CONVERT(DATE, @AttendanceDate), 'MMM'), @AttendanceDate,
							@ClassName, @SectionName, @StEnRCode, @Srno, @StudentName, @FatherName, @AttendanceValue,
							@SessionName, @BranchCode, @RecordDate, 1
						)

						--Select 	@StudentMachineId, N'Date Wise', FORMAT(CONVERT(DATE, @AttendanceDate), 'MMM'), @AttendanceDate,
						--	@ClassName, @SectionName, @StEnRCode, @Srno, @StudentName, @FatherName, @AttendanceValue,
						--	@SessionName, @BranchCode, @RecordDate, 1
					END
				END

				IF EXISTS (
					SELECT 1 FROM dbo.AttendanceDetailsDateWise with(nolock) WHERE Srno = @Srno 
					AND Convert(date,AttendanceDate) = Convert(date,@AttendanceDate) and BranchCode=@BranchCode 
				)

				BEGIN
				
					UPDATE AttendanceDetailsDateWise
					SET AttendanceValue = @AttendanceValue
					WHERE Convert(date,AttendanceDate) = Convert(date,@AttendanceDate) AND Srno = @Srno 
					and BranchCode=@BranchCode and CategoryWise='Date Wise'

					UPDATE dbo.DownloadedPunchStudent SET IsAttendance = @IsAttendance 
					WHERE StudentMachineId = @StudentMachineId  and BranchCode=@BranchCode 
					AND CONVERT(DATE, Intime) = CONVERT(DATE, @AttendanceDate) and BranchCode=@BranchCode 

				END;

				Drop Table #Temmp_Table_DownloadedPunchStudent2
				FETCH NEXT FROM db_cursor
				INTO @StudentMachineId, @AttendanceDate, @ClassName, @SectionName, @StEnRCode, @Srno, @StudentName, 
				@FatherName, @AttendanceValue,@SessionName, @BranchCode, @RecordDate, @IsBiometric;

				END;
				CLOSE db_cursor;
				DEALLOCATE db_cursor;

				Drop Table #Temmp_Table_DownloadedPunchStudent
				Drop Table #Temmp_Table_DownloadedPunchStudent1

END TRY

    BEGIN CATCH

        IF (@@ROWCOUNT > 0)

            ROLLBACK;

    END CATCH;

END;
