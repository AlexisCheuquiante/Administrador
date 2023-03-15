using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Administrador.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Administrador.Models.ModelLogin model = new Models.ModelLogin();
            model.Usuario = Request.Cookies["BACK_USER"] != null ? Request.Cookies["BACK_USER"].Value.ToString() : "";
            model.Password = Request.Cookies["BACK_PASS"] != null ? Request.Cookies["BACK_PASS"].Value.ToString() : "";
            return View(model);
        }
        public JsonResult ValidarLogin(Entity.Filtro entity)
        {

            List<Entity.Usuario> usuarios = new List<Entity.Usuario>();
            usuarios = DAL.UsuarioDAL.ObtenerUsuario(entity);

            System.Web.HttpCookie c1 = new System.Web.HttpCookie("BACK_USER");
            c1.Value = entity.Usuario;
            c1.Expires = DateTime.Now.AddYears(10);
            Response.Cookies.Add(c1);


            System.Web.HttpCookie c2 = new System.Web.HttpCookie("BACK_PASS");
            c2.Value = entity.Password;
            c2.Expires = DateTime.Now.AddYears(10);
            Response.Cookies.Add(c2);


            if (usuarios != null && usuarios.Count == 1)
            {
                if (usuarios[0].Rol_Id == 1)
                {
                    SessionH.Usuario = usuarios[0];
                    return new JsonResult() { ContentEncoding = Encoding.Default, Data = "exito", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                if (usuarios[0].Rol_Id == 2)
                {
                    SessionH.Usuario = usuarios[0];
                    return new JsonResult() { ContentEncoding = Encoding.Default, Data = "exito2", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult AccionSalir()
        {
            //Entity.Bitacora bitacora = new Entity.Bitacora();
            //bitacora.UsrId = SessionH.Usuario.Id;
            //bitacora.EmpId = SessionH.Usuario.EmpId;
            //bitacora.BodeId = SessionH.Usuario.BodeId;
            //bitacora.Modulo = "Cierre de sesion";
            //bitacora.Glosa = "Salida del sistema";
            //DAL.BitacoraDAL.InsertarBitacora(bitacora);

            SessionH.Usuario = null;
            SessionH.Logueado = false;
            return RedirectToAction("Index", "Login");
        }
    }
}