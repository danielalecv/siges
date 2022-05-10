using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class TraspasoInsumoRepository : Repository<TraspasoInsumo>, ITraspasoInsumoRepository
    {
        public TraspasoInsumoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<TraspasoInsumo> GetAll(bool estatus)
        {
            return entities.Where(r => r.Estatus == estatus).Include(r => r.UbicacionDestino).Include(r => r.UbicacionOrigen).ThenInclude(r => r.Cliente).Include(r => r.Detalle).AsQueryable();
        }

        public TraspasoInsumo GetByIdTI(int id)
        {
            return entities.Where(r => r.Id == id).Include(r => r.UbicacionDestino).ThenInclude(r => r.Cliente).Include(r => r.UbicacionOrigen).ThenInclude(r => r.Cliente).Include(r => r.Detalle).ThenInclude(r => r.Insumo).Single();
        }
    }
}
