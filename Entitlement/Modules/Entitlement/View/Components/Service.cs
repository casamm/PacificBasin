using Entitlement.Modules.Entitlement.Model.Vo;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Entitlement.Modules.Entitlement.View.Components
{
    public class Service
    {
        public Service()
        {
        }

        public void Result(RequestVo requestVo)
        {
            HttpRequest request = requestVo.ContextVo.Context.Request;
            HttpResponse response = requestVo.ContextVo.Context.Response;
            RequestData requestData = requestVo.RequestData;
            ResultData resultData = requestVo.ResultData;

            response.StatusCode = 200;
            response.ContentType = "application/xml";

            switch (request.Path.Value)
            {
                case "/entitlement/SignInWithCredentials":
                case "/entitlement/RenewAuthToken":
                    Task.Run(() =>
                    {
                        response.WriteAsync(
                                $@"<result httpResponseCode=""200"">
                                    <authToken>{resultData.AuthToken}</authToken>
                                </result>");
                        requestVo.SetResult(null);
                    });
                    break;
                case "/entitlement/entitlements":
                    Task.Run(() =>
                    {
                        response.WriteAsync(
                            @"<result httpResponseCode=""200"">
                                <entitlements>");
                        foreach(string productId in resultData.ProductIds)
                        {
                            response.WriteAsync($"<productId>{productId}</productId>");
                        }
                        response.WriteAsync(
                                @"</entitlements>
                            </result>");
                        requestVo.SetResult(null);
                    });
                    break;
                default:
                    break;
            }
        }

        public void Fault(RequestVo requestVo)
        {
            HttpResponse response = requestVo.ContextVo.Context.Response;
            ResultData resultData = requestVo.ResultData;
            response.StatusCode = 401;
            response.ContentType = "application/xml";

            Task.Run(() =>
            {
                response.WriteAsync($@"<result httpResponseCode=""401"" errorCode="""" errorMessage=""{resultData.Exception.Message}""/>");
                requestVo.SetResult(resultData);
            });
            System.Diagnostics.Debug.WriteLine(resultData.Exception.Message);
        }
    }
}
