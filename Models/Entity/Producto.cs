using System.ComponentModel.DataAnnotations;

namespace CarritoDeComprasMVC.Models.Entity
{
    public class Producto
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; }
        [MaxLength(500)]
        public string Descripción { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string ImagenUrl { get; set; }
    }

}
