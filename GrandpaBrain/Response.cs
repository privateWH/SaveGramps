using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrandpaBrain
{
    public class Response
    {
        public Response() { 
            this.Numbers = new List<int>(); 
            this.Operands = new List<Operands>(); 
        }
        public IList<int> Numbers { get; set; }
        public IList<Operands> Operands { get; set; }
        public int Answer;
    }
}
