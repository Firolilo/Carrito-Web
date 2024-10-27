using CarritoDeComprasMVC.Data;
using CarritoDeComprasMVC.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


public class ProductoController : Controller
{
    private readonly MyDBContext _context;

    public ProductoController(MyDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var productos = await _context.Productos.ToListAsync();
        return View(productos);
    }

    public async Task<IActionResult> Detalles(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null) return NotFound();
        return View(producto);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Producto producto)
    {
        if (ModelState.IsValid)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(producto);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Editar(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null) return NotFound();
        return View(producto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(Producto producto)
    {
        if (ModelState.IsValid)
        {
            _context.Update(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(producto);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null) return NotFound();
        return View(producto);
    }

    [HttpPost, ActionName("Eliminar")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmarEliminar(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto != null)
        {
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
