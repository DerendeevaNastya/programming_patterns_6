using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example_06.ChainOfResponsibility;

namespace Example_06
{
    class Program
    {
        static void Main(string[] args)
        {
            var b = new Bancomat();
            var alternnatives = new List<string>();
            var banknote =  new  Banknote(CurrencyType.Ruble, "100500");
            var values = b.GetCash(banknote, alternnatives);
            Console.WriteLine(String.Join(", ", values));
        }
    }
}
