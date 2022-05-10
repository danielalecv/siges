using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class InventarioInsumoRepository : Repository<InventarioInsumo>, IInventarioInsumoRepository
    {
        public InventarioInsumoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
