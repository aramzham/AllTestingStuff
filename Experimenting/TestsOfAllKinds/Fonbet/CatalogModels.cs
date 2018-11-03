using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsOfAllKinds.Fonbet
{

    public class RootCatalogObject
    {
        public int catalogVersion { get; set; }
        public Catalog[] catalog { get; set; }
    }

    public class Catalog
    {
        public string name_rus { get; set; }
        public string name_eng { get; set; }
        public Grid[] grids { get; set; }
    }

    public class Grid
    {
        public int num { get; set; }
        public string name_rus { get; set; }
        public string name_eng { get; set; }
        public int f { get; set; }
        public Grid1[][] grid { get; set; }
        public bool hasBuyFactor { get; set; }
        public bool sortByParam { get; set; }
    }

    public class Grid1
    {
        public string rus { get; set; }
        public string eng { get; set; }
        public string kind { get; set; }
        public int factorId { get; set; }
        public int a { get; set; }
        public string b_rus { get; set; }
        public string b_eng { get; set; }
        public bool flexBet { get; set; }
        public bool flexParam { get; set; }
        public string e_rus { get; set; }
        public string e_eng { get; set; }
    }
}
