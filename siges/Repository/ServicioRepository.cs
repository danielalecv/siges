using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class ServicioRepository : Repository<Servicio>, IServicioRepository {
    public ServicioRepository(ApplicationDbContext context) : base(context) { }

    public IQueryable<Servicio> GetAll(bool estatus) {
      return entities.Where(r => r.Estatus == estatus).OrderBy(r => r.Nombre).AsQueryable();
    }

    public IQueryable<Servicio> GetServiciosByLineaNegocio(int lnId){
      return entities.Where(r => r.Estatus == true && r.LineaNegocioId == lnId).OrderBy(r => r.Nombre).AsQueryable();
    }
  }
}
