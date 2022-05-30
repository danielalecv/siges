using Microsoft.EntityFrameworkCore.ChangeTracking;
using siges.Data;
using siges.Models;
using System;

namespace siges.Services
{
    public class ClientContactService: IClientContactService
    {
        private readonly ApplicationDbContext _context;

        public ClientContactService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool UpdatePerson(Persona p)
        {
            try
            {
                _context.Persona.Update(p);
                _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public EntityEntry<ContactoCliente> InsertContactoCliente(ContactoCliente cc)
        {
            try
            {
                var res = _context.ContactoCliente.Add(cc);
                _context.SaveChangesAsync();
                return res;
            }
            catch
            {
                return null;
            }
        }
    }
    public interface IClientContactService
    {
        bool UpdatePerson(Persona p);
        EntityEntry<ContactoCliente> InsertContactoCliente(ContactoCliente cc);
    }
}