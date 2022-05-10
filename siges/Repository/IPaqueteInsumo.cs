using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace siges.Repository {

    public interface IPaqueteInsumo : IRepository<PaqueteInsumo> {
        IQueryable<PaqueteInsumo> GetAll(bool Estatus);
        IQueryable<PaqueteInsumo> GetAllPIbyInsumoId(int iId);
        IQueryable<PaqueteInsumo> GetAllPIbyPaqueteId(int pqId);
    }
}
