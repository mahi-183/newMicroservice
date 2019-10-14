using Swashbuckle.AspNetCore.Swagger;

namespace NotesMicroservice
{
    internal class OpenApiInfo : Info
    {
        public string Title { get; set; }
        public string Version { get; set; }
    }
}