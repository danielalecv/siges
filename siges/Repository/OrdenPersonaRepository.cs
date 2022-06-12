using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class OrdenPersonaRepository : Repository<OrdenPersona>, IOrdenPersona
    {
        public OrdenPersonaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<OrdenPersona> GetAll(bool estatus)
        {
            return entities.Where(r => r.Estatus == estatus).Include(r => r.Persona).Include(r => r.OrdenServicio).OrderByDescending(r => r.OrdenServicio.Id).AsQueryable();
        }

        public OrdenPersona GetById(int id)
        {
            return entities.Where(r => r.Id == id).Include(r => r.Persona).Include(r => r.OrdenServicio).Single();
        }

        public IQueryable<OrdenPersona> GetOSbyIdP(int id) {
            return entities.Where(r => r.Persona.Id == id && r.Estatus == true).Include(r => r.OrdenServicio).Where(r => r.OrdenServicio.Estatus == true).Include(r => r.OrdenServicio).ThenInclude(r => r.Ubicacion).Include(r => r.OrdenServicio).ThenInclude(r => r.Servicio).Include(r => r.OrdenServicio).ThenInclude(r => r.Cliente).Include(r => r.OrdenServicio).ThenInclude(r => r.Contrato).Include(r => r.OrdenServicio).ThenInclude(r => r.LineaNegocio).Include(r => r.OrdenServicio).ThenInclude(r => r.PersonaComercial).Include(r => r.OrdenServicio).ThenInclude(r => r.PersonaValida).Include(r => r.OrdenServicio).ThenInclude(rOS => rOS.Insumos).ThenInclude(rOS => rOS.Insumo).Include(r => r.OrdenServicio).ThenInclude(rOS => rOS.Personal).ThenInclude(rOS => rOS.Persona).Include(r => r.OrdenServicio).ThenInclude(rOS => rOS.Activos).ThenInclude(rOS => rOS.ActivoFijo).AsQueryable();
        }

        public IQueryable<OrdenPersona> GetOSbyIdOs(int id)
        {
            return entities.FromSqlRaw(
                "Select * " +
                "From OrdenPersona " +
                "Where OrdenServicioId = {0}", id
                );
        }

        public IQueryable<OrdenPersona> GetOrdenServicioDetail(int osId){
          return entities.Include(r => r.OrdenServicio).Where(r => r.OrdenServicio.Id == osId).Include(r => r.OrdenServicio).ThenInclude(r => r.Servicio).Include(r => r.OrdenServicio).ThenInclude(r => r.Ubicacion).Include(r => r.OrdenServicio).ThenInclude(r => r.Contrato).Include(r => r.Persona).OrderByDescending(r => r.OrdenServicio.Id);
        }
    }
}
