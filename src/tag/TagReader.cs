using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT.Tag
{
	internal class TagReader
	{
		public TagReader()
		{

		}

		public static List<TagInfo> Read(FileInfo file)
		{
			if (!file.Exists)
			{
				throw new FileNotFoundException(file.FullName);
			}

			var tags = new List<TagInfo>();
			
			using (StreamReader sreader = new StreamReader(file.FullName, Encoding.UTF8))
			{
				if (file == null)
				{
					// ファイルオープン失敗
					return null;
				}


				int lineNo = 0;
				while (sreader.Peek() != -1)
				{
					string lineText = sreader.ReadLine();

					var tag = ConvertTagFrom(lineText);
					tags.Add(tag);
					lineNo++;

					if(10 <= lineNo)
					{
						break;
					}
				}
			}

			//TODO or null
			return tags;
		}

		static TagInfo ConvertTagFrom(string text)
		{
			//TODO
			var deserializer = new YamlDotNet.Serialization.Deserializer();
			var a = deserializer.Deserialize<TagObject>(text);

			return new TagInfo("");
		}
	}
}
