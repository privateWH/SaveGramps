using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrandpaBrain
{
    public class Answer
    {
        private Response expectedResponse;
        private Response userResponse;
        
        public void AddNumber(int number)
        {
            userResponse.Numbers.Add(number);
        }

        public void AddOperand(Operands op)
        {
            userResponse.Operands.Add(op);
        }

        
    }
}
