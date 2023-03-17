using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administrador.Entity
{
    public class Filtro
    {
        public int Id { get; set; }
        public int Emp_Id { get; set; }
        public int Res_Id { get; set; }
        public int Per_Id { get; set; }
        public int Reg_Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Rut { get; set; }
    }
}
