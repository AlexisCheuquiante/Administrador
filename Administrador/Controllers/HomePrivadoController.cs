﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administrador.Controllers
{
    public class HomePrivadoController : Controller
    {
        // GET: HomePrivado
        public ActionResult Index()
        {
            Administrador.Models.HomeModel model = new Models.HomeModel();
            model.Nombre = SessionH.Usuario.NombreCompleto;
            return View(model);
        }
    }
}