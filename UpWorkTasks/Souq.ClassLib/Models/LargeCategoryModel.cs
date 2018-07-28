using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.ClassLib.Models
{
    [Serializable]
    public class LargeCategoryModel
    {
        public string Name { get; set; }
        public List<MediumCategoryModel> MediumCategories { get; set; } = new List<MediumCategoryModel>();
        public List<SmallCategoryModel> SmallCategories { get; set; } = new List<SmallCategoryModel>();

        public override string ToString()
        {
            return this.Name;
        }
    }
}
