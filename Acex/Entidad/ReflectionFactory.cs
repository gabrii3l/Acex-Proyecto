using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace SX.Entidad
{
    internal class ReflectionFactory
    {
        delegate object MethodInvoker();
        MethodInvoker methodHandler = null;

        /// <summary>
        /// constructor que crea una instancia de un objeto segun el tipo propuesto
        /// </summary>
        /// <param name="type">Representa las declaraciones de tipo: tipos de clases, tipos de interfaz,
        /// tipos de matriz, tipos de valores, tipos de enumeración, parámetros de tipo,
        /// definiciones de tipo genérico, y constructores abiertos o cerrados de tipos genéricos.
        /// </param>
        public ReflectionFactory(Type type)
        {
            CreateMethod(type.GetConstructor(Type.EmptyTypes));
        }

        /// <summary>
        /// constructor que crea una instancia de un objeto segun el tipo propuesto
        /// </summary>
        /// <param name="target">Descubre los atributos de un constructor de la clase
        /// y proporciona acceso a sus metadatos.
        /// </param>
        public ReflectionFactory(ConstructorInfo target)
        {
            CreateMethod(target);
        }

        /// <summary>
        /// metodo que crea dinamicamente una función delegado que retorna un nuevo objeto instanciado
        /// </summary>
        /// <param name="target">Descubre los atributos de un constructor de la clase
        /// y proporciona acceso a sus metadatos.
        /// </param>
        void CreateMethod(ConstructorInfo target)
        {
            DynamicMethod dynamic = new DynamicMethod(string.Empty,
                    typeof(object),
                    new Type[0],
                    target.DeclaringType);
            ILGenerator il = dynamic.GetILGenerator();

            il.DeclareLocal(target.DeclaringType);

            il.Emit(OpCodes.Newobj, target);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);

            methodHandler = (MethodInvoker)dynamic.CreateDelegate(typeof(MethodInvoker));
        }

        /// <summary>
        /// retorna objeto invocando al delegado creado dinamicamente.
        /// </summary>
        /// <returns>object</returns>
        public object CreateInstance()
        {
            return methodHandler();
        }
    }
}
