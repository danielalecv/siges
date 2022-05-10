using siges.Models;

using System.Linq;

namespace siges.Repository {
  public interface IContactoCliente : IRepository<ContactoCliente> {
    IQueryable<ContactoCliente> GetAll(bool estatus);
    IQueryable<ContactoCliente> GetByClienteId(int cId);
  }
}
