create table dbo.FeedBacks(
Id int not null identity(1,1) primary key,
Name nvarchar(50) not null,
[Message] nvarchar(max) not null,
[Date] Datetime not null,
Email nvarchar(50) not null,
Contacts nvarchar(200) null)
go
drop table dbo.Services;
go
