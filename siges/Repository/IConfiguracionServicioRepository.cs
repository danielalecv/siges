using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public interface IConfiguracionServicioRepository : IRepository<ConfiguracionServicio>
    {
        IQueryable<ConfiguracionServicio> GetAll(bool estatus);
        ConfiguracionServicio GetByIdCS(int id);
    }
}
