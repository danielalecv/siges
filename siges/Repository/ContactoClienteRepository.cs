using System.Linq;

using Microsoft.EntityFrameworkCore;

using siges.Models;
using siges.Data;
using System;

namespace siges.Repository
{
    public class ContactoClienteRepository : Repository<ContactoCliente>, IContactoCliente
    {
        public ContactoClienteRepository(ApplicationDbContext context) : base(context) { }
        public IQueryable<ContactoCliente> GetAll(bool estatus)
        {
            return entities.Where(r => r.Estatus == estatus);
        }
        public IQueryable<ContactoCliente> GetByClienteId(int cId)
        {
            var result = entities.Where(r => r.Cliente.Id == cId).Include(r => r.Cliente).Include(r => r.Contactos).FirstOrDefault();
            if(result != null)
            {
                return entities.Where(r => r.Cliente.Id == cId).Include(r => r.Cliente).Include(r => r.Contactos);
            }
            else
            {
                return null;
            }
        }
        public ContactoCliente GetByClienteToken(Persona personaToken)
        {
            var result = entities.Include(d => d.Cliente).Where(c => c.Contactos.Contains(personaToken)).FirstOrDefault();
            return result;
        }
    }
}