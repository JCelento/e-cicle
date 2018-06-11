namespace EletronicPartsCatalog.Api.Domain
{
    public class ProjectFavorite
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}