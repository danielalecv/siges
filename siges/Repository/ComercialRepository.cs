using siges.Models;
using Microsoft.EntityFrameworkCore;
using siges.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class ComercialRepository : Repository<Comercial>, IComercial {

    public ComercialRepository(ApplicationDbContext context) : base(context){}

    public IQueryable<Comercial> GetAll(bool estatus) {
      return entities.Where(r => r.Estatus == true).AsQueryable();
    }

  }
}
