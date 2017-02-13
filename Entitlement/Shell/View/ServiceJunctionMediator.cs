using Entitlement.Common.View.Components;
using Pipes.Plumbing;
using System.Collections.Generic;
using PureMVC.Interfaces;
using Pipes.Interfaces;
using Pipes.Messages;

namespace Entitlement.Shell.View
{
    public class ServiceJunctionMediator: JunctionMediator
    {
        public ServiceJunctionMediator(): base(NAME, new Junction())
        {
        }

        public override void OnRegister()
        {
            Junction.RegisterPipe(PipeAwareModule.STDOUT, Junction.OUTPUT, new TeeSplit());
        }

        public override string[] ListNotificationInterests()
        {
            List<string> interests = new List<string>(base.ListNotificationInterests())
            {
                ApplicationFacade.CONNECT_MODULE_TO_SHELL,
                ApplicationFacade.SERVICE
            };
            return interests.ToArray();
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case ApplicationFacade.SERVICE:
                    IPipeMessage message = new Message(Message.NORMAL, null, notification.Body);
                    Junction.SendMessage(PipeAwareModule.STDOUT, message);
                    break;
                case ApplicationFacade.CONNECT_MODULE_TO_SHELL:
                    IPipeAware module = (IPipeAware)notification.Body;
                    IPipeFitting pipe = new Pipe();
                    module.AcceptInputPipe(PipeAwareModule.STDIN, pipe);
                    TeeSplit teeSplit = (TeeSplit)Junction.RetrievePipe(PipeAwareModule.STDOUT);
                    teeSplit.Connect(pipe);
                    break;
                default:
                    break;
            }
        }

        public static new string NAME = "ServiceJunctionMediator";
    }
}
