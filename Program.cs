class Program
{
    private const string PIPER_MODEL_PATH = "piper-models/en_US-hfc_female-medium.onnx";

    static async Task Main(string[] args)
    {
        AIInterface aIInterface = new AIInterface(PIPER_MODEL_PATH);
        await aIInterface.Run();
    }
}