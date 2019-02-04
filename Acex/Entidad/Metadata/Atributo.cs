using SX.Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace SX.Entidad.Metadata
{
    [Conexion("Acex")]
    public class Atributo : EntidadBase
    {
        public int Id { get; set; }
        public int EntidadId { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public string Orden { get; set; }
        public int Largo { get; set; }
        public bool MostrarEnLista { get; set; }
        public bool MostrarEnExcel { get; set; }
        public bool MostrarEnFormulario { get; set; }
        public bool Editable { get; set; }
        public bool Requerido { get; set; }
        public string ValidacionCustom { get; set; }
    }
}
