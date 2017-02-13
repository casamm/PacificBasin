using PureMVC.Patterns.Facade;
using Entitlement.Shell.Controller;
using Entitlement.Shell.View.Components;

namespace Entitlement.Shell
{
    public class ApplicationFacade : Facade
    {

        public ApplicationFacade(string key) : base(key)
        {
        }

        protected override void InitializeController()
        {
            base.InitializeController();
            RegisterCommand(STARTUP, () => new StartupCommand());
        }

        public static ApplicationFacade GetInstance(string key)
        {
            return (ApplicationFacade)Facade.GetInstance(key, () => new ApplicationFacade(key));
        }

        public void Startup(Startup startup)
        {
            SendNotification(STARTUP, startup);
        }

        public const string STARTUP = "startup";
        public const string CONNECT_MODULE_TO_SHELL = "connectModuleToShell";
        public const string SERVICE = "service";
    }
}
