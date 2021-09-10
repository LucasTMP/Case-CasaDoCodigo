create database IF NOT EXISTS CasaDoCodigo;

use CasaDoCodigo;

create table IF NOT EXISTS Autores(
Id char(36) primary key,
Nome varchar(50) not null,
Email varchar(50) not null,
Descricao varchar(400) not null,
CreatedAt datetime not null,
UpdatedAt datetime not null
);