using EletronicPartsCatalog.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EletronicPartsCatalog.Api.Domain
{
    public class WhereToFindIt
    {
            public string WhereToFindItId { get; set; }

            public List<ComponentWhereToFindIt> ComponentWhereToFindIt { get; set; }
    }
}
