using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using siges.DTO;
using siges.Models;
using siges.Repository;

namespace siges.Controllers
{
  [Authorize(Roles = "Supervisor, SuperUser")]
  public class ActivoFijoController : Controller
    {
        private readonly IActivoFijoRepository _afRepo;
        private readonly IEntradaActivoFijoRepository _entradaAfRepo;
        private readonly IContratoRepository _cRepo;
        private readonly IUbicacionRepository _uRepo;
        private readonly ITraspasoActivoFijoRepository _tRepo;

        public ActivoFijoController(IActivoFijoRepository repository, IEntradaActivoFijoRepository entradaafrepo, IContratoRepository cRepo, IUbicacionRepository uRepo, ITraspasoActivoFijoRepository tRepo)
        {
            _afRepo = repository;
            _entradaAfRepo = entradaafrepo;
            _cRepo = cRepo;
            _uRepo = uRepo;
            _tRepo = tRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        /* --------- Entradas ------------ */
        /* Listado entrada de Activo Fijo */
        public IActionResult IndexListEntradaAF()
        {
            List<EntradaActivoFijo> entradas = _entradaAfRepo.GetAll().ToList();
            ViewData["entradas"] = entradas;
            return View("~/Views/ActivoFijos/ListEntradaActivoFijo.cshtml");
        }

        public IActionResult DeleteEntradaAF(int eafid)
        {
            EntradaActivoFijo nEAF = _entradaAfRepo.GetById(eafid);
            nEAF.Estatus = false;
            _entradaAfRepo.Update(nEAF);
            return RedirectToAction("IndexListEntradaAF", "ActivoFijo");
            //return View();
        }

        /* Crear entrada de activo fijo */
        public IActionResult IndexCreateEntradaAF()
        {
            List<Contrato> contratos = _cRepo.GetAll().ToList();
            ViewData["contratos"] = contratos;
            return View("~/Views/ActivoFijos/CreateEntradaActivoFijo.cshtml");
        }

        public IActionResult SaveEAF(EntradaActivoFijoDTO nEAF)
        {
            EntradaActivoFijo eaf = new EntradaActivoFijo();
            CultureInfo cin = new CultureInfo("es-MX");
            eaf.Remision = nEAF.Remision;
            eaf.Ubicacion = _uRepo.GetById(nEAF.Ubicacion);
            eaf.Tipo = nEAF.Tipo;
            eaf.FechaRemision = DateTime.Parse(nEAF.FechaRemision, cin);
            eaf.FechaRecepcion = DateTime.Parse(nEAF.FechaRecepcion, cin);
            eaf.Incidencia = nEAF.Incidencia;
            eaf.Observaciones = nEAF.Observaciones;
            eaf.Estatus = nEAF.Estatus;
            foreach (var i in nEAF.Desglose)
            {
                eaf.Desglose.Add(new DetalleActivoFijo { Referencia = _afRepo.GetById(i.Referencia), Descripcion = i.Descripcion, NumeroSerie = i.NumeroSerie,  Cantidad = i.Cantidad, Arrendamiento = i.Arrendamiento, Observaciones = i.Observaciones, Unidad = i.Unidad, Estatus = true });
            }

            _entradaAfRepo.Insert(eaf);

            return RedirectToAction("IndexListEntradaAF", "ActivoFijo");
        }

        /* Editar entrada de activo fijo */
        public IActionResult IndexEditEntradaAF(int entradaId)
        {
            EntradaActivoFijo eaf = _entradaAfRepo.GetByIdEAF(entradaId);
            List<ActivoFijo> activos = _afRepo.GetAll().ToList();
            List<Contrato> contratos = _cRepo.GetAll().ToList();
            //List<Ubicacion> ubicaciones = _uRepo.getUbicacionesByContrato(eaf.Ubicacion.ContratoId).ToList();
            ViewData["entrada"] = eaf;
            ViewData["contratos"] = contratos;
            ViewData["activos"] = activos;
            //ViewData["ubicaciones"] = ubicaciones;
            return View("~/Views/ActivoFijos/CreateEntradaActivoFijo.cshtml");
        }

        public IActionResult EditEAF(EntradaActivoFijoDTO eEAF)
        {
            EntradaActivoFijo eaf = _entradaAfRepo.GetByIdEAF(eEAF.Id);
            CultureInfo cin = new CultureInfo("es-MX");
            eaf.Remision = eEAF.Remision;
            eaf.Ubicacion = _uRepo.GetById(eEAF.Ubicacion);
            eaf.Tipo = eEAF.Tipo;
            eaf.FechaRemision = DateTime.Parse(eEAF.FechaRemision, cin);
            eaf.FechaRecepcion = DateTime.Parse(eEAF.FechaRecepcion, cin);
            eaf.Incidencia = eEAF.Incidencia;
            eaf.Observaciones = eEAF.Observaciones;
            eaf.Estatus = eEAF.Estatus;
            foreach (var i in eaf.Desglose)
            {
                i.Estatus = false;
            }
            foreach (var i in eEAF.Desglose)
            {
                if (i.Id > 0)
                {
                    foreach (var x in eaf.Desglose)
                    {
                        if (i.Id == x.Id)
                        {
                            x.Estatus = true;
                        }
                    }
                }
                else
                {
                    eaf.Desglose.Add(new DetalleActivoFijo { Referencia = _afRepo.GetById(i.Referencia), Descripcion = i.Descripcion, NumeroSerie = i.NumeroSerie, Cantidad = i.Cantidad, Arrendamiento = i.Arrendamiento, Observaciones = i.Observaciones, Unidad = i.Unidad, Estatus = true });
                }
            }

            _entradaAfRepo.Update(eaf);

            return RedirectToAction("IndexListEntradaAF", "ActivoFijo");
        }

        /* --------- Traspasos ------------ */
        /* Listado traspasos de Activo Fijo */
        public IActionResult IndexListTraspasoAF()
        {
            List<TraspasoActivoFijo> traspasos = _tRepo.GetAll(true).ToList();
            ViewData["traspasos"] = traspasos;
            return View("~/Views/ActivoFijos/ListTraspasoActivoFijo.cshtml");
        }

        public IActionResult DeleteTraspasoAF(int tafid)
        {
            TraspasoActivoFijo nTAF = _tRepo.GetById(tafid);
            nTAF.Estatus = false;
            _tRepo.Update(nTAF);
            return RedirectToAction("IndexListTraspasoAF", "ActivoFijo");
            //return View();
        }

        /* Crear traspaso de Activo Fijo */
        public IActionResult IndexCreateTraspasoAF()
        {
            List<Contrato> contratos = _cRepo.GetAll().ToList();
            List<ActivoFijo> activos = _afRepo.GetAll().ToList();
            ViewData["contratos"] = contratos;
            ViewData["activos"] = activos;
            return View("~/Views/ActivoFijos/CreateTraspasoActivoFijo.cshtml");
        }

        public IActionResult SaveTAF(TraspasoActivoFijoDTO nTAF)
        {
            TraspasoActivoFijo taf = new TraspasoActivoFijo();
            CultureInfo cin = new CultureInfo("es-MX");
            taf.Folio = nTAF.Folio;
            taf.UbicacionDestino = _uRepo.GetById(nTAF.UbicacionDestino);
            taf.UbicacionOrigen = _uRepo.GetById(nTAF.UbicacionOrigen);
            taf.FechaEnvio = DateTime.Parse(nTAF.FechaEnvio, cin);
            taf.FechaSalida = DateTime.Parse(nTAF.FechaSalida, cin);
            taf.MotivoSalida = nTAF.MotivoSalida;
            taf.NumGuia = nTAF.NumGuia;
            taf.Paqueteria = nTAF.Paqueteria;
            taf.Estatus = nTAF.Estatus;
            foreach (var i in nTAF.Detalle)
            {
                taf.Detalle.Add(new TraspasoDetalleActivoFijo { ActivoFijo = _afRepo.GetById(i.Id), Estatus = true });
            }

            _tRepo.Insert(taf);

            return RedirectToAction("IndexListTraspasoAF", "ActivoFijo");
        }

        /* Editar traspaso de Activo Fijo */
        public IActionResult IndexEditTraspasoAF(int traspasoId)
        {
            TraspasoActivoFijo taf = _tRepo.GetByIdTAF(traspasoId);
            List<Contrato> contratos = _cRepo.GetAll().ToList();
            //List<Ubicacion> ubicacionesOrig = _uRepo.getUbicacionesByContrato(taf.UbicacionOrigen.ContratoId).ToList();
            //List<Ubicacion> ubicacionesDes = _uRepo.getUbicacionesByContrato(taf.UbicacionDestino.ContratoId).ToList();
            List<ActivoFijo> activos = _afRepo.GetAll().ToList();
            ViewData["traspaso"] = taf;
            ViewData["contratos"] = contratos;
            ViewData["activos"] = activos;
            //ViewData["ubicacionesOrigen"] = ubicacionesOrig;
            //ViewData["ubicacionesDestino"] = ubicacionesDes;
            return View("~/Views/ActivoFijos/EditTraspasoActivoFijo.cshtml");
        }

        public IActionResult EditTAF(TraspasoActivoFijoDTO eTAF)
        {
            TraspasoActivoFijo taf = _tRepo.GetByIdTAF(eTAF.Id);
            CultureInfo cin = new CultureInfo("es-MX");
            taf.Folio = eTAF.Folio;
            taf.UbicacionDestino = _uRepo.GetById(eTAF.UbicacionDestino);
            taf.UbicacionOrigen = _uRepo.GetById(eTAF.UbicacionOrigen);
            taf.FechaEnvio = DateTime.Parse(eTAF.FechaEnvio, cin);
            taf.FechaSalida = DateTime.Parse(eTAF.FechaSalida, cin);
            taf.MotivoSalida = eTAF.MotivoSalida;
            taf.NumGuia = eTAF.NumGuia;
            taf.Paqueteria = eTAF.Paqueteria;
            taf.Estatus = eTAF.Estatus;

            foreach (var i in taf.Detalle)
            {
                i.Estatus = false;
            }
            foreach (var i in eTAF.Detalle)
            {
                if (i.Id > 0)
                {
                    foreach (var x in taf.Detalle)
                    {
                        if (i.Id == x.Id)
                        {
                            x.Estatus = true;
                        }
                    }
                }
                else
                {
                    taf.Detalle.Add(new TraspasoDetalleActivoFijo { ActivoFijo = _afRepo.GetById(i.Id), Estatus = true });
                }
            }

            _tRepo.Update(taf);

            return RedirectToAction("IndexListTraspasoAF", "ActivoFijo");
        }
    }
}