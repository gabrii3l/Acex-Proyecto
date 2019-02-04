using System;
using System.Collections.Generic;
using System.Text;

namespace SX.Entidad.Metadata
{
    public interface IMotorMetadata
    {
        bool Iniciar();
        bool Reload(int entidadId);
        List<Entidad> ObtenerTodas();
    }
}
