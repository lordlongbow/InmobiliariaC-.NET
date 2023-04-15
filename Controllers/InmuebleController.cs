using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using royectoInmobiliaria.net_MVC_.Models;

namespace royectoInmobiliaria.net_MVC_.Controllers
{
    public class InmuebleController : Controller
    {
        private InmuebleRepositorio InmuebleRepositorio = new InmuebleRepositorio();
        private PropietarioReositorio PropietarioRepositorio = new PropietarioReositorio();

        private TipoRepositorio TipoRepositorio = new TipoRepositorio();
        private UsoRepositorio UsoRepositorio = new UsoRepositorio();

        // GET: Inmueble
        public ActionResult Index()
        {
            var Inmuebles = InmuebleRepositorio.GetInmuebles();
            return View(Inmuebles);
        }

        // GET: Inmueble/Details/5
        public ActionResult Detalles(int id)
        {
            Inmueble Inmueble = InmuebleRepositorio.getInmueble(id);
            return View(Inmueble);
        }

        // GET: Inmueble/Create
        public ActionResult Crear()
        {
            ViewBag.Propietarios = PropietarioRepositorio.GetPropietarios();
            ViewBag.Tipos = TipoRepositorio.GetTipos();
            ViewBag.Usos = UsoRepositorio.GetUsos();

            Inmueble inmueble = new Inmueble();
            return View(inmueble);
        }

        // POST: Inmueble/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Inmueble inmueble)
        {
            try
            {
                var InmuebleNuevo = InmuebleRepositorio.Crear(inmueble);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // GET: Inmueble/Edit/5
        public ActionResult Editar(int id)
        {
            ViewBag.Propietarios = PropietarioRepositorio.GetPropietarios();
            ViewBag.Tipos = TipoRepositorio.GetTipos();
            ViewBag.Usos = UsoRepositorio.GetUsos();
            Inmueble Inmueble = InmuebleRepositorio.getInmueble(id);
            return View(Inmueble);
        }

        // POST: Inmueble/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Inmueble Inmueble)
        {
            try
            {
                Inmueble.InmuebleId = id;
                InmuebleRepositorio.Modificar(Inmueble);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)  
            {
                throw;
            }
        }

        // GET: Inmueble/Delete/5
        public ActionResult Borrar(int id)
        {
             Inmueble Inmueble = InmuebleRepositorio.getInmueble(id);
            return View(Inmueble);
        }

        // POST: Inmueble/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Borrar(int id, Inmueble Inmueble)
        {
            try
            {
                InmuebleRepositorio.Borrar(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
