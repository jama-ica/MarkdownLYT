using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT
{
	public enum E_COMMAND
	{
		UPDATE,
		EXIT,
		//--
		MAX,
	};

	public static class ExCommand
	{
		public static string GetName(this E_COMMAND command)
		{
			switch (command)
			{
			case E_COMMAND.UPDATE:	return "update";
			case E_COMMAND.EXIT:	return "exit"; 
			default:
				throw new Exception("unknow command");
			}
		}

		public static string GetDescription(this E_COMMAND command)
		{
			switch (command)
			{
				case E_COMMAND.UPDATE:	return "update moc";
				case E_COMMAND.EXIT:	return "quit this app";
				default:
					throw new Exception("unknow command");
			}
		}

		public static E_COMMAND ToCommand(string name)
		{
			foreach (int no in Enum.GetValues(typeof(E_COMMAND)))
			{
				E_COMMAND command = (E_COMMAND)no;
				if (command.GetName() == name)
				{
					return command;
				}
			}
			return E_COMMAND.MAX;
		}

	}
}
