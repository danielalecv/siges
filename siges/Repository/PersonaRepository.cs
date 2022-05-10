using siges.Data;
using siges.Models;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class PersonaRepository : Repository<Persona>, IPersonaRepository{
    public PersonaRepository(ApplicationDbContext context) : base(context){}

    public IQueryable<Persona> GetByEmail(string email){
      return entities.Where(r => r.Email == email && r.Estatus == true);
    }

    public IQueryable<Persona> GetAll(bool estatus){
      return entities.Where(r => r.Estatus == estatus).OrderBy(r => r.Nombre);
    }

    public bool Exist(string RFC, string CURP){
      var p = entities.Where(r => r.Estatus == true && r.RFC == RFC && r.CURP == CURP);
      if(p.Any()){
        return true;
      } else {
        return false;
      }
    }
  }
}
