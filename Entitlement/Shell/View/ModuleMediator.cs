using PureMVC.Patterns.Mediator;

namespace Entitlement.Shell.View
{
    public class ModuleMediator : Mediator
    {
        public ModuleMediator(string mediatorName, object viewComponent) : base(mediatorName, viewComponent)
        {
        }
    }
}
