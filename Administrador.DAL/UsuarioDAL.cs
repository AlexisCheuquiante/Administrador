using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace Administrador.DAL
{
    public class UsuarioDAL
    {
        public static Entity.Usuario InsertarUsuario(Entity.Usuario usuario)
        {
            Database db = DatabaseFactory.CreateDatabase("baseDatosEdificios");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_USR_USUARIO_INS");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, usuario.Id != 0 ? usuario.Id : (object)null);
            db.AddInParameter(dbCommand, "PER_ID", DbType.Int32, usuario.Per_Id != 0 ? usuario.Per_Id : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, usuario.EmpId != 0 ? usuario.EmpId : (object)null);
            db.AddInParameter(dbCommand, "RES_ID", DbType.Int32, usuario.Res_Id != 0 ? usuario.Res_Id : (object)null);
            db.AddInParameter(dbCommand, "ROL_ID", DbType.Int32, usuario.Rol_Id != 0 ? usuario.Rol_Id : (object)null);
            db.AddInParameter(dbCommand, "NOMBRE_COMPLETO", DbType.String, usuario.NombreCompleto != "" ? usuario.NombreCompleto.ToUpper() : (object)null);
            db.AddInParameter(dbCommand, "USUARIO", DbType.String, usuario.UsuarioStr != "" ? usuario.UsuarioStr.ToUpper() : (object)null);
            db.AddInParameter(dbCommand, "PASSWORD", DbType.String, usuario.Password != "" ? usuario.Password.ToUpper() : (object)null);
            db.AddInParameter(dbCommand, "ELIMINADO", DbType.Byte, usuario.Eliminado == true ? 1 : 0);

            usuario.Id = int.Parse(db.ExecuteScalar(dbCommand).ToString());

            return usuario;
        }
        public static List<Entity.Usuario> ObtenerUsuario(Entity.Filtro filtro)
        {
            List<Entity.Usuario> listaUsuarios = new List<Entity.Usuario>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosEdificios");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_USR_USUARIO_LEER");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, filtro.Id != 0 ? filtro.Id : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, filtro.Emp_Id != 0 ? filtro.Emp_Id : (object)null);
            db.AddInParameter(dbCommand, "USUARIO", DbType.String, filtro.Usuario != "" ? filtro.Usuario : (object)null);
            db.AddInParameter(dbCommand, "PASSWORD", DbType.String, filtro.Password != "" ? filtro.Password : (object)null);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int PER_ID = reader.GetOrdinal("PER_ID");
                int EMP_ID = reader.GetOrdinal("EMP_ID");
                int NOMBRE_EMPRESA = reader.GetOrdinal("NOMBRE_EMPRESA");
                int RES_ID = reader.GetOrdinal("RES_ID");
                int NOMBRE_COMPLETO = reader.GetOrdinal("NOMBRE_COMPLETO");
                int USUARIO = reader.GetOrdinal("USUARIO");
                int PASSWORD = reader.GetOrdinal("PASSWORD");
                int ROL_ID = reader.GetOrdinal("ROL_ID");
                int DESCRIPCION_ROL = reader.GetOrdinal("DESCRIPCION_ROL");

                while (reader.Read())
                {
                    Entity.Usuario OBJ = new Entity.Usuario();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.Per_Id = (int)(!reader.IsDBNull(PER_ID) ? reader.GetValue(PER_ID) : 0);
                    OBJ.EmpId = (int)(!reader.IsDBNull(EMP_ID) ? reader.GetValue(EMP_ID) : 0);
                    OBJ.NombreEmpresa = (String)(!reader.IsDBNull(NOMBRE_EMPRESA) ? reader.GetValue(NOMBRE_EMPRESA) : string.Empty);
                    OBJ.Res_Id = (int)(!reader.IsDBNull(RES_ID) ? reader.GetValue(RES_ID) : 0);
                    OBJ.NombreCompleto = (String)(!reader.IsDBNull(NOMBRE_COMPLETO) ? reader.GetValue(NOMBRE_COMPLETO) : string.Empty);
                    OBJ.UsuarioStr = (String)(!reader.IsDBNull(USUARIO) ? reader.GetValue(USUARIO) : string.Empty);
                    OBJ.Password = (String)(!reader.IsDBNull(PASSWORD) ? reader.GetValue(PASSWORD) : string.Empty);
                    OBJ.Rol_Id = (int)(!reader.IsDBNull(ROL_ID) ? reader.GetValue(ROL_ID) : 0);
                    OBJ.Descripcion_Rol = (String)(!reader.IsDBNull(DESCRIPCION_ROL) ? reader.GetValue(DESCRIPCION_ROL) : string.Empty);
                    //EndFields

                    listaUsuarios.Add(OBJ);
                }
            }
            catch (Exception ex)
            {
                //GlobalesDAO.InsertErrores(ex);
                throw ex;
            }
            finally
            {
                reader.Close();
            }

            //if (listaUsuario != null & listaUsuario.Count == 1)
            //{
            //    listaUsuario[0].Roles = DAL.RelacionUsrRolDAL.ObtenerRolUsuario(listaUsuario[0].Id);
            //}

            return listaUsuarios;

        }
    }
}
