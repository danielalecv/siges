using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public interface ITraspasoInsumoRepository: IRepository<TraspasoInsumo>
    {
        IQueryable<TraspasoInsumo> GetAll(bool estatus);
        TraspasoInsumo GetByIdTI(int id);
    }
}
