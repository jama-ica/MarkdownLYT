using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT
{
	class BreadcrumbTrail
	{
		const string TopLinkName = "[Home]";

		public static void AddBreadcrumbTrail(FileInfo file, TagLayerInfo tagLayer)
		{
			var breadcrumbTrail = CreateBreadcrumbTrail(homeDir, file);

			bool IsAlreadyHeadBreadcrumbTrail = false;
			String allText = "";

			using (StreamReader sr = new StreamReader(file.FullName))
			{
				var firstLine = sr.ReadLine();
				{
					// 先頭は既にパンくずリストか？
					if (firstLine != null)
					{
						IsAlreadyHeadBreadcrumbTrail = firstLine.StartsWith(TopLinkName);
					}

					if (IsAlreadyHeadBreadcrumbTrail)
					{
						allText = breadcrumbTrail + "\r\n";
					}
					else
					{
						allText = breadcrumbTrail + "\r\n" + "\r\n" + firstLine + "\r\n";
					}

					if (firstLine != null)
					{
						var line = "";
						while ((line = sr.ReadLine()) != null)
						{
							allText += line + "\r\n";
						}
					}
				}
			}

			using (var sw = new StreamWriter(file.FullName, false, Encoding.UTF8))
			{
				sw.Write(allText);
			}
		}


		static String CreateBreadcrumbTrail(TagLayerInfo tagLayerInfo)
		{
			string breadcrumbTrail = "";
			while(true)
			{
				breadcrumbTrail = $"[{";
			}
			var filePath = file.Directory.FullName;
			var fileRelativePath = filePath.Replace(homeDir.FullName, "");
			var layres = fileRelativePath.Split('\\');

			String breadcrumbTrail = "";
			for (int i = 0; i < layres.Length; i++)
			{
				// リンクタイトル
				{
					if (i == 0)
					{
						breadcrumbTrail += TopLinkName;
					}
					else
					{
						breadcrumbTrail += "[" + layres[i] + "]";
					}
				}

				// リンクパス階層
				{
					breadcrumbTrail += "(";
					int distance = layres.Length - i - 1;
					if (0 == distance)
					{
						breadcrumbTrail += "./";
					}
					else if (0 < distance)
					{
						for (int j = 0; j < distance; j++)
						{
							breadcrumbTrail += "../";
						}
					}
				}

				// リンク
				{
					if (i == 0)
					{
						breadcrumbTrail += "home.md)";
					}
					else
					{
						breadcrumbTrail += layres[i] + ".md)";
					}
				}

				// 区切り
				breadcrumbTrail += " / ";
			}

			// 自身のファイル
			{
				int nameSize = file.Name.Length;
				String name = file.Name; //TODO file.Name[..(nameSize - 3)];
				if (name != layres[layres.Length - 1])
				{
					breadcrumbTrail += file.Name;
				}

				return breadcrumbTrail;
			}
		}
	}
}
