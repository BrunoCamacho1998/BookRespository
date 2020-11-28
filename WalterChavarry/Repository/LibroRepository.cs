using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WalterChavarry.Models;

namespace WalterChavarry.Repository
{
    public class LibroRepository : ILibroRepository
    {
        SqlConnection cn;
        Conection con = new Conection();

        public List<Libros> BuscarDocumentos(string Palabra)
        {
            cn = con.GetConection();
            List<Libros> ListaLibros = new List<Libros>();
            SqlCommand cmd = new SqlCommand("BuscarDocumentos", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Palabra", Palabra);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            cn.Open();
            sd.Fill(dt);
            cn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Libros nuevoLibro = new Libros
                {
                    LibrosId = Convert.ToInt32(dr["LibroId"]),
                    Titulo = Convert.ToString(dr["Titulo"]),
                    Dia = Convert.ToInt32(dr["Dia"]),
                    Mes = Convert.ToInt32(dr["Mes"]),
                    Anio = Convert.ToInt32(dr["Anio"]),
                    Idioma = Convert.ToString(dr["Idioma"]),
                    PalabrasClave = Convert.ToString(dr["PalabrasClave"]),
                    Descripcion = Convert.ToString(dr["Descripcion"]),
                    Autor = Convert.ToString(dr["Autor"]),
                    TipoArchivo = Convert.ToString(dr["TipoArchivo"])
                };
                if (dr["Archivo"].GetType().ToString().CompareTo("System.DBNull") != 0)
                {
                    nuevoLibro.Archivo = (byte[])dr["Archivo"];
                }
                if (dr["Imagen_Libro"].GetType().ToString().CompareTo("System.DBNull") != 0)
                {
                    nuevoLibro.Imagen_libro = (byte[])dr["Imagen_Libro"];
                }

                ListaLibros.Add(nuevoLibro);
            }

            return ListaLibros;
        }

        public bool CreateLibros(Libros Libro)
        {
            bool resp = false;
            cn = con.GetConection();
            try
            {
                SqlCommand cmd = new SqlCommand("CreateLibros", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Titulo", Libro.Titulo);
                cmd.Parameters.AddWithValue("@Dia", Libro.Dia);
                cmd.Parameters.AddWithValue("@Mes", Libro.Mes);
                cmd.Parameters.AddWithValue("@Anio", Libro.Anio);
                cmd.Parameters.AddWithValue("@Idioma", Libro.Idioma);
                cmd.Parameters.AddWithValue("@PalabrasClave", Libro.PalabrasClave);
                cmd.Parameters.AddWithValue("@Descripcion", Libro.Descripcion);
                cmd.Parameters.AddWithValue("@Imagen", Libro.Imagen_libro);
                cmd.Parameters.AddWithValue("@Autor", Libro.Autor);
                cmd.Parameters.AddWithValue("@Archivo", Libro.Archivo);
                cmd.Parameters.AddWithValue("@TipoArchivo", Libro.TipoArchivo);
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

        public List<Libros> GetLibros(int FechaInicio, int FechaFinal)
        {
            cn = con.GetConection();
            List<Libros> ListaLibros = new List<Libros>();
            SqlCommand cmd = new SqlCommand("GetLibros", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AnioInicio", FechaInicio);
            cmd.Parameters.AddWithValue("@AnioFinal", FechaFinal);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            cn.Open();
            sd.Fill(dt);
            cn.Close();

            foreach(DataRow dr in dt.Rows)
            {
                Libros nuevoLibro = new Libros
                {
                    LibrosId = Convert.ToInt32(dr["LibroId"]),
                    Titulo = Convert.ToString(dr["Titulo"]),
                    Dia = Convert.ToInt32(dr["Dia"]),
                    Mes = Convert.ToInt32(dr["Mes"]),
                    Anio = Convert.ToInt32(dr["Anio"]),
                    Idioma = Convert.ToString(dr["Idioma"]),
                    PalabrasClave = Convert.ToString(dr["PalabrasClave"]),
                    Descripcion = Convert.ToString(dr["Descripcion"]),
                    Autor = Convert.ToString(dr["Autor"]),
                    TipoArchivo = Convert.ToString(dr["TipoArchivo"])
                };

                if (dr["Archivo"].GetType().ToString().CompareTo("System.DBNull")!=0)
                {
                    nuevoLibro.Archivo = (byte[])dr["Archivo"];
                }
                if(dr["Imagen_Libro"].GetType().ToString().CompareTo("System.DBNull") != 0)
                {
                    nuevoLibro.Imagen_libro = (byte[])dr["Imagen_Libro"];
                }
               
                ListaLibros.Add(nuevoLibro);
            }

            return ListaLibros;

        }

        public Libros GetLibroxId(int LibroId)
        {
            cn = con.GetConection();
            List<Libros> ListaLibros = new List<Libros>();
            SqlCommand cmd = new SqlCommand("GetLibrosxId", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LibroId", LibroId);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            cn.Open();
            sd.Fill(dt);
            cn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Libros nuevoLibro = new Libros
                {
                    LibrosId = Convert.ToInt32(dr["LibroId"]),
                    Titulo = Convert.ToString(dr["Titulo"]),
                    Dia = Convert.ToInt32(dr["Dia"]),
                    Mes = Convert.ToInt32(dr["Mes"]),
                    Anio = Convert.ToInt32(dr["Anio"]),
                    Idioma = Convert.ToString(dr["Idioma"]),
                    PalabrasClave = Convert.ToString(dr["PalabrasClave"]),
                    Descripcion = Convert.ToString(dr["Descripcion"]),
                    Autor = Convert.ToString(dr["Autor"]),
                    TipoArchivo = Convert.ToString(dr["TipoArchivo"])
                };

                if (dr["Archivo"].GetType().ToString().CompareTo("System.DBNull") != 0)
                {
                    nuevoLibro.Archivo = (byte[])dr["Archivo"];
                }
                if (dr["Imagen_Libro"].GetType().ToString().CompareTo("System.DBNull") != 0)
                {
                    nuevoLibro.Imagen_libro = (byte[])dr["Imagen_Libro"];
                }

                ListaLibros.Add(nuevoLibro);
            }

            return ListaLibros.FirstOrDefault();
        }
    }
}