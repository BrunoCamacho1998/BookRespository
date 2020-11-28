using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WalterChavarry.Models;
using WalterChavarry.Repository;

namespace WalterChavarry.Servicios
{
    public class LibrosService : ILibrosService
    {
        ILibroRepository libroRes;

        public LibrosService()
        {
            libroRes = new LibroRepository();
        }

        public List<Libros> BuscarDocumentos(string Palabra)
        {
            return libroRes.BuscarDocumentos(Palabra);
        }

        public bool CreateLibros(Libros Libro)
        {
            return libroRes.CreateLibros(Libro);
        }

        public List<Libros> GetLibros(int FechaInicio, int FechaFinal)
        {
            return libroRes.GetLibros(FechaInicio, FechaFinal);
        }

        public Libros GetLibroxId(int LibroId)
        {
            return libroRes.GetLibroxId(LibroId);
        }
    }
}