using System;
using Newtonsoft.Json;

namespace EletronicPartsCatalog.Api.Domain
{
   public class Comment
    {
        [JsonProperty("id")]
        public int CommentId { get; set; }

        public string Body { get; set; }

        public Person Author { get; set; }

        [JsonIgnore]
        public Project Project { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}