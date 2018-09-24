--drop trigger insert_trigger_Test

create trigger insert_trigger_Test
on Test
after insert, update
as
declare @ins int, @del int
select @ins = count(*) from inserted
select @del = count(*) from deleted

if @ins > 0 and @del = 0
begin
	insert into OtherTable
	([Name], Age, [Action])
	select [Name], Age, 'inserted' from inserted
end
else if @ins>0 and @del > 0
begin
	insert into OtherTable
	([Name], Age, [Action])
	select [Name], Age, 'inserted' from inserted

	insert into dbo.OtherTable
	([Name], Age, [Action])
	select [Name], Age, 'deleted' from deleted
end