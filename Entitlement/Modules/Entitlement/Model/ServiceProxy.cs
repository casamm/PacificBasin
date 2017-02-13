using System;
using Entitlement.Common.Model.Request;
using Entitlement.Modules.Entitlement.Model.Do;
using PureMVC.Patterns.Observer;
using PureMVC.Patterns.Proxy;

namespace Entitlement.Modules.Entitlement.Model
{
    public class ServiceProxy: Proxy
    {
        public ServiceProxy() : base (NAME)
        {
        }

        public async void SignInWithCredentialsAsync(ServiceRequest serviceRequest)
        {
            try
            {
                Customer.Parse(serviceRequest);
                await Customer.GetCustomersAsync(serviceRequest);
                Result(serviceRequest);
            } catch(Exception exception)
            {
                serviceRequest.RequestVo.ResultData.Exception = exception;
                Fault(serviceRequest);
            }
        }

        public async void GetEntitlementsAsync(ServiceRequest serviceRequest)
        {
            try
            {
                Customer.Parse(serviceRequest);
                if (Customer.Validate(serviceRequest))
                {
                    await Customer.GetCustomerAsync(serviceRequest);
                    Customer.GetProductIds(serviceRequest);
                    Result(serviceRequest);
                }
            } catch(Exception exception)
            {
                serviceRequest.RequestVo.ResultData.Exception = exception;
                Fault(serviceRequest);
            }
        }
    
        public void RenewAuthToken(ServiceRequest serviceRequest)
        {
            try
            {
                Customer.Parse(serviceRequest);
                if (Customer.Validate(serviceRequest))
                {
                    Result(serviceRequest);
                }
            } catch(Exception exception)
            {
                serviceRequest.RequestVo.ResultData.Exception = exception;
                Fault(serviceRequest);
            }
        }

        private void Result(ServiceRequest serviceRequest)
        {
            if (serviceRequest.HasCallback())
            {
                serviceRequest.NotifyObserver(new Notification(ServiceRequest.RESULT, serviceRequest));
            }
        }

        private void Fault(ServiceRequest serviceRequest)
        {
            if (serviceRequest.HasCallback())
            {
                serviceRequest.NotifyObserver(new Notification(ServiceRequest.FAULT, serviceRequest));
            }
        }

        private Customer Customer { get; set; } = new Customer();

        public static new string NAME = "ServiceProxy";
    }
}
