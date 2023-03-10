using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

namespace Administrador.Controllers
{
    public class MantenedorRecintosController : Controller
    {
        // GET: MantenedorRecintos
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ObtenerRegiones()
        {

            var lista = DAL.RegionDAL.ObtenerRegiones();

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ObtenerComunas(Entity.Filtro entity)
        {
            if (entity.Reg_Id <= 0)
            {
                entity.Reg_Id = 13;
            }
            var lista = DAL.ComunaDAL.ObtenerComunas(entity);

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ObtenerTiposPropiedades()
        {

            var lista = DAL.TipoPropiedadDAL.ObtenerTiposPropiedades();

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}