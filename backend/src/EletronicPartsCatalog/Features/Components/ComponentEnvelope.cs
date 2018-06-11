using EletronicPartsCatalog.Api.Domain;

namespace EletronicPartsCatalog.Features.Components

{
    public class ComponentEnvelope
    {
        public ComponentEnvelope(Component Component)
        {
            this.Component = Component;
        }

        public Component Component { get; }
    }
}