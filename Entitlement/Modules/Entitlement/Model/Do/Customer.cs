using CirculationPro;
using Entitlement.Common.Model.Request;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.Xml;
using Entitlement.Modules.Entitlement.Model.Vo;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using System.IO;
using System.Diagnostics;
using CirculationPro.Enum;

namespace Entitlement.Modules.Entitlement.Model.Do
{
    public class Customer
    {
        public void Parse(ServiceRequest serviceRequest)
        {
            RequestData requestData = serviceRequest.RequestVo.RequestData;
            HttpRequest request = serviceRequest.RequestVo.ContextVo.Context.Request;
            
            switch(request.Path.Value)
            {
                case "/entitlement/SignInWithCredentials":
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(request.Body);
                    requestData.EmailAddress = xmlDocument.GetElementsByTagName("emailAddress")[0].InnerText;
                    requestData.Password = xmlDocument.GetElementsByTagName("password")[0].InnerText;
                    break;

                case "/entitlement/RenewAuthToken":
                case "/entitlement/entitlements":
                    if (request.Query.TryGetValue("authToken", out StringValues authToken))
                    {
                        requestData.AuthToken = authToken;
                    } else
                    {
                        throw new Exception("Missing query param authToken");
                    }
                    break;
            }

            if (request.Query.TryGetValue("appId", out StringValues appId))
            {
                requestData.AppId = appId;
                requestData.PublicationId = AppIds.TryGetValue(appId, out string publicationId) ? publicationId : "";
            } else
            {
                throw new Exception("Missing query param appId");
            }
        }

        public async Task<ServiceRequest> GetCustomersAsync(ServiceRequest serviceRequest)
        {
            RequestData requestData = serviceRequest.RequestVo.RequestData;
            ResultData resultData = serviceRequest.RequestVo.ResultData;
            AdPowerWebService adPowerWebService = new AdPowerWebService();

            var customers = await adPowerWebService.GetCustomersAsync(null, null, null, null, "", "", null, null, requestData.EmailAddress, null, CIRC_NAME, CIRC_PASS);
            
            if(customers.Length > 0)
            {
                bool found = false;
                for (int i = 0; i < customers[0].Subscriptions.Length; i++)
                {
                    if (customers[0].Subscriptions[i].Account == requestData.Password && customers[0].Subscriptions[i].PublicationId == requestData.PublicationId)
                    {
                        if (!customers[0].Subscriptions[i].Status.Equals(SubscriptionEnum.ACTIVE.ToString()))
                        {
                            throw new Exception("Inactive Status");
                        }
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    throw new Exception("Subscription Not Found");
                }
                else
                {
                    resultData.AuthToken = crypto.Encrypt(SECRET_KEY + "|" + customers[0].Profile.NameId);
                }
            } else
            {
                throw new Exception("Invalid Credentials");
            }
            return serviceRequest;
        }

        public async Task<ServiceRequest> GetCustomerAsync(ServiceRequest serviceRequest)
        {
            RequestData requestData = serviceRequest.RequestVo.RequestData;
            ResultData resultData = serviceRequest.RequestVo.ResultData;
            AdPowerWebService adPowerWebService = new AdPowerWebService();
            var customer = await adPowerWebService.GetCustomerAsync(resultData.NameId, CIRC_NAME, CIRC_PASS);

            bool found = false;

            for (int i = 0; i < customer.Subscriptions.Length; i++)
            {
                if (customer.Subscriptions[i].PublicationId == requestData.PublicationId)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                throw new Exception("Invalid Credentials");
            }
            return serviceRequest;
        }

        public void GetProductIds(ServiceRequest serviceRequest)
        {
            RequestData requestData = serviceRequest.RequestVo.RequestData;
            ResultData resultData = serviceRequest.RequestVo.ResultData;
            using (StreamReader sr = new StreamReader(new FileStream($"c:\\SKU\\{requestData.PublicationId}.txt", FileMode.Open)))
            {
                string line;
                resultData.ProductIds = new List<string>();
                while ((line = sr.ReadLine()) != null)
                {
                    resultData.ProductIds.Add(line);
                }
            }
        }

        public bool Validate(ServiceRequest serviceRequest)
        {
            RequestData requestData = serviceRequest.RequestVo.RequestData;
            ResultData resultData = serviceRequest.RequestVo.ResultData;
            var data = crypto.Decrypt(requestData.AuthToken).Split('|');
            if(data.Length == 2)
            {
                resultData.NameId = data[1];
                resultData.AuthToken = requestData.AuthToken;
                return true;
            } else
            {
                throw new Exception("Invalid authToken");
            }
        }

        private Crypto crypto = new Crypto();

        public static string CIRC_NAME = Environment.GetEnvironmentVariable("CIRC_NAME");

        public static string CIRC_PASS = Environment.GetEnvironmentVariable("CIRC_PASS");

        public static string SECRET_KEY = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrkGQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

        public static Dictionary<string, string> AppIds = new Dictionary<string, string>()
        {
            { "com.pixelmags.reader.managed.pacific-basin-hawaii-magazine", "1000012" }, // Hawaii Magazine
            { "com.pixelmags.reader.managed.pacific-basin-hawaii-business", "1000002" }, // Hawaii Business Magazine
            { "com.pixelmags.reader.managed.pacific-basin-honolulu-magazine", "1000003" }, // Honolulu Magazine
            { "com.pixelmags.reader.managed.pacific-basin-mana-magazine", "1000019" }, // Mana Magazine
            { "com.pixelmags.reader.managed.pacific-basin-hawaii-home-remodeling-magazine", "1000008" }, // Hawaii Home Magazine
				
		    // Android (underscores) may not be used due to bug in DPS app builder
		    { "com.pixelmags.reader.managed.pacific_basin_hawaii_magazine", "1000012" }, // Hawaii Magazine
            { "com.pixelmags.reader.managed.pacific_basin_hawaii_business", "1000002" }, // Hawaii Business Magazine
            { "com.pixelmags.reader.managed.pacific_basin_honolulu_magazine", "1000003" }, // Honolulu Magazine
            { "com.pixelmags.reader.managed.pacific_basin_mana_magazine", "1000019" }, // Mana Magazine
            { "com.pixelmags.reader.managed.pacific_basin_hawaii_home_remodeling_magazine", "1000008" }, // Hawaii Home Magazine

            // Android (dots)
            { "com.pacificbasin.hawaii.magazine", "1000012" }, // Hawaii Magazine
            { "com.pacificbasin.hawaii.business", "1000002" }, // Hawaii Business Magazine
            { "com.pacificbasin.honolulu.magazine", "1000003" }, // Honolulu Magazine
            { "com.pacificbasin.mana.magazine", "1000019" }, // Mana Magazine
            { "com.pacificbasin.hawaii.home.remodeling.magazine", "1000008" } // Hawaii Home Magazine
        };
    }
}
