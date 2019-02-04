using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SX.Entidad
{
    public static class Util
    {
        /// <summary>
        /// Obtiene el valor de una propiedad
        /// </summary>
        /// <param name="obj">objeto que contiene el parametro a leer</param>
        /// <param name="nombrePropiedad">propiedad que va a leer</param>
        /// <returns>un objeto con el valor de la propiedad</returns>
        public static object Get(object obj, string nombrePropiedad)
        {
            PropertyInfo pi = obj.GetType().GetRuntimeProperty(nombrePropiedad);
            if (!(pi == null))
            {
                return pi.GetValue(obj, null);
            }
            return null;
        }


        /// <summary>
        /// Pone valor a una Propiedad
        /// </summary>
        /// <param name="obj">Objeto que contiene la propiedad</param>
        /// <param name="propiedad">Nombre de4 la propiedad</param>
        /// <param name="valor">Valor a poner en la propiedad</param>
        public static void Set(object obj, string propiedad, object valor)
        {
            PropertyInfo pi = obj.GetType().GetRuntimeProperty(propiedad);
            if (!(pi == null))
            {
                pi.SetValue(obj, valor, null);
            }
        }

        /// <summary>
        /// Obtiene la lista de Propiedades de un Objeto
        /// </summary>
        /// <param name="obj">Objeto origen</param>
        /// <returns>Lista de PropInfo (Reflection) de las propiedades</returns>
        public static List<PropertyInfo> ObtenerPropiedades(object obj)
        {
            List<PropertyInfo> propInfos = obj.GetType().GetRuntimeProperties().ToList();
            return propInfos;
        }

        //public static List<Atributo> ObtenerAtributos(object obj)
        //{
        //	List<Atributo> atributos = new List<Atributo>();
        //	List<PropertyInfo> propInfos = ObtenerPropiedades(obj);
        //	foreach (var propiedad in propInfos)
        //	{
        //		if (EsPrimitiva(propiedad))
        //			atributos.Add(ObtenerAtributo(propiedad));
        //	}
        //	return atributos;
        //}

        public static int ObtenerPaginacion(object obj)
        {
            var tipo = obj.GetType();
            var paginacion = tipo.GetCustomAttributes(typeof(Paginacion), true).FirstOrDefault() as Paginacion;
            if (paginacion != null)
            {
                return paginacion.RegistrosPorPagina;
            }
            return 0;
        }

        //private static Atributo ObtenerAtributo(PropertyInfo propiedad)
        //{
        //	Atributo atributo = new Atributo();
        //	atributo.Nombre = propiedad.Name;
        //	atributo.Seleccionar = true;
        //	atributo.Titulo = propiedad.Name;
        //	atributo.TipoAplicacion = propiedad.PropertyType.ToString();
        //	atributo.AnchoGrilla = 0;
        //	atributo.Actualizar = true;
        //	atributo.EsLlavePrimaria = propiedad.Name == "Id" ? true : false;
        //	return atributo;
        //}

        private static bool EsPrimitiva(PropertyInfo propInfo)
        {
            string tipo = propInfo.PropertyType.ToString();
            return (
                tipo == "System.String" ||
                tipo == "System.Int32" ||
                tipo == "System.Int64" ||
                tipo == "System.Double" ||
                tipo == "System.DateTime" ||
                tipo == "System.Decimal" ||
                tipo == "System.TimeSpan" ||
                tipo == "System.Guid" ||
                tipo == "System.Boolean"
                );

        }

        public static bool RutValido(string rut)
        {
            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }
    }
}