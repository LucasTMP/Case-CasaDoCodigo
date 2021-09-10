using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Models
{
    public class ClienteDetailsViewModel
    {

        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Documento { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }

        public string Complemento { get; set; }

        public string Cidade { get; set; }

        public string Pais { get; set; }

        public string Estado { get; set; }

        public string Cep { get; set; }

        public DateTime CriadoEm { get; set; }

    }
}
