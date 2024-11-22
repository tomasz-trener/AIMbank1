using Azure.AI.Vision.Common;
using Azure.AI.Vision.ImageAnalysis;
using Azure;
using System;
using System.IO;

class ImageAnalysis
{
    static void Main()
    {
        var serviceOptions = new VisionServiceOptions("adres", new AzureKeyCredential(" "));

        using var imageSource = VisionSource.FromUrl(new Uri("https://idodata.com/wp-content/uploads/2023/12/ComputerOperator-scaled.jpg"));

        var analysisOptions = new ImageAnalysisOptions()
        {
            Features = ImageAnalysisFeature.Caption | ImageAnalysisFeature.Text | ImageAnalysisFeature.Tags | ImageAnalysisFeature.Objects | ImageAnalysisFeature.DenseCaptions | ImageAnalysisFeature.People,
            Language = "en",
            GenderNeutralCaption = false
        };

        using var analyzer = new ImageAnalyzer(serviceOptions, imageSource, analysisOptions);
        var result = analyzer.Analyze();

        if (result.Reason == ImageAnalysisResultReason.Analyzed)
        {
            if (result.Caption != null)
            {
                Console.WriteLine($"Caption: {result.Caption.Content}");
            }

            if (result.DenseCaptions != null)
            {
                foreach (var varDensecaption in result.DenseCaptions)
                {
                    Console.WriteLine($"Dense Caption: {varDensecaption.Content}, {varDensecaption.Confidence}");
                }
            }

            if (result.Tags != null)
            {
                foreach (var varTag in result.Tags)
                {
                    Console.WriteLine($"Tag: {varTag.Name}");
                }
            }

            if (result.People != null)
            {
                foreach (var varPeople in result.People)
                {
                    Console.WriteLine($"People: {varPeople.Confidence}");
                }
            }

            if (result.Objects != null)
            {
                foreach (var varObject in result.Objects)
                {
                    Console.WriteLine($"Object: {varObject.Name}");
                }
            }

            if (result.Text != null)
            {
                foreach (var varLine in result.Text.Lines)
                {
                    Console.WriteLine($"Line: {varLine.Content}");
                    foreach (var varWord in varLine.Words)
                    {
                        Console.WriteLine($"Word: {varWord.Content}");
                    }
                }
            }

        }
        else
        {
            var errorDetails = ImageAnalysisErrorDetails.FromResult(result);
            Console.WriteLine(" Analysis failed.");
            Console.WriteLine($"   Error reason : {errorDetails.Reason}");
            Console.WriteLine($"   Error code : {errorDetails.ErrorCode}");
            Console.WriteLine($"   Error message: {errorDetails.Message}");
        }
    }
}