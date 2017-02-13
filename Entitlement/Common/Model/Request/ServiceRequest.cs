using Entitlement.Modules.Entitlement.Model.Vo;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;
using System;

namespace Entitlement.Common.Model.Request
{
    public class ServiceRequest: Observer
    {
        public ServiceRequest(RequestVo requestVo, Action<INotification> notifyMethod, object notifyContext): base(notifyMethod, notifyContext)
        {
            RequestVo = requestVo;
        }

        public bool HasCallback()
        {
            return NotifyMethod != null && NotifyContext != null;
        }

        public RequestVo RequestVo { get; set; }

        public const string RESULT = "result";

        public const string FAULT = "fault";
    }
}
