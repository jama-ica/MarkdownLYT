using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT.Tag
{
	internal class AllTagInfo
	{
		public List<Tag> tags { get; }

		public AllTagInfo()
		{
			this.tags = new List<Tag>();
		}

		public void AddTag(Tag tag)
		{
			if (this.tags.Contains(tag))
			{
				return;
			}

			this.tags.Add(tag);

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
			foreach (var tag in this.tags)
			{
				if (tag.text == text)
				{
					return true;
				}
			}
			return false;
		}



	}
}
