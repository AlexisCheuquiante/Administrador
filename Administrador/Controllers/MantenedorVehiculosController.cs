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
        public ActionResult Index(string limpiar)
        {
            Models.MantenedorVehiculosModel modelo = new Models.MantenedorVehiculosModel();
            Entity.Filtro filtro = new Entity.Filtro();
            if (Session["registrosEncontrados"] != null)
            {
                modelo.lista = Session["registrosEncontrados"] as List<Entity.Vehiculo>;
            }
            if (SessionH.Usuario.Rol_Id == 1 && limpiar != null)
            {
                Session["registrosEncontrados"] = null;
                filtro.Emp_Id = SessionH.Usuario.EmpId;
                filtro.Res_Id = SessionH.Usuario.Res_Id;
                modelo.lista = DAL.VehiculoDAL.ObtenerVehiculos(filtro);
            }
            if (SessionH.Usuario.Rol_Id == 2 && limpiar != null)
            {
                Session["registrosEncontrados"] = null;
                filtro.Emp_Id = SessionH.Usuario.EmpId;
                filtro.Res_Id = SessionH.Usuario.Res_Id;
                filtro.Per_Id = SessionH.Usuario.Per_Id;
                modelo.lista = DAL.VehiculoDAL.ObtenerVehiculos(filtro);
            }
            return View(modelo);
        }
        public JsonResult ObtenerTipoVehiculo()
        {
            
            var lista = DAL.TipoVehiculoDAL.ObtenerTipoVehiculo();

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ObtenerJefeHogar()
        {
            Entity.Filtro filtro = new Entity.Filtro();
            filtro.Emp_Id = SessionH.Usuario.EmpId;
            filtro.Res_Id = SessionH.Usuario.Res_Id;
            var lista = DAL.PersonaDAL.ObtenerJefeHogar(filtro);

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ObtenerPersonas(Entity.Filtro entity)
        {
            entity.Emp_Id = SessionH.Usuario.EmpId;
            entity.Res_Id = SessionH.Usuario.Res_Id;
            if (SessionH.Usuario.Rol_Id == 2)
            {
                entity.Per_Id = SessionH.Usuario.Per_Id;
            }
            var lista = DAL.PersonaDAL.ObtenerPersonas(entity);

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult InsertarVehiculo(Entity.Vehiculo entity)
        {
            try
            {
                entity.Res_Id = SessionH.Usuario.Res_Id;
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
            Session["idJefeHogar"] = entity.Per_Id;
            entity.Emp_Id = SessionH.Usuario.EmpId;
            entity.Res_Id = SessionH.Usuario.Res_Id;
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