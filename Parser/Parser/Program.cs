using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParserLib;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {

            do
            {
                try
                {
                    string s;
                    Console.WriteLine("Введите математическое выражение");
                    s = Console.ReadLine();
                    ListElements list = MathParse.ParseMath(s);
                    string res;
                    list.GetExpression(out res);
                    Console.WriteLine(res);
                    
                }
                catch (Exception mes) { Console.WriteLine(mes.Message); }
                Console.WriteLine("Для повторения нажмите ENTER");
            } while (Console.ReadKey().Key == ConsoleKey.Enter);


        }
    }
}
