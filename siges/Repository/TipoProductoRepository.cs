using System.Linq;

using siges.Models;
using siges.Data;

namespace siges.Repository{
  public class TipoProductoRepository : Repository<TipoProducto>, ITipoProducto {
    public TipoProductoRepository(ApplicationDbContext context) : base(context) {}
    public IQueryable<TipoProducto> GetAll(bool estatus) {
      return entities.Where(r => r.Estatus == estatus);
    }
  }
}
