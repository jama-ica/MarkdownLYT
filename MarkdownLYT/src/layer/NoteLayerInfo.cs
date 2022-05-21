using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkdownLYT.Note;
using MarkdownLYT.Tag;

namespace MarkdownLYT
{
	internal class NoteLayerInfo
	{
		public string tagName { get; }

		public NoteLayerInfo? parent { get; }
		public List<NoteLayerInfo> chilidren { get; }

		public List<NoteBook> notes { get; }

		public MocFile mocFile { get; }

		// Constructor
		public NoteLayerInfo(string path, string tagName, NoteLayerInfo? parent)
		{
			this.tagName = tagName;
			this.parent = parent;
			this.chilidren = new List<NoteLayerInfo>();
			this.notes = new List<NoteBook>();
			this.mocFile = new MocFile(@$"{path}{Path.DirectorySeparatorChar}{tagName}.md");
		}

		public virtual bool IsRoot()
		{
			return false;
		}

		public void AddLayer(string tagText, NoteBook note)
		{
			string[] layers = TagPath.GetLayers(tagText);

			var childPath = $@"{this.mocFile.GetDirectoryName()}{Path.DirectorySeparatorChar}{layers[0]}";

			if (1 == layers.Length)
			{
				var name = layers[0];
				var child = FindChildNoteLayer(name);
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
				var child = FindChildNoteLayer(name);
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

		public void GetAllTags(List<string> allTags)
		{
			allTags.Add(tagName);

			foreach (var child in chilidren)
			{
				child.GetAllTags(allTags);
			}
		}

		public NoteLayerInfo? FindChildNoteLayer(string tagName)
		{
			foreach (var child in chilidren)
			{
				if (child.tagName == tagName)
				{
					return child;
				}
			}
			return null;
		}
	}
}
