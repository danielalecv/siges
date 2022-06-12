using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class OrdenInsumoRepository : Repository<OrdenInsumo>, IOrdenInsumo
    {
        public OrdenInsumoRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<OrdenInsumo> GetOrdenesInsumosByIdInsumo(int iId)
        {
            return entities.Where(r => r.Insumo.Id == iId)
              .Include(r => r.Insumo)
              /*.Include(r => r.Lote)*/
              .Include(r => r.OrdenServicio)
              .AsQueryable();
        }

        public IQueryable<OrdenInsumo> GetAllOIbyInsumoIdsencillo(int iId)
        {
            return entities.Where(r => r.Insumo.Id == iId)
              .Include(r => r.Insumo)
              .Include(r => r.LoteType)
              .Include(r => r.OrdenServicio)
              .AsQueryable();
        }

        public IQueryable<OrdenInsumo> GetAllOIbyInsumoId(int iId)
        {
            return entities.Where(r => r.Insumo.Id == iId)
              .Include(r => r.Insumo)
              /*.Include(r => r.Lote)*/
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Cliente)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Contrato)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Ubicacion)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.LineaNegocio)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Servicio)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Insumos)
              .ThenInclude(rOS => rOS.Insumo)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Personal)
              .ThenInclude(rOS => rOS.Persona)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Activos)
              .ThenInclude(rOS => rOS.ActivoFijo)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.PersonaComercial)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.PersonaValida)
              .AsQueryable();
        }

        public IQueryable<OrdenInsumo> GetAllsencillo(bool estatus)
        {
            return entities.Where(r => r.Estatus == estatus)
              .Include(r => r.Insumo)
              /*.Include(r => r.Lote)*/
              .Include(r => r.OrdenServicio)
              .AsQueryable();
        }

        public IQueryable<OrdenInsumo> GetAll(bool estatus)
        {
            return entities.Where(r => r.Estatus == estatus)
              .Include(r => r.Insumo)
              /*.Include(r => r.Lote)*/
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Cliente)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Contrato)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Ubicacion)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.LineaNegocio)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Servicio)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Insumos)
              .ThenInclude(rOS => rOS.Insumo)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Personal)
              .ThenInclude(rOS => rOS.Persona)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Activos)
              .ThenInclude(rOS => rOS.ActivoFijo)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.PersonaComercial)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.PersonaValida)
              .AsQueryable();
        }

        public OrdenInsumo GetOIbyIdI(int Iid)
        {
            return entities.Where(r => r.Id == Iid)
              .Include(r => r.Insumo)
              /*.Include(r => r.Lote)*/
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Cliente)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Contrato)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Ubicacion)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.LineaNegocio)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Servicio)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Insumos)
              .ThenInclude(rOS => rOS.Insumo)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Personal)
              .ThenInclude(rOS => rOS.Persona)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Activos)
              .ThenInclude(rOS => rOS.ActivoFijo)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.PersonaComercial)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.PersonaValida)
              .Single();
        }
        public IQueryable<OrdenInsumo> GetOSbyIdOs(int id)
        {
            return entities.FromSqlRaw(
                "Select * " +
                "From OrdenInsumo " +
                "Where OrdenServicioId = {0}", id
                );
        }
    }
}