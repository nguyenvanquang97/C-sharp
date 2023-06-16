using System.Collections.Generic;
using System.Text;

namespace Wini.Simple
{
    public class ShopItem
    {
        public string Name { get; set; }
        public IEnumerable<EmploymentItem> Employments { get; set; }
    }
}
