use CasaDoCodigo;

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