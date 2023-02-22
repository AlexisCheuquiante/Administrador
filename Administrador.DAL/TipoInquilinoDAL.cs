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
    public class TipoInquilinoDAL
    {
        public static List<Entity.TipoInquilino> ObtenerTipoInquilino()
        {
            List<Entity.TipoInquilino> lista = new List<Entity.TipoInquilino>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosEdificios");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_TIIN_TIPO_INQUILINO_LEER");

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int DESCRIPCION = reader.GetOrdinal("DESCRIPCION");

                while (reader.Read())
                {
                    Entity.TipoInquilino OBJ = new Entity.TipoInquilino();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.Descripcion = (String)(!reader.IsDBNull(DESCRIPCION) ? reader.GetValue(DESCRIPCION) : string.Empty);
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
