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
  public class InsumoController : Controller
    {
        private readonly IInsumoRepository _iRepo;
        private readonly IEntradaInsumoRepository _entradaIRepo;
        private readonly IContratoRepository _cRepo;
        private readonly IUbicacionRepository _uRepo;
        private readonly ITraspasoInsumoRepository _tRepo;

        public InsumoController(IInsumoRepository repository, IEntradaInsumoRepository entradairepo, IContratoRepository cRepo, IUbicacionRepository uRepo, ITraspasoInsumoRepository tRepo)
        {
            _iRepo = repository;
            _entradaIRepo = entradairepo;
            _cRepo = cRepo;
            _uRepo = uRepo;
            _tRepo = tRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        /* --------- Entradas ------------ */
        /* Listado entrada de insumos */
        public IActionResult IndexListEntradaI()
        {
            List<EntradaInsumo> entradas = _entradaIRepo.GetAll(true).ToList();
            ViewData["entradas"] = entradas;
            return View("~/Views/Insumos/ListEntradaInsumo.cshtml");
        }

        public IActionResult DeleteEntradaI(int eiid)
        {
            EntradaInsumo nEAF = _entradaIRepo.GetById(eiid);
            nEAF.Estatus = false;
            _entradaIRepo.Update(nEAF);
            return RedirectToAction("IndexListEntradaI", "Insumo");
            //return View();
        }

        /* Crear entrada de insumos */
        public IActionResult IndexCreateEntradaI()
        {
            List<Contrato> contratos = _cRepo.GetAll().ToList();
            ViewData["contratos"] = contratos;
            return View("~/Views/Insumos/CreateEntradaInsumo.cshtml");
        }

        public IActionResult SaveEI(EntradaInsumoDTO nEI)
        {
            EntradaInsumo ei = new EntradaInsumo();
            CultureInfo cin = new CultureInfo("es-MX");
            ei.Pedido = nEI.Pedido;
            ei.Ubicacion = _uRepo.GetById(nEI.Ubicacion);
            ei.Tipo = nEI.Tipo;
            ei.FechaPedido = DateTime.Parse(nEI.FechaPedido, cin);
            ei.FechaRecepcion = DateTime.Parse(nEI.FechaRecepcion, cin);
            ei.Incidencia = nEI.Incidencia;
            ei.Observaciones = nEI.Observaciones;
            ei.Estatus = nEI.Estatus;
            foreach (var i in nEI.Desglose)
            {
                ei.Desglose.Add(new DetalleInsumo { Referencia = _iRepo.GetById(i.Referencia), ClaveInsumo = i.ClaveInsumo, Cantidad = i.Cantidad, Observaciones = i.Observaciones, Unidad = i.Unidad, Estatus = true });
            }

            _entradaIRepo.Insert(ei);

            return RedirectToAction("IndexListEntradaI", "Insumo");
        }

        /* Editar entrada de insumos */
        public IActionResult IndexEditEntradaI(int entradaId)
        {
            EntradaInsumo ei = _entradaIRepo.GetByIdEI(entradaId);
            List<Insumo> insumos = _iRepo.GetAll().ToList();
            List<Contrato> contratos = _cRepo.GetAll().ToList();
            //List<Ubicacion> ubicaciones = _uRepo.getUbicacionesByContrato(ei.Ubicacion.ContratoId).ToList();
            ViewData["entrada"] = ei;
            ViewData["insumos"] = insumos;
            ViewData["contratos"] = contratos;
            //ViewData["ubicaciones"] = ubicaciones;
            return View("~/Views/Insumos/EditEntradaInsumo.cshtml");
        }

        public IActionResult EditEI(EntradaInsumoDTO eEI)
        {
            EntradaInsumo ei = _entradaIRepo.GetByIdEI(eEI.Id);
            CultureInfo cin = new CultureInfo("es-MX");
            ei.Pedido = eEI.Pedido;
            ei.Ubicacion = _uRepo.GetById(eEI.Ubicacion);
            ei.Tipo = eEI.Tipo;
            ei.FechaPedido = DateTime.Parse(eEI.FechaPedido, cin);
            ei.FechaRecepcion = DateTime.Parse(eEI.FechaRecepcion, cin);
            ei.Incidencia = eEI.Incidencia;
            ei.Observaciones = eEI.Observaciones;
            ei.Estatus = eEI.Estatus;
            foreach (var i in ei.Desglose)
            {
                i.Estatus = false;
            }
            foreach (var i in eEI.Desglose)
            {
                if (i.Id > 0)
                {
                    foreach (var x in ei.Desglose)
                    {
                        if (i.Id == x.Id)
                        {
                            x.Estatus = true;
                        }
                    }
                }
                else
                {
                    ei.Desglose.Add(new DetalleInsumo { Referencia = _iRepo.GetById(i.Referencia), ClaveInsumo = i.ClaveInsumo, Cantidad = i.Cantidad, Observaciones = i.Observaciones, Unidad = i.Unidad, Estatus = true });
                }
            }

            _entradaIRepo.Update(ei);

            return RedirectToAction("IndexListEntradaI", "Insumo");
        }

        /* --------- Traspasos ------------ */
        /* Listado traspasos de insumos */
        public IActionResult IndexListTraspasoI()
        {
            List<TraspasoInsumo> traspasos = _tRepo.GetAll(true).ToList();
            ViewData["traspasos"] = traspasos;
            return View("~/Views/Insumos/ListTraspasoInsumo.cshtml");
        }

        public IActionResult DeleteTraspasoI(int tiid)
        {
            TraspasoInsumo nTI = _tRepo.GetById(tiid);
            nTI.Estatus = false;
            _tRepo.Update(nTI);
            return RedirectToAction("IndexListTraspasoI", "Insumo");
            //return View();
        }
        /* Crear traspasos de insumos */
        public IActionResult IndexCreateTraspasoI()
        {
            List<Contrato> contratos = _cRepo.GetAll().ToList();
            List<Insumo> insumos = _iRepo.GetAll().ToList();
            ViewData["contratos"] = contratos;
            ViewData["insumos"] = insumos;
            return View("~/Views/Insumos/CreateTraspasoInsumo.cshtml");
        }

        public IActionResult SaveTI(TraspasoInsumoDTO nTI)
        {
            TraspasoInsumo ti = new TraspasoInsumo();
            CultureInfo cin = new CultureInfo("es-MX");
            ti.Folio = nTI.Folio;
            ti.UbicacionDestino = _uRepo.GetById(nTI.UbicacionDestino);
            ti.UbicacionOrigen = _uRepo.GetById(nTI.UbicacionOrigen);
            ti.FechaEnvio = DateTime.Parse(nTI.FechaEnvio, cin);
            ti.FechaSalida = DateTime.Parse(nTI.FechaSalida, cin);
            ti.MotivoSalida = nTI.MotivoSalida;
            ti.NumGuia = nTI.NumGuia;
            ti.Paqueteria = nTI.Paqueteria;
            ti.Estatus = nTI.Estatus;
            foreach (var i in nTI.Detalle)
            {
                ti.Detalle.Add(new TraspasoDetalleInsumo { Insumo = _iRepo.GetById(i.Id), Estatus = true });
            }

            _tRepo.Insert(ti);

            return RedirectToAction("IndexListTraspasoI", "Insumo");
        }

        /* Editar traspasos de insumos */
        public IActionResult IndexEditTraspasoI(int traspasoId)
        {
            TraspasoInsumo ti = _tRepo.GetByIdTI(traspasoId);
            List<Contrato> contratos = _cRepo.GetAll().ToList();
            //List<Ubicacion> ubicacionesOrig = _uRepo.getUbicacionesByContrato(ti.UbicacionOrigen.ContratoId).ToList();
            //List<Ubicacion> ubicacionesDes = _uRepo.getUbicacionesByContrato(ti.UbicacionDestino.ContratoId).ToList();
            List<Insumo> insumos = _iRepo.GetAll().ToList();
            ViewData["traspaso"] = ti;
            ViewData["contratos"] = contratos;
            ViewData["insumos"] = insumos;
            //ViewData["ubicacionesOrigen"] = ubicacionesOrig;
            //ViewData["ubicacionesDestino"] = ubicacionesDes;
            return View("~/Views/Insumos/EditTraspasoInsumo.cshtml");
        }

        public IActionResult EditTI(TraspasoInsumoDTO eTI)
        {
            TraspasoInsumo ti = _tRepo.GetByIdTI(eTI.Id);
            CultureInfo cin = new CultureInfo("es-MX");
            ti.Folio = eTI.Folio;
            ti.UbicacionDestino = _uRepo.GetById(eTI.UbicacionDestino);
            ti.UbicacionOrigen = _uRepo.GetById(eTI.UbicacionOrigen);
            ti.FechaEnvio = DateTime.Parse(eTI.FechaEnvio, cin);
            ti.FechaSalida = DateTime.Parse(eTI.FechaSalida, cin);
            ti.MotivoSalida = eTI.MotivoSalida;
            ti.NumGuia = eTI.NumGuia;
            ti.Paqueteria = eTI.Paqueteria;
            ti.Estatus = eTI.Estatus;

            foreach (var i in ti.Detalle)
            {
                i.Estatus = false;
            }
            foreach (var i in eTI.Detalle)
            {
                if (i.Id > 0)
                {
                    foreach (var x in ti.Detalle)
                    {
                        if (i.Id == x.Id)
                        {
                            x.Estatus = true;
                        }
                    }
                }
                else
                {
                    ti.Detalle.Add(new TraspasoDetalleInsumo { Insumo = _iRepo.GetById(i.Id), Estatus = true });
                }
            }

            _tRepo.Update(ti);

            return RedirectToAction("IndexListTraspasoI", "Insumo");
        }

    }
}