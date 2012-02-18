using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrandpaBrain
{
    public class GeneratorHelper
    {
	    private static Random opRand = new Random();
        private static Random numRand = new Random();
	    public static Operands GetRandomOp(){
            return (Operands)Enum.ToObject(typeof(Operands),opRand.Next(1,4));
	    }
        public static int GetRandomInt(int min, int max)
        {
            return numRand.Next(min, max);
        }
    }
}
