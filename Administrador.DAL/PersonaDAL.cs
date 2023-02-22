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
    }
}
