using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
    public interface IBulkUploadTemplate : IRepository<BulkUploadTemplate> {
      IQueryable<BulkUploadTemplate> GetByTipoAndVersion(string v, string t);
    }
}
