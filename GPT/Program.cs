using OpenAI.Audio;
using OpenAI.Chat;
using OpenAI.Images;
using System.ClientModel;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;

namespace Example1
{
    //// setx OPENAI_API_KEY ""  // wstaw swój klucz API
    internal class Program
    {
        static void Main1(string[] args)
        {

            ChatClient client = new("gpt-4o", Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

            ChatCompletion completion = client.CompleteChat("policz 2+2");

            Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");
        }
    }
}