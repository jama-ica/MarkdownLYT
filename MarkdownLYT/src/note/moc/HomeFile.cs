using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;

namespace MarkdownLYT.Note
{
	internal class HomeFile : MocFile
	{

		public HomeFile(string path)
			: base(path)
		{
		}

		public void UpdateFile(RootNoteLayerInfo rootTagLayer)
		{

			if (!File.Exists(this.file.FullName))
			{
				FileUtil.SafeCreateFile(this.file.FullName);
			}

			using (var sw = new StreamWriter(this.file.FullName, append:false, Encoding.UTF8))
			{
				//// add breadcrumb trail
				//var breadcrumb = BreadcrumbTrail.CreateBreadcrumbTrail(rootTagLayer);
				//sw.WriteLine(breadcrumb);
				//sw.WriteLine();
				
				// add title
				sw.WriteLine("# Home");
				sw.WriteLine();

				// add note link
				if (0 < rootTagLayer.notes.Count)
				{
					sw.WriteLine("## Notes");
					sw.WriteLine();
				}
				foreach (var note in rootTagLayer.notes)
				{
					sw.WriteLine($"[{note.GetName()}]({note.GetFullName()})");
				}
				if (0 < rootTagLayer.notes.Count)
				{
					sw.WriteLine();
				}


				// add moc link
				if (0 < rootTagLayer.chilidren.Count)
				{
					sw.WriteLine("moc");
				}
				foreach (var childLayer in rootTagLayer.chilidren)
				{
					sw.WriteLine($"[{childLayer.tagName}](./{childLayer.tagName}/{childLayer.tagName}.md)");
				}
				if (0 < rootTagLayer.chilidren.Count)
				{
					sw.WriteLine();
				}
			}
		}


		public static bool IsHome(string filePath)
		{
			var splits = filePath.Split(Path.DirectorySeparatorChar);
			return splits[splits.Length - 1] == "Home.md";
		}
	}
}
