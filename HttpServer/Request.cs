namespace HttpServer
{
    public class Request
    {
        public string Type { get; set; }
        public string URL { get; set; }
        public string Host { get; set; }
        private Request(String type, String url, String host)
        {
            Type = type;
            Url = url;
            Host = host;
        }
        public static Request GetRequest(String request)
        {
            if (String.IsNullOrEmpty(request))
                return null;

            String[] tokens = request.Split(' ');
            String type = tokens[0];
            String url = tokens[1];
            String host = tokens[4];
            return new Request(type, url, host);
        }

    }
}
