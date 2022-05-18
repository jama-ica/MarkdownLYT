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

			// Load setting
			var setting = SettingData.GetInstance();
			{
				var dat = setting.Load();
				if (dat == null)
				{
					dat = setting.CreateDefaultData();
					setting.Save( dat );
				}
			}

			var workSpace = new WorkSpace();
			{
				var workspacePath = SettingData.GetData().workspace.path;
				if (workspacePath == String.Empty)
				{
					Log.Info("Workspace path is empty.");
					InputWorkspacePath();
				}
				var workspaceDir = new DirectoryInfo(workspacePath);
				if (!workspaceDir.Exists)
				{

				}
			}

			// Workspace内のファイルをロード
			workSpace.Load(@"C:\Users\jama-\source\repos\MarkdownLYT\test"); // for Desktop
			//workSpace.Load(@"C:\Users\jama\Project\MarkdownLYT\test"); // for Dell XPS13

		 // 全文章のタグを読み込んで、タグファイルを更新
		 var allTags = workSpace.GetAllTags();

			// Home ファイル作成
			workSpace.UpdateHomeFile();


			// フォルダ、ファイルの整理

			// 全文章にパンくずを追加、更新

			// MOCファイル作成


			// MOC ファイルにリンクを記載


			while (true)
			{
				string command = Console.ReadLine();
				if (command == "exit")
				{
					return;
				}
			}
		}

		static void InputWorkspacePath()
		{
			while (true)
			{
				Log.Info("Please input your workspace dir path.");
				string input = Console.ReadLine();
				var dir = new DirectoryInfo(input);
				if (!dir.Exists)
				{
					Log.Warn("The directory does not exist.");
					continue;
				}

				SettingData.GetData().workspace.path = dir.FullName;
				SettingData.GetInstance().Save();
			}
		}
	}
}
