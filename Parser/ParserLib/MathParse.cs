using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserLib
{
    public static class MathParse
    {
        static public ListElements ParseMath(string s)
        {
            List<StackElement> list = new List<StackElement>();
            Stack<StackElementFunction> stack = new Stack<StackElementFunction>();
            while (s.IndexOf(' ') > -1)
            {
                s = s.Remove(s.IndexOf(' '), 1);
            }
            int i = 0;
            while (i < s.Length)
            {
                string element = ReadElement(s, i);
                i += element.Length;
                double IsDouble;
                if (double.TryParse(element, out IsDouble) && IsDouble >= 0)
                {
                    list.Add(new StackElementNumber(IsDouble));
                }
                else
                {
                    StackElementFunction fun = StackElementFunction.GetElement(element);
                    if (fun != null)
                    {
                        if (fun.ToLine == ")" || fun.ToLine == "(")
                        {
                            if (fun.ToLine == "(")
                            {
                                stack.Push(fun);
                            }
                            else
                            {
                                while (stack.Peek().ToLine != "(")
                                    list.Add(stack.Pop());
                                stack.Pop();
                            }
                        }
                        else
                        {
                            if (stack.Count == 0 || fun.GetPriority <= stack.Peek().GetPriority)
                            {
                                while (stack.Count > 0 && stack.Peek().GetPriority >= fun.GetPriority)
                                {
                                    if (stack.Peek().GetPriority > 0)
                                        list.Add(stack.Pop());
                                }
                                if (fun.GetPriority > 0)
                                    stack.Push(fun);
                            }
                            else
                            {
                                stack.Push(fun);
                            }
                        }
                    }
                    else
                    {
                        StackElementVariable Var = new StackElementVariable(element);
                        list.Add(Var);
                    }
                }
            }
            while (stack.Count > 0)
            {
                list.Add(stack.Pop());
            }
            return new ListElements(list);
        }
        static string ReadElement(string s, int i)
        { 
            string element = "";
            if (s[i] == '+' || s[i] == '-' || s[i] == '*' || s[i] == '/' || s[i] == '^' || s[i] == '(' || s[i] == ')')
            {
                element += s[i];
                return element;
            }
            if (s[i] < '0' || s[i] > '9')
            {
                do
                {
                    element += s[i];
                    i++;
                } while (i < s.Length && !IsReserve(s[i]));
            }
            else
            {
                do
                {
                    element += s[i];
                    i++;
                } while (i < s.Length && ((s[i] >= '0' && s[i] <= '9') || (s[i] == ',')));
            }
            return element;
        }
        static bool IsReserve(char c)
        {
            if (c == '+') return true;
            if (c == '-') return true;
            if (c == '*') return true;
            if (c == '/') return true;
            if (c == '(') return true;
            if (c == ')') return true;

            if (c >= '0' && c <= '9') return true;

            return false;
        }
    }
}
