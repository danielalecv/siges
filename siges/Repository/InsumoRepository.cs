﻿using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class InsumoRepository : Repository<Insumo>, IInsumoRepository {
    public InsumoRepository(ApplicationDbContext context) : base(context) { }

    public IQueryable<Insumo> GetAll(bool estatus) {
      return entities.Where(r => r.Estatus == estatus).OrderBy(r => r.Descripcion).AsQueryable();
    }

    public bool Exist(string clave){
      var result = entities.Where(r => r.Estatus == true && r.Clave == clave);
      if(result.Any()){
        return true;
      }else{
        return false;
      }
    }
  }
}
