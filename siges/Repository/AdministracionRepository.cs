using siges.Models;
using Microsoft.EntityFrameworkCore;
using siges.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class AdministracionRepository : Repository<Administracion>, IAdministracion {
    public AdministracionRepository(ApplicationDbContext context) : base(context){}

    public IQueryable<Administracion> GetAll(bool estatus) {
      return entities.Where(r => r.Estatus == true).AsQueryable();
    }
  }
}
