using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WalterChavarry.Models
{
    public class ComentarioSesiones
    {
        public int ComentarioSesionesId { get; set; }
        public string Comentario_text { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public int ObjetoId { get; set; }
        public int Tipo { get; set; }
        public byte[] ArchivoComentario { get; set; }
        public string NombreArchivo { get; set; }
        public string TipoArchivo { get; set; }
    }
}