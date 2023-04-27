using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using royectoInmobiliaria.net_MVC_.Models;

namespace royectoInmobiliaria.net_MVC_.Controllers
{
    [Authorize]
    public class PropietarioController : Controller
    {
        private PropietarioReositorio propietarioReositorio;

        public PropietarioController(){
            propietarioReositorio = new PropietarioReositorio();
        }
        // GET: Propietario
        public ActionResult Index()
        {
            var listaPropietarios = propietarioReositorio.GetPropietarios();
            return View(listaPropietarios);
        }

        // GET: Propietario/Details/5
        public ActionResult Detalles(int id)
        {
            var propietario = propietarioReositorio.getPropietario(id);
            return View(propietario);

        }

        // GET: Propietario/Create
        public ActionResult Nuevo()
        {
         var propietario = new Propietario();

            return View(propietario);
        }

        // POST: Propietario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Nuevo(Propietario propietario)
        {
            try
            {
                var NuevoPropietario = propietarioReositorio.Crear(propietario);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
               throw e;
            }
        }

        // GET: Propietario/Edit/5
        public ActionResult Editar(int id)
        {
            var propietario = propietarioReositorio.getPropietario(id);
            return View(propietario);
        }

        // POST: Propietario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Propietario propietario)
        {
            try
            {
               propietarioReositorio.Modificar(propietario);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
               throw e;
            }
        }

        // GET: Propietario/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Borrar(int id)
        {
            var propietario = propietarioReositorio.getPropietario(id);
            return View(propietario);
        }

        // POST: Propietario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Borrar(int id, Propietario propietario)
        {
            try
            {
                propietarioReositorio.Borrar(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}