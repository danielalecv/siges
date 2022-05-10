using siges.Data;
using siges.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace siges.Repository {
  public class BitacoraEstatusRepository : Repository<BitacoraEstatus>,
  IBitacoraEstatusRepository {
    public BitacoraEstatusRepository(ApplicationDbContext context) : base(context) {}

    public IQueryable<BitacoraEstatus> GetAllByOSIdIfFaceApi(){
      return null;
    }

    public BitacoraEstatus GetByOSId(int id){
      return entities.Where(r => r.Os.Id == id).Include(r => r.Os)
        .Include(r => r.QuienCambia).Single();
    }

    public IQueryable<BitacoraEstatus> GetAllByOSId(int id){
      return entities.Where(r => r.Os.Id == id).Include(r => r.Os)
        .Include(r => r.QuienCambia).AsQueryable();
    }

    public IQueryable<BitacoraEstatus> GetAllByQuienCambia(int id){
      return entities.Where(r => r.QuienCambia.Id == id).Include(r => r.Os)
        .Include(r => r.QuienCambia).AsQueryable();
    }

    public IQueryable<BitacoraEstatus> GetAllByFechaAdministrativa(DateTime fecha){
      return entities.Where(r => r.FechaAdministrativa.Equals(fecha))
        .Include(r => r.Os).Include(r => r.QuienCambia).AsQueryable();
    }

    public IQueryable<BitacoraEstatus> GetFinalizadoDescriptionByOSid(int osId){
        return entities.Where(r => r.A == "finalizado" && r.Os.Id == osId);
    }
  }
}
