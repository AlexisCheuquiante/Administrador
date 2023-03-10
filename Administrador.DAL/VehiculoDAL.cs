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
    public class VehiculoDAL
    {
        public static Entity.Vehiculo InsertarVehiculo(Entity.Vehiculo vehiculo)
        {
            Database db = DatabaseFactory.CreateDatabase("baseDatosEdificios");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_VEHI_VEHICULO_INS");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, vehiculo.Id != 0 ? vehiculo.Id : (object)null);
            db.AddInParameter(dbCommand, "PATENTE", DbType.String, vehiculo.Patente != "" ? vehiculo.Patente.ToUpper() : (object)null);
            db.AddInParameter(dbCommand, "RES_ID", DbType.Int32, vehiculo.Res_Id != 0 ? vehiculo.Res_Id : (object)null);
            db.AddInParameter(dbCommand, "PER_ID", DbType.Int32, vehiculo.Per_Id != 0 ? vehiculo.Per_Id : (object)null);
            db.AddInParameter(dbCommand, "TIVE_ID", DbType.Int32, vehiculo.Tive_Id != 0 ? vehiculo.Tive_Id : (object)null);
            db.AddInParameter(dbCommand, "OBSERVACION", DbType.String, vehiculo.Observacion != "" ? vehiculo.Observacion.ToUpper() : (object)null);
            db.AddInParameter(dbCommand, "ESTACIONAMIENTO", DbType.String, vehiculo.Estacionamiento != "" ? vehiculo.Estacionamiento.ToUpper() : (object)null);
            db.AddInParameter(dbCommand, "ELIMINADO", DbType.Byte, vehiculo.Eliminado == true ? 1 : 0);

            vehiculo.Id = int.Parse(db.ExecuteScalar(dbCommand).ToString());

            return vehiculo;
        }
        public static List<Entity.Vehiculo> ObtenerVehiculos(Entity.Filtro filtro)
        {
            List<Entity.Vehiculo> lista = new List<Entity.Vehiculo>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosEdificios");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_VEHI_VEHICULO_LEER");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, filtro.Id != 0 ? filtro.Id : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, filtro.Emp_Id != 0 ? filtro.Emp_Id : (object)null);
            db.AddInParameter(dbCommand, "RES_ID", DbType.Int32, filtro.Res_Id != 0 ? filtro.Res_Id : (object)null);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int PATENTE = reader.GetOrdinal("PATENTE");
                int RES_ID = reader.GetOrdinal("RES_ID");
                int RECINTO = reader.GetOrdinal("RECINTO");
                int PER_ID = reader.GetOrdinal("PER_ID");
                int NOMBRE = reader.GetOrdinal("NOMBRE");
                int TIVE_ID = reader.GetOrdinal("TIVE_ID");
                int TIPO_VEHICULO = reader.GetOrdinal("TIPO_VEHICULO");
                int OBSERVACION = reader.GetOrdinal("OBSERVACION");
                int ESTACIONAMIENTO = reader.GetOrdinal("ESTACIONAMIENTO");
                
                while (reader.Read())
                {
                    Entity.Vehiculo OBJ = new Entity.Vehiculo();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.Patente = (String)(!reader.IsDBNull(PATENTE) ? reader.GetValue(PATENTE) : string.Empty);
                    OBJ.Res_Id = (int)(!reader.IsDBNull(RES_ID) ? reader.GetValue(RES_ID) : 0);
                    OBJ.Recinto = (String)(!reader.IsDBNull(RECINTO) ? reader.GetValue(RECINTO) : string.Empty);
                    OBJ.Per_Id = (int)(!reader.IsDBNull(PER_ID) ? reader.GetValue(PER_ID) : 0);
                    OBJ.NombrePersona = (String)(!reader.IsDBNull(NOMBRE) ? reader.GetValue(NOMBRE) : string.Empty);
                    OBJ.Tive_Id = (int)(!reader.IsDBNull(TIVE_ID) ? reader.GetValue(TIVE_ID) : 0);
                    OBJ.TipoVehiculo = (String)(!reader.IsDBNull(TIPO_VEHICULO) ? reader.GetValue(TIPO_VEHICULO) : string.Empty);
                    OBJ.Observacion = (String)(!reader.IsDBNull(OBSERVACION) ? reader.GetValue(OBSERVACION) : string.Empty);
                    OBJ.Estacionamiento = (String)(!reader.IsDBNull(ESTACIONAMIENTO) ? reader.GetValue(ESTACIONAMIENTO) : string.Empty);
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
        public static void EliminarVehiculo(int id)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db = DatabaseFactory.CreateDatabase("baseDatosEdificios");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_VEHI_VEHICULO_DEL");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, id);


            db.ExecuteNonQuery(dbCommand);

        }
    }
}

