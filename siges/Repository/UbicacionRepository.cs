using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class UbicacionRepository : Repository<Ubicacion>, IUbicacionRepository {
    public UbicacionRepository(ApplicationDbContext context) : base(context) { }

    public IQueryable<Ubicacion> GetAll(bool estatus) {
      return entities.Where(r => r.Estatus == estatus).Include(r => r.Cliente).OrderBy(r => r.Cliente.RazonSocial);
    }

    public IQueryable<Ubicacion> getUbicacionesByCliente(int clienteid) {
      return entities.Where(r => r.ClienteId == clienteid && r.Estatus == true).AsQueryable();
    }

    public bool Exist(int clienteid, string nombre){
      var u = entities.Where(r => r.Estatus == true && r.ClienteId == clienteid && r.Nombre == nombre);
      if(u.Any()){
        return true;
      }else{
        return false;
      }
    }
  }
}
