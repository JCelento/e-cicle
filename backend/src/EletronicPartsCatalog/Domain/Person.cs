using System.Collections.Generic;
using Newtonsoft.Json;
using EletronicPartsCatalog.Api.Resources.Images;

namespace EletronicPartsCatalog.Api.Domain
{
    public class Person
    {

        public Person()
        {
            Image = ImagePath.DefaultUserImage;
        }
        [JsonIgnore]
        public int PersonId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Bio { get; set; }

        public string Image { get; set; }

        [JsonIgnore]
        public List<ProjectFavorite> ProjectFavorites { get; set; }

        [JsonIgnore]
        public List<FollowedPeople> Following { get; set; }

        [JsonIgnore]
        public List<FollowedPeople> Followers { get; set; }

        [JsonIgnore]
        public byte[] Hash { get; set; }

        [JsonIgnore]
        public byte[] Salt { get; set; }
    }
}