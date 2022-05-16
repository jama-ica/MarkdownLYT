using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT.Tag
{
	internal class Tag : IEqualityComparer<Tag>
	{
		public string text { get; }

		public Tag(string text)
		{
			this.text = text;
		}

		public List<string> GetLayers()
		{
			return this.text.Split('/').ToList();
		}

		public string GetTextUntil(int layerLevel)
		{
			StringBuilder sb = new StringBuilder();
			var layers = GetLayers();
			for (int i = 0; i < layerLevel; i++)
			{
				sb.Append(layers[i]);
				if(i + 1 < layerLevel)
				{
					sb.Append('/');
				}
			}
			return sb.ToString();
		}

		public bool Equals(Tag x, Tag y)
		{
			return x.text == y.text;
		}

		public int GetHashCode(Tag obj)
		{
			return obj.text.GetHashCode();
		}

	}
}
