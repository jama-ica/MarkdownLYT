using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;

namespace MarkdownLYT
{
	class BreadcrumbTrail
	{
		const string TopLinkName = "[Home]";

		public static void AddBreadcrumbTrail(FileInfo file, TagLayerInfo tagLayer)
		{
			var breadcrumbTrail = CreateBreadcrumbTrail(tagLayer);

			String allText = "";
			E_BREADCRUMB_TRAIL_STATE state;
			using (var sr = new StreamReader(file.FullName))
			{
				var firstLine = sr.ReadLine();
				state = CheckBreadcrumbTrail(breadcrumbTrail, firstLine);
			}

			switch(state)
			{
				case E_BREADCRUMB_TRAIL_STATE.NONE:
					AppendBreadcrumbTrail(breadcrumbTrail, file);
					break;
				case E_BREADCRUMB_TRAIL_STATE.INCORRECT:
					ReplaceBreadcrumbTrail(breadcrumbTrail, file);
					break;
				case E_BREADCRUMB_TRAIL_STATE.CORRECT:
					// nothing to do
					break;
				default:
					Log.Error($"unexpect state ({state})");
					break;
			}
		}

		static void AppendBreadcrumbTrail(string breadcrumbTrail, FileInfo file)
		{
			using (var sw = new StreamWriter(file.FullName, append:true, Encoding.UTF8))
			{
				sw.WriteLine(breadcrumbTrail);
				sw.WriteLine("");
			}
		}

		static void ReplaceBreadcrumbTrail(string breadcrumbTrail, FileInfo file)
		{
			string[] lines = System.IO.File.ReadAllLines(file.FullName, Encoding.UTF8);

			lines[0] = breadcrumbTrail;

			using (var sw = new StreamWriter(file.FullName, append: false, Encoding.UTF8))
			{
				foreach (var line in lines)
				{
					sw.WriteLine(line);
				}
			}
		}

		enum E_BREADCRUMB_TRAIL_STATE
		{
			NONE,
			INCORRECT,
			CORRECT,
		};

		static E_BREADCRUMB_TRAIL_STATE CheckBreadcrumbTrail(string breadcrumbTrail, string text)
		{
			if (!text.StartsWith("[Home]("))
			{
				return E_BREADCRUMB_TRAIL_STATE.NONE;
			}

			if (breadcrumbTrail == text)
			{
				return E_BREADCRUMB_TRAIL_STATE.CORRECT;
			}

			return E_BREADCRUMB_TRAIL_STATE.INCORRECT;
		}

		static string CreateBreadcrumbTrail(TagLayerInfo tagLayerInfo)
		{
			var sb = new StringBuilder();

			TagLayerInfo parent = null;
			var relativePathSign = new StringBuilder();
			int layer = 0;
			while (true)
			{
				parent = tagLayerInfo.parent;
				if (parent == null)
				{
					break;
				}

				// update relative path sign
				if (layer == 0)
				{
					relativePathSign.Append("./");
				}
				else if (layer == 1)
				{
					relativePathSign.Insert(0, "."); // "./" -> "../"
				}
				else
				{
					relativePathSign.Append("../");
				}

				sb.Insert(0, $"[{parent.name}]({relativePathSign.ToString()}{parent.name}) / ");
				layer++;
			}

			sb.Insert(0, $"[Home]({relativePathSign.ToString()}Home.md) / ");
			return sb.ToString();
		}
	}
}
