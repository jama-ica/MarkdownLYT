using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownLYT
{
	internal class WorkspaceSettingFile
	{
		public static readonly string fileName = "workspace_setting.dat";

		static WorkspaceSettingFile? Instance = null;

		WorkspaceSettingObj? dataObj;

		private WorkspaceSettingFile()
		{
			this.dataObj = null;
		}

		public static WorkspaceSettingFile GetInstance()
		{
			if (Instance == null)
			{
				Instance = new WorkspaceSettingFile();
			}
			return Instance;
		}

		public static WorkspaceSettingObj? GetData()
		{
			return GetInstance().dataObj;
		}

		public bool Load()
		{
			if (!File.Exists(fileName))
			{
				return false;
			}

			var text = File.ReadAllText(fileName);
			var deserializer = new YamlDotNet.Serialization.Deserializer();
			this.dataObj = deserializer.Deserialize<WorkspaceSettingObj>(text);
			return true;
		}

		public void Save()
		{
			if (this.dataObj == null)
			{
				Logger.Error("dataObj is null");
				return;
			}
			Save(this.dataObj);
		}

		public void Save(WorkspaceSettingObj data)
		{
			this.dataObj = data;
			using TextWriter writer = File.CreateText(fileName);
			var serializer = new YamlDotNet.Serialization.Serializer();
			serializer.Serialize(writer, data);
		}

		public WorkspaceSettingObj CreateDefaultData()
		{
			var data = new WorkspaceSettingObj
			{
			};
			return data;
		}
	}
}
