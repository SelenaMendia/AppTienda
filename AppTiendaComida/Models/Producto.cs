using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using AppTiendaComida.Models;
using System.Threading.Tasks;

namespace AppTiendaComida.Models
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripción { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int? CategoriaId { get; set; }
        public string? ImagenUrl { get; set; }

        public FileResult Imagen { get; set; }
        //public string? ImagenUrllocal { get; set; }

        //public virtual Categoria? oCategoria { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<DetalleCarrito> DetalleCarritos { get; set; } = new List<DetalleCarrito>();
    }
}
