using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chess_MVC_APIversion.Models
{
    public class Instruction
    {
        public string header { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public string piece { get; set; }
    }
}
