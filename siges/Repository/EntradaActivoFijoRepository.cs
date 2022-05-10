using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class EntradaActivoFijoRepository : Repository<EntradaActivoFijo>, IEntradaActivoFijoRepository
    {
        public EntradaActivoFijoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<EntradaActivoFijo> GetAll(bool estatus)
        {
            return entities.Where(r => r.Estatus == estatus).Include(r => r.Ubicacion).Include(r => r.Desglose).ThenInclude(r => r.Referencia).AsQueryable();
        }

        public EntradaActivoFijo GetByIdEAF(int id)
        {
            return entities.Where(r => r.Id == id).Include(r => r.Ubicacion).Include(r => r.Desglose).ThenInclude(r => r.Referencia).Single();
        }
    }
}
