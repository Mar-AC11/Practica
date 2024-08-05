using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practica.Models;
using Practica.Models.TableModelAsignatura;

namespace Practica.Controllers
{
    public class AsignaturaController : Controller
    {
        // GET: Asignatura
        public ActionResult Index()
        {
            List<AsignaturaTableModel> lst;
            using (escuelaEntities1 db = new escuelaEntities1())
            {
                lst = (from d in db.asignatura
                       join e in db.carrera on d.Carrera equals e.IdCarrera
                       select new AsignaturaTableModel
                       {
                           IdAsignatura = d.IdAsignatura,
                           NomAsignatura = d.NomAsignatura,
                           Carrera = d.Carrera,
                           NomCarrera = e.NomCarrera
                       }).ToList();
            }
            return View(lst);
        }

        public ActionResult Nuevo()
        {
            List<SelectListItem> lts;
            using (var db = new escuelaEntities1())
            {
                lts = db.carrera.Select(c => new SelectListItem
                {
                    Value = c.IdCarrera.ToString(),
                    Text = c.NomCarrera
                }).ToList();
            }
            ViewBag.Carrera = new SelectList(lts, "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(AsignaturaModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (escuelaEntities1 db = new escuelaEntities1())
                    {
                        var oAsignatura = new asignatura();
                        oAsignatura.NomAsignatura = model.NomAsignatura;
                        oAsignatura.Carrera = model.Carrera;
                        db.asignatura.Add(oAsignatura);
                        db.SaveChanges();
                    }
                    return Redirect("~/Asignatura/");
                }

                List<SelectListItem> lts;
                using (var db = new escuelaEntities1())
                {
                    lts = db.carrera.Select(c => new SelectListItem
                    {
                        Value = c.IdCarrera.ToString(),
                        Text = c.NomCarrera
                    }).ToList();
                }
                ViewBag.Carrera = new SelectList(lts, "Value", "Text");
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View(model);
            }
        }

        public ActionResult Editar(int Id)
        {
            AsignaturaModel model = new AsignaturaModel();
            List<SelectListItem> lts;
            using (escuelaEntities1 db = new escuelaEntities1())
            {
                var oAsignatura = db.asignatura.Find(Id);
                if (oAsignatura != null)
                {
                    model.NomAsignatura = oAsignatura.NomAsignatura;
                    model.Carrera = oAsignatura.Carrera;
                    model.IdAsignatura = oAsignatura.IdAsignatura;
                }
                else
                {
                    ModelState.AddModelError("", "Asignatura no encontrada.");
                    return View(model);
                }
                lts = db.carrera.Select(d => new SelectListItem
                {
                    Value = d.IdCarrera.ToString(),
                    Text = d.NomCarrera
                }).ToList();

            }
            ViewBag.Carrera = new SelectList(lts, "Value", "Text", model.Carrera);
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(AsignaturaModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (escuelaEntities1 db = new escuelaEntities1())
                    {
                        var oAsignatura = db.asignatura.Find(model.IdAsignatura);
                        if (oAsignatura == null)
                        {
                            ModelState.AddModelError("", "Asignatura no encontrada.");
                            return View(model);
                        }

                        oAsignatura.NomAsignatura = model.NomAsignatura;
                        oAsignatura.Carrera = model.Carrera;

                        db.Entry(oAsignatura).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Asignatura/");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int IdAsignatura)
        {
            using (escuelaEntities1 db = new escuelaEntities1())
            {
                var oAsignatura = db.asignatura.Find(IdAsignatura);
                db.asignatura.Remove(oAsignatura);
                db.SaveChanges();
            }
            return Redirect("~/Asignatura/");
        }
    }
}