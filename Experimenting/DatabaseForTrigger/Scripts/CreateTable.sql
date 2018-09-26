create table Test(
Id int identity not null,
[Name] varchar(450) not null,
Age int null
)

create table OtherTable(
Id int identity not null,
[Name] varchar(450) not null,
Age int null,
[Action] varchar(450) not null,
CreationDate datetime default getdate()
)