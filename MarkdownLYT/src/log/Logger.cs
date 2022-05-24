using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT
{
	internal class Logger
	{
		enum E_Level
		{
			Error,
			Warn,
			Info,
			Debug,
		};

		static Logger Instance = null;

		private Logger()
		{

		}

		public static Logger GetInstance()
		{
			if (Instance == null)
			{
				Instance = new Logger();
			}
			return Instance;
		}

		public static void Error(string message)
		{
			GetInstance().ConsoleError(message);
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
		#if DEBUG
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(message);
		#endif
		}

		public void ConsoleError(string message)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Error: " + message);
		}

	}
}
