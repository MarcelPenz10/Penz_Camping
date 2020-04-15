create database if not exists db_registrierung collate utf8_general_ci;
use db_registrierung;

create table users(
   id int not null auto_increment,
   vorname varchar(100) null,
   nachname varchar(100) not null,
   gender int not null,
   birthdate date null,
   username varchar(100) not null unique,
   password varchar(128) not null,
   passwordAdmin varchar(128) null,
   
   constraint id_PK primary key(id)
)engine=InnoDB;

Insert Into users Values(null, "Marcel", "Penz", 0, "2002-01-25", "user", sha2("pwd", 512), sha2("pwdA", 512));

select * from users;

