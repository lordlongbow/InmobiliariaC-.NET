using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using royectoInmobiliaria.net_MVC_.Models;

namespace royectoInmobiliaria.net_MVC_.Controllers
{
    [Authorize]
    public class PagoController : Controller
    {
        private readonly PagoReositorio PagoReositorio = new PagoReositorio();
        private readonly ContratoReositorio ContratoReositorio = new ContratoReositorio();
        private readonly InmuebleRepositorio InmuebleRepositorio = new InmuebleRepositorio();

          private readonly InquilinoReositorio InquilinoReositorio = new InquilinoReositorio();


        // GET: Pago
        public IActionResult Index(int id)
        {
            var lista = PagoReositorio.GetPagos();

            if (lista.Count() < 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(lista);
            }
        }

        // GET: Pago/Details/5
        public IActionResult Detalles(int id)
        {
            Pago Pago = PagoReositorio.GetPago(id);
            Contrato contrato = ContratoReositorio.GetContrato(Pago.ContratoId);          
            ViewBag.Inmueble = InmuebleRepositorio.getInmueble(Pago.InmuebleId);
            ViewBag.Contrato = contrato;
            ViewBag.Inquilino = InquilinoReositorio.getInquilino(contrato.InquilinoId);
            return View(Pago);
        }

        // GET: Pago/Create
        public ActionResult Crear(int id)
        {
            //guardar datos en variables 
            Pago nuevoPago = new Pago();
            ViewBag.ContratoId = id;
            var contrato = ContratoReositorio.GetContrato(id);
            var inmueble  = InmuebleRepositorio.getInmueble(contrato.InmuebleId);
            
            nuevoPago.Precio = inmueble.Precio;
            ViewBag.Inmueble = inmueble;
            ViewBag.Contrato = contrato;
            return View(nuevoPago);
        }

        // POST: Pago/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Pago Pago, int IdContrato)
        {
            try
            {
                ViewBag.Inmueble = InmuebleRepositorio.getInmueble(IdContrato);
                ViewBag.Contrato = ContratoReositorio.GetContrato(IdContrato);
                PagoReositorio.CrearAumentado(Pago);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pago/Edit/5
        public IActionResult Editar(int id)
        {
            ViewBag.Contratos = ContratoReositorio.GetContratos();
            ViewBag.Inmuebles = InmuebleRepositorio.GetInmuebles();

            Pago Pago = PagoReositorio.GetPago(id);
            return View(Pago);
        }

        // POST: Pago/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, Pago Pago)
        {
            try
            {
                Pago.PagoId = id;
                PagoReositorio.Modificar(Pago);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                throw;
                //return View();
            }
        }

        // GET: Pago/Delete/5
        [Authorize(Roles = "Administrador")]
        public IActionResult Borrar(int id)
        {
            var Pago = PagoReositorio.GetPago(id);
            return View(Pago);
        }

        // POST: Pago/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public IActionResult Borrar(int id, Pago Pago)
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

    public ActionResult VerPagos(int id){
        var PagosDelContrato = PagoReositorio.GetPagosDelContrato(id);
         return View(PagosDelContrato);        
    }
    }

}
