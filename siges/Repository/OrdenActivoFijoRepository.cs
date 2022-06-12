using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class OrdenActivoFijoRepository : Repository<OrdenActivoFijo>, IOrdenActivoFijo
    {
        public OrdenActivoFijoRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<OrdenActivoFijo> GetAllOAFbyAFIdsencillo(int afId)
        {
            return entities.Where(r => r.ActivoFijo.Id == afId)
              .Include(r => r.ActivoFijo)
              .Include(r => r.OrdenServicio)
              .ThenInclude(r => r.Ubicacion)
              .Include(r => r.OrdenServicio)
              .ThenInclude(r => r.Servicio)
              .AsQueryable();
        }

        public IQueryable<OrdenActivoFijo> GetAllOAFbyAFId(int afId)
        {
            return entities.Where(r => r.ActivoFijo.Id == afId)
              .Include(r => r.ActivoFijo)
              .Include(r => r.OrdenServicio)
              .ThenInclude(r => r.Ubicacion)
              .Include(r => r.OrdenServicio)
              .ThenInclude(r => r.Servicio)
              .Include(r => r.OrdenServicio)
              .ThenInclude(r => r.Cliente)
              .Include(r => r.OrdenServicio)
              .ThenInclude(r => r.Contrato)
              .Include(r => r.OrdenServicio)
              .ThenInclude(r => r.LineaNegocio)
              .Include(r => r.OrdenServicio)
              .ThenInclude(r => r.PersonaComercial)
              .Include(r => r.OrdenServicio)
              .ThenInclude(r => r.PersonaValida)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Insumos)
              .ThenInclude(rOS => rOS.Insumo)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Personal)
              .ThenInclude(rOS => rOS.Persona)
              .Include(r => r.OrdenServicio)
              .ThenInclude(rOS => rOS.Activos)
              .ThenInclude(rOS => rOS.ActivoFijo)
              .AsQueryable();
        }

        public IQueryable<OrdenActivoFijo> GetAllByClienteAndByLineaNegocio(int cId, string lnNombre)
        {
            return entities.Where(r => r.OrdenServicio.Cliente.Id == cId && r.OrdenServicio.LineaNegocio.Nombre == lnNombre && r.ActivoFijo.Estatus == true)
              .Include(r => r.ActivoFijo)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Cliente)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Contrato)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Ubicacion)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.LineaNegocio)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Servicio)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.PersonaComercial)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.PersonaValida)
              .AsQueryable();
        }

        public IQueryable<OrdenActivoFijo> GetAll(bool estatus)
        {
            return entities.Where(r => r.Estatus == estatus)
              .Include(r => r.ActivoFijo)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Ubicacion)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Contrato)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Cliente)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.LineaNegocio)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Servicio)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Insumos)
              .ThenInclude(os => os.Insumo)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Personal)
              .ThenInclude(os => os.Persona)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Activos)
              .ThenInclude(os => os.ActivoFijo)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.PersonaComercial)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.PersonaValida)
              .AsQueryable();
        }

        public IQueryable<OrdenActivoFijo> GetByOSId(int id)
        {
            return entities.Where(r => r.OrdenServicio.Id == id)
              .Include(r => r.ActivoFijo)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Ubicacion)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Contrato)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Cliente)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.LineaNegocio)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Servicio)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Insumos)
              .ThenInclude(os => os.Insumo)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Personal)
              .ThenInclude(os => os.Persona)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Activos)
              .ThenInclude(os => os.ActivoFijo)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.PersonaComercial)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.PersonaValida)
              .AsQueryable();
        }

        public IQueryable<OrdenActivoFijo> GetByAFId(int id)
        {
            return entities.Where(r => r.ActivoFijo.Id == id)
              .Include(r => r.ActivoFijo)
              .Include(r => r.OrdenServicio)
              //.ThenInclude(os => os.Ubicacion)
              //.Include(r => r.OrdenServicio)
              //.ThenInclude(os => os.Contrato)
              //.Include(r => r.OrdenServicio)
              //.ThenInclude(os => os.Cliente)
              //.Include(r => r.OrdenServicio)
              .ThenInclude(os => os.LineaNegocio)
              .Include(r => r.OrdenServicio)
              .ThenInclude(os => os.Servicio)
              //.Include(r => r.OrdenServicio)
              //.ThenInclude(os => os.Insumos)
              //.ThenInclude(os => os.Insumo)
              //.Include(r => r.OrdenServicio)
              //.ThenInclude(os => os.Personal)
              //.ThenInclude(os => os.Persona)
              //.Include(r => r.OrdenServicio)
              //.ThenInclude(os => os.Activos)
              //.ThenInclude(os => os.ActivoFijo)
              //.Include(r => r.OrdenServicio)
              //.ThenInclude(os => os.PersonaComercial)
              //.Include(r => r.OrdenServicio)
              //.ThenInclude(os => os.PersonaValida)
              .AsQueryable();
        }

        public IQueryable<OrdenActivoFijo> GetOSbyIdOs(int id)
        {
            return entities.FromSqlRaw(
                "Select * " +
                "From OrdenActivoFijo " +
                "Where OrdenServicioId = {0}", id
                );
        }
    }
}