using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administrador.Entity
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string Patente { get; set; }
        public int Res_Id { get; set; }
        public string Recinto { get; set; }
        public int Per_Id { get; set; }
        public string NombrePersona { get; set; }
        public int Tive_Id { get; set; }
        public string TipoVehiculo { get; set; }
        public string Observacion { get; set; }
        public string Estacionamiento { get; set; }
        public string JefeHogar { get; set; }
        public bool Eliminado { get; set; }
    }
}
