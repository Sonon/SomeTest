using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserLib
{
    public class StackElementNumber : StackElement
    {
        double _Number;
        public StackElementNumber(double number)
        {
            _Number = number;
            Line = _Number.ToString();
        }
        public string ToLine
        {
            get
            {
                return Line;
            }
        }
        public double GetNumber
        {
            get
            {
                return _Number;
            }
        }
        public override string ToString()
        {
            return _Number.ToString();
        }
    }
}
