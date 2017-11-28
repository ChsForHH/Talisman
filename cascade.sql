alter table dbo.Photos
add constraint fk_tovarId foreign key(tovarId)
references dbo.Tovars(tovarId)
on delete cascade;