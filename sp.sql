select t1.TovarId, Name, Price, min(PhotoId) from dbo.Tovars t1 join 
 dbo.Photos t2 on t1.TovarId=t2.TovarId and t1.IsNew=1
 group by t1.tovarId, Name, Price
