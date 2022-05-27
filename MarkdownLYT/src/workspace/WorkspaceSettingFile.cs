using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT
{
	internal class WorkspaceSettingFile
	{
		public static readonly string FileFullname = "workspace_setting.dat";

		string workspaceDirFullname;
		WorkspaceSettingObj? dataObj;

		public WorkspaceSettingFile()
		{
			this.workspaceDirFullname = string.Empty;
			this.dataObj = null;
		}

		public bool Load(string workspaceDirFullname)
		{
			this.workspaceDirFullname = workspaceDirFullname;

			var fullname = GetFileFullname();

			if (!File.Exists(fullname))
			{
				FileUtil.SafeCreateFile(fullname);
				var obj = CreateDefaultData();
				Save(obj);
			}

			var text = File.ReadAllText(fullname);
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
			using TextWriter writer = File.CreateText(GetFileFullname());
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

		string GetFileFullname()
		{
			return GetFileFullname(this.workspaceDirFullname);
		}

		string GetFileFullname(string workspaceDirFullname)
		{
			if (workspaceDirFullname == string.Empty)
			{
				throw new Exception("workspaceDirFullname is Empty");
			}
			return Path.Combine(workspaceDirFullname, FileFullname);
		}
	}
}
