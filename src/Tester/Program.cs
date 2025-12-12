using AI;

namespace Tester;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        var imageClassification = new ImageClassification();

        string path = Path.Combine(AppContext.BaseDirectory, "Model", "test.jpg");
        byte[] imageBytes = File.ReadAllBytes(path);

        HashSet<string> results = imageClassification.Predict(new List<byte[]> { imageBytes });
        

        foreach(string label in results)
        {
            Console.WriteLine($"Predicted label: {label}");
        }

        imageClassification.Dispose();
    }
}
