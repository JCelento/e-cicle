using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace EletronicPartsCatalog.Domain
{
    public class Project
    {
        [JsonIgnore]
        public int ProjectId { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        public Person Author { get; set; }

        public List<Comment> Comments {get;set; }

        [NotMapped]
        public bool Favorited => ProjectFavorites?.Any() ?? false;

        [NotMapped]
        public int FavoritesCount => ProjectFavorites?.Count ?? 0;

        [NotMapped]
        public List<string> TagList => (ProjectTags?.Select(x => x.TagId) ?? Enumerable.Empty<string>()).ToList();

        [JsonIgnore]
        public List<ProjectTag> ProjectTags { get; set; }

        [JsonIgnore]
        public List<ProjectFavorite> ProjectFavorites { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}