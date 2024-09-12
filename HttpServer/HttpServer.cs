using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HttpServer
{
    public class HTTPServer
    {
        public const String MSG_DIR = "C:\\Users\\Gardna\\projects\\HttpServer\\HttpServer\\bin\\Debug\\net8.0\\Root\\msg";
        public const String WEB_DIR = "C:\\Users\\Gardna\\projects\\HttpServer\\HttpServer\\bin\\Debug\\net8.0\\Root\\web";
        public const String VERSION = "HTTP/1.1";
        public const String SERVER = "Dream";

        private bool running = false;

        private TcpListener listener;

        public HTTPServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            Thread serverThread = new Thread(new ThreadStart(Run));
            serverThread.Start();

        }

        private void Run()
        {
            running = true;
            listener.Start();

            while (running)
            {
                Console.WriteLine("Waiting for Connection...");

                TcpClient client = listener.AcceptTcpClient();

                Console.WriteLine("Client Conected.");
                HandleClient(client);

                client.Close();
            }
            running = false;
            listener.Stop();
        }

        private void HandleClient(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream());


            String msg = "";
            while (reader.Peek() != -1)
            {
                msg += reader.ReadLine() + "\n";
            }
            byte[] bytes = Encoding.Default.GetBytes(msg);
            msg = Encoding.UTF8.GetString(bytes);
            Debug.WriteLine("Request: \n" + msg);

            Request req = Request.GetRequest(msg);
            Response resp = Response.From(req);
            resp.Post(client.GetStream());
        }
    }
}
