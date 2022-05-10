using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public interface IUbicacionRepository: IRepository<Ubicacion>
    {
        IQueryable<Ubicacion> GetAll(bool estatus);
        IQueryable<Ubicacion> getUbicacionesByCliente(int clienteid);
        bool Exist(int clienteid, string nombre);
    }
}
