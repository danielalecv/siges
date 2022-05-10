using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public interface IInsumoRepository: IRepository<Insumo> {
    IQueryable<Insumo> GetAll(bool estatus);
    bool Exist(string clave);
  }
}
