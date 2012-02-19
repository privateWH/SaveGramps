using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrandpaBrain
{
    public class Answer
    {
        private Generator answerGenerator;
        private Response expectedResponse; // need to wire it up with the generator
        private Response userResponse = new Response();
        private bool isDirty = false;
        private int? result = null;

        private Answer(){ //internal test only
        }
        public Answer(Response expectedAnswer)
        {
            expectedResponse = expectedAnswer;
        }

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

        private bool IsCorrect()
        {
            if (isDirty)
            {
                result = ComputeResponse(userResponse.Numbers,userResponse.Operands);
                isDirty = false;
            }
            return (result.HasValue) ? result.Value == expectedResponse.Answer : false ;
        }

        public bool ShouldTerminate(out TerminateCond term, out string message){
            //Calls IsCorrect or GotPotential
            // Normal Termination: IsCorrect() == true
            // Abnormal Termination: GotPotential == false, meaning no possible combination in the selection to finish the game.
            message = string.Empty;
            term = TerminateCond.NoTerminate;
            if (!IsCorrect())
            {
                if (GotPotential())
                {
                    isDirty = false;
                    return false;
                }
                term = TerminateCond.Impossible;
                message = "NO POTENTIAL";
                isDirty = false;
                return true;
            }
            term = TerminateCond.Normal;
            isDirty = false;
            return true;
        }

        private bool GotPotentialHelper(IList<int> listNum,IList<Operands> listOp)
        {
            IList<int> remainNum = expectedResponse.Numbers.SkipWhile(it => listNum.Contains(it)).ToList();
            IList<Operands> remainOp = expectedResponse.Operands.SkipWhile(it => listOp.Contains(it)).ToList();
            int? result = ComputeResponse(listNum, listOp);
            if (result.HasValue && result.Value == expectedResponse.Answer) return true;
            foreach(var op in remainOp){
                    var newOp = listOp.ToList();
                    newOp.Add(op);
                    if (GotPotentialHelper(listNum.ToList(), newOp)) return true;
                    foreach (var num in remainNum)
                    {
                        var newNums = listNum.ToList();
                        newNums.Add(num);
                        if (GotPotentialHelper(newNums, newOp)) return true;
                    }
            }
            return false;
        }

        private bool GotPotential()
        {
            //assuming IsCorrect = false or result == null;
            // this implies we missing some operand(s) or number(s) to make the "potential" equation to have a result
            return GotPotentialHelper(userResponse.Numbers, userResponse.Operands);
        }

        public static int? ComputeResponse(IList<int> numbers, IList<Operands> ops)
        {
            int numOps = ops.Count;
            int numNum = numbers.Count;

            if (numOps == numNum - 1 && numNum > 0 && numOps > 0)
            {
                int i = 0;
                int result = numbers[i];

                while(++i < numbers.Count)
                {
                    switch (ops[i-1])
                    {
                        case Operands.Add:
                            result += numbers[i];
                            break;
                        case Operands.Divide:
                            result /= numbers[i];
                            break;
                        case Operands.Minus:
                            result -= numbers[i];
                            break;
                        case Operands.Times:
                            result *= numbers[i];
                            break;
                        default:
                            throw new Exception("Operand not implement");
                    }
                } 
                return result;
            }

            return null;
        }
        
    }
}
