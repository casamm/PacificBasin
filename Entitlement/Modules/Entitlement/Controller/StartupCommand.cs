using Entitlement.Modules.Entitlement.Model;
using Entitlement.Modules.Entitlement.View;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace Entitlement.Modules.Entitlement.Controller
{
    public class StartupCommand: SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            Facade.RegisterCommand(ApplicationFacade.SERVICE, () => new ServiceCommand());
            Facade.RegisterProxy(new ServiceProxy());
            Facade.RegisterMediator(new ServiceMediator());
            Facade.RegisterMediator(new ServiceJunctionMediator());
        }
    }
}
