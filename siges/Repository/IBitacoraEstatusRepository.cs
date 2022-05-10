using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public interface IBitacoraEstatusRepository : IRepository<BitacoraEstatus> {
    BitacoraEstatus GetByOSId(int id);
    IQueryable<BitacoraEstatus> GetAllByQuienCambia(int id);
    IQueryable<BitacoraEstatus> GetAllByFechaAdministrativa(DateTime fecha);
    IQueryable<BitacoraEstatus> GetAllByOSId(int id);
    IQueryable<BitacoraEstatus> GetFinalizadoDescriptionByOSid(int osId);
  }
}
