using System.Linq;

using siges.Models;
using siges.Data;

namespace siges.Repository{
  public class MarcaRepository : Repository<Marca>, IMarca {
    public MarcaRepository(ApplicationDbContext context) : base(context) {}
    public IQueryable<Marca> GetAll(bool estatus) {
      return entities.Where(r => r.Estatus == estatus);
    }
  }
}
