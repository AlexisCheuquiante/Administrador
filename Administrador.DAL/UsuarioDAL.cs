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
                int EMP_ID = reader.GetOrdinal("EMP_ID");
                int NOMBRE_EMPRESA = reader.GetOrdinal("NOMBRE_EMPRESA");
                int RES_ID = reader.GetOrdinal("RES_ID");
                int NOMBRE_COMPLETO = reader.GetOrdinal("NOMBRE_COMPLETO");
                int USUARIO = reader.GetOrdinal("USUARIO");
                int PASSWORD = reader.GetOrdinal("PASSWORD");

                while (reader.Read())
                {
                    Entity.Usuario OBJ = new Entity.Usuario();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.EmpId = (int)(!reader.IsDBNull(EMP_ID) ? reader.GetValue(EMP_ID) : 0);
                    OBJ.NombreEmpresa = (String)(!reader.IsDBNull(NOMBRE_EMPRESA) ? reader.GetValue(NOMBRE_EMPRESA) : string.Empty);
                    OBJ.Res_Id = (int)(!reader.IsDBNull(RES_ID) ? reader.GetValue(RES_ID) : 0);
                    OBJ.NombreCompleto = (String)(!reader.IsDBNull(NOMBRE_COMPLETO) ? reader.GetValue(NOMBRE_COMPLETO) : string.Empty);
                    OBJ.UsuarioStr = (String)(!reader.IsDBNull(USUARIO) ? reader.GetValue(USUARIO) : string.Empty);
                    OBJ.Password = (String)(!reader.IsDBNull(PASSWORD) ? reader.GetValue(PASSWORD) : string.Empty);
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
