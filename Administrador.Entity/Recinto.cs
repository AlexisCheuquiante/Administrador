using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administrador.Entity
{
    public class Recinto
    {
        public int Id { get; set; }
        public int Emp_Id { get; set; }
        public int Tipo_Id { get; set; }
        public string TipoPropiedad { get; set; }
        public string NombreRecinto { get; set; }
        public string DireccionRecinto { get; set; }
        public int Reg_Id { get; set; }
        public string Region { get; set; }
        public int Com_Id { get; set; }
        public string Comuna { get; set; }
        public int Pisos { get; set; }
        public int Casas { get; set; }
        public int Viviendas { get; set; }
        public bool Eliminado { get; set; }
    }
}
