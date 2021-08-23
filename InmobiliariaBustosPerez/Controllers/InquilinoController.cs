using InmobiliariaBustosPerez.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaBustosPerez.Controllers
{
    public class InquilinoController : Controller
    {
        RepositorioInquilino repositorioInquilino;

        private readonly IConfiguration config;
        public InquilinoController(IConfiguration config)
        {
            this.config = config;
            repositorioInquilino = new RepositorioInquilino(config);
        }

        public ActionResult Index()
        {
            try
            {
                var lista = repositorioInquilino.Obtener();
                ViewBag.Id = TempData["Id"];
                if (TempData.ContainsKey("Mensaje"))
                    ViewBag.Mensaje = TempData["Mensaje"];
                return View(lista);
            }
            catch (Exception ex)
            {

                Json(new { Error = ex.Message });
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Inquilino inquilino)
        {
            try
            {
                int res = repositorioInquilino.Alta(inquilino);
                TempData["Id"] = inquilino.Id;
                TempData["Mensaje"] = $"Inquilino creado! Id: {res}";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Editar(int id)
        {
            var inquilino = repositorioInquilino.ObtenerInquilino(id);
            return View(inquilino);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            try
            {
                Inquilino inquilino = repositorioInquilino.ObtenerInquilino(id);
                inquilino.Nombre = collection["Nombre"];
                inquilino.Apellido = collection["Apellido"];
                inquilino.Telefono = collection["Telefono"];
                inquilino.Dni = collection["Dni"];
                inquilino.Email = collection["Email"];

                repositorioInquilino.Modificar(inquilino);

                TempData["Mensaje"] = "Datos guardados";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(null);
            }
        }

        public ActionResult Eliminar(int id)
        {
            try
            {
                var entidad = repositorioInquilino.ObtenerInquilino(id);
                if (TempData.ContainsKey("Mensaje"))
                    ViewBag.Mensaje = TempData["Mensaje"];
                if (TempData.ContainsKey("Error"))
                    ViewBag.Error = TempData["Error"];
                return View(entidad);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, Inquilino entidad)
        {
            try
            {
                repositorioInquilino.Baja(id);
                TempData["Mensaje"] = "Eliminación realizada";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(entidad);
            }

        }
    }
}
