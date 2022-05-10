using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class LineaNegocioRepository : Repository<LineaNegocio>, ILineaNegocioRepository
    {
        public LineaNegocioRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<LineaNegocio> GetAll(bool estatus)
        {
            return entities.Where(r => r.Estatus == estatus).OrderBy(r => r.Nombre).AsQueryable();
        }
    }
}
