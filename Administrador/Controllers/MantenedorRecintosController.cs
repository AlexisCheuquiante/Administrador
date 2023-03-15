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
        public ActionResult Index(string limpiar)
        {
            Models.MantenedorRecintosModel modelo = new Models.MantenedorRecintosModel();
            Entity.Filtro filtro = new Entity.Filtro();
            filtro.Emp_Id = SessionH.Usuario.EmpId;
            modelo.listaRecintos = DAL.RecintoDAL.ObtenerRecintos(filtro);
            return View(modelo);
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
        public JsonResult InsertarRecinto(Entity.Recinto entity)
        {
            try
            {
                entity.Emp_Id = SessionH.Usuario.EmpId;
                entity = DAL.RecintoDAL.InsertarRecinto(entity);

                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "exito", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }
        public JsonResult ObtenerRecintos(int id)
        {
            Entity.Filtro filtro = new Entity.Filtro();
            filtro.Id = id;
            var lista = DAL.RecintoDAL.ObtenerRecintos(filtro);

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista[0], JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult EliminarRecinto(int id)
        {
            try
            {
                DAL.RecintoDAL.EliminarRecinto(id);

                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "exito", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}