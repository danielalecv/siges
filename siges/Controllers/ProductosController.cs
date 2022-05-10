using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using siges.DTO;
using siges.Data;
using siges.Models;
using siges.Repository;

namespace siges.Controllers
{
  [Authorize(Roles = "Supervisor")]
  public class ProductosController : Controller
  {
    private readonly ApplicationDbContext _context;
    private readonly IBitacoraRepository _bRepo;
    private readonly IProducto _prodRepo;
    private readonly IPersonaRepository _pRepo;
    private String LoggedUser;

    public ProductosController(ApplicationDbContext context, IBitacoraRepository bRepo, IProducto prodRepo, IPersonaRepository pRepo)
    {
      _context = context;
      _bRepo = bRepo;
      _prodRepo = prodRepo;
      _pRepo = pRepo;
    }

    // GET: Productos
    public async Task<IActionResult> Index()
    {
      return View(await _context.Producto.Where(r => r.Estatus == true).ToListAsync());
    }

    // GET: Producto/Detail/2
    public async Task<IActionResult> Detail(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var producto = await _context.Producto
        .FirstOrDefaultAsync(m => m.Id == id);
      if (producto == null)
      {
        return NotFound();
      }

      return View(producto);
    }

    // GET: Producto/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Producto/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Marca,Estatus,Opcional1,Opcional2")] Producto producto)
    {
      if (ModelState.IsValid)
      {
        producto.Estatus = true;
        _context.Add(producto);
        await _context.SaveChangesAsync();
        LoggedUser = this.User.Identity.Name;
        _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "Producto", Description = "Producto no. " + producto.Id + " agregado." });
        return RedirectToAction(nameof(Index));
      }
      return View(producto);
    }

    // GET: Producto/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var producto = await _context.Producto.FindAsync(id);
      if (producto == null)
      {
        return NotFound();
      }
      return View(producto);
    }

    // POST: Producto/Edit/2
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Marca,Estatus,Opcional1,Opcional2")] Producto producto)
    {
      if (id != producto.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          producto.Estatus = true;
          _context.Update(producto);
          await _context.SaveChangesAsync();
          LoggedUser = this.User.Identity.Name;
          _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Producto", Description = "Producto no. " + producto.Id + " actualizado." });
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ProductoExists(producto.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(producto);
    }

    // GET: Producto/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var producto = await _context.Producto
        .FirstOrDefaultAsync(m => m.Id == id);
      if (producto == null)
      {
        return NotFound();
      }

      return View(producto);
    }

    // POST: Producto/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var producto = await _context.Producto.FindAsync(id);
      producto.Estatus = false;
      _context.Update(producto);
      //_context.ActivoFijo.Remove(producto);
      await _context.SaveChangesAsync();
      LoggedUser = this.User.Identity.Name;
      _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Delete", Section = "Producto", Description = "Producto no. " + producto.Id + " eliminado." });
      return RedirectToAction(nameof(Index));
    }

    private bool ProductoExists(int id)
    {
      return _context.Producto.Any(e => e.Id == id);
    }
  }
}
