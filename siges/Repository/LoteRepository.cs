using siges.Models;
using siges.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace siges.Repository {
  public class LoteRepository : Repository<Lote>, ILote {
    public LoteRepository(ApplicationDbContext context) : base(context){}

    public IQueryable<Lote> GetAll(bool Estatus) {
      return entities.Where(r => r.Estatus == Estatus).Include(r => r.Insumo).Where(i => i.Estatus == true).Include(r => r.Persona).AsQueryable();
    }

    public IQueryable<Lote> GetLotesByInsumoId(int InsumoId){
      return entities.Where(r => r.Insumo.Id == InsumoId).Include(r => r.Insumo).Where(i => i.Estatus == true).Include(r => r.Persona).AsQueryable();
    }
    //public IQueryable<Operador> GetAtencionServicio(int OrdenServicioId) {
    //return entities.Where(r => r.OrdenServicioId == OrdenServicioId ).Include(r => r.EstadoN).ThenInclude(r => r.ArchivosEvidencia).AsQueryable();
    //}

    //public IQueryable<Insumo> GetAll(bool estatus) {
    //return entities.Where(r => r.Estatus == true).Include(r => r.Insumo).Include(r => r.Persona).AsQueryable();
    //}

    //public IQueryable<Operador> GetAtencionServicioAtendidos(bool estatus) {
    //return entities.Where(r => r.Estatus == estatus).Include(r => r.EstadoN.Where(s => s.NuevoEstado == "atendido")).AsQueryable();
    //}

    // (Derived d) => d.MyProperty'
    // context.People.Include(person => ((Student)person).School).ToList()
    // context.People.Include(person => (person as Student).School).ToList()
    // context.People.Include("Student").ToList()
    //public IQueryable<Operador> GetAtencionServicioNoAtendidos(bool estatus) {
    // No funciona
    //return entities.Include(estado => ((Operador.Estado)estado).NuevoEstado == "noatendido").AsQueryable();
    //return entities.Where(r => r.Estatus == estado).Include(r => r.EstadoN.Where((Operador.Estado s) => s.NuevoEstado == "noatendido")).AsQueryable();
    //return entities.Where(operador => operador.Estatus == estado).ThenInclude()
    //}
  }
}
