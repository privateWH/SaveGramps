using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrandpaBrain
{
    interface ILevel
    {
        Response Get(int lv);
    }
}