namespace Projekt2.Exceptions;

public static class GlobalExceptionHandler
{
    public static void Setup()
    {
        AppDomain.CurrentDomain.UnhandledException += (_, args) => 
        {
            Console.WriteLine("Fatální chyba aplikace. Probíhá ukončení.");
        };
    }
}