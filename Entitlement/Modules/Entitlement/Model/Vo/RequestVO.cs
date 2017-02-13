using Entitlement.Common.Model.Vo;

namespace Entitlement.Modules.Entitlement.Model.Vo
{
    public class RequestVo
    {
        public RequestVo(ContextVo contextVo)
        {
            ContextVo = contextVo;
        }

        public void SetResult(object result=null)
        {
            ContextVo.TaskCompletionSource.SetResult(result ?? ResultData);
        }

        public ContextVo ContextVo { get; set; }

        public RequestData RequestData { get; set; } = new RequestData();

        public ResultData ResultData { get; set; } = new ResultData();
    }
}
