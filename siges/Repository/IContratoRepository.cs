using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public interface IContratoRepository: IRepository<Contrato>
    {
        IQueryable<Contrato> GetAll(bool estatus);

        IQueryable<Contrato> GetContratoByCliente(int clienteid);
    }
}
