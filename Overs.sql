create table dbo.Overs(
Id int identity(1,1) not null primary key,
CompanyName nvarchar(50) not null,
Bussines nvarchar(100) not null,
[Address] nvarchar(150) not null,
Phone nvarchar(20) not null,
ArticleMain nvarchar(max) not null,
Article2 nvarchar(max) not null,
ArticleFoot nvarchar(max) not null,
Email nvarchar(30) not null,
Link1 nvarchar(100) not null,
Link2 nvarchar(100) not null,
Link3 nvarchar(100) not null)
go