using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practica.Models;
using Practica.Models.TableModelCarrera;

namespace Practica.Controllers
{
    public class CarreraController : Controller
    {
        // GET: Carrera
        public ActionResult Index()
        {
            List<CarreraTableModel> lst;
            using (escuelaEntities1 db = new escuelaEntities1())
            {
                lst = (from d in db.carrera
                      select new CarreraTableModel
                      {
                          IdCarrera = d.IdCarrera,
                          NomCarrera = d.NomCarrera,
                          Clave = d.Clave
                      }).ToList();
            }
            return View(lst);
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(CarreraModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    using(escuelaEntities1 db = new escuelaEntities1())
                    {
                        var oCarrera = new carrera();
                        oCarrera.NomCarrera = model.NomCarrera;
                        oCarrera.Clave = model.Clave;

                        db.carrera.Add(oCarrera);
                        db.SaveChanges();
                    }
                    return Redirect("~/Carrera/");
                }
                return View(model);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Editar(int Id)
        {
            CarreraModel model = new CarreraModel();
            using (escuelaEntities1 db = new escuelaEntities1())
            {
                var oCarrera = db.carrera.Find(Id);
                model.NomCarrera = oCarrera.NomCarrera;
                model.Clave = oCarrera.Clave;
                model.IdCarrera = oCarrera.IdCarrera;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(CarreraModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (escuelaEntities1 db = new escuelaEntities1())
                    {
                        var oCarrera = db.carrera.Find(model.IdCarrera);
                        oCarrera.NomCarrera = model.NomCarrera;
                        oCarrera.Clave = model.Clave;

                        db.Entry(oCarrera).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Carrera/");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int Id)
        {
            using (escuelaEntities1 db = new escuelaEntities1())
            {
                var oCarrera = db.carrera.Find(Id);
                db.carrera.Remove(oCarrera);
                db.SaveChanges();
            }
            return Redirect("~/Carrera/");
        }
    }
}