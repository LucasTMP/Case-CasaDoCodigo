using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Models.ViewModels
{
    public class EstadoAddViewModel
    {

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [NonEmptyGuid(ErrorMessage = "O campo de identificação do pais é obrigatorio!")]
        public Guid PaisId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [StringLength(50, ErrorMessage = "O campo {0} tem que possuir entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string Nome { get; set; }

    }
}
