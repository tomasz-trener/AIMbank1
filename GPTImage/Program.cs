using OpenAI.Audio;
using OpenAI.Chat;
using OpenAI.Images;
using System.ClientModel;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;


internal class Program
{
    static void Main(string[] args)
    {
        // Inicjalizacja klienta ImageClient z kluczem API
        string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        if (string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine("Brak klucza API. Ustaw zmienną środowiskową OPENAI_API_KEY.");
            return;
        }

        ImageClient client = new("dall-e-3", apiKey);

        // Opis obrazu (prompt), który chcemy wygenerować
        string prompt = "Uczelnia wyższa do której przylatują samolotami studenci";

        // Konfiguracja opcji generowania obrazu
        ImageGenerationOptions options = new()
        {
            Quality = GeneratedImageQuality.Standard,
            Size = GeneratedImageSize.W1024xH1792,
            Style = GeneratedImageStyle.Natural,
            ResponseFormat = GeneratedImageFormat.Bytes // Dane obrazu jako BinaryData, które można zapisać jako plik
        };

        try
        {
            // Generowanie obrazu
            GeneratedImage image = client.GenerateImage(prompt, options);

            // Przechwycenie danych binarnych obrazu
            BinaryData bytes = image.ImageBytes;

            // Zapis obrazu do pliku PNG
            string fileName = $"{Guid.NewGuid()}.png";
            using FileStream stream = File.OpenWrite(fileName);
            bytes.ToStream().CopyTo(stream);

            Console.WriteLine($"Obraz został zapisany jako {fileName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd podczas generowania obrazu: {ex.Message}");
        }
    }
}