using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WalterChavarry.Models;
using WalterChavarry.Servicios;

namespace WalterChavarry.Controllers
{
    public class LibrosController : Controller
    {

        ILibrosService libroServ;
        IComentarioService comentarioServ;

        public LibrosController()
        {
            libroServ = new LibrosService();
            comentarioServ = new ComentarioService();
        }


        public FileContentResult GetImage(int libroId)
        {
            Libros lb = libroServ.GetLibroxId(libroId);
            if (lb != null)
            {
                string type = string.Empty;
                type = "image/jpeg";

                return File(lb.Imagen_libro, type);
            }
            else
            {
                return null;
            }
        }

        public FileContentResult GetDocumento(int libroId)
        {
            Libros lb = libroServ.GetLibroxId(libroId);
            if (lb != null)
            {
                string type = string.Empty;
                switch (lb.TipoArchivo)
                {
                    case "docx":
                        type = "application/msword";
                        break;

                    case "xlsx":
                        type = "application/vnd.ms-excel";
                        break;
                    case "pptx":
                        type = "application/vnd.ms-powerpoint";
                        break;
                    case "pdf":
                        type = "application/pdf";
                        break;
                    case "csv":
                        type = "text/csv";
                        break;
                    case "jpeg":
                        type = "image/jpeg";
                        break;
                    case "jpg":
                        type = "image/jpeg";
                        break;
                    case "rar":
                        type = "application/x-rar-compressed";
                        break;
                    case "zip":
                        type = "application/zip";
                        break;
                }
                  
                return File(lb.Archivo, type);
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.error = "";
            List<Libros> ListaLibros = new List<Libros>();
            ListaLibros = libroServ.GetLibros(1980, DateTime.Now.Year);
            Libros lb = ListaLibros.FirstOrDefault();
            ViewBag.numeroLibros = ListaLibros.Count;
            ViewBag.descripcion = lb.Descripcion;
            ViewBag.palabra = "";
            return View(ListaLibros.ToList());
        }

        [HttpPost]
        public ActionResult Index(int? FechaInicial, int? FechaFinal, string var_orden, string idioma, string Palabra)
        {
            List<Libros> ListaLibros = new List<Libros>();
            ViewBag.numeroLibros = 0;
            bool OnlyAnio = false;

            if(Palabra == "")
            {
                ViewBag.palabra = "";
                if (FechaInicial == null)
                {
                    FechaInicial = 1980;
                }

                if (FechaFinal == null)
                {
                    FechaFinal = DateTime.Now.Year;

                    ListaLibros = libroServ.GetLibros((int)FechaInicial, (int)FechaFinal);
                    ViewBag.anioInicial = FechaInicial;
                    ViewBag.numeroLibros = ListaLibros.Count;
                    OnlyAnio = true;
                }

                if ((int)FechaFinal == 0)
                {
                    FechaFinal = DateTime.Now.Year;

                    ListaLibros = libroServ.GetLibros((int)FechaInicial, (int)FechaFinal);
                    ViewBag.anioInicial = FechaInicial;
                    ViewBag.numeroLibros = ListaLibros.Count;
                    OnlyAnio = true;
                }

                if (OnlyAnio == false)
                {
                    if ((int)FechaInicial > (int)FechaFinal || (int)FechaFinal == (int)FechaInicial)
                    {
                        ViewBag.error = "El año inicial no puede ser mayor o igual al año final.";
                        return View(ListaLibros.ToList());
                    }
                    else
                    {
                        ListaLibros = libroServ.GetLibros((int)FechaInicial, (int)FechaFinal);
                        ViewBag.numeroLibros = ListaLibros.Count;
                    }
                }

                if (idioma != null)
                {
                    if (idioma.CompareTo("Cualquier idioma") == 0)
                    {
                        ListaLibros = libroServ.GetLibros((int)FechaInicial, (int)FechaFinal);

                        ViewBag.numeroLibros = ListaLibros.Count;

                    }
                    else if (idioma.CompareTo("Solo español") == 0)
                    {
                        ListaLibros = libroServ.GetLibros((int)FechaInicial, (int)FechaFinal);
                        ListaLibros = ListaLibros.Where(x => x.Idioma.CompareTo("Español") == 0).ToList();
                        ViewBag.numeroLibros = ListaLibros.Count;
                    }
                }

                if (var_orden != null)
                {
                    if (var_orden == "Por fecha")
                    {
                        ListaLibros = ListaLibros.OrderBy(x => x.Anio).ToList();
                        ViewBag.numeroLibros = ListaLibros.Count;
                    }
                    else if (var_orden == "Por nombre")
                    {
                        ListaLibros = ListaLibros.OrderBy(x => x.Titulo).ToList();
                        ViewBag.numeroLibros = ListaLibros.Count;
                    }
                }
            }
            else
            {
                ViewBag.palabra = Palabra;
                ListaLibros = libroServ.BuscarDocumentos(Palabra);

                if (FechaInicial == null)
                {
                    FechaInicial = 1980;
                }

                if (FechaFinal == null)
                {
                    FechaFinal = DateTime.Now.Year;

                    ListaLibros = ListaLibros.Where(x => x.Anio >= (int)FechaInicial && x.Anio <= (int)FechaFinal).ToList();
                    ViewBag.anioInicial = FechaInicial;
                    ViewBag.numeroLibros = ListaLibros.Count;
                    OnlyAnio = true;
                }

                if ((int)FechaFinal == 0)
                {
                    FechaFinal = DateTime.Now.Year;

                    ListaLibros = ListaLibros.Where(x => x.Anio >= (int)FechaInicial && x.Anio <= (int)FechaFinal).ToList();
                    ViewBag.anioInicial = FechaInicial;
                    ViewBag.numeroLibros = ListaLibros.Count;
                    OnlyAnio = true;
                }

                if (OnlyAnio == false)
                {
                    if ((int)FechaInicial > (int)FechaFinal || (int)FechaFinal == (int)FechaInicial)
                    {
                        ViewBag.error = "El año inicial no puede ser mayor o igual al año final.";
                        return View(ListaLibros.ToList());
                    }
                    else
                    {
                        ListaLibros = ListaLibros.Where(x => x.Anio >= (int)FechaInicial && x.Anio <= (int)FechaFinal).ToList();
                        ViewBag.numeroLibros = ListaLibros.Count;
                    }
                }

                if (idioma != null)
                {
                    if (idioma.CompareTo("Cualquier idioma") == 0)
                    {
                        ListaLibros = ListaLibros.Where(x => x.Anio >= (int)FechaInicial && x.Anio <= (int)FechaFinal).ToList();

                        ViewBag.numeroLibros = ListaLibros.Count;

                    }
                    else if (idioma.CompareTo("Solo español") == 0)
                    {
                        ListaLibros = ListaLibros.Where(x => (x.Anio >= (int)FechaInicial || x.Anio <= (int)FechaFinal) && x.Idioma.CompareTo("Español")==0).ToList();
                        
                        ViewBag.numeroLibros = ListaLibros.Count;
                    }
                }

            }
            

            return View(ListaLibros.ToList());
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Titulo,Autor,Dia,Mes,Anio,Idioma,PalabrasClave,Descripcion,TipoArchivo")]Libros libro, HttpPostedFileBase imagen_libro,HttpPostedFileBase archivo_libro)
        {
            bool respuesta = false;
            try
            {
                byte[] file = null;
                if (ModelState.IsValid)
                {
                    Stream FileStream = archivo_libro.InputStream;
                    using(MemoryStream ms = new MemoryStream())
                    {
                        FileStream.CopyTo(ms);
                        file = ms.ToArray();
                    }

                    WebImage image = new WebImage(imagen_libro.InputStream);
                    libro.Archivo = file;
                    libro.Imagen_libro = image.GetBytes();
                    respuesta = libroServ.CreateLibros(libro);
                    ModelState.Clear();
                    return RedirectToAction("Index", "Libros");
                    
                }
                else
                {
                    return View();
                }
                
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult Detalle_Libro(int? LibroId)
        {
            if(LibroId == null)
            {
                return RedirectToAction("Index", "Libros");
            }

            Libros lb = libroServ.GetLibroxId((int)LibroId);
            List<Comentario> ListaComentario = new List<Comentario>();
            ListaComentario = comentarioServ.GetComentarios((int)LibroId,1);
            if(ListaComentario.Count() == 0)
            {
                ViewBag.CantidadComentario = "No tiene comentarios.";
            }
            else
            {
                ViewBag.CantidadComentario = "";
            }

            ViewBag.ListaComentario = ListaComentario.ToList();
            return View(lb);
        }

        public ActionResult CreateComentario(int LibroId,string comentario,string usuario)
        {
            Comentario comenta = new Comentario();
            comenta.Comentario_text = comentario;
            comenta.Usuario = usuario;
            comenta.LibroId = LibroId;
            comenta.Tipo = 1;
            comentarioServ.CreateComentario(comenta);

            return RedirectToAction("Detalle_Libro", "Libros", new { LibroId = LibroId });
        }
    }
}