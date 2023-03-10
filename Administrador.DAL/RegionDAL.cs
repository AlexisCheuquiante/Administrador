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
    public class RegionDAL
    {
        public static List<Entity.Region> ObtenerRegiones()
        {
            List<Entity.Region> lista = new List<Entity.Region>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosEdificios");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_REG_REGION_LEER");

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int NOMBRE = reader.GetOrdinal("NOMBRE");
                int TIDE_ID = reader.GetOrdinal("TIDE_ID");
                int DEFECTO = reader.GetOrdinal("DEF");

                while (reader.Read())
                {
                    Entity.Region OBJ = new Entity.Region();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.NombreRegion = (String)(!reader.IsDBNull(NOMBRE) ? reader.GetValue(NOMBRE) : string.Empty);
                    OBJ.Tide_Id = (int)(!reader.IsDBNull(TIDE_ID) ? reader.GetValue(TIDE_ID) : 0);
                    OBJ.Defecto = (bool)(!reader.IsDBNull(DEFECTO) ? reader.GetValue(DEFECTO) : false);
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
