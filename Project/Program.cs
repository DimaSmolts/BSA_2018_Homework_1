using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello Binary Studio Academy 2018!");
			while (true)
			{
				Console.WriteLine("\n*******************************************");
				Console.WriteLine("  choose the task (enter only one number)");
				Console.WriteLine("  1 ");
				Console.WriteLine("  2 ");
				Console.WriteLine("  3 ");
				Console.WriteLine("  4 ");
				Console.WriteLine("  5 ");
				Console.WriteLine("  6 ");
				Console.WriteLine("  7 - Update");
				Console.WriteLine("  8 - Exit");
				Console.WriteLine("*******************************************");
				Console.Write("===> ");
				string input = Console.ReadLine();
				Console.WriteLine("*******************************************");
				switch (input)
				{
					case "1":						
						break;
					case "2":
						break;
					case "3":
						break;
					case "4":
						break;
					case "5":
						break;
					case "6":
						break;
					case "7":
						break;
					case "8":
						Environment.Exit(0);
						break;
					default:
						Console.WriteLine("wrong input");
						break;
				}
			}
		}
	}
}
