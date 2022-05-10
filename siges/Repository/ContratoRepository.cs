using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
  public class ContratoRepository : Repository<Contrato>, IContratoRepository
  {
    public ContratoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<Contrato> GetAll(bool estatus)
    {
      return entities.Where(r => r.Estatus == estatus).Include(r => r.Cliente).Include(r => r.Servicio).OrderBy(r => r.Nombre).AsQueryable();
    }

    public IQueryable<Contrato> GetContratoByCliente(int clienteid)
    {
      return entities.Where(r => r.ClienteId == clienteid && r.Estatus == true).Include(r => r.Cliente).Include(r => r.Servicio).AsQueryable();
    }
  }
}
