select * from test

insert into test
values
('Samo',32)

INSERT INTO OPENROWSET('Microsoft.ACE.OLEDB.12.0','Excel 12.0;Database=C:\Users\Aram\Downloads\Database.xlsx;','SELECT * FROM [Sheet1$]') 
SELECT [Name], Age, 'inserted', getdate() FROM dbo.test

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
EXEC sp_MSset_oledb_prop N'Microsoft.ACE.OLEDB.12.0', N'AllowInProcess', 1
GO 
EXEC sp_MSset_oledb_prop N'Microsoft.ACE.OLEDB.12.0', N'DynamicParameters', 1
GO

EXEC sp_configure 'show advanced options', 1
RECONFIGURE
EXEC sp_configure 'Ad Hoc Distributed Queries', 1
RECONFIGURE
