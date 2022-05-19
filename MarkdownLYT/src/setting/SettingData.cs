using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarkdownLYT
{
	internal class SettingData
	{
		public static readonly string fileName = "setting.dat";

		static SettingData Instance = null;

		SettingDataObj dataObj;

		public SettingData()
		{
			this.dataObj = null;
		}

		public SettingDataObj GetData()
		{
			if (this.dataObj != null)
			{
				return this.dataObj;
			}

			var data = Load();
			if (data != null)
			{
				this.dataObj = data;
				return this.dataObj;
			}

			return CreateDefaultData();
		}

		public SettingDataObj Load()
		{
			if (!File.Exists(fileName))
			{
				return null;
			}

			var deserializer = new YamlDotNet.Serialization.Deserializer();
			return deserializer.Deserialize<SettingDataObj>(fileName);
		}

		void Save(SettingDataObj data)
		{
			using TextWriter writer = File.CreateText(fileName);
			var serializer = new YamlDotNet.Serialization.Serializer();
			serializer.Serialize(writer, data);
		}

		SettingDataObj CreateDefaultData()
		{
			var data = new SettingDataObj
			{
				workspace = new SettingDataObj.WorkspaceData
				{
					path = "",
				}
			};
			return data;
		}

	}
}
