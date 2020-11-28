using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalterChavarry.Models;

namespace WalterChavarry.Servicios
{
    public interface ILibrosService
    {
        List<Libros> GetLibros(int FechaInicio, int FechaFinal);

        bool CreateLibros(Libros Libro);

        Libros GetLibroxId(int LibroId);

        List<Libros> BuscarDocumentos(string Palabra);
    }
}
