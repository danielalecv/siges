using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class ConciliacionInsumoRepository : Repository<ConciliacionInsumo>, IConciliacionInsumoRepository
    {
        public ConciliacionInsumoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
