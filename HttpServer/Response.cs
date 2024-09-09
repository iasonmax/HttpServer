using System.Net.Sockets;

namespace HttpServer
{
    public class Response
    {
        private byte[] data = null;
        private Response(Byte[] data)
        {

        }

        public static Response From(Request request)
        {

        }

        public void Post(NetworkStream stream)
        {
            stream.Write()
        }
    }
}
