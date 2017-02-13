using Pipes.Interfaces;
using Pipes.Plumbing;
using PureMVC.Interfaces;

namespace Entitlement.Common.View.Components
{
    public class PipeAwareModule: IPipeAware
    {

        public PipeAwareModule(IFacade facade)
        {
            Facade = facade;
        }

        public void AcceptInputPipe(string name, IPipeFitting pipe)
        {
            Facade.SendNotification(JunctionMediator.ACCEPT_INPUT_PIPE, pipe, name);
        }

        public void AcceptOutputPipe(string name, IPipeFitting pipe)
        {
            Facade.SendNotification(JunctionMediator.ACCEPT_OUTPUT_PIPE, pipe, name);
        }

        protected IFacade Facade { get; set; }

        public static string STDIN = "standardInput";
        public static string STDOUT = "standardOutput";
    }
}
