using PureMVC.Patterns.Facade;
using Entitlement.Modules.Entitlement.Controller;

namespace Entitlement.Modules.Entitlement
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

        public void Startup()
        {
            SendNotification(STARTUP);
        }

        public const string STARTUP = "startup";
        public const string SERVICE = "service";
        public const string SERVICE_RESULT = "serviceResult";
        public const string SERVICE_FAULT = "serviceFault";
    }
}
