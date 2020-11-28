using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalterChavarry.Models;

namespace WalterChavarry.Servicios
{
    interface IComentarioService
    {

        List<Comentario> GetComentarios(int LibroId, int Tipo);

        bool CreateComentario(Comentario comentario);
    }
}
