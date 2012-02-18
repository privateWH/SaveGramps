using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrandpaBrain
{
    public interface ILevel
    {
        Response Get(int lv);
    }
}
