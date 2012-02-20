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
    public class DefaultLevel : ILevel
    {
        public Response Get(int lv)
        {
            Response r = new Response();
            Random rnd = new Random();
            int numBalls = 3;
            int numOps = 2;
            for (int i = 0; i < numBalls; i++)
            {
                r.Numbers.Add(GeneratorHelper.GetRandomInt(1, 20));
            }
            for (int i = 0; i < numOps; i++)
            {
                r.Operands.Add(Operands.Add);
            }
            r.Operands[1] = Operands.Minus;
            r.Answer = Answer.ComputeResponse(new List<int>(new int[]{ r.Numbers[0], r.Numbers[1]}), new List<Operands>(new Operands[]{r.Operands[0]})).Value;
            return r;
        }
    }

}
