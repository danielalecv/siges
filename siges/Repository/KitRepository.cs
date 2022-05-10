using siges.Data;
using siges.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class KitRepository : Repository<Kit>, IKit {
    public KitRepository(ApplicationDbContext context) : base(context) { }

    public IQueryable<Kit> GetAll(bool estatus) {
      return entities.Where(r => r.Estatus == estatus).AsQueryable();
    }
  }
}
