use internship

create table Plants( 
	Name varchar(30) primary key, 
	PlantState bit, 
	WaterAmount int
)

insert into Plants (Name, PlantState, WaterAmount) values ('Marinela',0,100)
insert into Plants (Name, PlantState, WaterAmount) values ('Jon',1,1000)
insert into Plants (Name, PlantState, WaterAmount) values ('Marcel',1,500)
insert into Plants (Name, PlantState, WaterAmount) values ('Megaluza',0,450)
insert into Plants (Name, PlantState, WaterAmount) values ('Alexandrina',1,700)





create table Users(
	Mail varchar(50) primary key, 
	Pass varchar(30)
)

insert into Users (Mail, Pass) values ('mari@yahoo.com','maricica')
insert into Users (Mail, Pass) values ('lazea_alina@gmail.com','alynutza')
insert into Users (Mail, Pass) values ('nimeni.andreea@yahoo.com','andreiutza')
insert into Users (Mail, Pass) values ('maxim_ale@yahoo.com','aletaci')
insert into Users (Mail, Pass) values ('fluture_alexa@gmail.com','alexalove')






create table Subscribes(
	MailSubscriber varchar(50) references Users(Mail), 
	PlantName varchar(30) references Plants(Name)
)

insert into Subscribes (MailSubscriber, PlantName) values ('mari@yahoo.com','Marinela')
insert into Subscribes (MailSubscriber, PlantName) values ('lazea_alina@gmail.com','Alexandrina')
insert into Subscribes (MailSubscriber, PlantName) values ('nimeni.andreea@yahoo.com','Marcel')
insert into Subscribes (MailSubscriber, PlantName) values ('maxim_ale@yahoo.com','Jon')
insert into Subscribes (MailSubscriber, PlantName) values ('fluture_alexa@gmail.com','Megaluza')




create table History(
	PlantName varchar(30) references Plants(Name),
	WateredOn datetime
)
	
insert into History (PlantName, WateredOn) values ('Marinela','07/10/2014 10:05')
insert into History (PlantName, WateredOn) values ('Marinela','07/11/2014 8:05')
insert into History (PlantName, WateredOn) values ('Marinela','07/11/2014 18:05')
insert into History (PlantName, WateredOn) values ('Marinela','07/12/2014 13:05')
insert into History (PlantName, WateredOn) values ('Jon','07/09/2014 14:05')
insert into History (PlantName, WateredOn) values ('Jon','07/10/2014 18:25')
insert into History (PlantName, WateredOn) values ('Megaluza','07/13/2014 11:30')
insert into History (PlantName, WateredOn) values ('Alexandrina','07/09/2014 14:50')

--CRUD plants

insert into Plants (Name, PlantState, WaterAmount) values ('A',0,100)

update Plants set PlantState = 1 where Name=''

delete from Plants where Name=''


--CRUD users

insert into Users (Mail, Pass) values ('mail','pass')

update Users set Pass='' where Mail=''

delete from Users where Mail=''


-- subscribe

insert into Subscribes (MailSubscriber, PlantName) values ('','')


select * from Plants