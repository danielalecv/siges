using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class Repository<T> : IRepository<T> where T : class {
    private readonly ApplicationDbContext _context;
    public DbSet<T> entities;
    string errorMessage = string.Empty;

    public Repository(ApplicationDbContext context) {
      _context = context;
      entities = context.Set<T>();
    }

    public IQueryable<T> GetAll() {
      return entities.AsNoTracking().AsQueryable();
    }

    public T GetById(int id) {
      return entities.Find(id);
    }

    public void Insert(T entity) {
      if (entity == null) {
        throw new ArgumentNullException("entity");
      }
      entities.Add(entity);
      SaveChange();
    }

    public void Update(T entity) {
      if (entity == null) {
        throw new ArgumentNullException("entity");
      }
      SaveChange();
    }

    public void Delete(T entity) {
      if (entity == null) {
        throw new ArgumentNullException("entity");
      }
      entities.Remove(entity);
      SaveChange();
    }
    private void SaveChange() {
      _context.SaveChanges();
    }
  }
}
