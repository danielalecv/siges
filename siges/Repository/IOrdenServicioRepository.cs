using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public interface IOrdenServicioRepository : IRepository<OrdenServicio>
    {
        IQueryable<OrdenServicio> GetAll(bool estatus);
        OrdenServicio GetByIdOS(int id);
        IQueryable<OrdenServicio> GetByPersonaComercialId(int id);
        OrdenServicio GetOSbyFolio(string folio);
        IQueryable<OrdenServicio> GetOrdenServicio();
        IQueryable<OrdenServicio> GetAllDeleted();
        bool IsCancelable(int osId);
        //IQueryable<OrdenServicio> GetOrdenServicioDetail(int id);
    }
}
