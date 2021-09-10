using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Models.ViewModels
{
    public class LivroViewModel
    {

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [NonEmptyGuid(ErrorMessage = "O campo de identificador do livro é obrigatorio!")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [StringLength(2083, ErrorMessage = "O campo {0} tem que possuir entre {2} e {1} caracteres.", MinimumLength = 1)]
        [IsValidUri(ErrorMessage = "O endereço da imagem não é válido.")]
        public string Imagem { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [StringLength(50, ErrorMessage = "O campo {0} tem que possuir entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [StringLength(50, ErrorMessage = "O campo {0} tem que possuir entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string SubTitulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [StringLength(500, ErrorMessage = "O campo {0} tem que possuir entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string Resumo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [MinLength(1, ErrorMessage = "O campo {0} tem que possuir mais de {1} caracter!")]
        public string Sumario { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatorio!")]
        [Range(20, int.MaxValue, ErrorMessage = "O {0} precisa ser maior ou igual a {1}!")]
        public decimal Valor { get; set; }

        [Display(Name = "Total de paginas")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [Range(100, int.MaxValue, ErrorMessage = "O {0} precisa ser maior ou igual a {100}!")]
        public int TotalDePaginas { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [StringLength(13, ErrorMessage = "O campo {0} tem que possuir entre {2} e {1} caracteres.", MinimumLength = 10)]
        public string ISBN { get; set; }

        [Display(Name = "Data de publicação")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [NonEmptyDate(ErrorMessage = "É necessario informar uma data válida!")]
        [DataType(DataType.DateTime)]
        public DateTime DataPublicacao { get; set; }

        [Display(Name = "Data de cadastro")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [NonEmptyDate(ErrorMessage = "É necessario informar uma data válida!")]
        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Ultima modificação")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [NonEmptyDate(ErrorMessage = "É necessario informar uma data válida!")]
        [DataType(DataType.DateTime)]
        public DateTime UltimaModificacao { get; set; }

        /* EF CORE RELATIONS */

        public string CategoriaNome { get; set; }
        public string AutorNome { get; set; }
        public string AutorDescricao { get; set; }


    }
}
