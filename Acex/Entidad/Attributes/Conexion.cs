using System;
using System.Collections.Generic;
using System.Text;

namespace SX.Entidad
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class Conexion : Attribute
    {
        public Conexion(string nombreConexion)
        {
            NombreConexion = nombreConexion;
        }
        public string NombreConexion { get; set; }
    }
}
