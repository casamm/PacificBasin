using System;
using System.Collections.Generic;

namespace Entitlement.Modules.Entitlement.Model.Vo
{
    public class ResultData
    {
        public string AuthToken { get; set; }
        public string NameId { get; set; }
        public List<string> ProductIds { get; set; }
        public Exception Exception { get; set; }
    }
}
