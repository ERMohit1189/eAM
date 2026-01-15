/****** Object:  Index [idx_StudentOfficialDetails]    Script Date: 08-09-2025 00:51:12 ******/
DROP INDEX [idx_StudentOfficialDetails] ON [dbo].[StudentOfficialDetails]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [idx_StudentOfficialDetails]    Script Date: 08-09-2025 00:51:13 ******/
CREATE NONCLUSTERED INDEX [idx_StudentOfficialDetails] ON [dbo].[StudentOfficialDetails]
(
	[SrNo] ASC,
	[AdmissionForClassId] ASC,
	[SectionId] ASC,
	[Course] ASC,
	[Branch] ASC,
	[Streamid] ASC,
	[Medium] ASC,
	[Card] ASC,
	[EducationActId] ASC,
	[SessionName] ASC,
	[BranchCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
Alter Table StudentOfficialDetails Drop Column shiftId