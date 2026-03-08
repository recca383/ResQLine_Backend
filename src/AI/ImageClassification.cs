using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Text.Json;

namespace AI;

public class ImageClassification : IDisposable
{
    private readonly InferenceSession _session;
    private readonly Dictionary<int, string> _labelMap;

    private const int IMG_SIZE = 224;
    //private const float PREDICTION_THRESHOLD = 0.46f;

    public ImageClassification()
    {
        string basePath = AppContext.BaseDirectory;

        string modelPath = Path.Combine(basePath, "Model", "image_recognition.onnx");
        string labelPath = Path.Combine(basePath, "Model", "label_map.json");

        _session = new InferenceSession(modelPath);

        // Load label map from JSON
        string json = File.ReadAllText(labelPath);
        _labelMap = JsonSerializer.Deserialize<Dictionary<int, string>>(json)!;
    }

    public Dictionary<string, float> Predict(List<byte[]> inputs)
    {
        Dictionary<string, float> finalPredictions = new();

        foreach (byte[] bytes in inputs)
        {
            float[] probabilities = PredictSingle(bytes);

            for (int i = 0; i < probabilities.Length; i++)
            {
                if (!_labelMap.TryGetValue(i, out string? label))
                {
                    continue;
                }
                finalPredictions.Add(label, probabilities[i]);
            }
        }

        return finalPredictions;
    }

    private float[] PredictSingle(byte[] imageBytes)
    {
        Tensor<float> inputTensor = ProcessImageBytes(imageBytes);

        var input = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("input_1", inputTensor)
        };

        using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = _session.Run(input);

        Tensor<float> outputTensor = results[0].AsTensor<float>();

        return outputTensor.ToArray();
    }

    private Tensor<float> ProcessImageBytes(byte[] imageBytes)
    {
        using var image = Image.Load<Rgb24>(imageBytes);

        // Resize to 224x224
        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(IMG_SIZE, IMG_SIZE),
            Mode = ResizeMode.Stretch
        }));

        // Allocate tensor buffer
        float[] inputData = new float[1 * IMG_SIZE * IMG_SIZE * 3];

        int index = 0;

        for (int y = 0; y < IMG_SIZE; y++)
        {
            for (int x = 0; x < IMG_SIZE; x++)
            {
                Rgb24 pixel = image[x, y];

                // Normalize [0,255] → [0,1]
                inputData[index++] = pixel.R / 255f;
                inputData[index++] = pixel.G / 255f;
                inputData[index++] = pixel.B / 255f;
            }
        }

        // Shape: [1, 224, 224, 3]
        return new DenseTensor<float>(inputData, new[] { 1, IMG_SIZE, IMG_SIZE, 3 });
    }

    public void Dispose()
    {
        _session.Dispose();
    }
}
