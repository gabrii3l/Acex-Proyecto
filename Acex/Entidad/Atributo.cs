using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SX.Entidad
{
    public enum TipoFiltro
    {
        /// <summary>
        /// No implementa el botón de Exportar
        /// </summary>
        /// 
        Ninguno = 0,

        /// <summary>
        /// Implementa un botón simple que exporta la grilla actual, incluyendo sus vidrios
        /// </summary>
        /// 
        Filtro = -1,

        /// <summary>
        /// Implementa un menu que permite exportar el filtro o toda la Lista desde la BD
        /// </summary>
        /// 
        Multiple = 1,

        /// <summary>
        /// Implementa un menu que permite exportar el filtro o toda la Lista desde la BD
        /// </summary>
        /// 
        Rango = 2
    }

    /// <summary>
    /// Clase que abstrae la implementación de los atributos de una entidad de la metadata
    /// </summary>
    /// 
    public class Atributo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        private string _titulo;
        public string Titulo { get; set; }

        public int Largo { get; set; }

        public Boolean EsLlavePrimaria { get; set; }

        public string TipoAplicacion { get; set; }

        public Boolean Mostrar { get; set; }

        public Boolean Crear { get; set; }

        public Boolean Actualizar { get; set; }

        public bool SoloLectura { get; set; }

        public Boolean Seleccionar { get; set; }

        public Boolean Grilla { get; set; }

        public string TextoVacio { get; set; }

        public Boolean PermiteNulo { get; set; }

        public int AnchoGrilla { get; set; }

        public string Formato { get; set; }

        public string ComboObjeto { get; set; }

        public string ComboCampoValue { get; set; }

        public string ComboCampoTexto { get; set; }

        public bool AutoReferencia { get; set; }

        public string Validacion { get; set; }

        public string ValidacionTexto { get; set; }

        public string Field
        {
            get
            {
                if (!string.IsNullOrEmpty(ComboObjeto))
                    return string.Format("{{ name: \"{0}\", type: \"select\", items: obtenerCombo{1}(), textField: \"{2}\", valueField: \"{3}\" {4} {5} ,title: \"{6}\" }}", Nombre, ComboObjeto, ComboCampoTexto, ComboCampoValue, AnchoGrilla != 0 ? ",width: " + AnchoGrilla.ToString() : "", Actualizar ? "" : ",readOlny: true", Titulo);
                else
                    switch (TipoAplicacion)
                    {
                        case "System.Boolean": return string.Format("{{ name: \"{0}\", type: \"checkbox\" {1} ,editing: {2} ,title: \"{3}\" }}", Nombre, AnchoGrilla != 0 ? ",width: " + AnchoGrilla.ToString() : "", Actualizar.ToString().ToLower(), Titulo);
                        case "System.DateTime": return string.Format("{{ name: \"{0}\", type: \"date\" {1} ,editing: {2} ,title: \"{3}\" }}", Nombre, AnchoGrilla != 0 ? ",width: " + AnchoGrilla.ToString() : "", Actualizar.ToString().ToLower(), Titulo);
                        case "System.Double": return string.Format("{{ name: \"{0}\", type: \"number\" {1} ,editing: {2} ,title: \"{3}\" }}", Nombre, AnchoGrilla != 0 ? ",width: " + AnchoGrilla.ToString() : "", Actualizar.ToString().ToLower(), Titulo);
                        case "System.Guid": return string.Format("{{ name: \"{0}\", type: \"text\" {1} ,editing: {2} ,title: \"{3}\" }}", Nombre, AnchoGrilla != 0 ? ",width: " + AnchoGrilla.ToString() : "", Actualizar.ToString().ToLower(), Titulo);
                        case "System.Int32": return string.Format("{{ name: \"{0}\", type: \"number\" {1} ,editing: {2} ,title: \"{3}\" }}", Nombre, AnchoGrilla != 0 ? ",width: " + AnchoGrilla.ToString() : "", Actualizar.ToString().ToLower(), Titulo);
                        case "System.Int64": return string.Format("{{ name: \"{0}\", type: \"number\" {1} ,editing: {2} ,title: \"{3}\" }}", Nombre, AnchoGrilla != 0 ? ",width: " + AnchoGrilla.ToString() : "", Actualizar.ToString().ToLower(), Titulo);
                        case "System.String": return string.Format("{{ name: \"{0}\", type: \"text\" {1} ,editing: {2} ,title: \"{3}\" }}", Nombre, AnchoGrilla != 0 ? ",width: " + AnchoGrilla.ToString() : "", Actualizar.ToString().ToLower(), Titulo);
                        default: return string.Empty;
                    }
            }
        }
    }
}
