using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Models.ViewModels
{
    public class LivroSimplesDetailsViewModel
    {

        public Guid Id { get; set; }

        public string Titulo { get; set; }

        public string SubTitulo { get; set; }


    }
}
