using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WalterChavarry;
using WalterChavarry.Models;
using WalterChavarry.Servicios;

namespace WalterChavarry.Controllers
{
    public class SesionController : Controller
    {
        private AdoCollect db = new AdoCollect();

        IComentarioService comentarioServ;

        public SesionController()
        {
            comentarioServ = new ComentarioService();
        }
        // GET: Sesion
        public ActionResult Index()
        {
            return View(db.Sesiones.ToList());
        }

        [HttpPost]
        public ActionResult Index(int? FechaInicial, int? FechaFinal, string var_orden, string Palabra)
        {
            List<Sesion> listaSesiones = new List<Sesion>();
            bool OnlyAnio = false;
            if (Palabra == "")
            {
                ViewBag.palabra = "";
                if (FechaInicial == null)
                {
                    FechaInicial = 1980;
                }

                if (FechaFinal == null)
                {
                    FechaFinal = DateTime.Now.Year;

                    listaSesiones = db.Sesiones.Where(x => x.Fecha.Year >= (int)FechaInicial && x.Fecha.Year <= (int)FechaFinal).ToList();
                    ViewBag.anioInicial = FechaInicial;
                    ViewBag.numeroLibros = listaSesiones.Count;
                    OnlyAnio = true;
                }

                if ((int)FechaFinal == 0)
                {
                    FechaFinal = DateTime.Now.Year;

                    listaSesiones = db.Sesiones.Where(x => x.Fecha.Year >= (int)FechaInicial && x.Fecha.Year <= (int)FechaFinal).ToList();
                    ViewBag.anioInicial = FechaInicial;
                    ViewBag.numeroLibros = listaSesiones.Count;
                    OnlyAnio = true;
                }

                if (OnlyAnio == false)
                {
                    if ((int)FechaInicial > (int)FechaFinal || (int)FechaFinal == (int)FechaInicial)
                    {
                        ViewBag.error = "El año inicial no puede ser mayor o igual al año final.";
                        return View(listaSesiones.ToList());
                    }
                    else
                    {
                        listaSesiones = db.Sesiones.Where(x => x.Fecha.Year >= (int)FechaInicial && x.Fecha.Year <= (int)FechaFinal).ToList();
                        ViewBag.numeroLibros = listaSesiones.Count;
                    }
                }
                
                if (var_orden != null)
                {
                    if (var_orden == "Por fecha")
                    {
                        listaSesiones = db.Sesiones.OrderBy(x => x.Fecha).ToList();
                        ViewBag.numeroLibros = listaSesiones.Count;
                    }
                    else if (var_orden == "Por nombre")
                    {
                        listaSesiones = db.Sesiones.OrderBy(x => x.NombreSesion).ToList();
                        ViewBag.numeroLibros = listaSesiones.Count;
                    }
                }
            }
            else
            {
                ViewBag.palabra = Palabra;
                listaSesiones = db.Sesiones.Where(x=>x.NombreSesion.ToLower().Contains(Palabra.ToLower()) || x.Descripcion.ToLower().Contains(Palabra.ToLower()) || x.Palabras_Clave.ToLower().Contains(Palabra.ToLower())).ToList();

                if (FechaInicial == null)
                {
                    FechaInicial = 1980;
                }

                if (FechaFinal == null)
                {
                    FechaFinal = DateTime.Now.Year;

                    listaSesiones = listaSesiones.Where(x => x.Fecha.Year >= (int)FechaInicial && x.Fecha.Year <= (int)FechaFinal).ToList();
                    ViewBag.anioInicial = FechaInicial;
                    ViewBag.numeroLibros = listaSesiones.Count;
                    OnlyAnio = true;
                }

                if ((int)FechaFinal == 0)
                {
                    FechaFinal = DateTime.Now.Year;

                    listaSesiones = listaSesiones.Where(x => x.Fecha.Year >= (int)FechaInicial && x.Fecha.Year <= (int)FechaFinal).ToList();
                    ViewBag.anioInicial = FechaInicial;
                    ViewBag.numeroLibros = listaSesiones.Count;
                    OnlyAnio = true;
                }

                if (OnlyAnio == false)
                {
                    if ((int)FechaInicial > (int)FechaFinal || (int)FechaFinal == (int)FechaInicial)
                    {
                        ViewBag.error = "El año inicial no puede ser mayor o igual al año final.";
                        return View(listaSesiones.ToList());
                    }
                    else
                    {
                        listaSesiones = listaSesiones.Where(x => x.Fecha.Year >= (int)FechaInicial && x.Fecha.Year <= (int)FechaFinal).ToList();
                        ViewBag.numeroLibros = listaSesiones.Count;
                    }
                }
                
            }
            
            return View(listaSesiones.ToList());
        }

        // GET: Sesion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sesion sesion = db.Sesiones.Find(id);
            if (sesion == null)
            {
                return HttpNotFound();
            }
            return View(sesion);
        }

