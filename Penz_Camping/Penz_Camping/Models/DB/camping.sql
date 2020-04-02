create database if not exists db_camping collate utf8_general_ci;
use db_camping;

create table Reservierungsanfragen(
   vorname varchar(100) null,
   nachname varchar(100) not null,
   kreditkartennummer int not null auto_increment,
   ersterTagBuchung date not null,
   letzterTagBuchung date not null,
   paket int not null,
   
   constraint id_PK primary key(kreditkartennummer)
)engine=InnoDB;

Insert Into Reservierungsanfrage Values("Marcel", "Penz", 0, "2020-01-01", "2020-12-12", null);

select * from Reservierungsanfragen;

