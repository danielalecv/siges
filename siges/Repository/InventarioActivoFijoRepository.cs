using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class InventarioActivoFijoRepository : Repository<InventarioActivoFijo>, IInventarioActivoFijoRepository
    {
        public InventarioActivoFijoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
