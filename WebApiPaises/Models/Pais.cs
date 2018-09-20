using System;
using System.Collections.Generic;

namespace WebApiPaises.Models
{
    public partial class Pais
    {
        public Pais()
        {
            Provincia = new HashSet<Provincia>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public ICollection<Provincia> Provincia { get; set; }
    }
}
