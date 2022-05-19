using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkdownLYT.Moc;

namespace MarkdownLYT.Tag
{
	internal class TagLayerInfo
	{
		public string directory { get; }
		public string name { get; }

		public TagLayerInfo parent { get; }
		public List<TagLayerInfo> chilidren { get; }

		public List<NoteBook> notes { get; }

		public MocFile mocFile { get; }

		// Constructor
		public TagLayerInfo(string directory, string name, TagLayerInfo parent)
		{
			this.directory = directory;
			this.name = name;
			this.parent = parent;
			this.chilidren = new List<TagLayerInfo>();
			this.notes = new List<NoteBook>();
			this.mocFile = new MocFile(@$"{directory}\{name}.md");
		}

		public virtual bool IsRoot()
		{
			return false;
		}

		public void AddLayer(string tagText, NoteBook note)
		{
			string[] layers = TagPath.GetLayers(tagText);

			var childPath = $@"{this.directory}\{layers[0]}";

			if (1 == layers.Length)
			{
				var name = layers[0];
				var child = GetChild(name);
				if (child == null)
				{
					child = new TagLayerInfo(childPath, name, this);
					AddChild(child);
				}
				child.AddNote(note);
				return;
			}

			if (1 < layers.Length)
			{
				//Debug.Assert(index > -1);
				var name = TagPath.GetTopLayerName(tagText);
				var child = GetChild(name);
				if (child == null)
				{
					child = new TagLayerInfo(childPath, name, this);
					AddChild(child);
				}
				child.AddLayer(TagPath.RemoveTopLayer(tagText), note);
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
