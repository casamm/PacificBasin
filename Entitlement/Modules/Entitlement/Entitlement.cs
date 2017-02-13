using Entitlement.Common.View.Components;

namespace Entitlement.Modules.Entitlement
{
    public class Entitlement : PipeAwareModule
    {
        public Entitlement() : base(ApplicationFacade.GetInstance(NAME, () => new ApplicationFacade(NAME)))
        {
            ((ApplicationFacade)Facade).Startup();
        }

        public static string NAME = "Entitlement.Entitlement";
    }
}
