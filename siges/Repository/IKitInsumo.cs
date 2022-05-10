using System.Linq;
using siges.Models;

namespace siges.Repository {
  public interface IKitInsumo : IRepository<KitInsumo> {
    IQueryable<KitInsumo> GetAll(bool estatus);
    IQueryable<KitInsumo> GetAllKIbyInsumoId(int iId);
  }
}
