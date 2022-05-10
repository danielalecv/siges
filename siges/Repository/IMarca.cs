using siges.Models;

using System.Linq;

namespace siges.Repository {
  public interface IMarca : IRepository<Marca> {
    IQueryable<Marca> GetAll(bool estatus);
  }
}
