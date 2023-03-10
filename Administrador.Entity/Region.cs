using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administrador.Entity
{
    public class Region
    {
        public int Id { get; set; }
        public string NombreRegion { get; set; }
        public int Tide_Id { get; set; }
        public bool Defecto { get; set; }
    }
}
