using System;
using System.Collections.Generic;
using exerlinq1.Entities;
using System.Linq;
using System.IO;
using System.Globalization;

namespace exerlinq1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            List<Product> products = new List<Product>();

            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] campos = sr.ReadLine().Split(',');
                    string name = campos[0];
                    double price = double.Parse(campos[1],CultureInfo.InvariantCulture);
                    products.Add(new Product(name, price));
                }
            }

            var r1 = products.Select(p => p.Price).DefaultIfEmpty(0.0).Average(); // caso a lista seja vazia irá trazer o 0, melhor fazer o select antes do average para transformar a lista em double 
            Console.WriteLine("Average price: " + r1.ToString("F2",CultureInfo.InvariantCulture));
            var r2 = products.Where(p => p.Price < r1).OrderByDescending(p => p.Name).Select(p => p.Name);
            foreach(string p in r2)
            {
                Console.WriteLine(p);
            }



        }
    }
}
