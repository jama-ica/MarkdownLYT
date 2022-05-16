using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Debug.Info( "version: " + Define.MAJOR_VERSION + "." + Define.MINOR_VERSION + "." + Define.BUILD_VERSION );

			var setting = new SettingInfo();

			var workSpace = new WorkSpace();

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
	}
}
