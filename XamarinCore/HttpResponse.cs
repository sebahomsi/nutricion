using System.Net;

namespace Presentacion.NutricionXamarin.core.Modelos.Response
{
    public class HttpResponse
    {
        public string Content { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}