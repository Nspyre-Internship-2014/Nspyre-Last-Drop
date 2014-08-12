use LastDropDB
create table Plants( 
	Name varchar(30) NOT NULL, 
	PlantState varchar(30), 
	WaterAmount int,
	CoolDown int,
	DryValue int,
	HumidValue int,
	PlantID int
)

alter table Plants
add constraint pk_plants primary key (Name);

create table Users(
	Mail varchar(50) NOT NULL, 
	Pass varchar(100)
)

alter table Users
add constraint pk_users primary key (Mail);

create table Subscribes(
	MailSubscriber varchar(50), 
	PlantName varchar(30)
)

alter table Subscribes
add constraint fk_subbedUsers 
foreign key (MailSubscriber) 
references Users(Mail)
on update cascade
on delete cascade

alter table Subscribes
add constraint fk_subbedPlants
foreign key (PlantName) 
references Plants(Name)
on update cascade
on delete cascade

create table History(
	PlantName varchar(30),
	WateredOn datetime
)

alter table History
add constraint fk_plantHistory
foreign key (PlantName) 
references Plants(Name)
on update cascade
on delete cascade

create table UserNotificationOptions( 
	Mail varchar(50) NOT NULL, 
	IFrom time,
	ITo time,
	MailToggle bit,
	DesktopToggle bit,
	Interval int 
)

alter table UserNotificationOptions
add constraint fk_userNotificationOptions
foreign key (Mail) 
references Users(Mail)
on update cascade
on delete cascade