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

ALTER TABLE Autores ADD CONSTRAINT UNI_AUTORES_EMAIL UNIQUE (Email);

Create table IF NOT EXISTS Categorias(
Id char(36) primary key,
Nome varchar(50) not null unique
);

create table IF NOT EXISTS Livros (
Id char(36) primary key,
Titulo varchar(50) unique not null,
Resumo varchar(500) not null,
Sumario MEDIUMTEXT not null,
Valor decimal not null,
TotalDePaginas int not null,
ISBN  varchar(13) unique not null,
DataPublicacao datetime not null,
CategoriaId char(36) not null,
AutorId  char(36) not null,
CreatedAt datetime	not null,
UpdatedAt datetime not null,

CONSTRAINT `fk_livros_autores` FOREIGN KEY ( `AutorId` ) REFERENCES `Autores` ( `Id` ) ,
CONSTRAINT `fk_livros_categorias` FOREIGN KEY ( `CategoriaId` ) REFERENCES `Categorias` ( `Id` )
);

alter table Livros add column SubTitulo varchar(50) not null;
alter table Livros add column Imagem varchar(2083) not null;

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

create table IF NOT EXISTS Clientes (
Id char(36) primary key,
Nome varchar(50) not null,
Sobrenome varchar(50) not null,
Documento varchar(14) not null  unique, 
Email varchar(50) not null unique,
Endereco varchar(50) null,
Complemento varchar(50) not null,
Cidade varchar(50) not null,
PaisId char(36) not null,
EstadoId char(36),
Telefone varchar(12) not null, 
Cep char(8) not null,

CONSTRAINT `fk_clientes_pais` foreign key (`PaisId`) references `Paises` ( `Id` ),
CONSTRAINT `fk_clientes_estado` foreign key (`EstadoId`) references `Estados` (`Id`)
);

create table IF NOT EXISTS Clientes (
Id char(36) primary key,
Nome varchar(50) not null,
Sobrenome varchar(50) not null,
Documento varchar(14) not null  unique, 
Email varchar(50) not null unique,
Endereco varchar(50) not null,
Complemento varchar(50) not null,
Cidade varchar(50) not null,
PaisId char(36) not null,
EstadoId char(36) default "00000000-0000-0000-0000-000000000000",
Telefone varchar(11) not null, 
Cep char(8) not null,
CreatedAt datetime not null,
UpdatedAt datetime not null,

CONSTRAINT `fk_clientes_pais` foreign key (`PaisId`) references `Paises` ( `Id` )
-- CONSTRAINT `fk_clientes_estado` foreign key (`EstadoId`) references `Estados` (`Id`)
);