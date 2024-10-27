using Microsoft.AspNetCore.Mvc;
using CarritoDeComprasMVC.Data;
using CarritoDeComprasMVC.Models.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarritoDeComprasMVC.Controllers
{
    public class CarritoController : Controller
    {
        private readonly MyDBContext _context;

        public CarritoController(MyDBContext context)
        {
            _context = context;
        }

        // Agregar producto al carrito
        [HttpPost]
        public async Task<IActionResult> Agregar(int productoId, int cantidad)
        {
            var producto = await _context.Productos.FindAsync(productoId);
            if (producto == null || producto.Stock < cantidad) return NotFound();

            // Lógica para agregar el producto al carrito
            var carritoItem = new CarritoItem
            {
                ProductoId = productoId,
                Producto = producto,
                Cantidad = cantidad
            };

            _context.CarritoItems.Add(carritoItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Ver");
        }

        // Ver carrito
        public async Task<IActionResult> Ver()
        {
            var carritoItems = await _context.CarritoItems
                .Include(ci => ci.Producto)
                .ToListAsync();
            ViewBag.Total = carritoItems.Sum(ci => ci.PrecioTotal);
            return View(carritoItems);
        }

        // Actualizar cantidad en el carrito
        [HttpPost]
        public async Task<IActionResult> Actualizar(int carritoItemId, int nuevaCantidad)
        {
            var carritoItem = await _context.CarritoItems.Include(ci => ci.Producto).FirstOrDefaultAsync(ci => ci.Id == carritoItemId);
            if (carritoItem == null || carritoItem.Producto.Stock < nuevaCantidad) return NotFound();

            carritoItem.Cantidad = nuevaCantidad;
            await _context.SaveChangesAsync();

            return RedirectToAction("Ver");
        }

        // Eliminar producto del carrito
        [HttpPost]
        public async Task<IActionResult> Eliminar(int carritoItemId)
        {
            var carritoItem = await _context.CarritoItems.FindAsync(carritoItemId);
            if (carritoItem == null) return NotFound();

            _context.CarritoItems.Remove(carritoItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Ver");
        }

        // Proceso de compra
        public async Task<IActionResult> Comprar()
        {
            var carritoItems = await _context.CarritoItems.Include(ci => ci.Producto).ToListAsync();
            if (!carritoItems.Any()) return RedirectToAction("Ver");

            decimal totalOrden = carritoItems.Sum(ci => ci.PrecioTotal);
            var orden = new Orden
            {
                FechaCreacion = DateTime.Now,
                UsuarioId = User.Identity.Name,
                Total = totalOrden
            };

            // Crear OrdenItems y actualizar stock
            foreach (var item in carritoItems)
            {
                var ordenItem = new OrdenItem
                {
                    ProductoId = item.ProductoId,
                    Producto = item.Producto,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.Producto.Precio,
                };

                orden.Items.Add(ordenItem);
                item.Producto.Stock -= item.Cantidad;
            }

            _context.Orders.Add(orden);
            _context.CarritoItems.RemoveRange(carritoItems);  // Vaciar el carrito
            await _context.SaveChangesAsync();

            return View("Confirmacion", orden);
        }
    }
}
