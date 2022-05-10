using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class ClienteRepository : Repository<Cliente>, IClienteRepository {
    public ClienteRepository(ApplicationDbContext context) : base(context) { }
    public IQueryable<Cliente> GetAll(bool estatus) {
      return entities.Where(r => r.Estatus == estatus).OrderBy(r => r.RazonSocial);
    }

    public bool Exist(string rfc) {
      var c = entities.Where(r => r.Estatus == true && r.RFC == rfc);
      if(c.Any())
        return true;
      else
        return false;
    }
  }
}
