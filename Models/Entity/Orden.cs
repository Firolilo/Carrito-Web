namespace CarritoDeComprasMVC.Models.Entity
{
    public class Orden
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioId { get; set; }  // Identificador del usuario en Identity
        public decimal Total { get; set; }
        public List<OrdenItem> Items { get; set; } = new List<OrdenItem>();
    }

}
