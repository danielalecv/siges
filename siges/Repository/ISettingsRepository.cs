using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {

  public interface ISettingsRepository : IRepository<Settings> {
    Settings GetByVersion(string ver);
    IQueryable<Settings> GetTemplateByTipo(string tipo);
  }
}
