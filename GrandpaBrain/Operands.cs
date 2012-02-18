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

    static class OperandHelper
    {
        static String ConvertOperandToString(Operands operand)
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
    }
}
