USE TestTaskDb
GO
create table University
(
	[Id] int identity primary key not null,
	[Name] varchar(500) not null,
	[Address] varchar(300) null
)
GO
CREATE TABLE Employee
(   
 [Id] int identity primary key NOT NULL,
 [Name] varchar(100) NULL,
 [Surname] varchar(100) not NULL,
 [Salary] MONEY NULL,
 [IsGettingBonus] bit not null default 0,
 [UniversityId] int foreign key references University(id),
 [Info] nvarchar(3000) null
) ON [PRIMARY]
GO

insert into University
values
('YSU','Alek Manukian 1')
insert into Employee
values
('secondName','secondSurname','500000','0',1,'here comes John Cena'),
('Aram','Zhamkochyan','270000','1',1,'the best .net developer')

SELECT * FROM Employee