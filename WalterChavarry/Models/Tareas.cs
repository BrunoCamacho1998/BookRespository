using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WalterChavarry.Models
{
    public class Tareas
    {
        public int TareasId { get; set; }
        public string Titulo_Tarea { get; set; }
        public string Descripcion { get; set; }
        public string Instrucciones { get; set; }
        public int SesionId { get; set; }
        public string UrlImagen { get; set; }
    }
}