using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Models
{
    public class Autor : Base
    {
        public Autor(string nome, string email, string descricao)
        {
            Nome = nome;
            Email = email;
            Descricao = descricao;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Descricao { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
