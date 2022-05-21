using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT.Tag
{
	internal class TagInfo : IEqualityComparer<TagInfo>
	{
		public string fullName { get; }
		public List<string> layers { get; }

		public TagInfo(string fullName)
		{
			this.fullName = fullName;
			this.layers = this.fullName.Split('/').ToList();
		}

		public string GetName()
		{
			return layers.Last();
		}

		public string GetName(int layerLevel)
		{
			if(Define.LEYER_TOP_LEVEL > layerLevel)
			{
				Log.Error($"invalid layer level (= {layerLevel}) is passed");
				return "";
			}
			if (layerLevel >= layers.Count)
			{
				Log.Error($"layer level ({layerLevel}) over the layers count ({layers.Count})");
				return "";
			}

			return layers[layerLevel];
		}

		public string GetPath(int layerLevel)
		{
			if (Define.LEYER_TOP_LEVEL > layerLevel)
			{
				Log.Error($"invalid layer level (= {layerLevel}) is passed");
				return "";
			}
			if (layerLevel >= layers.Count)
			{
				Log.Error($"layer level ({layerLevel}) over the layers count ({layers.Count})");
				return "";
			}

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < layerLevel; i++)
			{
				sb.Append(layers[i]);
				if(i + 1 >= layerLevel)
				{
					sb.Append('/');
				}
			}
			return sb.ToString();
		}




		public bool Equals(TagInfo x, TagInfo y)
		{
			if (x == null || y == null)
			{
				return false;
			}
			return x.fullName == y.fullName;
		}

		public int GetHashCode(TagInfo obj)
		{
			return obj.fullName.GetHashCode();
		}

	}
}
