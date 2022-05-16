using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT.Tag
{
	internal class AllTagInfo
	{
		public List<TagInfo> tagInfos { get; }

		public AllTagInfo()
		{
			this.tagInfos = new List<TagInfo>();
		}

		public void AddTagInfo(TagInfo tagInfo)
		{
			if (this.tagInfos.Contains(tagInfo))
			{
				return;
			}

			this.tagInfos.Add(tagInfo);

			//var layers = tag.GetLayers();
			//for (int i = 1; i < layers.Count; i++)
			//{
			//	string text = tag.GetTextUntil(i);
			//	if (Exist(text))
			//	{
			//		continue;
			//	}
			
			//	this.tags.Add(new TagInfo(text));
			//}
		}

		bool Exist(string text)
		{
			foreach (var tagInfo in this.tagInfos)
			{
				if (tagInfo.tag.text == text)
				{
					return true;
				}
			}
			return false;
		}



	}
}
