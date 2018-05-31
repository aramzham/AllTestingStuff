using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTesting.Models
{
    public class MatchMemberModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool? IsHome { get; set; }

        //[JsonIgnore]
        public long BookmakerId { get; set; }
    }
}
