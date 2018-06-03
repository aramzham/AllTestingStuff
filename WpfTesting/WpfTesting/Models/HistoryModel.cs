using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTesting.Models
{
    public class HistoryModel
    {
        public string SelectionName { get; set; }
        public string Time { get; set; } = DateTime.Now.ToString("MM-dd HH:mm:ss");
        public decimal From { get; set; }
        public decimal To { get; set; }
        public ChangeEnum Change { get; set; }
    }

    public enum ChangeEnum
    {
        Created,
        Suspended,
        Up, 
        Down
    }
}
