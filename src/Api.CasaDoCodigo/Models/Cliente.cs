using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Models
{
    public class Cliente : Base
    {
        public Cliente(string nome, string sobrenome, string documento, string email, string endereco, string complemento,
                       string cidade, Guid paisId, string telefone, string cep)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Documento = documento;
            Email = email;
            Endereco = endereco;
            Complemento = complemento;
            Cidade = cidade;
            PaisId = paisId;
            Telefone = telefone;
            Cep = cep;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public Guid PaisId { get; set; }
        public Guid EstadoId { get; set; }
        public string Telefone { get; set; }
        public string Cep { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        /* EFCORE RELATIONS */

        public Pais Pais { get; set; }
        public Estado Estado { get; set; }
    }
}
