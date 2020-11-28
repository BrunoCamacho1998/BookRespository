using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WalterChavarry.Models
{
    public class Comentario
    {
        public int ComentarioId { get; set; }
        public string Comentario_text { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public int LibroId { get; set; }
        public int Tipo { get; set; }
    }
}