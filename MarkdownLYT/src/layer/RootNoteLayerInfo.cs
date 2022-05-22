using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MarkdownLYT.Note;
using MarkdownLYT.Tag;

namespace MarkdownLYT
{
	internal class RootNoteLayerInfo : NoteLayerInfo
	{
		// Constructor
		public RootNoteLayerInfo(string path)
			: base(path, "Home", null)
		{
		}

		public override bool IsRoot()
		{
			return true;
		}
		
		public void AddLayer(NoteBook note)
		{
			foreach (var tag in note.tags)
			{
				AddLayer(tag.fullName, note);
			}
		}

		public List<string> GetAllTags()
		{
			var allTags = new List<string>();

			foreach (var child in chilidren)
			{
				child.GetAllTags(allTags);
			}

			return allTags;
		}

		public NoteLayerInfo SearchNoteLayer(TagInfo tag)
		{
			var layers = tag.layers;

			if (0 >= layers.Count)
			{
				throw new Exception($"layers.Count is {layers.Count}");
			}

			var childLayer = FindChildNoteLayer(layers[0]);
			if (childLayer == null)
			{
				throw new Exception($"Layer not fond. Tag name is {layers[0]}");
			}

			if (1 == layers.Count)
			{
				return childLayer;
			}
			else
			{
				layers.RemoveAt(0);
				return childLayer.SearchNoteLayer(layers);
			}
		}
	}
}
