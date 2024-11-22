using System;
using System.IO;
using System.Text;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;


namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Klucz i region usługi
            string subscriptionKey = "klucz..."; // tutaj wstaw swój klucz zasobu
            string region = "northeurope"; // tutaj wstaw swój region, np. "westeurope"

            // Ścieżka do pliku WAV
            string audioFilePath = @"test_20sek.wav";  // podaj ścieżkę do pliku WAV
            string outputFilePath = @"transcript.txt"; // ścieżka do pliku wynikowego

            // Konfiguracja Speech SDK
            var config = SpeechConfig.FromSubscription(subscriptionKey, region);
            config.SpeechRecognitionLanguage = "pl-PL"; // dla polskiego języka

            // Odczyt pliku WAV
            using var audioInput = AudioConfig.FromWavFileInput(audioFilePath);

            // Inicjalizacja rozpoznawania mowy
            using var recognizer = new SpeechRecognizer(config, audioInput);

            // Rozpoczęcie rozpoznawania mowy
            var result = await recognizer.RecognizeOnceAsync();

            // Sprawdzenie wyników i zapis do pliku
            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                Console.WriteLine($"Transkrypcja: {result.Text}");
                await File.WriteAllTextAsync(outputFilePath, result.Text);
            }
            else if (result.Reason == ResultReason.NoMatch)
            {
                Console.WriteLine("Nie rozpoznano mowy.");
            }
        }
    }
}
