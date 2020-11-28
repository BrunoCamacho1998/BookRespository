using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WalterChavarry.Models
{
    public class Archivo
    {
        public int ArchivoId { get; set; }
        public string NombreArchivo { get; set; }
        public byte[] ArchivoFile { get; set; }
        public string Tipo { get; set; }
        public int TareaId { get; set; }
    }
}