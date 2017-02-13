using Entitlement.Common.Model.Request;
using Entitlement.Modules.Entitlement.Model.Vo;
using Microsoft.AspNetCore.Http;
using Entitlement.Modules.Entitlement.Model;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace Entitlement.Modules.Entitlement.Controller
{
    public class ServiceCommand: SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            RequestVo requestVo = (RequestVo)notification.Body;
            HttpRequest request = requestVo.ContextVo.Context.Request;
            HttpResponse response = requestVo.ContextVo.Context.Response;
            ServiceRequest serviceRequest = new ServiceRequest(requestVo, Result, this);
            ServiceProxy serviceProxy = (ServiceProxy)Facade.RetrieveProxy(ServiceProxy.NAME);

            switch (request.Path.Value)
            {
                case "/entitlement/SignInWithCredentials":
                    serviceProxy.SignInWithCredentialsAsync(serviceRequest);
                    break;
                case "/entitlement/RenewAuthToken":
                    serviceProxy.RenewAuthToken(serviceRequest);
                    break;
                case "/entitlement/entitlements":
                    serviceProxy.GetEntitlementsAsync(serviceRequest);
                    break;
                default:
                    response.WriteAsync(request.Path.Value);
                    serviceRequest.RequestVo.SetResult();
                    break;
            }
        }

        private void Result(INotification notification)
        {
            ServiceRequest serviceRequest = (ServiceRequest)notification.Body;
            switch (notification.Name)
            {
                case ServiceRequest.RESULT:
                    SendNotification(ApplicationFacade.SERVICE_RESULT, serviceRequest.RequestVo);
                    break;

                case ServiceRequest.FAULT:
                    SendNotification(ApplicationFacade.SERVICE_FAULT, serviceRequest.RequestVo);
                    break;
            }
        }
    }
}
