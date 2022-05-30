using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        IQueryable<Cliente> GetAll(bool estatus);
        bool Exist(string rfc);
        bool ExistById(int client_id);
    }
}