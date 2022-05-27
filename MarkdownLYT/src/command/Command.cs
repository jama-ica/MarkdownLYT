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
		REPLACE,
		MOC,
		TODAY,
		MONTH,
		NEXT_MONTH,
		BACKUP,
		EXIT,
		CREATE_NOTE,
		OPEN_WORKSPACE,
	};

	public static class ExCommand
	{
		public static string GetName(this E_COMMAND command)
		{
			switch (command)
			{
			case E_COMMAND.UPDATE:	return "update";
			case E_COMMAND.EXIT:	return "exit"; 
			case E_COMMAND.REPLACE:	return "replace"; 
			case E_COMMAND.MOC:		return "moc"; 
			case E_COMMAND.TODAY:	return "today"; 
			case E_COMMAND.MONTH:	return "month"; 
			case E_COMMAND.NEXT_MONTH:	return "next month";
			case E_COMMAND.BACKUP:	return "backup";
			case E_COMMAND.CREATE_NOTE:	return "create new note";
			case E_COMMAND.OPEN_WORKSPACE:	return "open workspace";
				default:
				throw new Exception("unknow command");
			}
		}

		static string[] updateAliasNames = {"up"};
		static string[] exitAliasNames = { "exit", "quit", "q" };
		static string[] replaceAliasNames = { "rep", "re" };
		static string[] mocAliasNames = { "moc" };
		static string[] todayAliasNames = { "to" };
		static string[] monthAliasNames = { "mo" };
		static string[] nextMonthAliasNames = { "nmo", };
		static string[] backupAliasNames = { "ba", "back", };
		static string[] createNoteAliasNames = { "cre", "new", };
		static string[] opneWorkspaceAliasNames = { "open", "op" };

		public static string[] GetAliasNames(this E_COMMAND command)
		{
			switch (command)
			{
				case E_COMMAND.UPDATE:	return updateAliasNames;
				case E_COMMAND.EXIT:	return exitAliasNames;
				case E_COMMAND.REPLACE: return replaceAliasNames;
				case E_COMMAND.MOC:		return mocAliasNames;
				case E_COMMAND.TODAY:	return todayAliasNames;
				case E_COMMAND.MONTH:	return monthAliasNames;
				case E_COMMAND.NEXT_MONTH: return nextMonthAliasNames;
				case E_COMMAND.BACKUP:	return backupAliasNames;
				case E_COMMAND.CREATE_NOTE:	return createNoteAliasNames;
				case E_COMMAND.OPEN_WORKSPACE:	return opneWorkspaceAliasNames;
				default:
					throw new Exception("unknow command");
			}
		}

		public static string GetDescription(this E_COMMAND command)
		{
			switch (command)
			{
				case E_COMMAND.UPDATE:	return "... Update moc";
				case E_COMMAND.EXIT:	return "... Quit this app";
				case E_COMMAND.REPLACE: return "... Replace all notes";
				case E_COMMAND.MOC:		return "... Update home & moc";
				case E_COMMAND.TODAY:	return "... Create or open today note";
				case E_COMMAND.MONTH:	return "... Create notes for this month";
				case E_COMMAND.NEXT_MONTH: return "... Create notes for next month";
				case E_COMMAND.BACKUP:	return "... Create backup of all moc and notes";
				case E_COMMAND.CREATE_NOTE:	return "... Create new note in note folder";
				case E_COMMAND.OPEN_WORKSPACE:	return "... Open workspaenote folder";
				default:
					throw new Exception("unknow command");
			}
		}

		public static E_COMMAND? ToCommand(string name)
		{
			foreach (int no in Enum.GetValues(typeof(E_COMMAND)))
			{
				E_COMMAND command = (E_COMMAND)no;
				if (command.GetName() == name)
				{
					return command;
				}
				foreach (var aliasName in command.GetAliasNames())
				{
					if (aliasName == name)
					{
						return command;
					}
				}
			}
			return null;
		}
	}
}
