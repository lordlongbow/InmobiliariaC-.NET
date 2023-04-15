using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using royectoInmobiliaria.net_MVC_.Models;

namespace royectoInmobiliaria.net_MVC_.Controllers
{
    public class ContratoController : Controller
    {
        private ContratoReositorio ContratoRepositorio = new ContratoReositorio();
        private InquilinoReositorio InquilinoRepositorio = new InquilinoReositorio();
        private InmuebleRepositorio InmuebleRepositorio = new InmuebleRepositorio();
        private PagoReositorio PagoRepositorio = new PagoReositorio();

        public ContratoController() { }

        // GET: Contrato
        public ActionResult Index()
        {
            var lista = ContratoRepositorio.GetContratos();
            if (lista.Count() < 0)
            {
                return RedirectToAction("Crear");
            }
            else
            {
                return View(lista);
            }
        }

        // GET: Contrato/Details/5
        public ActionResult Detalles(int id)
        {
            Contrato contrato = ContratoRepositorio.GetContrato(id);
            return View(contrato);
        }

        // GET: Contrato/Create
        public ActionResult Crear()
        {
            ViewBag.Inquilinos = InquilinoRepositorio.GetInquilinos();
            ViewBag.Inmuebles = InmuebleRepositorio.GetInmuebles();
            ViewBag.Pagos = PagoRepositorio.GetPagos();
            Contrato contrato = new Contrato() { FechaInicio = DateTime.Today };
            return View(contrato);
        }

        // POST: Contrato/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Contrato contrato)
        {
            try
            {
                ContratoRepositorio.Crear(contrato);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Contrato/Edit/5
        public ActionResult Editar(int id)
        {
            ViewBag.Inquilinos = InquilinoRepositorio.GetInquilinos();
            ViewBag.Inmuebles = InmuebleRepositorio.GetInmuebles();
            ViewBag.Pagos = PagoRepositorio.GetPagos();
            Contrato contrato = ContratoRepositorio.GetContrato(id);
            return View(contrato);
        }

        // POST: Contrato/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Contrato contrato)
        {
            try
            {
                contrato.ContratoId = id;
                ContratoRepositorio.Modificar(contrato);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // GET: Contrato/Delete/5
        public ActionResult Borrar(int id)
        {
            Contrato contrato = ContratoRepositorio.GetContrato(id);
            return View(contrato);
        }

        // POST: Contrato/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Borrar(int id, Contrato contrato)
        {
            try
            {
                ContratoRepositorio.Borrar(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

//dotnet-aspnet-codegenerator view Index List -outDir "Views/Contratos" -udl --model vsTest.Models.Contrato -f
