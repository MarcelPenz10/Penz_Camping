create database if not exists db_camping collate utf8_general_ci;
use db_camping;

create table Anfragen(
   vorname varchar(100) not null,
   nachname varchar(100) not null,
   kreditkartennummer int not null auto_increment,
   ersterTagBuchung date not null,
   letzterTagBuchung date not null,
   paket int not null,
   password varchar(128) not null,
   
   constraint id_PK primary key(kreditkartennummer)
)engine=InnoDB;

Insert Into Anfragen Values("Marcel", "Penz", 0, "2020-01-01", "2020-12-12", 0, sha2("pwd", 256));

select * from Anfragen;

