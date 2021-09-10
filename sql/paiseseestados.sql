use CasaDoCodigo;

create table IF NOT EXISTS Paises (
Id char(36) primary key,
Nome varchar(50) not null unique
);

create table IF NOT EXISTS Estados (
Id char(36) primary key,
Nome varchar(50) not null,
PaisId char(36) not null,

CONSTRAINT `fk_estados_paises` FOREIGN KEY ( `PaisId` ) REFERENCES `Paises` ( `Id` )
);