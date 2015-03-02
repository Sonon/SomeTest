using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserLib
{
    abstract public class StackElement
    {
        protected string Line;
        public override string ToString()
        {
            return Line;
        }
    }
}
