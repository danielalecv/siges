using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class ConfiguracionServicioRepository : Repository<ConfiguracionServicio>, IConfiguracionServicioRepository
    {
        public ConfiguracionServicioRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<ConfiguracionServicio> GetAll(bool estatus)
        {
            return entities.Where(r => r.Estatus == estatus).Include(r => r.Cliente).Include(r => r.Contrato).Include(r => r.Ubicacion).Include(r => r.Detalle).AsQueryable();
        }

        public ConfiguracionServicio GetByIdCS(int id)
        {
            return entities.Where(r => r.Id == id).Include(r => r.Ubicacion).Include(r => r.Cliente).Include(r => r.Contrato).Include(r => r.Detalle).Single();
        }
    }
}
