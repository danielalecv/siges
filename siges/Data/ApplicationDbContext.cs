using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using siges.Models;
using siges.Areas.Identity.Data;

namespace siges.Data {
  public class ApplicationDbContext : IdentityDbContext<RoatechIdentityUser> {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<siges.Models.Contrato> Contrato { get; set; }
    public DbSet<siges.Models.ActivoFijo> ActivoFijo { get; set; }
    public DbSet<siges.Models.Insumo> Insumo { get; set; }
    public DbSet<siges.Models.LineaNegocio> LineaNegocio { get; set; }
    public DbSet<siges.Models.Persona> Persona { get; set; }
    public DbSet<siges.Models.Servicio> Servicio { get; set; }
    public DbSet<siges.Models.Ubicacion> Ubicacion { get; set; }
    public DbSet<siges.Models.Cliente> Cliente { get; set; }
    public DbSet<siges.Models.DetalleActivoFijo> DetalleActivoFijo { get; set; }
    public DbSet<siges.Models.EntradaActivoFijo> EntradaActivoFijo { get; set; }
    public DbSet<siges.Models.DetalleInsumo> DetalleInsumo { get; set; }
    public DbSet<siges.Models.EntradaInsumo> EntradaInsumo { get; set; }
    public DbSet<siges.Models.DetalleConfiguracionServicio> DetalleConfiguracionServicio { get; set; }
    public DbSet<siges.Models.ConfiguracionServicio> ConfiguracionServicio { get; set; }
    public DbSet<siges.Models.OrdenServicio> OrdenServicio { get; set; }
    public DbSet<siges.Models.ConciliacionActivoFijo> ConciliacionActivoFijo { get; set; }
    public DbSet<siges.Models.InventarioActivoFijo> InventarioActivoFijo { get; set; }
    public DbSet<siges.Models.ConciliacionInsumo> ConciliacionInsumo { get; set; }
    public DbSet<siges.Models.InventarioInsumo> InventarioInsumo { get; set; }
    public DbSet<siges.Models.TraspasoActivoFijo> TraspasoActivoFijo { get; set; }
    public DbSet<siges.Models.TraspasoInsumo> TraspasoInsumo { get; set; }
    public DbSet<siges.Models.Bitacora> Bitacora { get; set; }
    public DbSet<siges.Models.Operador> Operador { get; set; }
    public DbSet<siges.Models.InventarioAF> InventarioAF { get; set; }
    public DbSet<siges.Models.Administracion> Administracion { get; set; }
    public DbSet<siges.Models.Comercial> Comercial {get; set;}
    public DbSet<siges.Models.BitacoraEstatus> BitacoraEstatus {get; set;}
    public DbSet<siges.Models.Settings> Settings {get; set;}
    public DbSet<siges.Models.OrdenActivoFijo> OrdenActivoFijo {get; set;}
    public DbSet<siges.Models.Producto> Producto {get; set;}
    public DbSet<siges.Models.Direccion> Direccion {get; set;}
    public DbSet<siges.Models.SemaphoreParams> SemaphoreParams {get; set;}
    public DbSet<siges.Models.OrdenInsumo> OrdenInsumo {get; set;}
    public DbSet<siges.Models.Lote> Lote {get; set;}
    public DbSet<siges.Models.Paquete> Paquete {get; set;}
    public DbSet<siges.Models.Kit> Kit {get; set;}
    public DbSet<siges.Models.PaqueteInsumo> PaqueteInsumo {get; set;}
    public DbSet<siges.Models.KitInsumo> KitInsumo {get; set;}
    public DbSet<siges.Models.ClienteIdentity> ClienteIdentity {get; set;}
    public DbSet<siges.Models.EstructuraExifBi> EstructuraExifBi {get; set;}
    public DbSet<siges.Models.EstructuraCustomVision> EstructuraCustomVision {get; set;}
    public DbSet<siges.Models.Marca> Marca {get; set;}
    public DbSet<siges.Models.TipoProducto> TipoProducto {get; set;}
    public DbSet<siges.Models.ContactoCliente> ContactoCliente {get; set;}
    public DbSet<siges.DTO.IndexListOSDTO> IndexListOSDTO {get; set;}
    public DbSet<siges.Models.OsRecurrente> OsRecurrente { get; set; }
  }
}
