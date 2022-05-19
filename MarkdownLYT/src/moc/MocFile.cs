using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;

namespace MarkdownLYT.Moc
{
	internal class MocFile
	{
		public string path { get; }

		public MocFile(string path)
		{
			this.path = path;
		}

		public void UpdateFile(TagLayerInfo tagLayer)
		{
			Log.Debug($"MocFile: update file: {tagLayer.name}");

			if (!File.Exists(this.path))
			{
				FiltUtil.SafeCreateFile(this.path);
			}

			using (var sw = new StreamWriter(this.path, append: false, Encoding.UTF8))
			{
				// add breadcrumb trail
				var breadcrumb = BreadcrumbTrail.CreateBreadcrumbTrail(tagLayer);
				sw.WriteLine(breadcrumb);
				sw.WriteLine();

				// add title
				sw.WriteLine($"# {tagLayer.name}");
				sw.WriteLine();

				// add note link
				if (0 < tagLayer.notes.Count)
				{
					sw.WriteLine("notes");
					sw.WriteLine();
				}
				foreach (var note in tagLayer.notes)
				{
					var relativePath = note.GetRelativePath(tagLayer.directory);
					sw.WriteLine($"[{note.GetName()}]({relativePath})");
				}
				if (0 < tagLayer.notes.Count)
				{
					sw.WriteLine();
				}

				// add moc link
				if (0 < tagLayer.chilidren.Count)
				{
					sw.WriteLine("moc");
					sw.WriteLine();
				}
				foreach (var child in tagLayer.chilidren)
				{
					var relativePath = child.mocFile.GetRelativePath(tagLayer.directory);
					sw.WriteLine($"[{child.name}]({relativePath})");
				}
				if (0 < tagLayer.chilidren.Count)
				{
					sw.WriteLine();
				}
			}
		}

		public string GetRelativePath(string currentDir)
		{
			return Path.GetRelativePath(currentDir, this.path);
		}

	}
}
