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
<<<<<<< Updated upstream
		string path;
=======
		public string path { get; }
>>>>>>> Stashed changes

		public MocFile(string path)
		{
			this.path = path;
		}

<<<<<<< Updated upstream
		public void UpdateFile(TagLayerInfo rootTagLayer)
		{
			if (!File.Exists(this.path))
			{
				throw new FileNotFoundException(this.path);
			}

			using (var sw = new StreamWriter(this.path, append:false, Encoding.UTF8))
			{
				sw.WriteLine("# Home");
				sw.WriteLine();

				foreach (var tagLayer in rootTagLayer.chilidren)
				{
					sw.WriteLine("# " + tagLayer.name);
					sw.WriteLine();

					if ( 0 < tagLayer.chilidren.Count)
					{
						// add moc link
						foreach (var child in tagLayer.chilidren)
						{
							sw.WriteLine($"[{child.name}](./{child.name}/{child.name}.md)");
						}
					}
					else
					{
						// add files link
						foreach (var note in tagLayer.notes)
						{
							sw.WriteLine($"[{note.GetName()}]({note.GetFullName()})");
						}
					}
				}
			}
		}
=======
		public void UpdateFile(TagLayerInfo tagLayer)
		{
			Log.Debug($"MocFile: update file: {tagLayer.name}");

			if (!File.Exists(this.path))
			{
				using (File.Create(this.path)) { };
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
					sw.WriteLine($"[{note.GetName()}]({note.GetPath()})");
				}
				if (0 < tagLayer.notes.Count)
				{
					sw.WriteLine();
				}


				// add moc link
				if (0 < tagLayer.chilidren.Count)
				{
					sw.WriteLine("moc");
				}
				foreach (var childLayer in tagLayer.chilidren)
				{
					sw.WriteLine($"[{childLayer.name}](./{childLayer.name}/{childLayer.name}.md)");
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

>>>>>>> Stashed changes
	}
}
