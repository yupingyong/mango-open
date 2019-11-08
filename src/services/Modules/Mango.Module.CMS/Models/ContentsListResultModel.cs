using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Module.CMS.Models
{
    public class ContentsListResultModel
    {
        public int TotalCount { get; set; }
        public List<ContentsListDataModel> ListData { get; set; }
    }
}
