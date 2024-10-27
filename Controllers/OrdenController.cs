using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CarritoDeComprasMVC.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarritoDeComprasMVC.Controllers
{
    [Authorize]
    public class OrdenController : Controller
    {
        private readonly MyDBContext _context;

        public OrdenController(MyDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Historial()
        {
            var userEmail = User.Identity.Name; // Obtener el correo electrónico del usuario autenticado
            var ordenes = await _context.Orders
                .Where(o => o.UsuarioId == userEmail) // Filtrar las órdenes por el correo electrónico
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Producto)
                .ToListAsync();

            return View(ordenes);
        }


        // Ver detalles de una orden específica
        public async Task<IActionResult> Detalles(int id)
        {
            var userEmail = User.Identity.Name; // Obtener el correo electrónico del usuario autenticado
            var orden = await _context.Orders
                .Where(o => o.Id == id && o.UsuarioId == userEmail) // Filtrar por correo en lugar del ID
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Producto)
                .FirstOrDefaultAsync();

            if (orden == null) return NotFound();

            return View(orden);
        }

    }
}
