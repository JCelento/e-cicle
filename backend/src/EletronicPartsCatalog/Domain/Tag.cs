using System.Collections.Generic;

namespace EletronicPartsCatalog.Domain
{
    public class Tag
    {
        public string TagId { get; set; }

        public List<ProjectTag> ProjectTags { get; set; }
    }
}