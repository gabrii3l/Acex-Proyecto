using System;
using System.Collections.Generic;
using System.Text;

namespace SX.Entidad
{
    public class Paginacion : Attribute
    {
        public Paginacion(int registrosPorPagina)
        {
            RegistrosPorPagina = registrosPorPagina;
        }

        public int RegistrosPorPagina { get; set; }
    }
}
