using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public interface IOrdenActivoFijo : IRepository<OrdenActivoFijo>
    {
        IQueryable<OrdenActivoFijo> GetAll(bool estatus);
        IQueryable<OrdenActivoFijo> GetByOSId(int id);
        IQueryable<OrdenActivoFijo> GetByAFId(int id);
        IQueryable<OrdenActivoFijo> GetAllByClienteAndByLineaNegocio(int cId, string lnNombre);
        IQueryable<OrdenActivoFijo> GetAllOAFbyAFId(int afId);
        IQueryable<OrdenActivoFijo> GetAllOAFbyAFIdsencillo(int afId);
        IQueryable<OrdenActivoFijo> GetOSbyIdOs(int id);
    }
}
