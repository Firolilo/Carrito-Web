namespace CarritoDeComprasMVC.Models.Entity
{
    public class CarritoItem
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }  // Relación con Producto
        public int Cantidad { get; set; }
        public decimal PrecioTotal => Producto != null ? Producto.Precio * Cantidad : 0;
    }

}
