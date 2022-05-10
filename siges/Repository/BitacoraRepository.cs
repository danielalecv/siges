using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository
{
    public class BitacoraRepository : Repository<Bitacora>, IBitacoraRepository
    {
        public BitacoraRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
