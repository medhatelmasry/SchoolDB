USE master
GO

IF EXISTS (SELECT * FROM sysdatabases WHERE name='school')
BEGIN
  raiserror('Dropping existing school database ....',0,1)
  DROP database school
END
GO

raiserror('Creating school database....',0,1)
GO

CREATE DATABASE school
GO

USE School;
CREATE TABLE Instructors  
(  
  InstructorId int IDENTITY(1,1) PRIMARY KEY,  
  LastName varchar(50) NOT NULL,
  FirstName varchar(50) NOT NULL,  
  Email varchar(100) NOT NULL,  
);
GO

INSERT INTO Instructors  
   (LastName, FirstName, Email)  
VALUES  
   ('Max', 'Tom', 'tom@max.com'),
   ('Fay', 'Ann', 'ann@fay.com'),
   ('Sun', 'Joe', 'joe@sun.com'),
   ('Fox', 'Sue', 'sue@fox.com'),
   ('Lee', 'Ben', 'ben@lee.com');

CREATE TABLE Courses  
(  
  CourseId int IDENTITY(1,1) PRIMARY KEY,  
  CourseName varchar(100) NOT NULL,
  CourseDescription varchar(1000) NOT NULL,  
  Credits decimal,  
  InstructorId int FOREIGN KEY REFERENCES Instructors(InstructorId)
);
GO

INSERT INTO Courses  
  (CourseName,CourseDescription,Credits,InstructorId)
VALUES
  ('COMP3602','Application Development with C#',4,1),
  ('COMP3832','Object Oriented Software Design',2,2),
  ('COMP3916','Docker for DevOps',1.5,3),
  ('COMP3973','ASP.NET for web Apps',3,4),
  ('COMP1288','IT Project Management Fundamentals',1.5,5),
  ('COMP2909','Angular and Vue.js Fundamentals',1.5,5),
  ('COMP2913','React and Modern JavaScript',1.5,4),
  ('COMP3012','Back-End Web Development with Node.js',3,2),
  ('COMP2831','Business Analysis and Systems Design',3,2),
  ('COMP1516','Programming Fundamental',4,5);
GO
