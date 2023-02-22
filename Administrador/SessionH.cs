using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administrador
{
    public class SessionH
    {
        private const string VAR_LOGUEADO = "logueado";
        private const string VAR_USUARIO = "usuario";

        private static T Lee<T>(string variable)
        {
            object valor = HttpContext.Current.Session[variable];
            if (valor == null)
                return default(T);
            else return ((T)valor);
        }

        private static void Escribe(string variable, object valor)
        {
            HttpContext.Current.Session[variable] = valor;
        }

        public static bool Logueado
        {
            get { return Lee<bool>(VAR_LOGUEADO); }
            set { Escribe(VAR_LOGUEADO, value); }
        }

        public static Entity.Usuario Usuario
        {
            get { return Lee<Entity.Usuario>(VAR_USUARIO); }
            set { Escribe(VAR_USUARIO, value); }
        }
    }
}