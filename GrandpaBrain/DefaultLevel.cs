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
            int numBalls = 4;
            int numOps = 2;
            for (int i = 0; i < numBalls; i++)
            {
                r.Numbers.Add(GeneratorHelper.GetRandomInt(1, 20));
            }
            for (int i = 0; i < numOps; i++)
            {
                r.Operands.Add(GeneratorHelper.GetRandomOp());
            }
            return r;
        }
    }

}
