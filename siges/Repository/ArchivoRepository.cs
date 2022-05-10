using siges.Models;
using Microsoft.EntityFrameworkCore;
using siges.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class ArchivoRepository : Repository<Operador.Estado.Archivo>, IArchivo {

    public ArchivoRepository(ApplicationDbContext context) : base(context){}
  }
}
