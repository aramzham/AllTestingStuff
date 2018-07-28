using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.ClassLib.Models
{
    [Serializable]
    public class SmallCategoryModel
    {
        public string Name { get; set; }
        public string Link { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
