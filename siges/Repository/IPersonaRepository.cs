using siges.Models;

using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public interface IPersonaRepository : IRepository<Persona>{
    IQueryable<Persona> GetByEmail(string email);
    IQueryable<Persona> GetAll(bool status);
    bool Exist(string RFC, string CURP);
  }
}
