//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPersonas.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class persona
    {
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public System.DateTime fechaNacim { get; set; }
        public Nullable<System.DateTime> fechaFallec { get; set; }
        public int paisNacim { get; set; }
    
        public virtual pais pais { get; set; }
    }
}
