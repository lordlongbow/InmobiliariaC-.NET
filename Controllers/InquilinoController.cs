using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using royectoInmobiliaria.net_MVC_.Models;

namespace royectoInmobiliaria.net_MVC_.Controllers
{
    public class InquilinoController : Controller
    {
        private InquilinoReositorio inquilinoReositorio;
        public InquilinoController()
        {
            inquilinoReositorio = new InquilinoReositorio();
        }

        // GET: Inquilino
        public ActionResult Index()
        {

            var listaInquilinos = inquilinoReositorio.GetInquilinos();
            return View(listaInquilinos);
        }

        // GET: Inquilino/Details/5
        public ActionResult Detalles(int id)
        {
            var inquilino = inquilinoReositorio.getInquilino(id);
            return View(inquilino);
        }

        // GET: Inquilino/Create
        public ActionResult Nuevo()
        {

            Inquilino inquilino = new Inquilino();
            return View(inquilino);
        }

        // POST: Inquilino/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Nuevo(Inquilino inquilino)
        {
            try
            {
               var InuilinoNuuevo = inquilinoReositorio.Crear(inquilino);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
            throw new Exception(e.Message);
                
            }
        }

        // GET: Inquilino/Edit/5
        public ActionResult Editar(int id)
        {
            var inquilino = inquilinoReositorio.getInquilino(id);
            return View(inquilino);
        }

        // POST: Inquilino/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Inquilino inquilino)
        {
            try
            {
                
              inquilinoReositorio.Modificar(inquilino);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
               throw e;
            }
        }

        // GET: Inquilino/Delete/5
        public ActionResult Borrar(int id)
        {
            var inquilino = inquilinoReositorio.getInquilino(id);
            return View(inquilino);
        }

        // POST: Inquilino/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Borrar(int id, Inquilino inquilino)
        {
            try
            {
                inquilinoReositorio.Borrar(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}