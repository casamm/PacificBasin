using PureMVC.Patterns.Mediator;
using Entitlement.Shell.View.Interfaces;
using Entitlement.Shell.View.Components;
using System.Threading.Tasks;
using Entitlement.Common.Model.Vo;

namespace Entitlement.Shell.View
{
    public class ServiceMediator : Mediator, IService
    {
        public ServiceMediator(object viewComponent) : base(NAME, viewComponent)
        {
        }

        public override void OnRegister()
        {
            ((Startup)ViewComponent).Delegate = this;
        }

        public Task<object> Service(ContextVo contextVo)
        {
            SendNotification(ApplicationFacade.SERVICE, contextVo);
            return contextVo.TaskCompletionSource.Task;
        }

        public static new string NAME = "ServiceMediator";
    }
}
