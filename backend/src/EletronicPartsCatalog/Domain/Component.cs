using EletronicPartsCatalog.Api.Domain;
using EletronicPartsCatalog.Api.Resources.Images;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EletronicPartsCatalog.Api.Domain
{
    public class Component
    {

        public Component()
        {
            ComponentImage = ImagePath.ImageNotProvided;
        }

        public string Slug { get; set; }

        public string ComponentId { get; set; }

        public string Description { get; set; }
        
        public string ComponentImage { get; set; }

        public List<Comment> Comments { get; set; }

        [NotMapped]
        public List<string> WhereToFindItList => (ComponentWhereToFindIt?.Select(x => x.WhereToFindItId) ?? Enumerable.Empty<string>()).ToList();

        [JsonIgnore]
        public List<ComponentWhereToFindIt> ComponentWhereToFindIt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<ProjectComponent> ProjectComponents { get; set; }

    }
}
