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
        public ActionResult Index(string limpiar, string actualizar)
        {
            Models.MantenedorVehiculosModel modelo = new Models.MantenedorVehiculosModel();
            Entity.Filtro filtro = new Entity.Filtro();
            if (limpiar != null)
            {
                Session["registrosEncontrados"] = null;
                modelo.lista = new List<Entity.Vehiculo>();
            }
            if (Session["registrosEncontrados"] != null)
            {
                modelo.lista = Session["registrosEncontrados"] as List<Entity.Vehiculo>;
            }
            if (actualizar != null)
            {
                filtro.Emp_Id = SessionH.Usuario.EmpId;
                filtro.Res_Id = int.Parse(Session["idRecinto"].ToString());
                List<Entity.Vehiculo> historicosEncontrados = DAL.VehiculoDAL.ObtenerVehiculos(filtro);
                Session["registrosEncontrados"] = historicosEncontrados;
                modelo.lista = Session["registrosEncontrados"] as List<Entity.Vehiculo>;
            }
            return View(modelo);
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
        public JsonResult ObtenerTipoVehiculo()
        {
            
            var lista = DAL.TipoVehiculoDAL.ObtenerTipoVehiculo();

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ObtenerPersonas(Entity.Filtro entity)
        {
            if (entity.Res_Id <= 0)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            entity.Emp_Id = SessionH.Usuario.EmpId;
            var lista = DAL.PersonaDAL.ObtenerPersonas(entity);

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult InsertarVehiculo(Entity.Vehiculo entity)
        {
            try
            {
                entity = DAL.VehiculoDAL.InsertarVehiculo(entity);

                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "exito", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }
        public ActionResult BusquedaFiltro(Entity.Filtro entity)
        {
            Session["idRecinto"] = entity.Res_Id;
            entity.Emp_Id = SessionH.Usuario.EmpId;
            List<Entity.Vehiculo> historicosEncontrados = DAL.VehiculoDAL.ObtenerVehiculos(entity);
            Session["registrosEncontrados"] = historicosEncontrados;

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ObtenerVehiculos(int id)
        {
            Entity.Filtro filtro = new Entity.Filtro();
            filtro.Id = id;
            var lista = DAL.VehiculoDAL.ObtenerVehiculos(filtro);

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista[0], JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult EliminarVehiculo(int id)
        {
            try
            {
                DAL.VehiculoDAL.EliminarVehiculo(id);
                
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "exito", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}