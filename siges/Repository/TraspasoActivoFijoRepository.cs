using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class TraspasoActivoFijoRepository : Repository<TraspasoActivoFijo>, ITraspasoActivoFijoRepository
    {
        public TraspasoActivoFijoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<TraspasoActivoFijo> GetAll(bool estatus)
        {
            return entities.Where(r => r.Estatus == estatus).Include(r => r.UbicacionDestino).ThenInclude(r => r.Cliente).Include(r => r.UbicacionOrigen).ThenInclude(r => r.Cliente).Include(r => r.Detalle).AsQueryable();
        }

        public TraspasoActivoFijo GetByIdTAF(int id)
        {
            return entities.Where(r => r.Id == id).Include(r => r.UbicacionDestino).ThenInclude(r => r.Cliente).Include(r => r.UbicacionOrigen).ThenInclude(r => r.Cliente).Include(r => r.Detalle).ThenInclude(r => r.ActivoFijo).Single();
        }
    }
}
