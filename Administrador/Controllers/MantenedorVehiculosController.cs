using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

namespace Administrador.Controllers
{
    public class MantenedorVehiculosController : Controller
    {
        // GET: MantenedorVehiculos
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ObtenerRecintos()
        {
            Entity.Filtro filtro = new Entity.Filtro();
            filtro.Emp_Id = SessionH.Usuario.EmpId;
            var lista = DAL.RecintoDAL.ObtenerRecintos(filtro);

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}