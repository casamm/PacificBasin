using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Entitlement.Common.Model.Vo
{
    public class ContextVo
    {
        public ContextVo(HttpContext context)
        {
            Context = context;
        }

        public HttpContext Context { get; set; }

        public TaskCompletionSource<object> TaskCompletionSource { get; set; } = new TaskCompletionSource<object>();
    }
}
