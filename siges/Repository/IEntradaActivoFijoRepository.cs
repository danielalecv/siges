using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public interface IEntradaActivoFijoRepository: IRepository<EntradaActivoFijo>
    {
        IQueryable<EntradaActivoFijo> GetAll(bool estatus);
        EntradaActivoFijo GetByIdEAF(int id);
    }
}
