using siges.Models;
using Microsoft.EntityFrameworkCore;
using siges.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class BulkUploadTemplateRepository : Repository<BulkUploadTemplate>, IBulkUploadTemplate {
    public BulkUploadTemplateRepository(ApplicationDbContext context) : base(context){}
    public IQueryable<BulkUploadTemplate> GetByTipoAndVersion(string v, string t){
      return entities.Where(r => r.Version == v && r.Tipo == t);
    }
  }
}