        // GET: Sesion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sesion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NombreSesion,Descripcion,Palabras_Clave")] Sesion sesion)
        {
            Random rn = new Random();
            int numeroRandom = rn.Next(1, 3);
            string url = "";
            switch (numeroRandom)
            {
                case 1:
                    url = "https://images.vexels.com/media/users/3/152133/raw/b5d509f8fa813fb3136e2da1f36df154-ilustracion-de-los-iconos-de-la-escuela-de-tarea.jpg";
                    break;
                case 2:
                    url = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcTzttkiX1bUJQ6p8tAkA87_J3utvlBXxq_A9NsJxf6068FISV_C&usqp=CAU";
                    break;
                case 3:
                    url = "https://png.pngtree.com/thumb_back/fw800/back_our/20190619/ourmid/pngtree-world-book-day-cartoon-flat-book-in-the-world-background-material-image_139871.jpg";
                    break;
            }
            if (ModelState.IsValid)
            {
                sesion.UrlImagen = url;
                sesion.Fecha = DateTime.Now;
                db.Sesiones.Add(sesion);
                db.SaveChanges();
                Sesion nuevoSesion = db.Sesiones.Where(x => x.NombreSesion.CompareTo(sesion.NombreSesion) == 0).FirstOrDefault();
                return RedirectToAction("CrearTarea","Sesion",new { SesionId = nuevoSesion.SesionId });
            }

            return View(sesion);
        }

        [HttpGet]
        public ActionResult CrearTarea(int? SesionId)
        {
            if(SesionId == null)
            {
                return RedirectToAction("Create", "Sesion");
            }
            ViewBag.listaTareas = db.Tareas.Where(x => x.SesionId == SesionId).ToList();
            ViewBag.sesion = db.Sesiones.Where(x => x.SesionId == SesionId).ToList().FirstOrDefault();
            
            return View();
        }

        [HttpPost]
        public ActionResult CrearTarea([Bind(Include = "Titulo_Tarea,Descripcion,Instrucciones,SesionId")] Tareas tarea,int? SesionId)
        {
            Random rn = new Random();
            int numeroRandom = rn.Next(1, 3);
            string url = "";
            switch (numeroRandom)
            {
                case 1:
                    url = "https://image.freepik.com/vector-gratis/personaje-dibujos-animados-chica-hace-tarea_61103-14.jpg";
                    break;
                case 2:
                    url = "data:image/jpeg;base64,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";
                    break;
                case 3:
                    url = "https://st3.depositphotos.com/5383684/12819/v/450/depositphotos_128190584-stock-illustration-cute-boy-working-on-homework.jpg";
                    break;
            }
            if (SesionId == null)
            {
                return RedirectToAction("Create", "Sesion");
            }
            ViewBag.listaTareas = db.Tareas.Where(x => x.SesionId == SesionId).ToList();
            ViewBag.sesion = db.Sesiones.Where(x => x.SesionId == SesionId).ToList().FirstOrDefault();
            tarea.UrlImagen = url;
            db.Tareas.Add(tarea);
            db.SaveChanges();
            return RedirectToAction("CrearTarea", "Sesion", new { SesionId = SesionId });
        }

