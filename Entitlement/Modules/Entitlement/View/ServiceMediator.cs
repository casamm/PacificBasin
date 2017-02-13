using PureMVC.Patterns.Mediator;
using PureMVC.Interfaces;
using Entitlement.Modules.Entitlement.Model.Vo;
using Entitlement.Modules.Entitlement.View.Components;

namespace Entitlement.Modules.Entitlement.View
{
    public class ServiceMediator : Mediator
    {
        public ServiceMediator() : base(NAME, new Service())
        {
        }

        public override string[] ListNotificationInterests()
        {
            return new string[] {
                ApplicationFacade.SERVICE_RESULT,
                ApplicationFacade.SERVICE_FAULT
            };
        }

        public override void HandleNotification(INotification notification)
        {
            RequestVo requestVo = (RequestVo)notification.Body;
            switch (notification.Name)
            {
                case ApplicationFacade.SERVICE_RESULT:
                    Service.Result(requestVo);
                    break;

                case ApplicationFacade.SERVICE_FAULT:
                    Service.Fault(requestVo);
                    break;
                default:
                    break;
            }
        }

        public Service Service { get { return (Service)ViewComponent; } }

        public static new string NAME = "ServiceMediator";
    }
}
