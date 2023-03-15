using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administrador.Entity
{
    public class Usuario
    {
        public int Id { get; set; }
        public int Per_Id { get; set; }
        public int EmpId { get; set; }
        public string NombreEmpresa { get; set; }
        public int Res_Id { get; set; }
        public string UsuarioStr { get; set; }
        public string NombreCompleto { get; set; }
        public string Password { get; set; }
        public bool Eliminado { get; set; }
        public int Rol_Id { get; set; }
        public string Descripcion_Rol { get; set; }
    }
}
