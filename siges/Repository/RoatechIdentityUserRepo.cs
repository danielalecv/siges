using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using siges.Areas.Identity.Data;
using siges.Data;

namespace siges.Repository {
  public class RoatechIdentityUserRepo : Repository<RoatechIdentityUser>, IRoatechIdentityUserRepo {
    public RoatechIdentityUserRepo(ApplicationDbContext context) : base(context){}
    public IQueryable<RoatechIdentityUser> GetAllInfoById(string Id) {
      return entities.Where(r => r.Id == Id).Include(r => r.per).Where(r => r.per.Estatus == true).Include(r => r.dir);
    }
    /*public IQueryable<RoatechIdentityUser> GetAllInfoByEmail(string email) {
      return entities.Where(r => r.Email == email).Include(r => r.per).Where(r => r.per.Estatus == true).Include(r => r.dir);
    }*/
    public IQueryable<RoatechIdentityUser> GetAllInfoByEmail(string email) {
      return entities.Where(r => r.Email == email).Include(r => r.per).Where(r => r.per.Estatus == true).Include(r => r.dir);
    }
    
    public RoatechIdentityUser GetAllOperadorSinFaceApiByRiuId(string riuId){
      return entities.Where(r => r.Id == riuId).Include(r => r.per).Where(r => r.per.Estatus == true && r.per.FaceApiId != "SINFACEAPIID").Single();
    }
  }
}
