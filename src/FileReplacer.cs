using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT
{
	internal class FileReplacer
	{
		public FileReplacer()
		{
		}

		public void Replace(DirectoryInfo dir)
		{
			// path 以下の全ファイル

			FileInfo[] files = dir.GetFiles("*.txt", System.IO.SearchOption.AllDirectories);

			foreach (System.IO.FileInfo f in files)
			{
				// タグを読み込み

			}

			// 不要フォルダの削除


		}
	}
}
