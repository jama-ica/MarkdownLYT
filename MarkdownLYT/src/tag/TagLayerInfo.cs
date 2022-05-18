using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT.Tag
{
	internal class TagLayerInfo
	{
		public string name { get;  }

		public TagLayerInfo parent { get; }
		public List<TagLayerInfo> chilidren { get; }

		public List<NoteBook> notes { get; }

		// Constructor
		public TagLayerInfo(string name, TagLayerInfo parent)
		{
			this.name = name;
			this.parent = parent;
			this.chilidren = new List<TagLayerInfo>();
			this.notes = new List<NoteBook>();
		}

		public virtual bool IsRoot()
		{
			return false;
		}

		public void AddLayer(string path, NoteBook note)
		{
			string[] layers = TagPath.GetLayers(path);

			if (1 == layers.Length)
			{
				var child = GetChild(layers[0]);
				if (child == null)
				{
					child = new TagLayerInfo(layers[0], this);
					AddChild(child);
				}
				child.AddNote(note);
				return;
			}

			if (1 < layers.Length)
			{
				//Debug.Assert(index > -1);
				var name = TagPath.GetTopLayerName(path);
				var child = GetChild(name);
				if (child == null)
				{
					child = new TagLayerInfo(name, this);
					AddChild(child);
				}
				child.AddLytFile(TagPath.RemoveTopLayer(path), note);
			}
		}


		void AddChild(TagLayerInfo tagLayerInfo)
		{
			this.chilidren.Add(tagLayerInfo);
		}

		void AddNote(NoteBook note)
		{
			this.notes.Add(note);
		}

		TagLayerInfo? GetChild(string name)
		{
			foreach (var child in this.chilidren)
			{
				if (child.name == name)
				{
					return child;
				}
			}
			return null;
		}
	}
}
