using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Models
{
    public class ClienteSimplesDetailsViewModel
    {
        public ClienteSimplesDetailsViewModel()
        {

        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [StringLength(50, ErrorMessage = "O campo {0} tem que possuir entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [StringLength(50, ErrorMessage = "O campo {0} tem que possuir entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [StringLength(14, ErrorMessage = "O campo {0} tem que possuir entre {2} e {1} caracteres.", MinimumLength = 11)]
        public string Documento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [StringLength(50, ErrorMessage = "O campo {0} tem que possuir entre {2} e {1} caracteres.", MinimumLength = 1)]
        [EmailAddress(ErrorMessage = "O {0} não é valido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [StringLength(11, ErrorMessage = "O campo {0} deve possuir {2} ou {1} digitos.", MinimumLength = 10)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        public string Pais { get; set; }

        public string Estado { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [StringLength(8, ErrorMessage = "O campo {0} deve possuir {1} digitos.", MinimumLength = 8)]
        public string Cep { get; set; }

    }
}
