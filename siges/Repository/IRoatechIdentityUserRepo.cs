using System.Linq;
using siges.Models;
using siges.Areas.Identity.Data;

namespace siges.Repository {
  public interface IRoatechIdentityUserRepo : IRepository<RoatechIdentityUser>{
    IQueryable<RoatechIdentityUser> GetAllInfoById(string Id);
    IQueryable<RoatechIdentityUser> GetAllInfoByEmail(string email);
    RoatechIdentityUser GetAllOperadorSinFaceApiByRiuId(string riuId);
  }
}
