using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
  public interface IProducto : IRepository<Producto>
  {
    IQueryable<Producto> GetAll(bool estatus);
    IQueryable<Producto> GetAllByProdName(string prod);
  }
}
