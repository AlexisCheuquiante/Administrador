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
    public class ConsultaSi_NoDAL
    {
        public static List<Entity.ConsultaSi_No> Obtener()
        {
            List<Entity.ConsultaSi_No> lista = new List<Entity.ConsultaSi_No>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosEdificios");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_CON_CONSULTA_SI_NO_LEER");

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int DESCRIPCION = reader.GetOrdinal("DESCRIPCION");

                while (reader.Read())
                {
                    Entity.ConsultaSi_No OBJ = new Entity.ConsultaSi_No();
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
