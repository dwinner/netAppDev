/************************************************************
 * Code formatted by SoftTree SQL Assistant © v6.4.230
 * Time: 05.05.2014 17:43:12
 ************************************************************/

USE [CourseManagement]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 11/08/2009 19:19:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students]
(
	[StudentId]     [int] IDENTITY(1, 1) NOT NULL,
	[FirstName]     [nvarchar](50) NOT NULL,
	[LastName]      [nvarchar](50) NOT NULL,
	[Company]       [nvarchar](50) NULL,
	CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED([StudentId] ASC)WITH (
	                                                                        PAD_INDEX 
	                                                                        = 
	                                                                        OFF,
	                                                                        STATISTICS_NORECOMPUTE 
	                                                                        = 
	                                                                        OFF,
	                                                                        IGNORE_DUP_KEY 
	                                                                        = 
	                                                                        OFF,
	                                                                        ALLOW_ROW_LOCKS 
	                                                                        = ON,
	                                                                        ALLOW_PAGE_LOCKS 
	                                                                        = ON
	                                                                    ) ON 
	                                                                    [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 11/08/2009 19:19:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses]
(
	[CourseId]     [int] IDENTITY(1, 1) NOT NULL,
	[Number]       [nchar](10) NOT NULL,
	[Title]        [nvarchar](50) NULL,
	CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED([CourseId] ASC)WITH (
	                                                                      PAD_INDEX 
	                                                                      = OFF,
	                                                                      STATISTICS_NORECOMPUTE 
	                                                                      = OFF,
	                                                                      IGNORE_DUP_KEY 
	                                                                      = OFF,
	                                                                      ALLOW_ROW_LOCKS 
	                                                                      = ON,
	                                                                      ALLOW_PAGE_LOCKS 
	                                                                      = ON
	                                                                  ) ON 
	                                                                  [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseDates]    Script Date: 11/08/2009 19:19:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseDates]
(
	[CourseDateId]        [int] IDENTITY(1, 1) NOT NULL,
	[CourseId]            [int] NOT NULL,
	[StartDay]            [date] NULL,
	[Length]              [int] NULL,
	[MaxStudentCount]     [int] NULL,
	CONSTRAINT [PK_CourseDates] PRIMARY KEY CLUSTERED([CourseDateId] ASC)WITH (
	                                                                              PAD_INDEX 
	                                                                              = 
	                                                                              OFF,
	                                                                              STATISTICS_NORECOMPUTE 
	                                                                              = 
	                                                                              OFF,
	                                                                              IGNORE_DUP_KEY 
	                                                                              = 
	                                                                              OFF,
	                                                                              ALLOW_ROW_LOCKS 
	                                                                              = 
	                                                                              ON,
	                                                                              ALLOW_PAGE_LOCKS 
	                                                                              = 
	                                                                              ON
	                                                                          ) 
	                                                                          ON 
	                                                                          [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseAttendees]    Script Date: 11/08/2009 19:19:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseAttendees]
(
	[CourseAttendeeId]     [int] IDENTITY(1, 1) NOT NULL,
	[StudentId]            [int] NOT NULL,
	[CourseId]             [int] NOT NULL,
	CONSTRAINT [PK_CourseAttendees] PRIMARY KEY CLUSTERED([CourseAttendeeId] ASC)WITH (
	                                                                                      PAD_INDEX 
	                                                                                      = 
	                                                                                      OFF,
	                                                                                      STATISTICS_NORECOMPUTE 
	                                                                                      = 
	                                                                                      OFF,
	                                                                                      IGNORE_DUP_KEY 
	                                                                                      = 
	                                                                                      OFF,
	                                                                                      ALLOW_ROW_LOCKS 
	                                                                                      = 
	                                                                                      ON,
	                                                                                      ALLOW_PAGE_LOCKS 
	                                                                                      = 
	                                                                                      ON
	                                                                                  ) 
	                                                                                  ON 
	                                                                                  [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_CourseDates_Courses]    Script Date: 11/08/2009 19:19:07 ******/
ALTER TABLE [dbo].[CourseDates]  
WITH CHECK ADD CONSTRAINT [FK_CourseDates_Courses] FOREIGN KEY([CourseId])
     REFERENCES [dbo].[Courses] ([CourseId])
GO
ALTER TABLE [dbo].[CourseDates] CHECK CONSTRAINT [FK_CourseDates_Courses]
GO
/****** Object:  ForeignKey [FK_CourseAttendees_Courses]    Script Date: 11/08/2009 19:19:07 ******/
ALTER TABLE [dbo].[CourseAttendees]  
WITH CHECK ADD CONSTRAINT [FK_CourseAttendees_Courses] FOREIGN KEY([CourseId])
     REFERENCES [dbo].[Courses] ([CourseId])
GO
ALTER TABLE [dbo].[CourseAttendees] CHECK CONSTRAINT 
[FK_CourseAttendees_Courses]
GO
/****** Object:  ForeignKey [FK_CourseAttendees_Students]    Script Date: 11/08/2009 19:19:07 ******/
ALTER TABLE [dbo].[CourseAttendees]  
WITH CHECK ADD CONSTRAINT [FK_CourseAttendees_Students] FOREIGN KEY([StudentId])
     REFERENCES [dbo].[Students] ([StudentId])
GO
ALTER TABLE [dbo].[CourseAttendees] CHECK CONSTRAINT 
[FK_CourseAttendees_Students]
GO
