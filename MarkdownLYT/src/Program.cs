using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Log.Info("Markdown LYT");
			Log.Info( "- version: " + Define.MAJOR_VERSION + "." + Define.MINOR_VERSION + "." + Define.BUILD_VERSION );
			Log.Info("");

			// Load Setting
			var setting = SettingFile.GetInstance();
			{
				if(false == setting.Load())
				{
					var dat = setting.CreateDefaultData();
					setting.Save( dat );
				}
			}

			// Load workspace
			{
				var workspacePath = SettingFile.GetData().workspace.path;
				if (workspacePath == String.Empty)
				{
					Log.Info("Workspace path is not set yet.");
					InputWorkspacePath();
				}

				var workspaceDir = new DirectoryInfo(workspacePath);
				if (!workspaceDir.Exists)
				{
					Log.Warn($"Workspace path {workspaceDir.FullName} is not found.");
					InputWorkspacePath();
				}
			}

			var workspace = new WorkSpace();
			InputCommand(workspace);
		}

		static void InputWorkspacePath()
		{
			while (true)
			{
				Log.Info("Please input your workspace dir path.");
				string? input = Console.ReadLine();
				if (input == null)
				{
					continue;
				}
				var dir = new DirectoryInfo(input);
				if (!dir.Exists)
				{
					Log.Warn("The directory does not exist.");
					continue;
				}

				SettingFile.GetData().workspace.path = dir.FullName;
				SettingFile.GetInstance().Save();
				break;
			}
		}

		static void InputCommand(WorkSpace workspace)
		{
			// コマンド待ち
			while (true)
			{
				Log.Info("Please input command");
				foreach (int no in Enum.GetValues(typeof(E_COMMAND)))
				{
					E_COMMAND cmd = (E_COMMAND)no;
					if (cmd == E_COMMAND.MAX) { continue; }
					Log.Info($"   {cmd.GetName()}   {cmd.GetDescription()}");
				}

				string? text = Console.ReadLine();
				if (text == null)
				{
					continue;
				}

				var command = ExCommand.ToCommand(text);

				if (command == E_COMMAND.UPDATE)
				{
					RunCommandUpdate(workspace);
				}
				else if (command == E_COMMAND.EXIT)
				{
					break;
				}
			}
		}

		static void RunCommandUpdate(WorkSpace workspace)
		{
			// Load workspace
			var workspacePath = SettingFile.GetData().workspace.path;
			Log.Info($"Load workspace: {workspacePath}");
			workspace.Load(workspacePath);

			// Update tag file
			var allTags = workspace.GetAllTags();

			// Update home and MOC
			workspace.UpdateHomeFile();

			// Update breadcrumb trail

			// replace file and directory
		}
	}
}
