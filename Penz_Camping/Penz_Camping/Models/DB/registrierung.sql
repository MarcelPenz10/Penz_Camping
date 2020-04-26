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
   rolle int not null,
   
   constraint id_PK primary key(id)
)engine=InnoDB;

Insert Into users Values(null, "Marcel", "Penz", 0, "2002-01-25", "user", sha2("pwd", 512), 3);
Insert Into users Values(null, "Admin", "admin", 0, "2002-05-02", "adminuser", sha2("admin", 512), 3);

select * from users;

