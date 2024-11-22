using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using System.Net;

namespace P06DocumentIntelligence
{
    // Document reading model
    internal class Program1
    {
        static async Task Main(string[] args)
        {
            string key = "...";
            string endpoint = "...";

            DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), new AzureKeyCredential(key));

            Uri documentUri = new Uri("https://tomaszles.pl/pliki/pdf_sample.pdf");

            AnalyzeDocumentOperation operation = await client.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, "prebuilt-read",
            documentUri);

            AnalyzeResult result = operation.Value;

            foreach (DocumentPage page in result.Pages)
            {
                Console.WriteLine($"Document Page {page.PageNumber} has {page.Lines.Count} lines and {page.Words.Count} words");

                for (int i = 0; i < page.Lines.Count; i++)
                {
                    DocumentLine line = page.Lines[i];
                    Console.WriteLine($"Line {i + 1}: {line.Content}");
                }

                for (int i = 0; i < page.Words.Count; i++)
                {
                    DocumentWord word = page.Words[i];
                    Console.WriteLine($"Word {i + 1}: {word.Content} ({word.Confidence})");
                }

            }

            foreach (DocumentParagraph paragraph in result.Paragraphs)
            {
                Console.WriteLine($"{paragraph.Role}: {paragraph.Content}");
            }
        }
    }
}
