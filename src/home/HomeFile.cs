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

		public void UpdateFile(List<TagInfo> tagInfos)
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException(path);
			}

			using (var sw = new StreamWriter(this.path, append:false, Encoding.UTF8))
			{
				sw.WriteLine("# Home");
				sw.WriteLine();

				foreach (var tagInfo in tagInfos)
				{
					var layers = tagInfo.tag.GetLayers();

					sw.WriteLine("# " + tagInfo.tag.text);
					sw.WriteLine();

					if ( Define.LEYER_TOP_LEVEL < layers.Count 
						|| 3 <= tagInfo.lytFiles.Count)
					{
						//TODO add moc link
					}
					else
					{
						foreach (var lytFile in tagInfo.lytFiles)
						{ 
							//TODO add link to this file
						}
					}

				}
			}
		}
	}
}
