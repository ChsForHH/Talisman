create table dbo.Services(
Id int not null identity(1,1) primary key,
[ServiceName] nvarchar(50) not null,
[Article] nvarchar(max) not null)
go
