using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Models.ViewModels
{
    public class AutorViewModel
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio.")]
        [StringLength(50, ErrorMessage = "O campo {0} não pode ter menos de {2} ou mais de {1} caracteres", MinimumLength = 1)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio.")]
        [StringLength(50, ErrorMessage = "O campo {0} não pode ter menos de {2} ou mais de {1} caracteres", MinimumLength = 1)]
        [EmailAddress(ErrorMessage = "O {0} não é valido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio.")]
        [StringLength(400, ErrorMessage = "O campo {0} não pode ter menos de {2} ou mais de {1} caracteres", MinimumLength = 1)]
        public string Descricao { get; set; }

        [Display(Name = "Data de Criação")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [NonEmptyDate(ErrorMessage = "É necessario informar uma {0} válida!")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Data de Edição")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [NonEmptyDate(ErrorMessage = "É necessario informar uma {0} válida!")]
        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }

    }
}
