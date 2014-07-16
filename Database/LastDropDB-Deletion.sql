alter table History
drop constraint fk_plantHistory

drop table History

alter table Subscribes
drop constraint fk_subbedPlants

alter table Subscribes
drop constraint fk_subbedUsers

drop table Subscribes

alter table Users
drop constraint pk_users

drop table Users

alter table Plants
drop constraint pk_plants 

drop table Plants