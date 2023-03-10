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
    public class ComunaDAL
    {
        public static List<Entity.Comuna> ObtenerComunas(Entity.Filtro filtro)
        {
            List<Entity.Comuna> lista = new List<Entity.Comuna>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosEdificios");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_COM_COMUNA_LEER");

            db.AddInParameter(dbCommand, "REG_ID", DbType.Int32, filtro.Reg_Id != 0 ? filtro.Reg_Id : (object)null);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int REG_ID = reader.GetOrdinal("REG_ID");
                int NOMBRE = reader.GetOrdinal("NOMBRE");
                int TIDE_ID = reader.GetOrdinal("TIDE_ID");

                while (reader.Read())
                {
                    Entity.Comuna OBJ = new Entity.Comuna();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.Reg_Id = (int)(!reader.IsDBNull(REG_ID) ? reader.GetValue(REG_ID) : 0);
                    OBJ.NombreComuna = (String)(!reader.IsDBNull(NOMBRE) ? reader.GetValue(NOMBRE) : string.Empty);
                    OBJ.Tide_Id = (int)(!reader.IsDBNull(TIDE_ID) ? reader.GetValue(TIDE_ID) : 0);
                    //EndFields

                    lista.Add(OBJ);
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

            return lista;

        }
    }
}
