using siges.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public interface IClienteIdentity : IRepository<ClienteIdentity> {
    IQueryable<ClienteIdentity> GetAll(bool estatus);
    ClienteIdentity GetByRiuId(string id);
    /*IQueryable<ClienteIdentity> GetByClienteId(int id);
    IQueryable<ClienteIdentity> GetByIdentityEmail(string email);*/
  }
}
