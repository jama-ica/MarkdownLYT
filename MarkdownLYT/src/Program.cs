using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Note;

namespace MarkdownLYT
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.Clear();

			Logger.Info("Markdown LYT");
			Logger.Info("- version: " + Define.MAJOR_VERSION + "." + Define.MINOR_VERSION + "." + Define.BUILD_VERSION );
			Logger.Info("");

			// Load Setting
			var setting = AppSettingFile.GetInstance();
			{
				if(false == setting.Load())
				{
					var dat = setting.CreateDefaultData();
					setting.Save( dat );
				}
			}

			// Check workspace settings
			{
				if (AppSettingFile.GetData().workspace.path == String.Empty)
				{
					Logger.Info("Workspace path is not set yet.");
					InputWorkspacePath();
				}

				var workspacePath = AppSettingFile.GetData().workspace.path;
				var workspaceDir = new DirectoryInfo(workspacePath);
				if (!workspaceDir.Exists)
				{
					Logger.Warn($"Workspace path {workspaceDir.FullName} is not found.");
					InputWorkspacePath();
				}
			}

			// Load workspace
			{
				var workspacePath = AppSettingFile.GetData().workspace.path;
				Logger.Info($@"Open workspace: {workspacePath}");
				var workspace = new WorkSpace(workspacePath);
				workspace.Start();
				InputCommand(workspace);
			}
			Environment.Exit(0);
		}

		static void InputWorkspacePath()
		{
			while (true)
			{
				Logger.Info("Please input your workspace dir path.");
				string? input = Console.ReadLine();
				if (input == null)
				{
					continue;
				}
				var dir = new DirectoryInfo(input);
				if (!dir.Exists)
				{
					Logger.Warn("The directory does not exist.");
					continue;
				}

				AppSettingFile.GetData().workspace.path = dir.FullName;
				AppSettingFile.GetInstance().Save();
				break;
			}
		}

		static void InputCommand(WorkSpace workspace)
		{
			while (true)
			{
				Logger.Info("");
				Logger.Info("Please input command");
				foreach (int no in Enum.GetValues(typeof(E_COMMAND)))
				{
					E_COMMAND cmd = (E_COMMAND)no;
					Logger.Info($"  {String.Format("{0, -10}", cmd.GetName())} {cmd.GetDescription()}");
				}
				Logger.Info("");

				string? text = Console.ReadLine();
				if (text == null)
				{
					continue;
				}

				var command = ExCommand.ToCommand(text);
				if (command == null)
				{
					Logger.Info("Unknown command");
					continue;
				}
				else if (command == E_COMMAND.EXIT)
				{
					break;
				}
				else if (command == E_COMMAND.MOC)
				{
					RunCommandUpdate(workspace);
				}
				else if (command == E_COMMAND.TODAY)
				{
					RunCommandToday();
				}
				else if (command == E_COMMAND.MONTH)
				{
					RunCommandMonth();
				}
				else if (command == E_COMMAND.NEXT_MONTH)
				{
					RunCommandNextMonth();
				}
				else if (command == E_COMMAND.CREATE_NOTE)
				{
					RunCommandCreateNote(workspace);
				}
				else if (command == E_COMMAND.OPEN_WORKSPACE)
				{
					RunCommandOpenWorkspace(workspace);
				}
				else
				{
					Logger.Warn("Unknown command");
				}
			}
		}

		static void RunCommandUpdate(WorkSpace workspace)
		{

			// Backup notes
			//workspace.Backup(noteDirectoryName);

			// Clean notes
			workspace.CleanUpMoc();

			// Load NoteBooks
			workspace.LoadNotebooks();

			// replace file and directory
			//workspace.ReplaceAllNotes();

			// Update tag file
			workspace.UpdateTagFile();

			// Update home and MOC
			workspace.UpdateAllMocFiles();

			// Update breadcrumb trail
			workspace.UpdateAllNoteBooks();
		}


		static void RunCommandToday()
		{
			DiaryNote.Create(DateTime.Today);
			//DiaryNote.Open(DateTime.Today);
		}

		static void RunCommandMonth()
		{
			var today = DateTime.Today;
			var day = new DateTime(today.Year, today.Month, 1);

			for (int i = 0; i < 31; i++)
			{
				if (day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday)
				{
					DiaryNote.Create(day);
				}

				day = day.AddDays(1);

				if(day.Month != today.Month)
				{
					break;
				}
			}
		}

		static void RunCommandNextMonth()
		{
			var today = DateTime.Today;
			var day = new DateTime(today.Year, today.Month+1, 1);

			for (int i = 0; i < 31; i++)
			{
				if (day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday)
				{
					DiaryNote.Create(day);
				}

				day = day.AddDays(1);
				if (day.Month != today.Month)
				{
					break;
				}
			}
		}

		static void RunCommandBackup(WorkSpace workspace)
		{
			var noteDirectoryName = workspace.GetNoteDirectoryName();
			workspace.Backup(noteDirectoryName);

			//TODO 全ファイルを note にコピーする
		}

		static void RunCommandCreateNote(WorkSpace workspace)
		{
			Logger.Info("Input file neme:");

			string? input = Console.ReadLine();
			if (input == null)
			{
				Logger.Warn("Invalid file name.");
				return;
			}

			workspace.CreateNewNote(input);
		}

		static void RunCommandOpenWorkspace(WorkSpace workspace)
		{
			workspace.OpenWorkspace();
		}
	}
}
