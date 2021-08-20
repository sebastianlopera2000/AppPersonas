using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppPersonas.Models
{
    public class PersonaBU
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Nombres")]
        public string nombres { get; set; }
        [Required]
        [Display(Name = "Apellidos")]
        public string apellidos { get; set; }
        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        public System.DateTime fechaNacim { get; set; }
        [Display(Name = "Fecha de Fallecimiento")]
        public Nullable<System.DateTime> fechaFallec { get; set; }
        [Required]
        [Display(Name = "País de Nacimiento")]
        public int paisNacim { get; set; }

        public string NombrePais { get; set; }

        public string agno { get; set; }

        public string nacimientos { get; set; }

        public string muertes { get; set; }

    }
    [MetadataType(typeof(PersonaBU))]
    public partial class Persona
    {
        public string NombrePais { get; set; }
    }
}