using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public interface IPaquete : IRepository<Paquete> {
    IQueryable<Paquete> GetAll(bool estatus);
  }
}
