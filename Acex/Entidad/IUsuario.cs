using System;
using System.Collections.Generic;
using System.Text;

namespace SX.Entidad
{
    public interface IUsuario
    {
        Guid Id { get; set; }
        string Nombre { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string EmailVerificado { get; set; }
        DateTime FechaCreacion { get; set; }
        bool Activo { get; set; }
        string PasswordSalt { get; set; }
    }
}
