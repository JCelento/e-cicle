using System.Collections.Generic;
using EletronicPartsCatalog.Api.Domain;

namespace EletronicPartsCatalog.Features.Components
{
    public class ComponentsEnvelope
    {
        public List<Component> Components { get; set; }

        public int ComponentsCount { get; set; }
    }
}