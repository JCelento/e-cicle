using System.Collections.Generic;
using EletronicPartsCatalog.Api.Domain;

namespace EletronicPartsCatalog.Features.Projects
{
    public class ProjectsEnvelope
    {
        public List<Project> Projects { get; set; }

        public int ProjectsCount { get; set; }
    }
}