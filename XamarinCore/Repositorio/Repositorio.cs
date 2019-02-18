using Presentacion.NutricionXamarin.core.Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace XamarinCore.Repositorio
{
    public class Repositorio
    {
        public async Task<T> Get<T>(string url)
        {
            try
            {
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);


                }          
            }
            catch (Exception e)
            {               
            }

            return default(T);
        }




        public HttpResponse Get(string url)
        {
            var request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse httpResponse = request.GetResponse() as HttpWebResponse)
            {
                return BuildResponse(httpResponse);
            }

        }


        public List<T> ObtenerTodos<T>()
        {
            var response = Get("http://192.168.1.3/WebApi/api/Paciente");

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ApplicationException("news not found");
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(response.Content);
        }
        
        public T ObtenerPorId<T>(long id)
        {
            var response = Get("http://192.168.1.3/WebApi/api/Paciente/"+id);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ApplicationException("news not found");
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content);
        }


        private static HttpResponse BuildResponse(HttpWebResponse httpResponse)
        {
            using (StreamReader reader = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException()))
            {
                var content = reader.ReadToEnd();
                var response = new HttpResponse
                {
                    Content = content,
                    HttpStatusCode = httpResponse.StatusCode
                };
                return response;
            }
        }


    }
}