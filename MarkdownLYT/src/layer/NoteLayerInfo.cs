using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkdownLYT.Note;
using MarkdownLYT.Tag;
using System.IO;

namespace MarkdownLYT
{
	internal class NoteLayerInfo
	{
		public string tagName { get; }

		public NoteLayerInfo? parent { get; }
		public List<NoteLayerInfo> chilidren { get; }

		public List<Notebook> notes { get; }

		public MocFile mocFile { get; }

		// Constructor
		public NoteLayerInfo(string dirFullname, string tagName, NoteLayerInfo? parent)
		{
			this.tagName = tagName;
			this.parent = parent;
			this.chilidren = new List<NoteLayerInfo>();
			this.notes = new List<Notebook>();
			this.mocFile = CreateMocFile(dirFullname, tagName);
		}

		public virtual MocFile CreateMocFile(string dirFullname, string tagName)
		{
			return new MocFile(Path.Combine(dirFullname, tagName, $"{tagName}.md"));
		}

		public virtual bool IsRoot()
		{
			return false;
		}

		public void AddLayer(string tagText, Notebook note)
		{
			string[] layers = TagPath.GetLayers(tagText);

			//var childPath = $@"{this.mocFile.GetDirectoryName()}{Path.DirectorySeparatorChar}{layers[0]}";

			if (1 == layers.Length)
			{
				var name = layers[0];
				var child = FindChildNoteLayer(name);
				if (child == null)
				{
					child = new NoteLayerInfo(this.mocFile.GetDirectoryName(), name, this);
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
					child = new NoteLayerInfo(this.mocFile.GetDirectoryName(), name, this);
					AddChild(child);
				}
				child.AddLayer(TagPath.RemoveTopLayer(tagText), note);
			}
		}


		void AddChild(NoteLayerInfo noteLayer)
		{
			this.chilidren.Add(noteLayer);
		}

		void AddNote(Notebook note)
		{
			this.notes.Add(note);
		}

		public void GetAllTags(ref List<string> allTags)
		{
			allTags.Add(tagName);

			foreach (var child in chilidren)
			{
				child.GetAllTags(ref allTags);
			}
		}

		public string GetFullTagName()
		{
			var sb = new StringBuilder();
			sb.Append(this.tagName);

			var p = this.parent;
			while (p != null && !p.IsRoot())
			{
				sb.Insert(0, $"{p.tagName} / ");
				p = p.parent;
			}

			return sb.ToString();
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

		public NoteLayerInfo SearchNoteLayer(List<string> tagLayers)
		{
			if (0 >= tagLayers.Count)
			{
				throw new Exception($"layers.Count is {tagLayers.Count}");
			}

			var childLayer = FindChildNoteLayer(tagLayers[0]);
			if (childLayer == null)
			{
				throw new Exception($"Layer not fond. Tag name is {tagLayers[0]}");
			}

			if (1 == tagLayers.Count)
			{
				return childLayer;
			}
			else
			{
				tagLayers.RemoveAt(0);
				return childLayer.SearchNoteLayer(tagLayers);
			}
		}
	}
}
