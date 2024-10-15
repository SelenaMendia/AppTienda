using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppTiendaComida.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }

        public string Nombre { get; set; } = null!;

        //si quiero ignorar la collection de productos de esta categoria
        [JsonIgnore]
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
