DECLARE @DatabaseName nvarchar(50)
SET @DatabaseName = N'powermeter'

DECLARE @SQL varchar(max)

SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';'
FROM MASTER..SysProcesses
WHERE DBId = DB_ID(@DatabaseName) AND SPId <> @@SPId

--SELECT @SQL 
EXEC(@SQL)

DROP TABLE IF EXISTS record;
GO
DROP TABLE IF EXISTS device;
GO
DROP TABLE IF EXISTS users;
GO
