use CasaDoCodigo;

Create table IF NOT EXISTS Categorias(
Id char(36) primary key,
Nome varchar(50) not null unique
);