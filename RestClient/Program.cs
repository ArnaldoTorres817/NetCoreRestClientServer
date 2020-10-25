using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestClient
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            // client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            
            try
            {
                // Analizar gramaticalmente el URI
                Uri requestUri = new Uri(args[0]);
                // Hacer la peticion
                HttpResponseMessage response = await client.GetAsync(requestUri);
                // Tirar excepcion si el status code no es exitoso
                response.EnsureSuccessStatusCode();
                // Peticion exitosa
                // Console.WriteLine(response.StatusCode);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            catch (UriFormatException e) {
                Console.WriteLine(e.Message);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
