using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace SX.Entidad
{
    public abstract class EntidadBase : IEntidad
    {
        public EntidadBase()
        {
        }

        public object Get(string propiedad)
        {
            return Util.Get((object)this, propiedad);
        }

        public void Set(string propiedad, object valor)
        {
            Util.Set((object)this, propiedad, valor);
        }

        EstadoEntidad IEntidad.EstadoEntidad { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        protected string ObtenerUltimoString(string input, char separador = '.')
        {
            if (input == null)
                return null;
            string[] arreglo = input.Split(separador);
            if (arreglo.Length == 0)
                return input;

            return arreglo[arreglo.Length - 1];
        }

        //public IMetaEntidad Metadata()
        //{
        //    var entidad = new Entidad();
        //    entidad.Nombre = ObtenerUltimoString(GetType().ToString());
        //    entidad.NombreCompleto = GetType().ToString();
        //    entidad.Titulo = entidad.Nombre;
        //    entidad.TituloPlural = entidad.Titulo + "s";
        //    entidad.Paginacion = Util.ObtenerPaginacion(this);
        //    entidad.Atributos = Util.ObtenerAtributos(this);
        //    return entidad;
        //}

        private string ObtenerAssemblyName(string clase)
        {
            string[] arreglo = clase.Split('.');
            string assembly = string.Empty;
            if (arreglo.Length == 1)
                return clase;
            for (int i = 0; i < arreglo.Length - 1; i++)
            {
                assembly += string.Format("{0}.", arreglo[i]);
            }
            return assembly.Substring(0, assembly.Length - 1);
        }

        public bool TieneValidador()
        {
            var nombreClase = this.GetType().ToString() + "Validator";
            var tipo = Type.GetType(String.Format("{0}, {1}", nombreClase, ObtenerAssemblyName(nombreClase)));
            return tipo != null;
        }

        public ValidationResult Validar()
        {
            var nombreClase = this.GetType().ToString() + "Validator";
            var tipo = Type.GetType(String.Format("{0}, {1}", nombreClase, ObtenerAssemblyName(nombreClase)));
            var factory = new ReflectionFactory(tipo);
            var obj = factory.CreateInstance();

            MethodInfo method = obj.GetType().GetMethod("Validate", new Type[] { this.GetType() });
            ValidationResult result = (ValidationResult)method.Invoke(obj, new object[] { this });
            return result;
        }

        public ValidationResult Validar(string ruleSet)
        {
            var nombreClase = this.GetType().ToString() + "Validator";
            var tipo = Type.GetType(String.Format("{0}, {1}", nombreClase, ObtenerAssemblyName(nombreClase)));
            var factory = new ReflectionFactory(tipo);
            var obj = factory.CreateInstance();

            MethodInfo methodInfo = GetType().GetMethod("ValidarRuleSet");
            methodInfo = methodInfo.MakeGenericMethod(new Type[] { this.GetType() });
            object nuevo = methodInfo.Invoke(this, new object[] { obj, this, ruleSet });
            return (ValidationResult)nuevo;
        }

        public ValidationResult ValidarRuleSet<T>(object validator, object entidad, string ruleSet)
        {
            return DefaultValidatorExtensions.Validate<T>((IValidator<T>)validator, (T)entidad, null, ruleSet: ruleSet);
        }

        public ValidationResult ValidaRuleSet<T>(object validator, object entidad, string regla)
        {
            IValidator<T> validador = (IValidator<T>)validator;
            return DefaultValidatorExtensions.Validate<T>(validador, (T)entidad, null, ruleSet: "");
        }
    }

    public class EntidadActualizable : EntidadBase
    {
        public virtual EstadoEntidad EstadoEntidad { get; set; }
    }
}
