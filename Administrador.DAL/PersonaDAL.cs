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
    public class PersonaDAL
    {
        public static Entity.Persona InsertarPersona(Entity.Persona persona)
        {
            Database db = DatabaseFactory.CreateDatabase("baseDatosEdificios");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_PER_PERSONA_INS");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, persona.Id != 0 ? persona.Id : (object)null);
            db.AddInParameter(dbCommand, "PER_ID", DbType.Int32, persona.Per_Id != 0 ? persona.Per_Id : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, persona.Emp_Id != 0 ? persona.Emp_Id : (object)null);
            db.AddInParameter(dbCommand, "RES_ID", DbType.Int32, persona.Res_Id != 0 ? persona.Res_Id : (object)null);
            db.AddInParameter(dbCommand, "FECHA_INGRESO", DbType.DateTime, persona.Fecha_Ingreso != DateTime.MinValue ? persona.Fecha_Ingreso : (object)null);
            db.AddInParameter(dbCommand, "RUT", DbType.String, persona.Rut != "" ? persona.Rut.ToUpper() : (object)null);
            db.AddInParameter(dbCommand, "NOMBRE", DbType.String, persona.Nombre != "" ? persona.Nombre.ToUpper() : (object)null);
            db.AddInParameter(dbCommand, "FECHA_NACIMIENTO", DbType.DateTime, persona.Fecha_Nacimiento != DateTime.MinValue ? persona.Fecha_Nacimiento : (object)null);
            db.AddInParameter(dbCommand, "CORREO", DbType.String, persona.Correo != "" ? persona.Correo : (object)null);
            db.AddInParameter(dbCommand, "TELEFONO1", DbType.String, persona.Telefono_1 != "" ? persona.Telefono_1 : (object)null);
            db.AddInParameter(dbCommand, "TELEFONO2", DbType.String, persona.Telefono_2 != "" ? persona.Telefono_2 : (object)null);
            db.AddInParameter(dbCommand, "ES_MOVILIDAD_REDUCIDA", DbType.Byte, persona.Es_Movilidad_Reducida == true ? 1 : 0);
            db.AddInParameter(dbCommand, "ES_NIÑO", DbType.Byte, persona.Es_Menor_Edad == true ? 1 : 0);
            db.AddInParameter(dbCommand, "TIIN_ID", DbType.Int32, persona.Tiin_Id != 0 ? persona.Tiin_Id : (object)null);
            db.AddInParameter(dbCommand, "PISO", DbType.Int32, persona.Piso != 0 ? persona.Piso : (object)null);
            db.AddInParameter(dbCommand, "DEPTO", DbType.Int32, persona.Depto != 0 ? persona.Depto : (object)null);
            db.AddInParameter(dbCommand, "CASA", DbType.Int32, persona.Casa != 0 ? persona.Casa : (object)null);
            db.AddInParameter(dbCommand, "ELIMINADO", DbType.Byte, persona.Eliminado == true ? 1 : 0);

            persona.Id = int.Parse(db.ExecuteScalar(dbCommand).ToString());

            return persona;
        }
        public static List<Entity.Persona> ObtenerPersonas(Entity.Filtro filtro)
        {
            List<Entity.Persona> lista = new List<Entity.Persona>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosEdificios");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_PER_PERSONA_LEER");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, filtro.Id != 0 ? filtro.Id : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, filtro.Emp_Id != 0 ? filtro.Emp_Id : (object)null);
            db.AddInParameter(dbCommand, "RES_ID", DbType.Int32, filtro.Res_Id != 0 ? filtro.Res_Id : (object)null);
            db.AddInParameter(dbCommand, "PER_ID", DbType.Int32, filtro.Per_Id != 0 ? filtro.Per_Id : (object)null);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int PER_ID = reader.GetOrdinal("PER_ID");
                int JEFE_HOGAR = reader.GetOrdinal("JEFE_HOGAR");
                int EMP_ID = reader.GetOrdinal("EMP_ID");
                int RES_ID = reader.GetOrdinal("RES_ID");
                int RUT = reader.GetOrdinal("RUT");
                int NOMBRE = reader.GetOrdinal("NOMBRE");
                int FECHA_NACIMIENTO = reader.GetOrdinal("FECHA_NACIMIENTO");
                int CORREO = reader.GetOrdinal("CORREO");
                int TELEFONO1 = reader.GetOrdinal("TELEFONO1");
                int TELEFONO2 = reader.GetOrdinal("TELEFONO2");
                int ES_MOVILIDAD_REDUCIDA = reader.GetOrdinal("ES_MOVILIDAD_REDUCIDA");
                int ES_NIÑO = reader.GetOrdinal("ES_NIÑO");
                int TIIN_ID = reader.GetOrdinal("TIIN_ID");
                int PISO = reader.GetOrdinal("PISO");
                int DEPTO = reader.GetOrdinal("DEPTO");
                int CASA = reader.GetOrdinal("CASA");

                while (reader.Read())
                {
                    Entity.Persona OBJ = new Entity.Persona();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.Per_Id = (int)(!reader.IsDBNull(PER_ID) ? reader.GetValue(PER_ID) : 0);
                    OBJ.JefeHogar = (String)(!reader.IsDBNull(JEFE_HOGAR) ? reader.GetValue(JEFE_HOGAR) : string.Empty);
                    OBJ.Emp_Id = (int)(!reader.IsDBNull(EMP_ID) ? reader.GetValue(EMP_ID) : 0);
                    OBJ.Res_Id = (int)(!reader.IsDBNull(RES_ID) ? reader.GetValue(RES_ID) : 0);
                    OBJ.Rut = (String)(!reader.IsDBNull(RUT) ? reader.GetValue(RUT) : string.Empty);
                    OBJ.Nombre = (String)(!reader.IsDBNull(NOMBRE) ? reader.GetValue(NOMBRE) : string.Empty);
                    OBJ.Fecha_Nacimiento = (DateTime)(!reader.IsDBNull(FECHA_NACIMIENTO) ? reader.GetValue(FECHA_NACIMIENTO) : DateTime.MinValue);
                    OBJ.Correo = (String)(!reader.IsDBNull(CORREO) ? reader.GetValue(CORREO) : string.Empty);
                    OBJ.Telefono_1 = (String)(!reader.IsDBNull(TELEFONO1) ? reader.GetValue(TELEFONO1) : string.Empty);
                    OBJ.Telefono_2 = (String)(!reader.IsDBNull(TELEFONO2) ? reader.GetValue(TELEFONO2) : string.Empty);
                    OBJ.Es_Movilidad_Reducida = (bool)(!reader.IsDBNull(ES_MOVILIDAD_REDUCIDA) ? reader.GetValue(ES_MOVILIDAD_REDUCIDA) : false);
                    OBJ.Es_Menor_Edad = (bool)(!reader.IsDBNull(ES_NIÑO) ? reader.GetValue(ES_NIÑO) : false);
                    OBJ.Tiin_Id = (int)(!reader.IsDBNull(TIIN_ID) ? reader.GetValue(TIIN_ID) : 0);
                    OBJ.Piso = (int)(!reader.IsDBNull(PISO) ? reader.GetValue(PISO) : 0);
                    OBJ.Depto = (int)(!reader.IsDBNull(DEPTO) ? reader.GetValue(DEPTO) : 0);
                    OBJ.Casa = (int)(!reader.IsDBNull(CASA) ? reader.GetValue(CASA) : 0);
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
        public static void UpdatePer_Id(int id)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db = DatabaseFactory.CreateDatabase("baseDatosEdificios");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_PER_PERSONA_UPDATE_PER");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, id);


            db.ExecuteNonQuery(dbCommand);

        }
        public static List<Entity.Persona> ObtenerJefeHogar(Entity.Filtro filtro)
        {
            List<Entity.Persona> lista = new List<Entity.Persona>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosEdificios");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_JEFE_HOGAR_LEER");

            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, filtro.Emp_Id != 0 ? filtro.Emp_Id : (object)null);
            db.AddInParameter(dbCommand, "RES_ID", DbType.Int32, filtro.Res_Id != 0 ? filtro.Res_Id : (object)null);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int PER_ID = reader.GetOrdinal("PER_ID");
                int JEFE_HOGAR = reader.GetOrdinal("NOMBRE");

                while (reader.Read())
                {
                    Entity.Persona OBJ = new Entity.Persona();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.Per_Id = (int)(!reader.IsDBNull(PER_ID) ? reader.GetValue(PER_ID) : 0);
                    OBJ.JefeHogar = (String)(!reader.IsDBNull(JEFE_HOGAR) ? reader.GetValue(JEFE_HOGAR) : string.Empty);
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
        public static void EliminarPersona(int id)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db = DatabaseFactory.CreateDatabase("baseDatosEdificios");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_PER_PERSONA_DEL");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, id);


            db.ExecuteNonQuery(dbCommand);

        }
    }
}
