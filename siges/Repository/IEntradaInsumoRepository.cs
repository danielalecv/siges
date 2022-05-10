using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public interface IEntradaInsumoRepository : IRepository<EntradaInsumo>
    {
        IQueryable<EntradaInsumo> GetAll(bool estatus);
        EntradaInsumo GetByIdEI(int id);
    }
}
