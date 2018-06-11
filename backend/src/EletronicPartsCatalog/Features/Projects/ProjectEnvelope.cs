using EletronicPartsCatalog.Api.Domain;

namespace EletronicPartsCatalog.Features.Projects
{
    public class ProjectEnvelope
    {
        public ProjectEnvelope(Project Project)
        {
            this.Project = Project;
        }

        public Project Project { get; }
    }
}