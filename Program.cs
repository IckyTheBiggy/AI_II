using System.Diagnostics;
using System.Threading.Tasks;
using OllamaSharp;
using Spectre.Console;

class Program
{
    static async Task Main(string[] args)
    {
        var uri = new Uri("http://localhost:11434");
        var ollama = new OllamaApiClient(uri);

        ollama.SelectedModel = "gemma2";

        await foreach (var stream in ollama.GenerateAsync("Hello"))
        {
            Console.Write(stream?.Response);
        }
    }
}