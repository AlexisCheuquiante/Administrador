﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace Administrador.DAL
{
    public class RecintoDAL
    {
        public static List<Entity.Recinto> ObtenerRecintos(Entity.Filtro filtro)
        {
            List<Entity.Recinto> lista = new List<Entity.Recinto>();
            Database db = DatabaseFactory.CreateDatabase("baseDatosEdificios");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_RES_RECINTO_LEER");

            db.AddInParameter(dbCommand, "ID", DbType.Int32, filtro.Id != 0 ? filtro.Id : (object)null);
            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, filtro.Emp_Id != 0 ? filtro.Emp_Id : (object)null);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int EMP_ID = reader.GetOrdinal("EMP_ID");
                int TIPO_ID = reader.GetOrdinal("TIPO_ID");
                int NOMBRE_RECINTO = reader.GetOrdinal("NOMBRE");
                int DIRECCION_RECINTO = reader.GetOrdinal("DIRECCION");
                int REG_ID = reader.GetOrdinal("REG_ID");
                int COM_ID = reader.GetOrdinal("COM_ID");
                int PISOS = reader.GetOrdinal("PISOS");

                while (reader.Read())
                {
                    Entity.Recinto OBJ = new Entity.Recinto();
                    //BeginFields
                    OBJ.Id = (int)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    OBJ.Emp_Id = (int)(!reader.IsDBNull(EMP_ID) ? reader.GetValue(EMP_ID) : 0);
                    OBJ.Tipo_Id = (int)(!reader.IsDBNull(TIPO_ID) ? reader.GetValue(TIPO_ID) : 0);
                    OBJ.NombreRecinto = (String)(!reader.IsDBNull(NOMBRE_RECINTO) ? reader.GetValue(NOMBRE_RECINTO) : string.Empty);
                    OBJ.DireccionRecinto = (String)(!reader.IsDBNull(DIRECCION_RECINTO) ? reader.GetValue(DIRECCION_RECINTO) : string.Empty);
                    OBJ.Reg_Id = (int)(!reader.IsDBNull(REG_ID) ? reader.GetValue(REG_ID) : 0);
                    OBJ.Com_Id = (int)(!reader.IsDBNull(COM_ID) ? reader.GetValue(COM_ID) : 0);
                    OBJ.Pisos = (int)(!reader.IsDBNull(PISOS) ? reader.GetValue(PISOS) : 0);
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
