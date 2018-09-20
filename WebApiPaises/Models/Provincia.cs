using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WebApiPaises.Models
{
    public partial class Provincia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        
        public int? IdPais { get; set; }
        [JsonIgnore]
        public Pais IdPaisNavigation { get; set; }
    }
}
