using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.ClassLib.Models
{
    [Serializable]
    public class MediumCategoryModel
    {
        public string Name { get; set; }
        public List<SmallCategoryModel> SmallCategories { get; set; } = new List<SmallCategoryModel>();

        public override string ToString()
        {
            return this.Name;
        }
    }
}
