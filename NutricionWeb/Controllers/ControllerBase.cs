﻿using NutricionWeb.Helpers.Identity;
using System.Web.Mvc;

namespace NutricionWeb.Controllers
{
    public class ControllerBase : Controller
    {
        public long? ObtenerEstablecimientoIdUser()
        {
            return !User.IsInRole("Administrador") ? User.Identity.GetEstablecimientoId() : null;
        }
    }
}