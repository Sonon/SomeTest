using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserLib
{
    class StackElementVariable : StackElement
    {
        public StackElementVariable(string Name, int priority = 0)
        {
            Line = Name;
            _PriorityOfOperation = priority;
        }
        public string ToLine
        {
            get
            {
                return Line;
            }
        }
        public override string ToString()
        {
            return ToLine;
        }
        private int _PriorityOfOperation = 0;
        public  int PriorityOfOperation
        {
            get
            {
                return _PriorityOfOperation;
            }
        }
    }
}
