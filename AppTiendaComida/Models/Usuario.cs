using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTiendaComida.Models
{
    public partial class Usuario
    {
        public int UsuarioId { get; set; }

        public string? Nombre { get; set; } = null!;

        public string? Usuario1 { get; set; } = null!;

        public string? Telefono { get; set; } = null!;

        public string? Correo { get; set; } = null!;

        public string? Contraseña { get; set; } = null!;

        public string? Rol { get; set; } = null!;
    }
}
