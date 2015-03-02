using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserLib
{
    public class ListElements
    {
        List<StackElement> list = new List<StackElement>();
        public ListElements(List<StackElement> list)
        {
            this.list = list;
        }
        public void GetResult()
        {
            List<StackElement> list = new List<StackElement>();
            foreach (StackElement q in this.list)
            {
                list.Add(q);
            }
            bool f = false;
            try
            {
                do
                {
                    f = false;
                    int i = -1;
                    bool ff = true;
                    do
                    {
                        i++;
                        for (int z = i; z <= list.Count; z++, i++)
                        {
                            if (z == list.Count) throw new Exception("Не соталось свободных операций");
                            if (list[z] is StackElementFunction)
                            {
                                break;
                            }
                        }
                        ff = true;
                        StackElementFunction fun1 = list[i] as StackElementFunction;
                        for (int j = i - fun1.CountNumbers, k = 0; j < i; j++, k++)
                        {
                            if (!(list[j] is StackElementNumber))
                            {
                                ff = false;
                            }
                        }
                    } while (!ff);
                    StackElementFunction fun = list[i] as StackElementFunction;
                    StackElementNumber[] Arr = new StackElementNumber[fun.CountNumbers];
                    for (int j = i - fun.CountNumbers, k = 0; j < i; j++, k++)
                    {
                        Arr[k] = (StackElementNumber)list[j];
                    }
                    double res = DoStep(fun, Arr);
                    list[i - fun.CountNumbers] = new StackElementNumber(res);
                    for (int j = 0; j < fun.CountNumbers; j++)
                    {
                        list.Remove(list[i - fun.CountNumbers + 1]);
                    }


                    foreach (StackElement q in list)
                    {
                        if (q is StackElementFunction) f = true;
                    }
                } while (f);
            }
            catch (Exception mes)
            {
                if (mes.Message != "Не соталось свободных операций")
                    throw new Exception("Ошибка в вычислении/сокращении выражения");
            }
            this.list = list;
        }
        private double DoStep(StackElementFunction fun, StackElementNumber[] args)
        {
            if (fun.ToLine == "+")
            {
                return args[0].GetNumber + args[1].GetNumber;
            }
            if (fun.ToLine == "-")
            {
                return args[0].GetNumber - args[1].GetNumber;
            }
            if (fun.ToLine == "*")
            {
                return args[0].GetNumber * args[1].GetNumber;
            }
            if (fun.ToLine == "/")
            {
                return args[0].GetNumber / args[1].GetNumber;
            }
            if (fun.ToLine == "^")
            {
                return Math.Pow(args[0].GetNumber, args[1].GetNumber);
            }
            if (fun.ToLine == "Cos")
            {
                return Math.Cos(args[0].GetNumber * Math.PI / 180);
            }
            if (fun.ToLine == "Sin")
            {
                return Math.Sin(args[0].GetNumber * Math.PI / 180);
            }
            if (fun.ToLine == "Tan")
            {
                return Math.Tan(args[0].GetNumber * Math.PI / 180);
            }
            if (fun.ToLine == "Log")
            {
                return Math.Log(args[0].GetNumber, args[1].GetNumber);
            }
            return 0;
        }
        public bool GetNumber(out double res)
        {
            if (list.Count < 2)
            {
                res = ((StackElementNumber)list[0]).GetNumber;
                return true;
            }
            else
            {
                res = 0;
                return false;
            }
        }
        public bool GetExpression(out string res)
        {
            if (list.Count < 2)
            {
                double dres;
                res = GetNumber(out dres).ToString();
                return false;
            }
            else
            {
                bool first = true;
                int lastPriority = 0;
                int lastPosition = 0;
                List<StackElement> tlist = new List<StackElement>();
                string s = "";
                foreach (StackElement q in this.list)
                {
                    tlist.Add(q);
                }
                try
                {
                    do
                    {
                        int i = 0;
                        for (i = 0; i <= tlist.Count; i++)
                        {
                            if (i == tlist.Count)
                                throw new Exception("Строка сформирована");
                            if (tlist[i] is StackElementFunction)
                            {
                                break;
                            }
                        }
                        s = string.Empty;
                        if (first)
                        {
                            lastPriority = ((StackElementFunction)tlist[i]).GetPriority;
                            lastPosition = i - ((StackElementFunction)tlist[i]).CountNumbers;
                            first = false;
                        }
                        if (lastPriority != ((StackElementFunction)tlist[i]).GetPriority)
                            first = true;
                        StackElementFunction fun = ((StackElementFunction)tlist[i]);
                        StackElement[] Arr = new StackElement[fun.CountNumbers];
                        for (int j = i - fun.CountNumbers, k = 0; j < i; j++, k++)
                        {
                            Arr[k] = tlist[j];
                        }
                        for (int j = 0; j < Arr.Length; j++)
                        {
                            if ((Arr[j] as StackElementVariable) != null)
                            {
                                StackElementVariable Var = (StackElementVariable)Arr[j];
                                if (Var.PriorityOfOperation < fun.GetPriority)
                                {
                                    Arr[j] = new StackElementVariable("(" + Arr[j].ToString() + ")", Var.PriorityOfOperation);
                                }
                            }
                        }
                        for (int j = 0; j < Arr.Length; j++)
                        {
                            if (j == Arr.Length - 1)
                            {
                                s += fun.ToLine;
                            }
                            s += Arr[j];
                        }
                        i -= fun.CountNumbers;
                        tlist[i] = new StackElementVariable(s, fun.GetPriority);
                        for (int j = i + 1, k = 0; k < fun.CountNumbers; k++)
                        {
                            tlist.Remove(tlist[j]);
                        }
                    } while (true);
                }
                catch (Exception mes)
                {
                    if (mes.Message == "Строка сформирована")
                    {
                        res = ((StackElementVariable)tlist[0]).ToLine;
                        return true;
                    }
                    else
                    {
                        throw mes;
                    }
                }
            }
        }
    }
}
