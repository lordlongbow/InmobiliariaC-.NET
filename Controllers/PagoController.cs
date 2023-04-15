using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using royectoInmobiliaria.net_MVC_.Models;

namespace royectoInmobiliaria.net_MVC_.Controllers
{
    public class PagoController : Controller
    {

      private PagoReositorio PagoReositorio = new PagoReositorio();
        // GET: Pago
        public ActionResult Index(int id )
        { 
            var lista = PagoReositorio.GetPagos();
            
            if(lista.Count()>0){
               return View(lista); 
            }else {
                return View();
            }
            
        }

        // GET: Pago/Details/5
        public ActionResult Detalles(int id)
        {
            var Pago = PagoReositorio.GetPago();
            return View(Pago);
        }

        // GET: Pago/Create
        public ActionResult Crear(int id)
        {
//Aca viene id del contrato

            return View();
        }

        // POST: Pago/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Pago Pago)
        {
            try
            {
                PagoReositorio.Crear();
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pago/Edit/5
        public ActionResult Editar(int id)
        {
             var Pago = PagoReositorio.GetPago();
            return View(Pago);
        }

        // POST: Pago/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Pago Pago)
        {
            try
            {
                PagoReositorio.Modificar(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pago/Delete/5
        public ActionResult Borrar(int id)
        {
             var Pago = PagoReositorio.GetPago();
            return View(Pago);
        }

        // POST: Pago/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Borrar(int id, Pago Pago)
        {
            try
            {
               PagoReositorio.Borrar(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}