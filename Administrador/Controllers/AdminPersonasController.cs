using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

namespace Administrador.Controllers
{
    public class AdminPersonasController : Controller
    {
        // GET: AdminPersonas
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ObtenerTipoInquilino()
        {
            var lista = DAL.TipoInquilinoDAL.ObtenerTipoInquilino();

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult InsertarPersona(Entity.Persona entity)
        {
            try
            {
                entity.Emp_Id = SessionH.Usuario.EmpId;
                entity.Res_Id = SessionH.Usuario.Res_Id;
                entity = DAL.PersonaDAL.InsertarPersona(entity);

                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "exito", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }
    }
}