using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WalterChavarry.Models
{
    public class Sesion
    {
        public int SesionId { get; set; }
        public string NombreSesion { get; set; }
        public string Descripcion { get; set; }
        public string Palabras_Clave { get; set; }
        public DateTime Fecha { get; set; }
        public string UrlImagen { get; set; }
    }
}