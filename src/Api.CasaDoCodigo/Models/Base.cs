using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Models
{
    public abstract class Base
    {

        public Base()
        {

            Id = Guid.NewGuid();

        }

        [Key]
        public Guid Id { get; set; }

    }
}