        // GET: Sesion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sesion sesion = db.Sesiones.Find(id);
            if (sesion == null)
            {
                return HttpNotFound();
            }
            return View(sesion);
        }

        // POST: Sesion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "NombreSesion,Descripcion,Palabras_Clave")] Sesion sesion,int SesionId)
        {
            if (ModelState.IsValid)
            {
                Sesion sesionNuevo = db.Sesiones.Where(x => x.SesionId == SesionId).FirstOrDefault();
                sesionNuevo.NombreSesion = sesion.NombreSesion;
                sesionNuevo.Descripcion = sesion.Descripcion;
                sesionNuevo.Palabras_Clave = sesion.Palabras_Clave;
                sesionNuevo.Fecha = DateTime.Now;
                db.Entry(sesionNuevo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CrearTarea","Sesion",new { SesionId = SesionId });
            }
            return View(sesion);
        }

        // GET: Sesion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sesion sesion = db.Sesiones.Find(id);
            if (sesion == null)
            {
                return HttpNotFound();
            }
            return View(sesion);
        }

        // POST: Sesion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sesion sesion = db.Sesiones.Find(id);
            db.Sesiones.Remove(sesion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult DeleteTareaConfirmed(int TareaId,int SesionId)
        {
            Tareas tarea = db.Tareas.Find(TareaId);
            List<Archivo> archivoLista = db.Archivos.Where(x => x.TareaId == TareaId).ToList();
            foreach (var archivo in archivoLista)
            {
                db.Archivos.Remove(archivo);
                db.SaveChanges();
            }
            db.Tareas.Remove(tarea);
            db.SaveChanges();
            return RedirectToAction("CrearTarea","Sesion",new { SesionId = SesionId });
        }


        [HttpPost]
        public ActionResult CreateArchivo([Bind(Include = "NombreArchivo,Tipo")] Archivo archivo,int SesionId, HttpPostedFileBase ArchivoFile)
        {
            byte[] file = null;
            if(ArchivoFile != null)
            {
                Stream FileStream = ArchivoFile.InputStream;
                using (MemoryStream ms = new MemoryStream())
                {
                    FileStream.CopyTo(ms);
                    file = ms.ToArray();
                }
            }

            archivo.ArchivoFile = file;

            int TareaID = db.Tareas.Max(x => x.TareasId);
            archivo.TareaId = TareaID;

            db.Archivos.Add(archivo);
            db.SaveChanges();
            return RedirectToAction("CrearTarea", "Sesion", new { SesionId = SesionId });
        }

        public FileContentResult GetDocumento(int archivoId)
        {
            Archivo lb = db.Archivos.Where(x => x.ArchivoId == archivoId).FirstOrDefault();
            if (lb != null)
            {
                string type = string.Empty;
                switch (lb.Tipo)
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

                return File(lb.ArchivoFile, type);
            }
            else
            {
                return null;
            }
        }

        public FileContentResult GetDocumentoxComentario(int comentarioId)
        {
            ComentarioSesiones lb = db.ComentarioSesiones.Where(x => x.ComentarioSesionesId == comentarioId).FirstOrDefault();
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

                return File(lb.ArchivoComentario, type);
            }
            else
            {
                return null;
            }
        }

        public ActionResult DetalleSesion(int? SesionId)
        {
            if (SesionId == null)
            {
                return RedirectToAction("Index", "Sesion");
            }

            Sesion lb = db.Sesiones.Where(x => x.SesionId == SesionId).FirstOrDefault();
            List<ComentarioSesiones> ListaComentario = new List<ComentarioSesiones>();
            ListaComentario = db.ComentarioSesiones.Where(x => x.ObjetoId == SesionId && x.Tipo == 2).ToList();
            if (ListaComentario.Count() == 0)
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

        public ActionResult CreateComentario(int SesionId, string comentario, string usuario,int Tipo,HttpPostedFileBase ArchivoComentario, string NombreArchivo, string TipoArchivo)
        {
            ComentarioSesiones comenta = new ComentarioSesiones();
            byte[] file = null;
            if (ArchivoComentario != null)
            {
                Stream FileStream = ArchivoComentario.InputStream;
                using (MemoryStream ms = new MemoryStream())
                {
                    FileStream.CopyTo(ms);
                    file = ms.ToArray();
                }
               
            }

            comenta.ArchivoComentario = file;
            comenta.NombreArchivo = NombreArchivo;
            comenta.TipoArchivo = TipoArchivo;
            comenta.Comentario_text = comentario;
            comenta.Fecha = DateTime.Now;
            comenta.Usuario = usuario;
            comenta.ObjetoId = SesionId;
            comenta.Tipo = Tipo;
            db.ComentarioSesiones.Add(comenta);
            db.SaveChanges();
            if(Tipo == 2)
            {
                return RedirectToAction("DetalleSesion", "Sesion", new { SesionId = SesionId });
            }
            else
            {
                return RedirectToAction("DetalleTarea", "Sesion", new { TareaId = SesionId });
            }
           
        }

        public ActionResult DetalleTarea(int? TareaId)
        {
            if (TareaId == null)
            {
                return RedirectToAction("Index", "Sesion");
            }

            Tareas lb = db.Tareas.Where(x => x.TareasId == TareaId).FirstOrDefault();
            List<ComentarioSesiones> ListaComentario = new List<ComentarioSesiones>();
            ListaComentario = db.ComentarioSesiones.Where(x => x.ObjetoId == TareaId && x.Tipo == 3).ToList();
            if (ListaComentario.Count() == 0)
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

        public ActionResult DeleteSesion(int SesionId)
        {
            List<Tareas> listaTareas = db.Tareas.Where(x => x.SesionId == SesionId).ToList();
            foreach(Tareas tarea in listaTareas)
            {
                db.Archivos.RemoveRange(db.Archivos.Where(x => x.TareaId == tarea.TareasId).ToList());
                db.ComentarioSesiones.RemoveRange(db.ComentarioSesiones.Where(x => x.ObjetoId == tarea.TareasId && x.Tipo == 3).ToList());
                db.SaveChanges();
            }
            db.Tareas.RemoveRange(listaTareas.ToList());
            db.SaveChanges();
            db.ComentarioSesiones.RemoveRange(db.ComentarioSesiones.Where(x => x.ObjetoId == SesionId && x.Tipo == 2).ToList());
            db.SaveChanges();
            db.Sesiones.Remove(db.Sesiones.Where(x => x.SesionId == SesionId).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("Index", "Sesion");
        }
    }
}
