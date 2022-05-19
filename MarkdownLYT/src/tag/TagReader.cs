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

			using (var sr = new StreamReader(file.FullName, Encoding.UTF8))
			{
				var deserializer = new YamlDotNet.Serialization.Deserializer();
				for (int i = 0; sr.Peek() != -1 || i < 10; i++)
				{
					var line = sr.ReadLine();
					var tgs = DeserializeTag(deserializer, line);
					if (tgs == null)
					{
						continue;
					}
					tags.AddRange(tgs);
				}
			}

			return tags;
		}

		static List<TagInfo> DeserializeTag(YamlDotNet.Serialization.Deserializer deserializer, string text)
		{
			if (text == null)
			{
				return null;
			}
			if(!text.StartsWith("Tag: ") && !text.StartsWith("Tags: ") && !text.StartsWith("tag: ") && !text.StartsWith("tags: "))
			{
				return null;
			}
			

			var tags = new List<TagInfo>();

			try
			{
				var dict = deserializer.Deserialize<Dictionary<string, string[]>>(text);
				if (dict != null)
				{
					foreach (KeyValuePair<string, string[]> pair in dict)
					{
						if (pair.Key == "tag" || pair.Key == "Tag" || pair.Key == "tags" || pair.Key == "Tags")
						{
							foreach (var val in pair.Value)
							{
								var tag = new TagInfo(val);
								tags.Add(tag);
							}
						}
					}
				}
			}
			catch
			{
			}

			try
			{
				var dict = deserializer.Deserialize<Dictionary<string, string>>(text);
				if (dict != null)
				{
					foreach (KeyValuePair<string, string> pair in dict)
					{
						if (pair.Key == "tag" || pair.Key == "Tag" || pair.Key == "tags" || pair.Key == "Tags")
						{
							var tag = new TagInfo(pair.Value);
							tags.Add(tag);
						}
					}
				}
			}
			catch
			{
			}

			return tags;
		}


		static List<TagInfo> ConvertTagFrom3(string text)
		{
			var tags = new List<TagInfo>();

			var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
							.WithNamingConvention(YamlDotNet.Serialization.NamingConventions.CamelCaseNamingConvention.Instance)
							.Build();

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
			catch
			{
			}

			return tags;
		}

		static List<TagInfo> ConvertTagFrom2(string text)
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
