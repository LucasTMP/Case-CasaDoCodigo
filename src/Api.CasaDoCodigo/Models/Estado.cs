using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Models
{
    public class Estado : Base
    {
        public Estado(string nome, Guid paisId)
        {
            Nome = nome;
            PaisId = paisId;
        }

        public string Nome { get; set; }
        public Guid PaisId { get; set; }

        /* EFCORE RELATIONS */

        public Pais Pais { get; set; }

    }
}
