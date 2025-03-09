using System.Diagnostics;
using System.Text.RegularExpressions;
using OllamaSharp;

public class AIInterface
{
    private string _piperModelPath;

    private string? _prompt;
    private List<string> _response = new();

    public AIInterface(string piperModelPath)
    {
        this._piperModelPath = piperModelPath;
    }

    public async Task Run()
    {
        var uri = new Uri("http://localhost:11434");
        var ollama = new OllamaApiClient(uri);

        ollama.SelectedModel = "gemma2";

        while (true)
        {
            _prompt = Console.ReadLine();

            if (!string.IsNullOrEmpty(_prompt))
            {
                await foreach (var stream in ollama.GenerateAsync(_prompt))
                {
                    _response.Add(stream?.Response);
                }

                string finalResponse = string.Join("", _response);
                finalResponse = Regex.Replace(finalResponse, @"[^\w\s.,!?;:""'()\-]", "");
                _response.Clear();

                await CallTTS(finalResponse);
            }

            else
            {
                Logger.LogError("Prompt is empty!");
            }
        }
    }


    public Task CallTTS(string message)
    {
        message = message.Replace("\n", " ")
                                     .Replace("\r", "");

        Logger.Log(message);

        Process process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = $"-c \"echo \\\"{message}\\\" | piper-tts --model {_piperModelPath} --output-file output.wav\"",
            UseShellExecute = false,
            CreateNoWindow = true
        };

        process.Start();
        process.WaitForExit();

        PlayTTS();

        return Task.CompletedTask;
    }

    public void PlayTTS()
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