using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Farplane.FarplaneMod
{
    public static class ModSettings
    {
        private const string ModSettingsFile = "FarplaneMods.xml";

        public static T ReadSetting<T>(string settingName)
        {
            var scriptName = Assembly.GetCallingAssembly().GetName().Name;
            return ReadSetting<T>(scriptName, settingName);
        }

        public static void WriteSetting(string settingName, object value)
        {
            var scriptName = Assembly.GetCallingAssembly().GetName().Name;
            WriteSetting(scriptName, settingName, value);
        }

        internal static T ReadSetting<T>(string scriptName, string settingName)
        {
            if (!File.Exists(ModSettingsFile)) return default(T);
            try
            {
                using (var settingsStream = new FileStream(ModSettingsFile, FileMode.Open))
                {
                    var xmlSerializer = new XmlSerializer(typeof (SettingsFile));
                    var settingsObject = (SettingsFile) xmlSerializer.Deserialize(settingsStream);
                    var modSettings =
                        settingsObject.ModSettings.First(
                            mod => string.Equals(mod.ScriptName, scriptName, StringComparison.CurrentCultureIgnoreCase));
                    var setting =
                        modSettings.Settings.First(
                            set => string.Equals(set.Name, settingName, StringComparison.CurrentCultureIgnoreCase));
                    return (T) setting.Value;
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        internal static void WriteSetting(string scriptName, string settingName, object value)
        {
            using (var settingsStream = new FileStream(ModSettingsFile, FileMode.OpenOrCreate))
            {
                var xmlSerializer = new XmlSerializer(typeof (SettingsFile));
                SettingsFile settingsObject;
                try
                {
                    settingsObject = (SettingsFile) xmlSerializer.Deserialize(settingsStream);
                }
                catch
                {
                    settingsObject = new SettingsFile();
                }

                if (settingsObject.ModSettings == null)
                    settingsObject.ModSettings = new List<ModSetting>();
                var modSettings =
                    settingsObject.ModSettings.FirstOrDefault(
                        mod => string.Equals(mod.ScriptName, scriptName, StringComparison.CurrentCultureIgnoreCase));

                if (modSettings == null)
                {
                    settingsObject.ModSettings.Add(new ModSetting()
                    {
                        ScriptName = scriptName,
                        Settings = new List<Setting>()
                    });
                    modSettings = settingsObject.ModSettings.Last();
                }


                var setting =
                    modSettings.Settings.FirstOrDefault(
                        set => string.Equals(set.Name, settingName, StringComparison.CurrentCultureIgnoreCase));

                if (setting == null)
                {
                    modSettings.Settings.Add(new Setting() {Name = settingName, Value = value});
                    setting = modSettings.Settings.Last();
                }


                setting.Value = value;
                settingsStream.Position = 0;
                settingsStream.SetLength(0);
                xmlSerializer.Serialize(settingsStream, settingsObject);
            }
        }
    }

    [XmlType("SettingsFile")]
    public class SettingsFile
    {
        [XmlElement("ModSettings")] public List<ModSetting> ModSettings;
    }

    [XmlType("Settings")]
    public class ModSetting
    {
        [XmlAttribute("Mod")]
        public string ScriptName { get; set; }

        [XmlAttribute("Activated")]
        public bool Activated { get; set; }

        [XmlElement("Settings")]
        public List<Setting> Settings { get; set; }
    }

    [XmlType("Setting")]
    public class Setting
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlElement("Value")]
        public object Value { get; set; }
    }
}