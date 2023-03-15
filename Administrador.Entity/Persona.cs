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
        public string Correo { get; set; }
        public string Telefono_1 { get; set; }
        public string Telefono_2 { get; set; }
        public bool Es_Movilidad_Reducida { get; set; }
        public bool Es_Menor_Edad { get; set; }
        public int Tiin_Id { get; set; }
        public int Piso { get; set; }
        public int Depto { get; set; }
        public int Casa { get; set; }
        public bool Eliminado { get; set; }
    }
}
