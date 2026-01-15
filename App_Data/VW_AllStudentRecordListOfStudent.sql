ALTER VIEW [dbo].[VW_AllStudentRecordListOfStudent]
AS
  SELECT DISTINCT
     T2.id,
                  T1.srno,
                  T1.stenrcode,
                  T2.firstname,
                  T2.middlename,
                  T2.lastname,
                  Upper(T2.firstname + CASE WHEN T2.middlename = ' ' THEN ' '
                        ELSE
                        ' ' +
                        T2.middlename +
                        ' ' END + T2.lastname)                   AS NAME,
                  Upper(T4.classname)                            AS ClassName,
                  CASE
                    WHEN Isnull(T11.isdisplay, 0) = 1 THEN Upper(
                    T4.classname + ' ' + T11.branchname + ' ('
                    + T5.sectionname + ')')
                    ELSE Upper(T4.classname + ' (' + T5.sectionname + ')')
                  END                                            AS
                     CombineClassName,
                  T2.aadharno,
                  Upper(T5.sectionname)                          AS SectionName,
                  Upper(T1.medium)                               AS Medium,
                  T1.medium                                      AS Medium1,
                  CONVERT(VARCHAR(11), T2.dob, 106)              AS DOB,
                  Upper(T3.fathername)                           AS FatherName,
                  T3.fathercontactno,
                  T3.fatherincomemonthly,
                  Isnull(Upper(T3.fatheroccupation), '')         AS
                     FatherOccupation,
                  T4.cidorder,
                  Upper(T3.mothername)                           AS MotherName,
                  T3.mothercontactno,
                  T3.motherincomemonthly,
                  Isnull(Upper(T3.motheroccupation), '')         AS
                     MotherOccupation,
                  Upper(T3.familyguardianname)                   AS
                     FamilyGuardianName,
                  T3.familycontactno,
                  Upper(T3.guardiantwoname)                      AS
                     GuardiantwoName,
                  T3.guardiantwocontact,
                  Upper(T2.stperaddress)                         AS StPerAddress
     ,
                  Upper(T6.statename)                            AS
     StPerStateName
                     ,
                  Upper(T7.cityname)
                     AS StPerCityName,
                  Upper(T2.stlocaladdress)                       AS
     StLocalAddress
                     ,
                  Upper(T8.statename)
                     AS StLocalStateName,
                  Upper(T7.cityname)                             AS
                     StLocalCityName,
                  CONVERT(VARCHAR(11), T1.dateofadmiission, 106) AS
                     DateOfAdmiission,
                  T1.transportrequired,
                  Upper(T2.category)                             AS Category,
                  Upper(T1.card)                                 AS Card,
                  T1.card                                        AS Card1,
                  Upper(T1.typeofadmision)                       AS
     TypeOFAdmision
                     ,
                  T1.withdrwal,
                  Upper(T2.gender)                               AS Gender,
                  T2.gender                                      AS Gender1,
                  Upper(T2.bloodgroup)                           AS BloodGroup,
                  Upper(T2.religion)                             AS Religion,
                  Upper(T1.board)                                AS Board,
                  Upper(T1.housename)                            AS HouseName,
                  T1.housename                                   AS HouseName1,
                  T1.libraryrequired,
                  T1.hostelrequired,
                  T1.blocked,
                  T2.photopath,
                  T3.fatherphotopath,
                  T3.motherphotopath,
                  T1.sessionname,
                  T1.promotion,
                  Upper(T10.coursename)                          AS CourseName,
                  T11.branchname,
                  CASE
                    WHEN Isnull(T11.isdisplay, 0) = 1 THEN Upper(T11.branchname)
                    ELSE ''
                  END                                            AS
                     BranchNameWithDisplay,
                  T10.id                                         AS CourseId,
                  T11.id                                         AS BranchId,
                  T1.instituterollno,
                  T2.height,
                  T2.weight,
                  T2.visionl,
                  T2.visionr,
                  T2.dentalhygiene,
                  T4.id                                          AS ClassId,
                  T11.isdisplay,
                  T2.nationality,
                  T5.id                                          AS SectionID,
                  T2.mobilenumber,
                  T1.modfortransport,
                  T1.cardno,
                  T1.modforfeedeposit,
                  T12.stream,
                  T12.id                                         AS StreamId,
                  T1.boarduniversityrollno,
                  T2.email                                       AS stEmail,
                  T2.caste                                       AS stCaste,
                  T3.familyemail,
                  -- T13.Id AS MediumId,     
                  T14.id                                         AS CardId,
                  T2.oralhygiene,
                  T2.specificailment,
                  T2.stpercountryid,
                  T2.stperstateid,
                  T2.stpercityid,
                  T2.stlocalcountryid,
                  T2.stlocalstateid,
                  T2.stlocalcityid,
                  T1.isupdate,
                  T2.physicalldisabledcategory,
                  T2.phystname,
                  T2.identificationmark,
                  T2.mothertongue,
                  T3.fathernationality,
                  T3.mothernationality,
                  T3.fatherdesignation,
     Isnull(T3.fatherqualification, '')             FatherQualification,
     CASE
       WHEN Isnull(T3.fatherqualification, '') = '' THEN ''
       ELSE (SELECT qualificationname
             FROM   parentsqualificationmaster
             WHERE  id = T3.fatherqualification)
     END
     FatherQualificationName,
                  T3.fatheraadhaarcardno,
                  T3.fatherofficeaddress,
                  T3.fatheremail,
                  T3.familyrelationship,
                  T3.parenttotalincome,
                  T3.motherdesignation,
     Isnull(T3.motherqualification, '')             MotherQualification,
     CASE
       WHEN Isnull(T3.motherqualification, '') = '' THEN ''
       ELSE (SELECT qualificationname
             FROM   parentsqualificationmaster
             WHERE  id = T3.motherqualification)
     END                                            MotherQualificationName,
     T3.motheraadhaarcardno,
     T3.motherofficeaddress,
     T3.motheremail,
     T3.g1address,
     T3.groupphotopath,
     T2.stlocalzip,
     T2.perphoneno,
     T2.permobileno,
     T3.g1pin,
     T3.g1state,
     T2.stperzip,
     T1.fileno,
     T1.groupna,
     T1.dfa,
     T1.cfa,
     T1.cofa,
     T1.sfa,
     T1.admissiondoneat,
     T1.scholarship,
     T1.typeofeducation,
     T1.branchcode,
     T15.shiftid,
     T1.educationactid,
     T17.shiftname,
     T16.actname,
     T2.pen,
     T1.machineno,
     T1.remark                                      AS RemarkShift,
     TT.schoolname,
     Isnull(TT.board, '')                           AS PreviousBoard,
     TT.rank                                        AS PreviousRank,
     TT.udisecode                                   AS PreviousUDISECode,
     TT.result                                      AS PreviousResultStatus,
     TT.catrank                                     AS PreviousCategoryRank,
     TT.contactno                                   AS PreviousContactNo,
     TT.attendance                                  AS PreviousAttendance,
     TT.schooladdress                               AS PreviousSchooladdress,
     TT.markspercentage                             AS PreviousMarksPercentage,
     TT.previousclass,
     (SELECT examname
      FROM   entranceexammaster
      WHERE  branchcode = TT.branchcode
             AND id = TT.examid)                    PreviousExamName,
     Isnull(TT.medium, '')                          AS PreviousMedium,
     TT.rollno                                      AS PreviousRollNo,
     T1.apaarid,
     T1.bookletno,
     T2.recorddate                                  AS CreatedDate,
     T2.loginname                                   AS CreatedBy,
     T2.modifiedby                                  AS ModifiedBy,
     T2.modifieddate                                AS ModifiedDate,
     T1.tcissued                                    AS TCIssued
  FROM   dbo.studentofficialdetails AS T1
         INNER JOIN dbo.studentgenaraldetail AS T2
                 ON T2.srno = T1.srno
                    AND T2.sessionname = T1.sessionname
                    AND T2.branchcode = T1.branchcode
         INNER JOIN dbo.studentfamilydetails AS T3
                 ON T3.srno = T1.srno
                    AND T3.sessionname = T1.sessionname
                    AND T3.branchcode = T1.branchcode
         LEFT JOIN dbo.studentpreviousschool AS TT
                ON TT.srno = T1.srno
                   AND TT.sessionname = T1.sessionname
                   AND T2.branchcode = T1.branchcode
         INNER JOIN dbo.classmaster AS T4
                 ON T4.id = T1.admissionforclassid
                    AND T4.sessionname = T1.sessionname
                    AND T4.branchcode = T1.branchcode
         INNER JOIN dbo.sectionmaster AS T5
                 ON T5.id = T1.sectionid
                    AND T5.sessionname = T1.sessionname
                    AND T5.branchcode = T1.branchcode
         LEFT OUTER JOIN dbo.statemaster AS T6
                      ON T6.id = T2.stperstateid
         LEFT OUTER JOIN dbo.citymaster AS T7
                      ON T7.id = T2.stpercityid
                         AND T7.id = T2.stlocalcityid
         LEFT OUTER JOIN dbo.statemaster AS T8
                      ON T8.id = T2.stlocalstateid
         INNER JOIN dbo.coursemaster AS T10
                 ON T10.id = T1.course
                    AND T10.branchcode = T1.branchcode
         --  AND T10.SessionName = T1.SessionName           
         INNER JOIN dbo.branchmaster AS T11
                 ON T11.id = T1.branch
                    AND T11.branchcode = T1.branchcode
                    AND T11.sessionname = T1.sessionname
         LEFT OUTER JOIN dbo.streammaster AS T12
                      ON T12.id = T1.streamid
                         AND T12.branchcode = T1.branchcode
                         AND T12.sessionname = T1.sessionname
         --INNER JOIN                        
         --               dbo.MediumMaster AS T13 ON T13.Medium = T1.Medium           
         --   AND T13.BranchCode = T1.BranchCode     
         INNER JOIN dbo.feegroupmaster AS T14
                 ON T14.feegroupname = T1.card
                    AND T14.sessionname = T1.sessionname
                    AND T14.branchcode = T1.branchcode
         LEFT OUTER JOIN dbo.studentshiftmapping AS T15
                      ON T15.srno = T1.srno
                         AND T15.sessionname = T1.sessionname
                         AND T15.branchcode = T1.branchcode
         LEFT OUTER JOIN dbo.tbleducationact AS T16
                      ON T16.id = T1.educationactid
                         --AND T16.SessionName = T1.SessionName     
                         AND T16.branchcode = T1.branchcode
         LEFT OUTER JOIN dbo.studentshiftmaster AS T17
                      ON T17.id = T15.shiftid
                         AND T17.branchcode = T15.branchcode
