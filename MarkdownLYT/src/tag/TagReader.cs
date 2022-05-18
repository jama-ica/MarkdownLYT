using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

					tags.AddRange( ConvertTagFrom(lineText) );
					lineNo++;

					if(10 <= lineNo)
					{
						break;
					}
				}
			}

			return tags;
		}

		static List<TagInfo> ConvertTagFrom(string text)
		{
			var tags = new List<TagInfo>();

			var deserializer = new YamlDotNet.Serialization.Deserializer();

			try
			{
				var tagObj = deserializer.Deserialize<TagObj>(text);
				if (tagObj != null)
				{
					foreach (var tagText in tagObj.tag)
					{
						var tag = new TagInfo(tagText);
						if (tags.Contains(tag))
						{
							continue;
						}
						tags.Add(tag);
					}
				}
			}
			catch {
			}

			try
			{
				var tagsObj = deserializer.Deserialize<TagsObj>(text);
				if (tagsObj != null)
				{
					foreach (var tagText in tagsObj.tags)
					{
						var tag = new TagInfo(tagText);
						if (tags.Contains(tag))
						{
							continue;
						}
						tags.Add(tag);
					}
				}
			}
			catch
			{
			}

			return tags;
		}
	}
}
