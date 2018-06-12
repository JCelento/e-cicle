
namespace EletronicPartsCatalog.Api.Domain
{
    public class ComponentWhereToFindIt
    {
        public string ComponentId { get; set; }
        public Component Component { get; set; }

        public string WhereToFindItId { get; set; }
        public WhereToFindIt WhereToFindIt { get; set; }
    }
}