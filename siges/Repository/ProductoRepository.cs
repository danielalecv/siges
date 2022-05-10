using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
  public class ProductoRepository: Repository<Producto>, IProducto
  {
    public ProductoRepository(ApplicationDbContext context): base(context)
    {
    }

    public IQueryable<Producto> GetAll(bool estatus)
    {
      return entities.Where(r => r.Estatus == estatus).AsQueryable();
    }

    public IQueryable<Producto> GetAllByProdName(string prod) {
      return entities.Where(r => r.Nombre == prod && r.Estatus == true).AsQueryable();
    }
  }
}
