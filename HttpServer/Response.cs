using System.Net.Sockets;

namespace HttpServer
{
    public class Response
    {
        private byte[] _data = null;
        private String _status;
        private String mineType;
        private Response(String status, String type, Byte[] data)
        {
            _data = data;
            _status = status;
            mineType = type;
        }

        public static Response From(Request request)
        {
            if (request == null)
            {
                return MakeNullRequest();
            }
            if (request.Type == "GET")
            {
                String file = Environment.CurrentDirectory + HTTPServer.WEB_DIR + request.URL;
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Exists && fileInfo.Extension.Contains("."))
                {
                    return MakeFromFile(fileInfo);
                }
                else
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(fileInfo + "/");
                    FileInfo[] files = directoryInfo.GetFiles();
                    foreach (FileInfo fileInFiles in files)
                    {
                        String fileName = fileInFiles.Name;
                        if (fileName.Contains("default.html") || fileName.Contains("default.htm") ||
                            fileName.Contains("index.htm") || fileName.Contains("default.html"))
                            MakeFromFile(fileInFiles);
                    }
                }
                if (!fileInfo.Exists)
                    return MakePageNotFound();
            }
            else
                return MakeMethodNotFound();


            return MakePageNotFound();
        }

        private static Response MakeFromFile(FileInfo fileInfo)
        {
            String file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "400.html";
            FileInfo fileInfo = new FileInfo(file);
            FileStream fileStream = fileInfo.OpenRead();
            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] data = new Byte[fileStream.Length];
            reader.Read(data, 0, data.Length);


            return new Response("400 bad request", "html/text", data);
        }

        private static Response MakeNullRequest()
        {
            String file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "400.html";
            FileInfo fileInfo = new FileInfo(file);
            FileStream fileStream = fileInfo.OpenRead();
            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] data = new Byte[fileStream.Length];
            reader.Read(data, 0, data.Length);


            return new Response("400 bad request", "html/text", data);
        }

        private static Response MakePageNotFound()
        {
            String file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "404.html";
            FileInfo fileInfo = new FileInfo(file);
            FileStream fileStream = fileInfo.OpenRead();
            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] data = new Byte[fileStream.Length];
            reader.Read(data, 0, data.Length);


            return new Response("404 page not found", "html/text", data);
        }
        private static Response MakeMethodNotFound()
        {
            String file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "405.html";
            FileInfo fileInfo = new FileInfo(file);
            FileStream fileStream = fileInfo.OpenRead();
            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] data = new Byte[fileStream.Length];
            reader.Read(data, 0, data.Length);


            return new Response("405 method not allowed", "html/text", data);
        }

        public void Post(NetworkStream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(String.Format("{0} {1}\r\nServer: {2}\r\nContent-Type: {3}\r\nAccept-Ranges\r\nContent-length: {4}\r\n",
                HTTPServer.VERSION, _status.Length, HTTPServer.SERVER, mineType, _data.Length));

            stream.Write(_data, 0, _data.Length);
        }
    }
}
