using siges.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public interface IKit : IRepository<Kit> {
    IQueryable<Kit> GetAll(bool estatus);
  }
}
