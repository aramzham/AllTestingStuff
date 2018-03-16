use master
go

CREATE DATABASE [TestTaskDb] ON  PRIMARY 
( NAME = N'TestTaskDb', FILENAME = 'D:\Aram\Programming\Databases\TestTaskDb.mdf' , 
  SIZE = 100MB , MAXSIZE = 1GB, FILEGROWTH = 20MB )
LOG ON 
( NAME = N'TestTaskDb_log', FILENAME = 'D:\Aram\Programming\Databases\TestTaskDb_log.ldf' , 
  SIZE = 100MB , MAXSIZE = 1GB, FILEGROWTH = 10%)
GO