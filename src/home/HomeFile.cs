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

		public void UpdateFile(List<Tag.Tag> tags)
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException(path);
			}

			using (var sw = new StreamWriter(this.path, append:false, Encoding.UTF8))
			{
				sw.WriteLine("# Home");
				sw.WriteLine();

				foreach (var tag in tags)
				{
					int layerLevel = tag.GetLayers().Count;
					if (layerLevel != Define.LEYER_TOP_LEVEL)
					{
						continue;
					}

					sw.WriteLine("# " + tag.text);
					sw.WriteLine();

				}
			}
		}
	}
}
