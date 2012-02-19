using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrandpaBrain
{
    public enum Operands
    {
        Add=0,
        Minus=1,
        Times=2,
        Divide=3
    }

    public static class OperandHelper
    {
        public static String ConvertOperandToString(Operands operand)
        {
            switch (operand)
            {
                case Operands.Add:
                    return "+";
                case Operands.Minus:
                    return "-";
                case Operands.Times:
                    return "X";
                case Operands.Divide:
                    return "/";
            }
            throw new Exception("Invalid operand");
        }

        public static Operands ConvertStringToOperands(String opString)
        {
            switch (opString)
            {
                case "+":
                    return Operands.Add;
                case "-":
                    return Operands.Minus;
                case "X":
                    return Operands.Times;
                case "/":
                    return Operands.Divide;
            }
            throw new Exception("Invalid operation string");
        }
 
    }
}
