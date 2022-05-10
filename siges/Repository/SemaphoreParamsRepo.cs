using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class SemaphoreParamsRepo : Repository<SemaphoreParams>, ISemaphoreParamsRepo {
    public SemaphoreParamsRepo(ApplicationDbContext context) : base(context) { }
  }
}
