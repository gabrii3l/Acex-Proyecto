using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SX.Entidad
{
    public abstract class Proxy
    {
        // AL Obtener una respuesta de etxto (JSON).
        public delegate void OnJsonResponseEventHandler(string json);
        public event OnJsonResponseEventHandler OnJsonResponse;

        // Configuración Url Base
        public delegate void OnConfigureEventHandler(ProxyConfiguracion configuracion);
        public static event OnConfigureEventHandler OnConfigure;

        // Seguridad
        public delegate string OnActivateSecureHandler();
        public event OnActivateSecureHandler OnSecure;

        public delegate string OnConfigureSecureEventHandler();
        public static event OnConfigureSecureEventHandler OnConfigureSecure;


        public static ProxyConfiguracion Configuracion;

        public enum Metodo
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public string OAuthToken { get; set; }

        public Proxy() { }

        protected async Task<String> GetStreamFromUrl(string url, Metodo metodo, string json = "")
        {
            if (Configuracion == null)
            {
                Configuracion = new ProxyConfiguracion();
                OnConfigure?.Invoke(Configuracion);
            }

            ConfigurarSeguridad();

            string urlFinal;
            string jsonResult = "";
            if (url.Contains("http"))
                urlFinal = url;
            else
                urlFinal = Configuracion.UrlBase + url;

            HttpClient cliente = new HttpClient();
            cliente.MaxResponseContentBufferSize = 2000000;
            cliente.Timeout = new TimeSpan(0, 0, 30);
            HttpResponseMessage response = null;

            // Add Token
            if (!string.IsNullOrEmpty(OAuthToken))
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", OAuthToken);

            try
            {
                if (metodo == Metodo.POST || metodo == Metodo.PUT)
                {
                    if (json != string.Empty)
                    {
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        if (metodo == Metodo.POST)
                            response = cliente.PostAsync(urlFinal, content).Result;

                        if (metodo == Metodo.PUT)
                            response = cliente.PutAsync(urlFinal, content).Result;
                    }

                    Stream receiveStream = await response.Content.ReadAsStreamAsync();
                    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    jsonResult = readStream.ReadToEnd();
                    if (jsonResult != string.Empty)
                        OnJsonResponse?.Invoke(jsonResult);
                }
                else
                {
                    if (metodo == Metodo.GET)
                        response = cliente.GetAsync(urlFinal).Result;

                    if (metodo == Metodo.DELETE)
                        response = cliente.DeleteAsync(urlFinal).Result;

                    Stream receiveStream = await response.Content.ReadAsStreamAsync();
                    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    jsonResult = readStream.ReadToEnd();
                    if (jsonResult != string.Empty)
                        OnJsonResponse?.Invoke(jsonResult);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return jsonResult;
        }

        protected virtual void ConfigurarSeguridad()
        {
            if (string.IsNullOrEmpty(OAuthToken))
            {
                OAuthToken = OnSecure?.Invoke();
                if (string.IsNullOrEmpty(OAuthToken))
                    OAuthToken = OnConfigureSecure?.Invoke();
            }
        }
    }
}