using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Farplane.FarplaneMod
{
    internal class ModSettings
    {
        private const string ModSettingsFile = "FarplaneMods.xml";
        public static List<ModSetting> ReadSettings()
        {
            try
            {
                using (var xmlStream = new FileStream(ModSettingsFile, FileMode.Open))
                {
                    var xmlSerializer = new XmlSerializer(typeof(List<ModSetting>));

                    var settingsList = (List<ModSetting>) xmlSerializer.Deserialize(xmlStream);
                    return settingsList;
                }
            }
            catch (Exception ex)
            {
                ModLogger.WriteLine("An attempt to read the settings failed, no settings have been loaded.");
                ModLogger.WriteLine(ex.Message);
                return new List<ModSetting>();
            }
        }

        public static void WriteSettings(List<ModSetting> settings)
        {
            try
            {
                using (var xmlStream = new FileStream(ModSettingsFile, FileMode.Create))
                {
                    var xmlSerializer = new XmlSerializer(typeof(List<ModSetting>));

                    xmlSerializer.Serialize(xmlStream, settings);
                }
            }
            catch (Exception ex)
            {
                ModLogger.WriteLine("An attempt to write the settings failed:");
                ModLogger.WriteLine(ex.Message);
            }
        }
    }

    public class ModSetting
    {
        public string ClassName { get; set; }
        public bool Activated { get; set; }
    }
}
