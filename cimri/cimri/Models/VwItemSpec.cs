using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class VwItemSpec
    {
        public int SpecGroupId { get; set; }
        public string SpecGroupName { get; set; }
        public List<ItemSpec> Values { get; set; }
    }
}