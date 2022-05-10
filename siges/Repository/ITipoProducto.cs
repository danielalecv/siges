using siges.Models;

using System.Linq;

namespace siges.Repository {
  public interface ITipoProducto : IRepository<TipoProducto> {
    IQueryable<TipoProducto> GetAll(bool estatus);
  }
}
