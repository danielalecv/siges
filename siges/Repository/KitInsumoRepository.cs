using siges.Data;
using siges.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace siges.Repository {
  public class KitInsumoRepository : Repository<KitInsumo>, IKitInsumo {
    public KitInsumoRepository(ApplicationDbContext context) : base(context) { }

    public IQueryable<KitInsumo> GetAllKIbyInsumoId(int iId){
      return entities.Where( r => r.Insumo.Id == iId)
        .Include(r => r.Insumo)
        .Include(r => r.Kit)
        .ThenInclude(rk => rk.Crea)
        .AsQueryable();
    }

    public IQueryable<KitInsumo> GetAll(bool estatus) {
      return entities.Where(r => r.Estatus == estatus)
        .Include(r => r.Insumo)
        .Include(r => r.Kit)
        .ThenInclude(rk => rk.Crea)
        .AsQueryable();
    }
  }
}
