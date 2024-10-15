using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTiendaComida.Models
{
    public class LoginResponseDto
    {
        public string Correo { get; set; } = null!;   // Correo en lugar de Email
        public string Usuario { get; set; } = null!;   
        public string Nombre { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public int UsuarioId { get; set; }
    }
}
