using System.Collections.Generic;

namespace NoobSharp.Common.WinUI.Helpers.Tenor.JSON.SearchResult
{
    public class MediaFormat
    {
        public string url { get; set; }
        public double duration { get; set; }
        public string preview { get; set; }
        public List<int> dims { get; set; }
        public int size { get; set; }
    }

    public class MediaFormats
    {
        public MediaFormat mediumgif { get; set; }
        public MediaFormat tinymp4 { get; set; }
        public MediaFormat gif { get; set; }
        public MediaFormat tinywebm { get; set; }
        public MediaFormat mp4 { get; set; }
        public MediaFormat tinygifpreview { get; set; }
        public MediaFormat nanowebm { get; set; }
        public MediaFormat nanomp4 { get; set; }
        public MediaFormat nanogif { get; set; }
        public MediaFormat loopedmp4 { get; set; }
        public MediaFormat webm { get; set; }
        public MediaFormat gifpreview { get; set; }
        public MediaFormat nanogifpreview { get; set; }
        public MediaFormat tinygif { get; set; }
    }
    public class Result
    {
        public string id { get; set; }
        public string title { get; set; }
        public MediaFormats media_formats { get; set; }
        public double created { get; set; }
        public string content_description { get; set; }
        public string itemurl { get; set; }
        public string url { get; set; }
        public List<string> tags { get; set; }
        public List<object> flags { get; set; }
        public bool hasaudio { get; set; }
    }

    public class Root
    {
        public List<Result> results { get; set; }
        public string next { get; set; }
    }

}