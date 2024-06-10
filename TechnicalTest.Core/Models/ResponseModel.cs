using System.Text.Json.Serialization;

namespace TechnicalTest.Core.Models
{
    public class ResponseModel
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; } = 200;
    }
}
