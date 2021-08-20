using AppPersonas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppPersonas.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                using (var db = new PersonasContext())
                {
                    var data = from ps in db.persona
                               join p in db.pais on ps.paisNacim equals p.id
                               select new PersonaBU()
                               {
                                   id = ps.id,
                                   nombres = ps.nombres,
                                   apellidos = ps.apellidos,
                                   fechaNacim = ps.fechaNacim,
                                   fechaFallec = ps.fechaFallec,
                                   NombrePais = p.nombre
                               };
                    return View(data.ToList());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(persona p)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                using (var db = new PersonasContext())
                {
                    db.Actualizar_Persona(p.id, p.nombres, p.apellidos, p.fechaNacim, p.fechaFallec, p.paisNacim);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "El registro no ha sido ingresado.");
                return View();
            }
        }

        public ActionResult ListarPaises()
        {
            using (var db = new PersonasContext())
            {
                return PartialView(db.pais.ToList());
            }
        }

        public ActionResult Editar(int id)
        {
            try
            {
                using (var db = new PersonasContext())
                {
                    persona p = db.persona.Find(id);
                    return View(p);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al mostrar los datos.");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(persona p)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                using (var db = new PersonasContext())
                {
                    db.Actualizar_Persona(p.id, p.nombres, p.apellidos, p.fechaNacim, p.fechaFallec, p.paisNacim);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "El registro no ha sido ingresado.");
                return View();
            }
        }

        public ActionResult Eliminar(int id)
        {
            using (var db = new PersonasContext())
            {
                persona p = db.persona.Find(id);
                db.Eliminar_Persona(id);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public static string NombrePais(int id)
        {
            using (var db = new PersonasContext())
            {
                return db.pais.Find(id).nombre;
            }
        }

        public ActionResult OrdenPais()
        {
            try
            {
                using (var db = new PersonasContext())
                {
                    var data = from ps in db.persona
                               join p in db.pais on ps.paisNacim equals p.id
                               orderby p.nombre, ps.fechaNacim
                               select new PersonaBU()
                               {
                                   id = ps.id,
                                   nombres = ps.nombres,
                                   apellidos = ps.apellidos,
                                   fechaNacim = ps.fechaNacim,
                                   fechaFallec = ps.fechaFallec,
                                   NombrePais = p.nombre
                               };
                    return View(data.ToList());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult OrdenEdad()
        {
            try
            {
                using (var db = new PersonasContext())
                {
                    var data = from ps in db.persona
                               join p in db.pais on ps.paisNacim equals p.id
                               orderby ps.fechaNacim
                               select new PersonaBU()
                               {
                                   id = ps.id,
                                   nombres = ps.nombres,
                                   apellidos = ps.apellidos,
                                   fechaNacim = ps.fechaNacim,
                                   fechaFallec = ps.fechaFallec,
                                   NombrePais = p.nombre
                               };
                    return View(data.ToList());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult Informe()
        {
            try
            {
                using (var db = new PersonasContext())
                {
                    var data = from t in (((from ps in db.persona
                               group ps by new
                               {
                                   column1 = ps.fechaNacim.Year
                               } into g
                               select new
                               {
                                   agno = g.Key.column1,
                                   nacidos = g.Count(p => p.fechaNacim != null),
                                   muertos = 0
                               }).Union(from ps in db.persona
                                        group ps by new
                                        {
                                            column1 = ps.fechaFallec.Value.Year
                                        } into g
                                        select new
                                        {
                                            agno = g.Key.column1,
                                            nacidos = 0,
                                            muertos = g.Count(p => p.fechaFallec != null)
                                        })))
                                        group t by new
                                        {
                                            t.agno
                                        } into g
                               select new
                               {
                                   g.Key.agno,
                                   nacidos = g.Sum(p => p.nacidos),
                                   muertos = g.Sum(p => p.muertos)
                               };
                    return View(data.ToList());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult OrdenFallecidos()
        {
            try
            {
                using (var db = new PersonasContext())
                {
                    var data = from ps in db.persona
                               join p in db.pais on ps.paisNacim equals p.id
                               where ps.fechaFallec != null
                               orderby ps.fechaFallec
                               select new PersonaBU()
                               {
                                   id = ps.id,
                                   nombres = ps.nombres,
                                   apellidos = ps.apellidos,
                                   fechaNacim = ps.fechaNacim,
                                   fechaFallec = ps.fechaFallec,
                                   NombrePais = p.nombre
                               };
                    return View(data.ToList());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}