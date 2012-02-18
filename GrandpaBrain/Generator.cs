using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrandpaBrain
{
    public class Generator
    {
        private static Generator gen;
        private static Generator()
        {
            if (gen == null)
                gen = new Generator();
        }
        public int CurrentLevel { get { return gen.current_lv; } }
        private int current_lv = 0;
        private Dictionary<int, ILevel> lv;
        static void RegisterLevel(ILevel l)
        {
            RegisterLevel(l, Int32.MinValue);
        }
        static void RegisterLevel(ILevel l, int lvs)
        {
            gen.current_lv = (lvs != Int32.MinValue) ? lvs : gen.current_lv;
            gen.lv.Add(gen.current_lv++, l);
        }
        public static Response Get(){
		    return Get(gen.current_lv);
	    }
    	private static Response Get(int lv){
		    ILevel gameLv;
		    if(gen.lv.TryGetValue(lv,out gameLv)){
			return gameLv.Get(lv);
		}
		else{
		    throw new Exception("DEBUG: Cannot find Level in Number Generator");
		}				
	}
    }
}
