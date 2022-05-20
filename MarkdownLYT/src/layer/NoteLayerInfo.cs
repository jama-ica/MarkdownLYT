using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkdownLYT.Moc;

namespace MarkdownLYT.Tag
{
	internal class NoteLayerInfo
	{
		public string directory { get; }
		public string tagName { get; }

		public NoteLayerInfo parent { get; }
		public List<NoteLayerInfo> chilidren { get; }

		public List<NoteBook> notes { get; }

		public MocFile mocFile { get; }

		// Constructor
		public NoteLayerInfo(string directory, string tagName, NoteLayerInfo parent)
		{
			this.directory = directory;
			this.tagName = tagName;
			this.parent = parent;
			this.chilidren = new List<NoteLayerInfo>();
			this.notes = new List<NoteBook>();
			this.mocFile = new MocFile(@$"{directory}\{tagName}.md");
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
					child = new NoteLayerInfo(childPath, name, this);
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
					child = new NoteLayerInfo(childPath, name, this);
					AddChild(child);
				}
				child.AddLayer(TagPath.RemoveTopLayer(tagText), note);
			}
		}


		void AddChild(NoteLayerInfo noteLayer)
		{
			this.chilidren.Add(noteLayer);
		}

		void AddNote(NoteBook note)
		{
			this.notes.Add(note);
		}

		NoteLayerInfo? GetChild(string name)
		{
			foreach (var child in this.chilidren)
			{
				if (child.tagName == name)
				{
					return child;
				}
			}
			return null;
		}

		public bool GetAllTags(List<string> allTags)
		{
			if (allTags == null)
			{
				throw new Exception("list is null");
			}

			allTags.Add(tagName);

			foreach (var child in chilidren)
			{
				child.GetAllTags(allTags);
			}
			return true;
		}
	}
}
