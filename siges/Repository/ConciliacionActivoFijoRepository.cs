using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class ConciliacionActivoFijoRepository : Repository<ConciliacionActivoFijo>, IConciliacionActivoFijoRepository
    {
        public ConciliacionActivoFijoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
