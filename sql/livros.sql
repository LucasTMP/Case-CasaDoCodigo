use CasaDoCodigo;

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