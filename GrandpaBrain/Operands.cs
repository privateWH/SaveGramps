/*
* Grandpa's Brain 
* Copyright (c) 2012 Weinian He
*    This program is free software: you can redistribute it and/or modify
*    it under the terms of the GNU Lesser General Public License as published by
*    the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*
*    This program is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU Lesser General Public License for more details.
*
*    You should have received a copy of the GNU Lesser General Public License
*    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*
*/
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
