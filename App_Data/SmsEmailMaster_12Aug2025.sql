DROP Table SmsEmailMaster
GO
CREATE TABLE [dbo].[SmsEmailMaster](
	[Id] [int] NULL,
	[SmsTitle] [nvarchar](200) NULL,
	[SmsSent] [nvarchar](50) NULL,
	[EmailSent] [nvarchar](50) NULL,
	[BranchCode] [int] NULL,
	[template] [nvarchar](max) NULL,
	[WhatsAppSent] [varchar](10) NULL,
	[DisplayOrder] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (1, N'Composite Fee Deposit', N'true', N'false', 1, N'12345', N'false', 1)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (2, N'Admission Form Fee', N'true', N'false', 1, N'12345', N'false', 2)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (3, N'Transfer Certificate Fee', N'true', N'false', 1, N'12345', N'false', 3)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (4, N'Character Certificate Fee', N'true', N'false', 1, N'12345', N'false', 4)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (5, N'Other Fee', N'true', N'false', 1, N'12345', N'false', 5)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (6, N'Additional Fee', N'true', N'false', 1, N'12345', N'false', 6)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (7, N'Product Fee', N'true', N'false', 1, N'12345', N'false', 7)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (47, N'Library Fine', N'true', N'false', 1, N'12345', N'false', 8)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (48, N'Transaction Clearance', N'true', N'false', 1, N'12345', N'false', 9)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (8, N'Receipt Cancellation', N'true', N'false', 1, N'12345', N'false', 10)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (9, N'Fee Overdue Reminder', N'true', N'false', 1, N'12345', N'false', 11)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (10, N'Student Registration (Student''s SMS)', N'true', N'false', 1, N'12345', N'false', 12)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (11, N'Student Registration (Guardian''s SMS)', N'true', N'false', 1, N'12345', N'false', 13)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (12, N'Staff Registration', N'true', N'false', 1, N'12345', N'false', 14)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (13, N'Admin Account Creation', N'true', N'false', 1, N'12345', N'false', 15)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (14, N'Guardian Login Credentials', N'true', N'false', 1, N'12345', N'false', 16)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (15, N'Student Login Credentials', N'true', N'false', 1, N'12345', N'false', 17)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (16, N'Forgot Password', N'true', N'false', 1, N'12345', N'false', 18)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (17, N'Student Gate Pass', N'true', N'false', 1, N'12345', N'false', 19)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (18, N'Visitor Gate Pass', N'true', N'false', 1, N'12345', N'false', 20)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (19, N'Admission Enquiry', N'true', N'false', 1, N'12345', N'false', 21)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (20, N'Admission Portal OTP', N'true', N'false', 1, N'12345', N'false', 22)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (21, N'Transport Alert', N'true', N'false', 1, N'12345', N'false', 23)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (22, N'Result Message', N'true', N'false', 1, N'12345', N'false', 24)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (23, N'Holiday Message', N'true', N'false', 1, N'12345', N'false', 25)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (24, N'Greeting Message', N'true', N'false', 1, N'12345', N'false', 26)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (25, N'Custom Message', N'true', N'false', 1, N'12345', N'false', 27)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (26, N'Student Daily Attendance (Manual): Present', N'true', N'false', 1, N'12345', N'false', 28)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (27, N'Student Daily Attendance (Manual): Absent', N'true', N'false', 1, N'12345', N'false', 29)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (28, N'Student Daily Attendance (Manual): Late', N'true', N'false', 1, N'12345', N'false', 30)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (29, N'Student Daily Attendance (Automated): Present', N'true', N'false', 1, N'12345', N'false', 31)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (30, N'Student Daily Attendance (Automated): Absent', N'true', N'false', 1, N'12345', N'false', 32)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (31, N'Student Daily Attendance (Automated): Late', N'true', N'false', 1, N'12345', N'false', 33)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (32, N'Staff Daily Attendance (Manual): Present', N'true', N'false', 1, N'12345', N'false', 34)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (33, N'Staff Daily Attendance (Manual): Absent', N'true', N'false', 1, N'12345', N'false', 35)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (34, N'Staff Daily Attendance (Manual): Late', N'true', N'false', 1, N'12345', N'false', 36)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (43, N'Staff Daily Attendance (Manual): Short Leave', N'true', N'false', 1, N'12345', N'false', 37)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (44, N'Staff Daily Attendance (Manual): Half Day', N'true', N'false', 1, N'12345', N'false', 38)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (35, N'Staff Daily Attendance (Automated): Present', N'true', N'false', 1, N'12345', N'false', 39)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (36, N'Staff Daily Attendance (Automated): Absent', N'true', N'false', 1, N'12345', N'false', 40)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (37, N'Staff Daily Attendance (Automated): Late', N'true', N'false', 1, N'12345', N'false', 41)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (45, N'Staff Daily Attendance (Automated): Short Leave', N'true', N'false', 1, N'12345', N'false', 42)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (46, N'Staff Daily Attendance (Automated): Half Day', N'true', N'false', 1, N'12345', N'false', 43)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (38, N'Alumni Portal: Registration', N'true', N'false', 1, N'12345', N'false', 44)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (39, N'Alumni Portal: Registration OTP', N'true', N'false', 1, N'12345', N'false', 45)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (40, N'Alumni Portal: Forgot Password', N'true', N'false', 1, N'12345', N'false', 46)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (41, N'Alumni Portal: Registration Confirmation/Activation with credentials', N'true', N'false', 1, N'12345', N'false', 47)
GO
INSERT [dbo].[SmsEmailMaster] ([Id], [SmsTitle], [SmsSent], [EmailSent], [BranchCode], [template], [WhatsAppSent], [DisplayOrder]) VALUES (42, N'Alumni Portal: Registration Rejection with reason', N'true', N'false', 1, N'12345', N'false', 48)
GO
