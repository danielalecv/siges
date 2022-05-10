using System.Linq;

using Microsoft.EntityFrameworkCore;

using siges.Models;
using siges.Data;

namespace siges.Repository{
  public class ContactoClienteRepository : Repository<ContactoCliente>, IContactoCliente {
    public ContactoClienteRepository(ApplicationDbContext context) : base(context) {}
    public IQueryable<ContactoCliente> GetAll(bool estatus) {
      return entities.Where(r => r.Estatus == estatus);
    }

    public IQueryable<ContactoCliente> GetByClienteId(int cId){
      return entities.Where(r => r.Cliente.Id == cId).Include(r => r.Cliente).Include(r => r.Contactos);
    }
  }
}
