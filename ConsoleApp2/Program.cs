 
namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string imie= "Jan";
            string nazwisko = "Kowalski";

            //teraz wypisujemy 10 razy imię i nazwisko 
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(imie + " " + nazwisko);
            }


        }
    }
}
