using siges.Models;
using Microsoft.EntityFrameworkCore;
using siges.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class OperadorRepository : Repository<Operador>, IOperador {
    public OperadorRepository(ApplicationDbContext context) : base(context){}

    public IQueryable<Operador> GetAtencionServicio(int OrdenServicioId) {
      return entities.Where(r => r.OrdenServicio.Id == OrdenServicioId ).Include(r => r.OrdenServicio).Include(r => r.Persona).Include(r => r.EstadoN).ThenInclude(r => r.ArchivosEvidencia).AsQueryable();
    }

    public IQueryable<Operador> GetAll(bool estatus) {
      return entities.Where(r => r.Estatus == true).Include(r => r.OrdenServicio).Include(r => r.Persona).Include(r => r.EstadoN).ThenInclude(r => r.ArchivosEvidencia).AsQueryable();
    }

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
