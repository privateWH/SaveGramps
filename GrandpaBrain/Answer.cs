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
        private bool isDirty = false; 
        public void AddNumber(int number)
        {
            isDirty = true;
            userResponse.Numbers.Add(number);
        }

        public void AddOperand(Operands op)
        {
            isDirty = true;
            userResponse.Operands.Add(op);
        }

        public bool IsCorrect()
        {
            int? result = ComputeResponse();
            return (result.HasValue) ? result.Value == expectedResponse.Answer : false ;
        }

        public bool ShouldTerminate(out string message){
            message = string.Empty;
            throw new NotImplementedException();
            return false;
        }

        private bool GotPotential()
        {
            if (isDirty)
            {

            }

            return true;
        }

        private int? ComputeResponse()
        {
            int numOps = userResponse.Operands.Count;
            int numNum = userResponse.Numbers.Count;

            if (numOps == numNum - 1 && numNum > 0 && numOps > 0)
            {
                int i = 0;
                int result = userResponse.Numbers[i];
                foreach (var num in userResponse.Numbers)
                {
                    switch (userResponse.Operands[i++])
                    {
                        case Operands.Add:
                            result += userResponse.Numbers[i];
                            break;
                        case Operands.Divide:
                            result /= userResponse.Numbers[i];
                            break;
                        case Operands.Minus:
                            result -= userResponse.Numbers[i];
                            break;
                        case Operands.Times:
                            result *= userResponse.Numbers[i];
                            break;
                    }
                }
                return result;
            }

            return null;
        }
        
    }
}
