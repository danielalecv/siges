using siges.Data;
using siges.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class PaqueteRepository : Repository<Paquete>, IPaquete {
    public PaqueteRepository(ApplicationDbContext context) : base(context) { }

    public IQueryable<Paquete> GetAll(bool estatus) {
      return entities.Where(r => r.Estatus == estatus).AsQueryable();
    }
  }
}
