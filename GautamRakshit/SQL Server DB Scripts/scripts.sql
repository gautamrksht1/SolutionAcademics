USE [master]
GO
/****** Object:  Database [Solution]    Script Date: 7/20/2020 2:55:54 PM ******/
CREATE DATABASE [Solution]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Solution', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER2014\MSSQL\DATA\Solution.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Solution_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER2014\MSSQL\DATA\Solution_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Solution] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Solution].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Solution] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Solution] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Solution] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Solution] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Solution] SET ARITHABORT OFF 
GO
ALTER DATABASE [Solution] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Solution] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Solution] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Solution] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Solution] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Solution] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Solution] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Solution] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Solution] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Solution] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Solution] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Solution] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Solution] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Solution] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Solution] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Solution] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Solution] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Solution] SET RECOVERY FULL 
GO
ALTER DATABASE [Solution] SET  MULTI_USER 
GO
ALTER DATABASE [Solution] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Solution] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Solution] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Solution] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Solution] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Solution', N'ON'
GO
USE [Solution]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 7/20/2020 2:55:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[CoureId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Detail] [nvarchar](max) NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CoureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CourseSubscription]    Script Date: 7/20/2020 2:55:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseSubscription](
	[StudId] [int] NULL,
	[CourseId] [int] NULL,
	[RegId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_CourseSubscription] PRIMARY KEY CLUSTERED 
(
	[RegId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Student]    Script Date: 7/20/2020 2:55:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Dob] [datetime] NULL,
	[ContactNo] [nchar](10) NULL,
	[StudId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CourseSubscription]  WITH CHECK ADD  CONSTRAINT [FK_CourseSubscription_Student] FOREIGN KEY([RegId])
REFERENCES [dbo].[CourseSubscription] ([RegId])
GO
ALTER TABLE [dbo].[CourseSubscription] CHECK CONSTRAINT [FK_CourseSubscription_Student]
GO
/****** Object:  StoredProcedure [dbo].[CreateCourse]    Script Date: 7/20/2020 2:55:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--Exec CreateStudent ''
Create PROCEDURE [dbo].[CreateCourse] 
	-- Add the parameters for the stored procedure here
	(@CourseName Nvarchar(50),
	@CourseDetail Nvarchar(max)		
	)

AS
BEGIN
 
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO Course  VALUES(
   @CourseName,@CourseDetail
   );
  
END

GO
/****** Object:  StoredProcedure [dbo].[CreateStudent]    Script Date: 7/20/2020 2:55:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--Exec CreateStudent ''
CREATE PROCEDURE [dbo].[CreateStudent] 
	-- Add the parameters for the stored procedure here
	(@FirstName Nvarchar(50),
	@LastName Nvarchar(50),
	@Dob datetime,
	@ContactNo Nvarchar(50)
	
	)

AS
BEGIN
 
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO Student  VALUES(
   @FirstName,@LastName,@Dob,@ContactNo
   );
  
END

GO
/****** Object:  StoredProcedure [dbo].[CreateSubscription]    Script Date: 7/20/2020 2:55:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[CreateSubscription]    Script Date: 7/20/2020 10:27:12 AM ******/

--SELECT * FROM cOURSEsUBSCRIPTION
--exec CreateSubscription 2,3
CREATE PROCEDURE [dbo].[CreateSubscription]
	(@StudId int ,@CourseID int,@StatusId int out)
AS
BEGIN
declare @CheckStudID int;
declare @CheckCourseID int;
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Set @CheckCourseID=0;
	Set @CheckStudID=0;
	select @CheckStudID =StudId from Student where StudId=@StudId;
	select @CheckCourseID =CoureId from Course where CoureId=@CourseID;
	 if(@CheckCourseID>0 AND @CheckStudID>0)	 
	 BEGIN
	 
		 IF NOT EXISTS(
		 SELECT * FROM CourseSubscription WHERE StudId=@StudId AND CourseId=@CourseID
		 )
		 BEGIN

			 INSERT INTO CourseSubscription  VALUES(
			   @StudId,@CourseID
			   );
			  SET @StatusId=1;
		 END
		 ELSE
		  BEGIN
				SET @StatusId=-1;
		   END
	 END
	 ELSE
	 BEGIN
	 SET @StatusId=0;
	 END
    
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteStudent]    Script Date: 7/20/2020 2:55:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from Student
Create PROCEDURE [dbo].[DeleteStudent]
	-- Add the parameters for the stored procedure here
	(@StudId int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	
	SET NOCOUNT ON;
	
   delete from Student where StudId=@StudId;
END

GO
/****** Object:  StoredProcedure [dbo].[GetCourses]    Script Date: 7/20/2020 2:55:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from Student
Create PROCEDURE [dbo].[GetCourses]
	-- Add the parameters for the stored procedure here
	(@PageIndex int,@PageSize int,@TotalPages int out )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	declare @totalRows int;
	declare @pages Numeric(10,2);
	
	SET NOCOUNT ON;
	Select @totalRows=count(CoureId) from Course;
	SET @pages=@totalRows/@PageSize
	if(@totalRows%@PageSize >0)
	BEGIN
    SET @pages=@pages+1;
	END
	Set @TotalPages=@pages;
	
    SELECT * 
	FROM Course
	 ORDER BY CoureId ASC
	  OFFSET @PageSize * (@PageIndex - 1) ROWS FETCH NEXT @PageSize ROWS ONLY  
END

GO
/****** Object:  StoredProcedure [dbo].[GetStudent]    Script Date: 7/20/2020 2:55:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--select * from Student
--exec GetStudent 1
CREATE PROCEDURE [dbo].[GetStudent]
	-- Add the parameters for the stored procedure here
	(@StudID int )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * 
	FROM Student
	WHERE StudId=@StudID;
END

GO
/****** Object:  StoredProcedure [dbo].[GetStudents]    Script Date: 7/20/2020 2:55:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from Student
CREATE PROCEDURE [dbo].[GetStudents]
	-- Add the parameters for the stored procedure here
	(@PageIndex int,@PageSize int,@TotalPages int out )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	declare @totalRows int;
	declare @pages Numeric(10,2);
	
	SET NOCOUNT ON;
	Select @totalRows=count(StudId) from Student;
	SET @pages=@totalRows/@PageSize
	if(@totalRows%@PageSize >0)
	BEGIN
    SET @pages=@pages+1;
	END
	Set @TotalPages=@pages;
	
    SELECT * 
	FROM Student
	 ORDER BY StudId ASC
	  OFFSET @PageSize * (@PageIndex - 1) ROWS FETCH NEXT @PageSize ROWS ONLY  
END

GO
/****** Object:  StoredProcedure [dbo].[GetSubscriptionDetail]    Script Date: 7/20/2020 2:55:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--exec GetSubscriptionDetail
CREATE PROCEDURE [dbo].[GetSubscriptionDetail]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select RegId ,concat(s.FirstName,s.LastName) as StudentName,c.Name as CourseName 
	from CourseSubscription cs
	inner join Student s on cs.StudId=s.StudId
	inner join Course c on cs.CourseId=c.CoureId;
END

GO
/****** Object:  StoredProcedure [dbo].[StudentsAndCourses]    Script Date: 7/20/2020 2:55:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[StudentsAndCourses] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT StudId,CONCAT(FirstName,LastName) as Name from Student
	SELECT CoureId,Name FROM Course
END

GO
/****** Object:  StoredProcedure [dbo].[StudentsAndCoursesList]    Script Date: 7/20/2020 2:55:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[StudentsAndCoursesList] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT StudId,CONCAT(FirstName,LastName) as Name from Student
	SELECT CoureId,Name FROM Course
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateStudent]    Script Date: 7/20/2020 2:55:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--select * from Student
--exec GetStudent 1
Create PROCEDURE [dbo].[UpdateStudent]
	-- Add the parameters for the stored procedure here
	(@StudID int,
	@FirstName Nvarchar(50),
	@LastName Nvarchar(50),
	@Dob datetime,
	@ContactNo Nvarchar(50) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE Student SET FirstName=@FirstName,LastName=@LastName,Dob=@Dob,ContactNo=@ContactNo
	 WHERE StudId=@StudID;
END

GO
USE [master]
GO
ALTER DATABASE [Solution] SET  READ_WRITE 
GO
