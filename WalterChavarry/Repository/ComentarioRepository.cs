using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WalterChavarry.Models;

namespace WalterChavarry.Repository
{
    public class ComentarioRepository : IComentarioRepository
    {
        SqlConnection cn;
        Conection con = new Conection();

        public bool CreateComentario(Comentario comentario)
        {
            cn = con.GetConection();
            try
            {
                SqlCommand cmd = new SqlCommand("CreateComentario", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Comentario_texto", comentario.Comentario_text);
                cmd.Parameters.AddWithValue("@Usuario", comentario.Usuario);
                cmd.Parameters.AddWithValue("@LibroId", comentario.LibroId);
                cmd.Parameters.AddWithValue("@Tipo", comentario.Tipo);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                cn.Close();

                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Comentario> GetComentarios(int LibroId,int Tipo)
        {
            cn = con.GetConection();
            List<Comentario> ListaComentarios = new List<Comentario>();
            SqlCommand cmd = new SqlCommand("GetComentario", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LibroId", LibroId);
            cmd.Parameters.AddWithValue("@Tipo", Tipo);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            cn.Open();
            sd.Fill(dt);
            cn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                ListaComentarios.Add(
                        new Comentario
                        {
                            ComentarioId = Convert.ToInt32(dr["ComentarioId"]),
                            Comentario_text = Convert.ToString(dr["Comentario_texto"]),
                            Usuario = Convert.ToString(dr["Usuario"]),
                            Fecha = Convert.ToDateTime(dr["Fecha"]),
                            LibroId = Convert.ToInt32(dr["LibroId"]),
                            Tipo = Convert.ToInt32(dr["Tipo"])
                        }
                    );
            }

            return ListaComentarios;
        }
    }
}