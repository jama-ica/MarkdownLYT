using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT
{
	internal class Log
	{
		public static void Error(string message)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Error: " + message);
		}

		public static void Warn(String message)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("Warn: " + message);
		}

		public static void Info(String message)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(message);
		}

		public static void Debug(String message)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(message);
		}
	}
}
