using siges.Data;
using siges.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace siges.Repository {
  public class ClienteIdentityRepository : Repository<ClienteIdentity>, IClienteIdentity {
    public ClienteIdentityRepository(ApplicationDbContext context) : base(context) { }
    public IQueryable<ClienteIdentity> GetAll(bool estatus) {
      return entities.Where(r => r.Estatus == estatus)/*.Include(r => r.Cliente).Include(r = r.CuentaUsuario).ThenInclude(r => r.CuentaUsuario.per).OrderBy(r => r.Cliente.RazonSocial)*/;
    }

    public ClienteIdentity GetByRiuId(string id){
      try{
        return entities.Where(r => r.Estatus == true && r.CuentaUsuario.Id == id).Include(r => r.Cliente).Include(r => r.CuentaUsuario).Single();
      } catch (Exception) {
        return null;
      }
      return null;
    }

    /*public IQueryable<ClienteIdentity> GetByClienteId(int id){
      return entities.Where(r => r.Cliente.Id == id).Include(r => r.Cliente).Include(r => r.CuentaUsuario).ThenInclude(r => r.CuentaUsuario.per).OrderBy(r => r.Cliente.RazonSocial);
    }

    public IQueryable<ClienteIdentity> GetByIdentityEmail(string email){
      return entities.Where(r => r.CuentaUsuario.Email == email).Include(r => r.Cliente).Include(r => r.CuentaUsuario).ThenInclude(r => r.CuentaUsuario.per).OrderBy(r => r.Cliente.RazonSocial);
    }*/
  }
}
