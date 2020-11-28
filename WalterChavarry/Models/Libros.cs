using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WalterChavarry.Models
{
    public class Libros
    {
        public int LibrosId { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public string Idioma { get; set; }
        public string PalabrasClave { get; set; }
        public string Descripcion { get; set; }
        public byte[] Imagen_libro { get; set; }
        public byte[] Archivo { get; set; }
        public string TipoArchivo { get; set; }

    }

    
    
}