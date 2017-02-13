using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using Entitlement.Shell.View;

namespace Entitlement.Shell.Controller
{
    public class StartupCommand: SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            Facade.RegisterMediator(new ServiceMediator(notification.Body));
            Facade.RegisterMediator(new ServiceJunctionMediator());

            Modules.Entitlement.Entitlement entitlement = new Modules.Entitlement.Entitlement();
            Facade.SendNotification(ApplicationFacade.CONNECT_MODULE_TO_SHELL, entitlement);
            Facade.RegisterMediator(new ModuleMediator(Modules.Entitlement.Entitlement.NAME, entitlement));
        }
    }
}
