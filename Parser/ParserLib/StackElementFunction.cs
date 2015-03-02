using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserLib
{
    class StackElementFunction : StackElement
    {
        int _CountNumbers;
        public int CountNumbers
        {
            get
            {
                return _CountNumbers;
            }
        }
        public StackElementFunction(int countNumbers, string functionName)
        {
            Line = functionName;
            _CountNumbers = countNumbers;
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
            return Line;
        }
        public static StackElementFunction GetElement(string s)
        {
            if (s == "+") return new StackElementFunction(2, s);
            if (s == "-") return new StackElementFunction(2, s);
            if (s == "*") return new StackElementFunction(2, s);
            if (s == "/") return new StackElementFunction(2, s);

            if (s == "(") return new StackElementFunction(1, s);
            if (s == ")") return new StackElementFunction(1, s);

            if (s == "^") return new StackElementFunction(2, s);

            if (s == "Cos") return new StackElementFunction(1, s);
            if (s == "Sin") return new StackElementFunction(1, s);
            if (s == "Tan") return new StackElementFunction(1, s);

            if (s == "Log") return new StackElementFunction(2, s);

            return null;
        }
        public int GetPriority
        {
            get
            {
                if (Line == "(" || Line == ")") return 0;
                if (Line == "+" || Line == "-") return 1;
                if (Line == "*" || Line == "/") return 2;
                if (Line == "^")                return 3;
                if (Line == "Log") return 4; 
                if (Line == "Cos" || Line == "Sin" || Line == "Tan") return 5;
                return 0;
            }
        }
    }
}
