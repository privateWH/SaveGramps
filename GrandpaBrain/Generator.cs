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
    public class Generator
    {
        private static Generator gen;
        static Generator()
        {
            if (gen == null)
                gen = new Generator();
        }
        public int CurrentLevel { get { return gen.current_lv; } }
        private int current_lv = 0;
        private Dictionary<int, ILevel> lv = new Dictionary<int,ILevel>();
        public static int RegisterLevel(ILevel l)
        {
            return RegisterLevel(l, Int32.MinValue);
        }
        static int RegisterLevel(ILevel l, int lvs)
        {
            gen.current_lv++;
            gen.current_lv = (lvs != Int32.MinValue) ? lvs : gen.current_lv;
            gen.lv.Add(gen.current_lv, l);
            return gen.current_lv;
        }
        public static Response GetExpectedResponseByLevel()
        {
            return GetExpectedResponseByLevel(gen.current_lv);
	    }
    	public static Response GetExpectedResponseByLevel(int lv){
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
