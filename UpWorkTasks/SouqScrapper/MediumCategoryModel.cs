using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouqScrapper
{
    public class MediumCategoryModel
    {
        public string Name { get; set; }
        public List<SmallCategoryModel> SmallCategories { get; set; } = new List<SmallCategoryModel>();
    }
}
