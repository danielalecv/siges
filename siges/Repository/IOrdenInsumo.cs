using System.Linq;
using siges.Models;

namespace siges.Repository {
  public interface IOrdenInsumo : IRepository<OrdenInsumo> {
    IQueryable<OrdenInsumo> GetAll(bool estatus);
    IQueryable<OrdenInsumo> GetAllsencillo(bool estatus);
    IQueryable<OrdenInsumo> GetAllOIbyInsumoId(int iId);
    IQueryable<OrdenInsumo> GetAllOIbyInsumoIdsencillo(int iId);
    OrdenInsumo GetOIbyIdI(int Iid);
    IQueryable<OrdenInsumo> GetOrdenesInsumosByIdInsumo(int iId);
  }
}
