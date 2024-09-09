using HttpServer;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Starting Server at port:420");
        HTTPServer server = new HTTPServer(420);
        server.Start();
    }
}