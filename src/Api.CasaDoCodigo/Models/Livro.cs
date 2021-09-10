using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Models
{
    public class Livro : Base
    {

        public Livro(string imagem, string titulo, string subTitulo, string resumo, string sumario, decimal valor, int totalDePaginas, string ISBN,
                    DateTime dataPublicacao, Guid categoriaId, Guid autorId)
        {
            Imagem = imagem;
            Titulo = titulo;
            SubTitulo = subTitulo;
            Resumo = resumo;
            Sumario = sumario;
            Valor = valor;
            TotalDePaginas = totalDePaginas;
            this.ISBN = ISBN;
            DataPublicacao = dataPublicacao;
            CategoriaId = categoriaId;
            AutorId = autorId;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public string Imagem { get; set; }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
        public string Resumo { get; set; }
        public string Sumario { get; set; }
        public decimal Valor { get; set; }
        public int TotalDePaginas { get; set; }
        public string ISBN { get; set; }
        public DateTime DataPublicacao { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid AutorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        /* EF CORE RELATIONS */

        public Categoria Categoria { get; set; }
        public Autor Autor { get; set; }

    }
}
