using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Acex.Models;
using System.Diagnostics;


namespace Acex.Controllers
{
    public class PrincipalController : Controller
    {
        public IActionResult Principal()
        {
            return View();
        }

        public IActionResult Contenedor() => View();


        public IActionResult Publicar()
        {

            return View();
        }

        public IActionResult Pais()
        {


            return View("~/Views/Catalogo/pais.cshtml");
        }
        public IActionResult carrito()
        {


            return View("~/Views/Catalogo/carrito.cshtml");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}