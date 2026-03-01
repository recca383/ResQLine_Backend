using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Versioning;
using System.Text.Json;
using AI;
namespace Tester;

internal sealed class Program
{

    public static void Main()
    {

        Image();
        //string sampleImagePath = Path.Combine(AppContext.BaseDirectory, "Model", "test.jpg");


        //using var predictor = new ImageClassification();
        //Console.WriteLine(Resources.MakingPrediction);
        //float[] fusedProbabilitiesFromPath = predictor.Predict("there's a flood here",sampleImagePath);

        //Console.WriteLine(Resources.Top10FromPath);
        //var sortedResultsFromPath = fusedProbabilitiesFromPath
        //    .Select((score, index) => new { Label = Constants.UNION_LABELS[index], Score = score })
        //    .OrderByDescending(x => x.Score)
        //    .Take(10);

        //foreach (var item in sortedResultsFromPath)
        //{
        //    Console.WriteLine($"{item.Label} : {item.Score:F4}");
        //}

        //// --- Demonstrate prediction with byte array image input ---
        //// Load image into byte array
        //byte[] sampleImageBytes = File.ReadAllBytes(sampleImagePath);

        //Console.WriteLine(Resources.MakingPredictionBytes);
        //float[] fusedProbabilitiesFromBytes = predictor.Predict("there's a flood here", sampleImageBytes);

        //Console.WriteLine(Resources.Top10FromBytes);
        //var sortedResultsFromBytes = fusedProbabilitiesFromBytes
        //    .Select((score, index) => new { Label = Constants.UNION_LABELS[index], Score = score })
        //    .OrderByDescending(x => x.Score)
        //    .Take(10);

        //foreach (var item in sortedResultsFromBytes)
        //{
        //    Console.WriteLine($"{item.Label} : {item.Score:F4}");
        //}

        
    }
    private static void Image()
    {
        var imageClassification = new ImageClassification();

        string path = Path.Combine(AppContext.BaseDirectory, "Model", "test.jpg");
        byte[] imageBytes = File.ReadAllBytes(path);

        HashSet<string> results = imageClassification.Predict(new List<byte[]> { imageBytes });


        foreach (string label in results)
        {
            Console.WriteLine($"Predicted label: {label}");
        }

        imageClassification.Dispose();
    }
}
