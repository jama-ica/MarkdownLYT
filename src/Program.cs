using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkdownLYT.Setting;

namespace MarkdownLYT
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var setting = new SettingInfo();

			var workSpace = new WorkSpace();

			workSpace.Load(@"C:\Users\jama-\source\repos\MarkdownLYT\test");




			// 全文章のタグを読み込んで、タグファイルを更新

			// フォルダ、ファイルの整理

			// 全文章にパンくずを追加、更新

			// MOCファイル作成
			// MOC ファイルにリンクを記載

			Console.WriteLine("Hello");
			Console.ReadKey(true);
		}
	}
}
