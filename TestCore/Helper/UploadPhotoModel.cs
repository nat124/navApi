using System.Net;

namespace TestCore
{
    public class UploadPhotoModel
    {
        public bool Success { get; set; }
        public string FileName { get; set; }
        public HttpStatusCode code { get; set; }
    }
}