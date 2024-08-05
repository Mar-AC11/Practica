using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Practica.Models;
using Practica.Models.Login;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Web.Services.Description;

namespace Practica.Controllers
{
    public class AccessController : Controller
    {
        string cadena = "Data Source=localhost; Initial Catalog=escuela; Integrated Security=true;";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Enter(string user, string password)
        {
            try
            {
                using (escuelaEntities1 db = new escuelaEntities1())
                {
                    string hashedPassword = ConvertirSha256(password);

                    // Puntos de depuración
                    System.Diagnostics.Debug.WriteLine("Usuario: " + user);
                    System.Diagnostics.Debug.WriteLine("Contraseña: " + password);
                    System.Diagnostics.Debug.WriteLine("Contraseña Encriptada: " + hashedPassword);

                    var lst = from d in db.usuario
                              where d.NomUsuario == user && d.Contrasena == hashedPassword
                              select d;
                    if (lst.Count() > 0)
                    {
                        usuario oUser = lst.First();
                        Session["User"] = oUser;
                        return Content("1");
                    }
                    else
                    {
                        return Content("Usuario invalido :(");
                    }
                }

            }
            catch (Exception ex)
            {
                return Content("Ocurrio un error :(" + ex.Message);
            }
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario oUsuario)
        {
            bool registrado;
            string mensaje;

            if (oUsuario.Contrasena == oUsuario.ConfirmarContra)
            {
                oUsuario.Contrasena = ConvertirSha256(oUsuario.Contrasena);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("registrarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Asegúrate de que los nombres de los parámetros coincidan con los esperados por el procedimiento almacenado
                cmd.Parameters.AddWithValue("@NomUsuario", oUsuario.NomUsuario);
                cmd.Parameters.AddWithValue("@Contrasena", oUsuario.Contrasena);
                cmd.Parameters.Add("@Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                cn.Open();
                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["@Registrado"].Value);
                mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Index", "Access");
            }
            else
            {
                return View();
            }
        }



        private string ConvertirSha256(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}