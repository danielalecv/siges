using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class DetalleInsumoRepository : Repository<DetalleInsumo>, IDetalleInsumoRepository
    {
        public DetalleInsumoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
