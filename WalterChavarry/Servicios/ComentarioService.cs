using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WalterChavarry.Models;
using WalterChavarry.Repository;

namespace WalterChavarry.Servicios
{
    public class ComentarioService : IComentarioService
    {
        IComentarioRepository comentarioRes;

        public ComentarioService()
        {
            comentarioRes = new ComentarioRepository();
        }

        public bool CreateComentario(Comentario comentario)
        {
            return comentarioRes.CreateComentario(comentario);
        }

        public List<Comentario> GetComentarios(int LibroId, int Tipo)
        {
            return comentarioRes.GetComentarios(LibroId, Tipo);
        }
    }
}