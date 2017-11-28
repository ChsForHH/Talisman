create table dbo.News(
Id int identity(1,1) not null primary key,
[Date] Date not null,
Content nvarchar(300) not null,
Url nvarchar(100) not null)
go