using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace RestServer
{
    class Program
    {
        static readonly HttpListener listener = new HttpListener();

        static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        static void Main(string[] args)
        {
            // Handle CTRL+C for shutting down the server cleanly.
            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("Received SIGINT signal. Shutting down the server.");
                listener.Stop();
                listener.Close();
                Environment.Exit(0);
            };
            Uri uri = BuildUri();
            StartListener(uri);
        }

        static Uri BuildUri()
        {
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Host = "localhost";
            uriBuilder.Port = 5000;
            return uriBuilder.Uri;
        }

        static void StartListener(Uri uri)
        {
            // Add uri to listener prefixes
            listener.Prefixes.Add(uri.AbsoluteUri);

            // Start listening...
            listener.Start();
            Console.WriteLine("Server is listening...");

            HttpListenerContext context;
            do
            {
                try
                {
                    context = listener.GetContext();
                    Console.WriteLine("Received request.");
                    HandleContext(context);
                }
                catch (HttpListenerException e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (listener.IsListening);
        }

        static void HandleContext(HttpListenerContext context)
        {
            if (context.Request.Url.AbsolutePath.Equals("/"))
            {
                SendReponse(context.Response);
            }
            else
            {
                SendNotFound(context.Response);
            }
        }

        static void SendReponse(HttpListenerResponse response)
        {   
            // TODO: Error handling and finally clause for closing output stream
            response.StatusCode = 200;
            Stream output = response.OutputStream;
            Utf8JsonWriter writer = new Utf8JsonWriter(output);
            SampleData data = new SampleData
            {
                Id = 0,
                UserName = "Arnaldo"
            };
            JsonSerializer.Serialize<SampleData>(writer, data, jsonSerializerOptions);
            output.Close();
        }

        static void SendNotFound(HttpListenerResponse response)
        {
            // TODO: Error handling and finally clause for closing output stream
            response.StatusCode = 404;
            Stream output = response.OutputStream;
            byte[] buffer = Encoding.UTF8.GetBytes("Not found.");
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }

    class SampleData
    {
        public Int32 Id { get; set; }

        public String UserName { get; set; }
    }
}
