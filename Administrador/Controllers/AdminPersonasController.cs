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
        public ActionResult Index(string limpiar, string actualizar)
        {
            Models.AdminPersonasModel modelo = new Models.AdminPersonasModel();
            Entity.Filtro filtro = new Entity.Filtro();
            if (SessionH.Usuario.Rol_Id == 1 && actualizar != null)
            {
                modelo.listaPersonas = Session["registrosEncontrados"] as List<Entity.Persona>;
            }
            if (SessionH.Usuario.Rol_Id == 1 && limpiar != null)
            {
                Session["registrosEncontrados"] = null;
                filtro.Emp_Id = SessionH.Usuario.EmpId;
                filtro.Res_Id = SessionH.Usuario.Res_Id;
                modelo.listaPersonas = DAL.PersonaDAL.ObtenerPersonas(filtro);
            }
            if (SessionH.Usuario.Rol_Id == 2 && limpiar != null)
            {
                Session["registrosEncontrados"] = null;
                filtro.Emp_Id = SessionH.Usuario.EmpId;
                filtro.Res_Id = SessionH.Usuario.Res_Id;
                filtro.Per_Id = SessionH.Usuario.Per_Id;
                modelo.listaPersonas = DAL.PersonaDAL.ObtenerPersonas(filtro);
            }
            return View(modelo);
        }
        public JsonResult FormateaRut(Entity.Filtro entity)
        {

            var rutFormateado = Utiles.FormateaRut(entity.Rut);

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = rutFormateado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
                var idPersona = entity.Id;
                var perId = 0;

                entity.Emp_Id = SessionH.Usuario.EmpId;
                entity.Res_Id = SessionH.Usuario.Res_Id;
                if (SessionH.Usuario.Rol_Id == 2)
                {
                    entity.Per_Id = SessionH.Usuario.Per_Id;
                    perId = entity.Per_Id;
                }
                entity = DAL.PersonaDAL.InsertarPersona(entity);
                if (SessionH.Usuario.Rol_Id == 1)
                {
                    int id = entity.Id;
                    DAL.PersonaDAL.UpdatePer_Id(id);
                    perId = id;
                }
                if (idPersona == 0)
                {
                    if (entity.Tiin_Id == 1 || entity.Tiin_Id == 2)
                    {
                        Entity.Usuario usuario = new Entity.Usuario();
                        usuario.EmpId = SessionH.Usuario.EmpId;
                        usuario.Res_Id = SessionH.Usuario.Res_Id;
                        usuario.Per_Id = perId;
                        usuario.Rol_Id = 2;
                        usuario.NombreCompleto = entity.Nombre;
                        usuario.UsuarioStr = entity.Rut;
                        usuario.Password = "123456";
                        var usuarioCreado = DAL.UsuarioDAL.InsertarUsuario(usuario);
                    }
                }

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
            List<Entity.Persona> historicosEncontrados = DAL.PersonaDAL.ObtenerPersonas(entity);
            Session["registrosEncontrados"] = historicosEncontrados;

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ObtenerPersonas(int id)
        {
            Entity.Filtro filtro = new Entity.Filtro();
            filtro.Id = id;
            var lista = DAL.PersonaDAL.ObtenerPersonas(filtro);

            if (lista == null || lista.Count == 0)
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { ContentEncoding = Encoding.Default, Data = lista[0], JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult EliminarPersona(int id)
        {
            try
            {
                DAL.PersonaDAL.EliminarPersona(id);

                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "exito", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { ContentEncoding = Encoding.Default, Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}