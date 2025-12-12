

using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;

namespace AI;

public class ImageClassification : IDisposable
{
    private readonly InferenceSession _session;
    private readonly string[] _labels = new string[]
    {
        "normal_scene", "traffic_accident", "damaged_structures", "fire",
        "flood", "injured_person", "smoke"
    };
    private const int IMG_SIZE = 224;
    private const float PREDICTION_THRESHOLD = 0.05f; // Same as Python's THRESHOLD

    public ImageClassification()
    {
        string modelPath = Path.Combine(AppContext.BaseDirectory, "Model", "imageClassmodel.onnx");
        _session = new InferenceSession(modelPath);
    }
    public HashSet<string> Predict(List<byte[]> inputs)
    {
        List<Dictionary<string, float>> predictions = new();
        HashSet<string> finalPredictions = new();

        foreach (byte[] bytes in inputs)
        {
            Dictionary<string, float> predictResult = PredictSingle(bytes);
            predictions.Add(predictResult);
        }


        foreach (Dictionary<string, float> pred in predictions)
        {
            foreach(KeyValuePair<string, float> entry in pred)
            {
                if (entry.Value >= PREDICTION_THRESHOLD)
                {
                    finalPredictions.Add(entry.Key);
                }
            }
        }
        return finalPredictions;
    }

    private Dictionary<string, float> PredictSingle(byte[] imageBytes)
    {
        // 1. Decode, Resize, and Normalize Image
        Tensor<float> inputTensor = ProcessImageBytes(imageBytes);

        // 2. Prepare Input for ONNX Runtime
        var input = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("input_1", inputTensor) // 'input_1' is the input name from conversion
        };

        // 3. Run Inference
        using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = _session.Run(input);
        // 4. Process Output
        // The output name might be 'Identity:0', 'dense_1/Sigmoid:0', or similar.
        // Inspect your ONNX model with a tool like Netron to confirm the exact output name.
        // For now, we'll assume there's one output and take its data.
        Tensor<float> outputTensor = results[0].AsTensor<float>();
        float[] probabilities = outputTensor.ToArray(); // This will be an array of 7 floats

        var predictions = new Dictionary<string, float>();
        for (int i = 0; i < _labels.Length; i++)
        {
            predictions[_labels[i]] = probabilities[i];
        }
        return predictions;
    }

    private Tensor<float> ProcessImageBytes(byte[] imageBytes)
    {
        using var ms = new MemoryStream(imageBytes);

        using var image = Image.Load<Rgb24>(ms);

        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(IMG_SIZE, IMG_SIZE),
            Mode = ResizeMode.Stretch // or ResizeMode.Crop/Max, depending on your needs
        }));

        // Create a tensor with shape [1, IMG_SIZE, IMG_SIZE, 3]
        // ONNX Runtime expects channels-last for TensorFlow conversions usually
        float[] inputData = new float[1 * IMG_SIZE * IMG_SIZE * 3];
        int index = 0;
        for (int y = 0; y < IMG_SIZE; y++)
        {
            for (int x = 0; x < IMG_SIZE; x++)
            {
                Rgb24 pixel = image[x, y];
                inputData[index++] = pixel.R / 255.0f;
                inputData[index++] = pixel.G / 255.0f;
                inputData[index++] = pixel.B / 255.0f;
            }
        }
        return new DenseTensor<float>(inputData, new[] { 1, IMG_SIZE, IMG_SIZE, 3 });
    }

    public void Dispose()
    {
        _session.Dispose();
    }
}
