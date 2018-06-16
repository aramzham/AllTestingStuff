using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NFT.MvcWebPage.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal Salary { get; set; }
        public bool IsGettingBonus { get; set; }
        public int UniversityId { get; set; }
        public string Info { get; set; }
    }
}