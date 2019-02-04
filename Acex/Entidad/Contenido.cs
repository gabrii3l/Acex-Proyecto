using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SX.Entidad
{
    public class Listado<T>
    {
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }
        public int PaginaActual { get; set; }
        public List<T> Data { get; set; }
        protected int ObtenerPaginador()
        {
            if (this.GetType().GetCustomAttributes(typeof(Paginador), true).FirstOrDefault() is Paginador paginador)
            {
                return paginador.Paginas;
            }
            return 0;
        }
        public string ObtenerHtmlPaginador(string url)
        {
            string activo = "";
            bool anterior = false;
            bool posterior = false;
            int paginador = ObtenerPaginador();
            int paginasReales = TotalPaginas >= paginador ? paginador : TotalPaginas;
            List<int> Paginas = new List<int>();
            int paginaMinima = PaginaActual - 1 < 1 ? 1 : PaginaActual - 1;
            for (int i = 1; i <= paginador; i++)
            {
                if (paginaMinima > TotalPaginas)
                    break;

                Paginas.Add(paginaMinima);
                paginaMinima++;
            }

            anterior = Paginas.Contains(PaginaActual - 1);
            posterior = Paginas.Contains(PaginaActual + 1);

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row\">");
            html.AppendLine("   <input type=\"hidden\" name=\"text\" id=\"text\" value=\"\" />");
            html.AppendLine("   <nav aria-label=\"Page navigation\" id=\"paginador\">");
            html.AppendLine("      <ul class=\"pagination\">");
            if (anterior)
            {
                html.AppendLine("          <li class=\"page-item\">");
                html.AppendLine("              <a class=\"page-link\" href=\"" + url.Replace("pag=" + PaginaActual.ToString(), "pag=" + (PaginaActual - 1).ToString()) + "\" aria-label=\"Previous\">");
                html.AppendLine("                  <span aria-hidden=\"true\">&laquo;</span>");
                html.AppendLine("                  <span class=\"sr-only\">Previous</span>");
                html.AppendLine("              </a>");
                html.AppendLine("          </li>");
            }
            foreach (var pagina in Paginas)
            {
                if (pagina == PaginaActual)
                    activo = " active";
                else
                    activo = "";

                html.AppendLine("          <li class=\"page-item" + activo + "\"><a class=\"page-link\" href=\"" + url.Replace("pag=" + PaginaActual.ToString(), "pag=" + pagina.ToString()) + "\">" + pagina.ToString() + "</a></li>");
            }
            if (posterior)
            {
                html.AppendLine("          <li class=\"page-item\">");
                html.AppendLine("              <a class=\"page-link\" href=\"" + url.Replace("pag=" + PaginaActual.ToString(), "pag=" + (PaginaActual + 1).ToString()) + "\" aria-label=\"Next\">");
                html.AppendLine("                  <span aria-hidden=\"true\">&raquo;</span>");
                html.AppendLine("                  <span class=\"sr-only\">Next</span>");
                html.AppendLine("              </a>");
                html.AppendLine("          </li>");
            }

            html.AppendLine("      </ul>");
            html.AppendLine("   </nav>");
            html.AppendLine("</div>");
            return html.ToString();
        }
    }
}
