using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public interface ITraspasoActivoFijoRepository: IRepository<TraspasoActivoFijo>
    {
        IQueryable<TraspasoActivoFijo> GetAll(bool estatus);
        TraspasoActivoFijo GetByIdTAF(int id);
    }
}
