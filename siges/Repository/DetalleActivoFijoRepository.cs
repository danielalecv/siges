using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class DetalleActivoFijoRepository : Repository<DetalleActivoFijo>, IDetalleActivoFijoRepository
    {
        public DetalleActivoFijoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
