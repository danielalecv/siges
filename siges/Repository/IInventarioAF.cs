using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace siges.Repository {

    public interface IInventarioAF : IRepository<InventarioAF> {
        IQueryable<InventarioAF> GetAllInventarioAF();
        //IQueryable<Operador> GetAtencionServicio(int OrdenServicioId);
        //IQueryable<Operador> GetAll(bool status);
        //IQueryable<Operador> GetAtencionServicioAtendidos(bool estatus);
        //IQueryable<Operador> GetAtencionServicioNoAtendidos(bool estatus);
    }
}
