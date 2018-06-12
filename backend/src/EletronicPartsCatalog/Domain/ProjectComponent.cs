using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EletronicPartsCatalog.Api.Domain
{
    public class ProjectComponent
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public string ComponentId { get; set; }
        public Component Component { get; set; }
    }
}
