using Entitlement.Common.Model.Vo;
using System.Threading.Tasks;

namespace Entitlement.Shell.View.Interfaces
{
    public interface IService
    {
        Task<object> Service(ContextVo contextVo);
    }
}
