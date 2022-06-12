using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class PersonaRepository : Repository<Persona>, IPersonaRepository
    {
        public PersonaRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<Persona> GetByEmail(string email)
        {
            return entities.Where(r => r.Email == email && r.Estatus == true);
        }
        public Persona GetByToken(string token)
        {
            var result = entities.Where(c => c.Token == token);
            if (result.Any())
            {
                return result.Single();
            }
            else
            {
                return null;
            }
        }
        public IQueryable<Persona> GetAll(bool estatus)
        {
            return entities.Where(r => r.Estatus == estatus).OrderBy(r => r.Nombre);
        }
        public bool Exist(string RFC, string CURP)
        {
            var p = entities.Where(r => r.Estatus == true && r.RFC == RFC && r.CURP == CURP);
            if (p.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ExistByEmail(string email)
        {
            var result = entities.Any(r => r.Email == email);
            return result;
        }
        public void DeleteByContactoClienteId(int id)
        {
            string connectionString = "Server=192.168.1.221;Database=SSM4_test;user id=ssm4Test;password=Ssm4Test&2021.!";
            string queryString = "delete Persona where ContactoClienteId = " + id;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}