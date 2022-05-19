using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;

namespace MarkdownLYT.Moc
{
	internal class HomeMocFile : MocFile
	{

		public HomeMocFile(string path)
			: base(path)
		{
		}

		public void UpdateFile(RootNoteLayerInfo rootTagLayer)
		{
			Log.Debug("HomeFile: update file");

			if (!File.Exists(this.path))
			{
				using (File.Create(this.path));
			}

			using (var sw = new StreamWriter(this.path, append:false, Encoding.UTF8))
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
					sw.WriteLine($"[{note.GetName()}]({note.GetPath()})");
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
					sw.WriteLine($"[{childLayer.name}](./{childLayer.name}/{childLayer.name}.md)");
				}
				if (0 < rootTagLayer.chilidren.Count)
				{
					sw.WriteLine();
				}
			}
		}
	}
}
