using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OllamaSharp;

class Program
{
    private const string PIPER_MODEL_PATH = "piper-models/en_US-hfc_female-medium.onnx";

    private static string? _prompt;
    private static List<string> _response = new();

    static async Task Main(string[] args)
    {
        var uri = new Uri("http://localhost:11434");
        var ollama = new OllamaApiClient(uri);

        ollama.SelectedModel = "gemma2";

        _prompt = Console.ReadLine();

        if (!string.IsNullOrEmpty(_prompt))
        {
            await foreach (var stream in ollama.GenerateAsync(_prompt))
            {
                _response.Add(stream?.Response);
            }
        }

        else
        {
            Logger.LogError("Prompt is empty!");
        }

        string finalResponse = string.Join("", _response);
        finalResponse = Regex.Replace(finalResponse, @"[^\w\s.,!?;:""'()\-]", "");

        await CallTTS(finalResponse);
    }

    private static Task CallTTS(string message)
    {
        message = message.Replace("\n", " ")
                                     .Replace("\r", "");

        Logger.Log(message);

        Process process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = $"-c \"echo \\\"{message}\\\" | piper-tts --model {PIPER_MODEL_PATH} --output-file output.wav\"",
            UseShellExecute = false,
            CreateNoWindow = true
        };

        process.Start();
        process.WaitForExit();

        PlayTTS();

        return Task.CompletedTask;
    }

    private static void PlayTTS()
    {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            FileName = "aplay",
            Arguments = $"\"output.wav",
            UseShellExecute = false,
            CreateNoWindow = true
        };

        process.Start();
    }
}