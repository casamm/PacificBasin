using Entitlement.Modules.Entitlement.Model.Vo;
using Entitlement.Common.View.Components;
using Microsoft.AspNetCore.Http;
using Pipes.Interfaces;
using Pipes.Plumbing;
using PureMVC.Interfaces;
using Entitlement.Common.Model.Vo;
using System;

namespace Entitlement.Modules.Entitlement.View
{
    public class ServiceJunctionMediator: JunctionMediator
    {

        public ServiceJunctionMediator() : base(NAME, new Junction())
        {
        }

        public override void HandleNotification(INotification notification)
        {
            switch(notification.Name)
            {
                case JunctionMediator.ACCEPT_INPUT_PIPE:
                    IPipeFitting pipe = (IPipeFitting)notification.Body;
                    Filter filter = new Filter("service", new PipeListener(this, HandlePipeMessage), (IPipeMessage message, object _params) => {
                        bool found = false;
                        HttpRequest request = ((ContextVo)message.Body).Context.Request;
                        foreach (string uri in (string[])_params)
                        {
                            if (request.Path.Value.IndexOf(uri) != -1) found = true;
                        }
                        if (found == false) throw new Exception("Filtering Route");
                    }, new string[] { "/entitlement" }); 
                    pipe.Connect(filter);
                    Junction.RegisterPipe(PipeAwareModule.STDIN, Junction.INPUT, pipe);
                    break;
                default:
                    base.HandleNotification(notification);
                    break;
            }
        }

        public override void HandlePipeMessage(IPipeMessage message)
        {
            SendNotification(ApplicationFacade.SERVICE, new RequestVo((ContextVo)message.Body));
        }

        public static new string NAME = "ServiceJunctionMediator";

    }
}
