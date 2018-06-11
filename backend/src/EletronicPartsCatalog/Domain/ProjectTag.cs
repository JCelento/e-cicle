namespace EletronicPartsCatalog.Api.Domain
{
    public class ProjectTag
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public string TagId { get; set; }
        public Tag Tag { get; set; }
    }
}