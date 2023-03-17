using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administrador.Entity
{
    public class Persona
    {
        public int Id { get; set; }
        public int Per_Id { get; set; }
        public string JefeHogar { get; set; }
        public int Emp_Id { get; set; }
        public int Res_Id { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Fecha_Nacimiento_Mostrar
        {
            get
            {
                string yy = Fecha_Nacimiento.Year.ToString("0000");
                string mm = Fecha_Nacimiento.Month.ToString("00");
                string dd = Fecha_Nacimiento.Day.ToString("00");

                return yy + "-" + mm + "-" + dd;
            }
        }
        public string Correo { get; set; }
        public string Telefono_1 { get; set; }
        public string Telefono_2 { get; set; }
        public bool Es_Movilidad_Reducida { get; set; }
        public string Es_Movilidad_Reducida_Str
        {
            get
            {
                if (Es_Movilidad_Reducida == true)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }
        public bool Es_Menor_Edad { get; set; }
        public string Es_Menor_Edad_Str
        {
            get
            {
                if (Es_Menor_Edad == true)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }
        public int Tiin_Id { get; set; }
        public int Piso { get; set; }
        public int Depto { get; set; }
        public int Casa { get; set; }
        public bool Eliminado { get; set; }
    }
}
