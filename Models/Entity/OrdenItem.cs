namespace CarritoDeComprasMVC.Models.Entity
{
    public class OrdenItem
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }  // Relación con Producto
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal => PrecioUnitario * Cantidad;
    }
}
