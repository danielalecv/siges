using siges.Models;
using Microsoft.EntityFrameworkCore;
using siges.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class SettingsRepository : Repository<Settings>, ISettingsRepository {
    public SettingsRepository(ApplicationDbContext context) : base(context){}
    public Settings GetByVersion(string ver) {
      return entities.Where(r => r.Version == ver).Include(r => r.Templates).Single();
    }
    public IQueryable<Settings> GetTemplateByTipo(string tipo){
      return entities.Where(r => r.Templates.Where(t => t.Tipo == tipo).Any());
    }
  }
}
