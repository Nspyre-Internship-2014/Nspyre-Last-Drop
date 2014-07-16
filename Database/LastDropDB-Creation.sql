create table Plants( 
	Name varchar(30) NOT NULL, 
	PlantState varchar(30), 
	WaterAmount int
)

alter table Plants
add constraint pk_plants primary key (Name);

create table Users(
	Mail varchar(50) NOT NULL, 
	Pass varchar(30)
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

alter table Subscribes
add constraint fk_subbedPlants
foreign key (PlantName) 
references Plants(Name)

create table History(
	PlantName varchar(30),
	WateredOn datetime
)

alter table History
add constraint fk_plantHistory
foreign key (PlantName) 
references Plants(Name)
