using System.Linq;
using siges.Models;

namespace siges.Repository {
    public interface IOrdenPersona : IRepository<OrdenPersona> {
        IQueryable<OrdenPersona> GetOSbyIdP(int id);
        IQueryable<OrdenPersona> GetOrdenServicioDetail(int osId);
        IQueryable<OrdenPersona> GetOSbyIdOs(int id);
    }
}
