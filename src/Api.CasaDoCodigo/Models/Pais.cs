using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Models
{
    public class Pais : Base
    {
        public Pais(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; }

    }
}
