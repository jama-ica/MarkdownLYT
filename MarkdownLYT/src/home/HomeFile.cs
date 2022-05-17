using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarkdownLYT.Tag;

namespace MarkdownLYT.Home
{
	internal class HomeFile
	{
		string path;

		public HomeFile(string path)
		{
			this.path = path;
		}

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
							$"[{child.name}](./{child.name}/{child.name}.md)";
						}
					}
					else
					{
						// add files link
						foreach (var lytFile in tagLayer.lytFiles)
						{
							$"[{lytFile.name}]({lytFile.fullPath})";
						}
					}
				}
			}
		}
	}
}
