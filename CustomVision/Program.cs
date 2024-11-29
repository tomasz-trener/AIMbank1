using System;
using System.IO;
using System.Net.Http;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;


// custom model 
class Program2
{
    static void Main(string[] args)
    {
        // Replace these with your Azure Custom Vision project details
        string endpoint = "/";
        string predictionKey = "";
        Guid projectId = new Guid("");
        string iterationName = "Iteration1";

        // Create a CustomVisionPredictionClient
        CustomVisionPredictionClient visionPredictionClient = new CustomVisionPredictionClient(new ApiKeyServiceClientCredentials(predictionKey))
        {
            Endpoint = endpoint
        };

        // URL of the image to be predicted
        string imageUrl = "https://idodata.com/wp-content/uploads/2023/12/Contoso-Tea-31.jpeg";

        // Download the image from the URL
        byte[] imageBytes;
        using (var httpClient = new HttpClient())
        {
            imageBytes = httpClient.GetByteArrayAsync(imageUrl).Result;
        }

        using (Stream imageStream = new MemoryStream(imageBytes))
        {
            // Perform the image classification OR object detection
            // var result = visionPredictionClient.ClassifyImage(projectId, iterationName, imageStream); // image classification
            var result = visionPredictionClient.DetectImage(projectId, iterationName, imageStream); // object detection

            // Process and display the prediction results
            if (result.Predictions != null && result.Predictions.Count > 0)
            {
                foreach (var prediction in result.Predictions)
                {
                    Console.WriteLine($"Tag: {prediction.TagName}, Probability: {prediction.Probability:P1}" + 
                        $"Bounding box: {prediction.BoundingBox.Top} - {prediction.BoundingBox.Left}");
                }
            }
            else
            {
                Console.WriteLine("No predictions found.");
            }
        }
    }
}


