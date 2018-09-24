create table Test(
Id int identity not null,
[Name] varchar(450) not null,
Age int null
)

insert into Test
values
('Aram', 29)

select * from Test

create table OtherTable(
Id int identity not null,
[Name] varchar(450) not null,
Age int null,
[Action] varchar(450) not null,
CreationDate datetime default getdate()
)

insert into Test
values
('Narek', 29)

update Test
set name =  'Aram'
where id = 7

select * from OtherTable