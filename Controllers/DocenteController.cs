using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practica.Models;
using Practica.Models.TableModelDocente;

namespace Practica.Controllers
{
    public class DocenteController : Controller
    {
        // GET: Docente
        public ActionResult Index()
        {
            List<DocenteTableModel> lst;
            using (escuelaEntities1 db = new escuelaEntities1())
            {
                lst = (from d in db.docente
                       join e in db.asignatura on d.Asignatura equals e.IdAsignatura
                       select new DocenteTableModel
                       {
                           IdDocente = d.IdDocente,
                           Cedula = d.Cedula,
                           Nombre = d.Nombre,
                           ApPaterno = d.ApPaterno,
                           ApMaterno = d.ApMaterno,
                           Asignatura = d.Asignatura,
                           NomAsignatura = e.NomAsignatura
                       }).ToList();
            }
            return View(lst); ;
        }
        public ActionResult Nuevo()
        {
            List<SelectListItem> lts;
            using (var db = new escuelaEntities1())
            {
                lts = db.asignatura.Select(c => new SelectListItem
                {
                    Value = c.IdAsignatura.ToString(),
                    Text = c.NomAsignatura
                }).ToList();
            }
            ViewBag.Asignatura = new SelectList(lts, "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(DocenteModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (escuelaEntities1 db = new escuelaEntities1())
                    {
                        var oDocente = new docente();
                        oDocente.Cedula = model.Cedula;
                        oDocente.Nombre = model.Nombre;
                        oDocente.ApPaterno = model.ApPaterno;
                        oDocente.ApMaterno = model.ApMaterno;
                        oDocente.Asignatura = model.Asignatura;
                        db.docente.Add(oDocente);
                        db.SaveChanges();
                    }
                    return Redirect("~/Docente/");
                }
                List<SelectListItem> lts;
                using (var db = new escuelaEntities1())
                {
                    lts = db.asignatura.Select(c => new SelectListItem
                    {
                        Value = c.IdAsignatura.ToString(),
                        Text = c.NomAsignatura
                    }).ToList();
                }
                ViewBag.Asignatura = new SelectList(lts, "Value", "Text");
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
            DocenteModel model = new DocenteModel();
            List<SelectListItem> lts;
            using (escuelaEntities1 db = new escuelaEntities1())
            {
                var oDocente = db.docente.Find(Id);
                if (oDocente != null)
                {
                    model.Cedula = oDocente.Cedula;
                    model.Nombre = oDocente.Nombre;
                    model.ApPaterno = oDocente.ApPaterno;
                    model.ApMaterno = oDocente.ApMaterno;
                    model.Asignatura = oDocente.Asignatura;
                    model.IdDocente = oDocente.IdDocente;
                }
                else
                {
                    ModelState.AddModelError("", "Docente no encontrado.");
                    return View(model);
                }
                lts = db.asignatura.Select(d => new SelectListItem
                {
                    Value = d.IdAsignatura.ToString(),
                    Text = d.NomAsignatura
                }).ToList();

            }
            ViewBag.Asignatura = new SelectList(lts, "Value", "Text", model.Asignatura);
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(DocenteModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (escuelaEntities1 db = new escuelaEntities1())
                    {
                        var oDocente = db.docente.Find(model.IdDocente);
                        if (oDocente == null)
                        {
                            ModelState.AddModelError("", "Docente no encontrado.");
                            return View(model);
                        }

                        oDocente.Cedula = model.Cedula;
                        oDocente.Nombre = model.Nombre;
                        oDocente.ApPaterno = model.ApPaterno;
                        oDocente.ApMaterno = model.ApMaterno;
                        oDocente.Asignatura = model.Asignatura;

                        db.Entry(oDocente).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Docente/");
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
        public ActionResult Eliminar(int IdDocente)
        {
            using (escuelaEntities1 db = new escuelaEntities1())
            {
                var oDocente = db.docente.Find(IdDocente);
                db.docente.Remove(oDocente);
                db.SaveChanges();
            }
            return Redirect("~/Docente/");
        }
    }
}