using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SX.Entidad
{
    public class Repositorio<T> : Proxy
    {
        protected string clase;
        public Repositorio() : base()
        {
            var arreglo = typeof(T).ToString().Split('.');
            clase = arreglo[arreglo.Length - 1];
        }

        public static Repositorio<T> Obtener()
        {
            return new Repositorio<T>();
        }


        public T Get(int id)
        {
            return ObtenerId(id);
        }
        public T Get(Guid id)
        {
            return ObtenerId(id);
        }
        public T Get(string id)
        {
            return ObtenerId(id);
        }

        protected T ObtenerId(object id)
        {
            var tarea = Task.Run(async () =>
            {
                return await GetStreamFromUrl(clase + "/" + id.ToString(), Metodo.GET);
            });
            var json = tarea.Result;
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Insertar(T entidad)
        {
            var json = JsonConvert.SerializeObject(entidad);
            var tarea = Task.Run(async () =>
            {
                return await GetStreamFromUrl(clase + "/", Metodo.POST, json);
            });
            tarea.Wait();
        }

        public void Actualizar(T entidad)
        {
            var json = JsonConvert.SerializeObject(entidad);
            var tarea = Task.Run(async () =>
            {
                return await GetStreamFromUrl(clase + "/", Metodo.PUT, json);
            });
            tarea.Wait();
        }

        public void Eliminar(T entidad)
        {
            var id = Util.Get(entidad, "Id").ToString();
            var tarea = Task.Run(async () =>
            {
                return await GetStreamFromUrl(clase + "/" + id, Metodo.DELETE);
            });
            tarea.Wait();
        }

        public Listado<T> Seleccionar()
        {
            var tarea = Task.Run(async () =>
            {
                return await GetStreamFromUrl(clase + "/", Metodo.GET);
            });
            var json = tarea.Result;
            return JsonConvert.DeserializeObject<Listado<T>>(json);
        }

        public Listado<T> Seleccionar(int pagina)
        {
            var tarea = Task.Run(async () =>
            {
                return await GetStreamFromUrl(clase + $"/Listar/{pagina}", Metodo.GET);
            });
            var json = tarea.Result;
            return JsonConvert.DeserializeObject<Listado<T>>(json);
        }

        public List<T> Seleccionar(string accion, Metodo metodo)
        {
            var tarea = Task.Run(async () =>
            {
                return await GetStreamFromUrl(clase + "/" + accion, metodo);
            });
            var json = tarea.Result;
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        public TEntity Obtener<TEntity>(string accion, Metodo metodo, string json)
        {
            var tarea = Task.Run(async () =>
            {
                return await GetStreamFromUrl(clase + "/" + accion, metodo, json);
            });
            string jsonResult = tarea.Result;
            return JsonConvert.DeserializeObject<TEntity>(jsonResult);
        }

        public Listado<T> Buscar(string texto)
        {
            var tarea = Task.Run(async () =>
            {
                return await GetStreamFromUrl(clase + $"/Buscar/{System.Web.HttpUtility.UrlEncode(texto)}", Metodo.GET);
            });
            var json = tarea.Result;
            return JsonConvert.DeserializeObject<Listado<T>>(json);
        }

        public Listado<T> Buscar(string texto, int pagina)
        {
            var tarea = Task.Run(async () =>
            {
                return await GetStreamFromUrl(clase + $"/Buscar/{System.Web.HttpUtility.UrlPathEncode(texto)}/{pagina}", Metodo.GET);
            });
            var json = tarea.Result;
            return JsonConvert.DeserializeObject<Listado<T>>(json);
        }
    }
}
