using SX.Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace SX.Entidad.Metadata
{
    [Conexion("Acex")]
    public class Entidad : EntidadBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ClaseBase { get; set; }
        public string Ensamblado { get; set; }
    }
}
