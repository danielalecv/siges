using siges.Models;
using Microsoft.EntityFrameworkCore;
using siges.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class PaqueteInsumoRepository : Repository<PaqueteInsumo>, IPaqueteInsumo {
    public PaqueteInsumoRepository(ApplicationDbContext context) : base(context){}

    public IQueryable<PaqueteInsumo> GetAll(bool Estatus) {
      return entities.Where(r => r.Estatus == Estatus)
        .Include(r => r.Paquete)
        .ThenInclude(p => p.Crea)
        .Include(r => r.Insumo)
        .AsQueryable();
    }

    public IQueryable<PaqueteInsumo> GetAllPIbyInsumoId(int iId) {
      return entities.Where(r => r.Insumo.Id == iId)
        .Include(r => r.Paquete)
        .ThenInclude(rp => rp.Crea)
        .Include(r => r.Insumo)
        .AsQueryable();
    }

    public IQueryable<PaqueteInsumo> GetAllPIbyPaqueteId(int pqId) {
      return entities.Where(r => r.Paquete.Id == pqId)
        .Include(r => r.Paquete)
        .ThenInclude(rp => rp.Crea)
        .Include(r => r.Insumo)
        .AsQueryable();
    }
  }
}
