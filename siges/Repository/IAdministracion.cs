using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {

    public interface IAdministracion : IRepository<Administracion> {
        IQueryable<Administracion> GetAll(bool status);
    }
}
