using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practica.Models;
using Practica.Models.TableModelEstudiante;

namespace Practica.Controllers
{
    public class EstudianteController : Controller
    {
        // GET: Estudiante
        public ActionResult Index()
        {
            List<EstudianteTableModel> lst;
            using (escuelaEntities1 db = new escuelaEntities1())
            {
                lst = (from d in db.estudiante
                       join e in db.carrera on d.Carreraa equals e.IdCarrera
                       select new EstudianteTableModel
                       {
                           IdEstudiante = d.IdEstudiante,
                           NoControl = d.NoControl,
                           Nombre = d.Nombre,
                           ApPaterno = d.ApPaterno,
                           ApMaterno = d.ApMaterno,
                           Carreraa = d.Carreraa,
                           NomCarrera = e.NomCarrera
                       }).ToList();
            }
            return View(lst);
        }

        public ActionResult Nuevo()
        {
            List<SelectListItem> lts;
            using(var db = new escuelaEntities1())
            {
                lts = db.carrera.Select(c => new SelectListItem
                {
                    Value = c.IdCarrera.ToString(),
                    Text = c.NomCarrera
                }).ToList();
            }
            ViewBag.Carreraa = new SelectList(lts, "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(EstudianteModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (escuelaEntities1 db = new escuelaEntities1())
                    {
                        var oEstudiante = new estudiante();
                        oEstudiante.NoControl = model.NoControl;
                        oEstudiante.Nombre = model.Nombre;
                        oEstudiante.ApPaterno = model.ApPaterno;
                        oEstudiante.ApMaterno = model.ApMaterno;
                        oEstudiante.Carreraa = model.Carreraa;
                        db.estudiante.Add(oEstudiante);
                        db.SaveChanges();
                    }
                    return Redirect("~/Estudiante/");
                }
                List<SelectListItem> lts;
                using(var db = new escuelaEntities1())
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
            EstudianteModel model = new EstudianteModel();
            List<SelectListItem> lts;
            using (escuelaEntities1 db = new escuelaEntities1())
            {
                var oEstudiante = db.estudiante.Find(Id);
                if (oEstudiante != null)
                {
                    model.NoControl = oEstudiante.NoControl;
                    model.Nombre = oEstudiante.Nombre;
                    model.ApPaterno = oEstudiante.ApPaterno;
                    model.ApMaterno = oEstudiante.ApMaterno;
                    model.Carreraa = oEstudiante.Carreraa;
                    model.IdEstudiante = oEstudiante.IdEstudiante;
                }
                else
                {
                    ModelState.AddModelError("", "Estudiante no encontrado.");
                    return View(model);
                }
                lts = db.carrera.Select(d => new SelectListItem
                {
                    Value = d.IdCarrera.ToString(),
                    Text = d.NomCarrera
                }).ToList();
                
            }
            ViewBag.Carreraa = new SelectList(lts, "Value", "Text", model.Carreraa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(EstudianteModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (escuelaEntities1 db = new escuelaEntities1())
                    {
                        var oEstudiante = db.estudiante.Find(model.IdEstudiante);
                        if (oEstudiante == null)
                        {
                            ModelState.AddModelError("", "Estudiante no encontrado.");
                            return View(model);
                        }

                        oEstudiante.NoControl = model.NoControl;
                        oEstudiante.Nombre = model.Nombre;
                        oEstudiante.ApPaterno = model.ApPaterno;
                        oEstudiante.ApMaterno = model.ApMaterno;
                        oEstudiante.Carreraa = model.Carreraa;

                        db.Entry(oEstudiante).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Estudiante/");
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
        public ActionResult Eliminar(int IdEstudiante)
        {
            using (escuelaEntities1 db = new escuelaEntities1())
            {
                var oEstudiante = db.estudiante.Find(IdEstudiante);
                db.estudiante.Remove(oEstudiante);
                db.SaveChanges();
            }
            return Redirect("~/Estudiante/");
        }
    }
}