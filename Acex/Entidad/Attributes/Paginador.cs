using System;
using System.Collections.Generic;
using System.Text;

namespace SX.Entidad
{
    public class Paginador : Attribute
    {
        public Paginador(int paginas)
        {
            Paginas = paginas;
        }

        public int Paginas { get; set; }
    }
}
