namespace WebApplication1.Models
{
    using Newtonsoft.Json;
    public class Student
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        /*
        [JsonProperty("birthdate")]
        public DateOnly BirthDate { get; set; }
        */

        [JsonProperty("gender")]
        public string? Gender { get; set; }

        [JsonProperty("university")]
        public string? University { get; set; }

    }
}
