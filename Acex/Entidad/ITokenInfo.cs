using System;
using System.Collections.Generic;
using System.Text;

namespace SX.Entidad
{
    public interface ITokenInfo
    {
        string Email { get; set; }
        string Nombre { get; set; }
        DateTime Expires { get; set; }
        string Token { get; set; }
    }
}
