using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class DetalleConfiguracionServicioRepository : Repository<DetalleConfiguracionServicio>, IDetalleConfiguracionServicioRepository
    {
        public DetalleConfiguracionServicioRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
