using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CSScriptLibrary;
using Farplane.Common;

namespace Farplane.FarplaneMod
{
    internal static class ModLoader
    {
        private static readonly string _modFolder = "mods";
        private static List<IFarplaneMod> _loadedMods = new List<IFarplaneMod>();
        private static List<Assembly> _modAssemblies = new List<Assembly>();

        public static void LoadAllMods(GameType type)
        {
            if (_loadedMods.Count > 0) UnloadAllMods();

            var modSettings = ModSettings.ReadSettings();

            ModLogger.StartNewLog();
            var fpVersion = Assembly.GetExecutingAssembly().GetName().Version;
            var stVersion = string.Format("{0}.{1}.{2}", fpVersion.Major, fpVersion.Minor, fpVersion.Build);
            ModLogger.WriteLine("Farplane {0} Mod Loader" + Environment.NewLine, stVersion);
            ModLogger.WriteLine("Searching for Farplane scripts in .\\mods...");

            var scriptFiles = Directory.GetFiles("mods", "*.cs");

            ModLogger.WriteLine("Found {0} scripts", scriptFiles.Length.ToString());
            ModLogger.NewLine();

            foreach (var script in scriptFiles)
            {
                // Attempt to compile mod script into assembly
                ModLogger.WriteLine("Compiling mod script {0}...", Path.GetFileName(script));
                var modAsm = CreateModAssembly(script);

                // Attempt to initialize assembly
                ModLogger.WriteLine("Loading compiled assembly...");
                var mod = CreateModInterface(modAsm);

                if (modAsm == null || mod == null)
                {
                    ModLogger.WriteLine("Failed to load mod.");
                    ModLogger.NewLine();
                    continue;
                }

                ModLogger.NewLine();
                ModLogger.WriteLine("Loaded assembly details:");
                ModLogger.WriteLine("Mod Name: {0}", mod.Name);
                ModLogger.WriteLine("Mod Author: {0}", mod.Author);
                ModLogger.WriteLine("Mod Type: {0}", mod.GameType.ToString());

                if (mod.GameType != type)
                {
                    ModLogger.WriteLine("Invalid GameType ({1}) for this game ({0}), mod will be ignored.",
                        type.ToString(), mod.GameType.ToString());
                    continue;
                }

                _loadedMods.Add(mod);
                _modAssemblies.Add(modAsm);

                ModLogger.WriteLine("The mod was successfully loaded.");
                ModLogger.NewLine();

                var modClassName = modAsm.DefinedTypes.First().Name;
                var modSetting = modSettings.FirstOrDefault(setting => setting.ClassName == modClassName);
                if (modSetting != null && modSetting.Activated)
                {
                    ModLogger.WriteLine("Previously set as active, reactivating mod.");
                    ActivateMod(mod, true);
                }

                ModLogger.NewLine();
            }
            ModLogger.WriteLine("Finished loading mods.");
            ModLogger.NewLine();
        }

        public static void UnloadAllMods()
        {
            ModLogger.WriteLine("Attempting to unload all mods...");
            for (int i = 0; i < _loadedMods.Count; i++)
            {
                ModLogger.WriteLine("Unloading {0}", _loadedMods[i].Name);
                // Deactivate mod
                try
                {
                    DeactivateMod(_loadedMods[i], true);
                    ModLogger.WriteLine("Successfully unloaded mod");
                }
                catch (Exception ex)
                {
                    ModLogger.WriteLine("Error unloading mod:\n{0}", ex.Message);
                }
            }
            _modAssemblies.Clear();
            _loadedMods.Clear();
            ModLogger.WriteLine("Finished unloading mods.");
        }

        public static string GetClassName(IFarplaneMod mod)
        {
            var modIndex = _loadedMods.IndexOf(mod);
            var modAssembly = _modAssemblies[modIndex];
            var modClass = modAssembly.DefinedTypes.First().Name;
            return modClass;
        }

        internal static IFarplaneMod[] GetLoadedMods()
        {
            return _loadedMods.ToArray();
        }

        internal static Assembly[] GetLoadedAssemblies()
        {
            return _modAssemblies.ToArray();
        }

        internal static bool GetModState(IFarplaneMod mod)
        {
            if (!_loadedMods.Contains(mod)) return false;

            var modSettings = ModSettings.ReadSettings();
            var modClass = GetClassName(mod);

            var setting = modSettings.FirstOrDefault(s => s.ClassName == modClass);

            return setting != null && setting.Activated;
        }

        internal static void ActivateMod(IFarplaneMod mod, bool temporarily = false)
        {
            ModLogger.WriteLine("Calling {0}.Activate()", GetClassName(mod));
            try
            {
                mod.Activate();
            }
            catch (Exception ex)
            {
                ModLogger.WriteLine("An exception occurred while executing Activate()");
                ModLogger.Write(ex.Message);
            }


            if (temporarily) return;

            var modSettings = ModSettings.ReadSettings();
            var modClass = GetClassName(mod);

            var setting = modSettings.FirstOrDefault(s => s.ClassName == modClass);
            if (setting == null)
            {
                modSettings.Add(new ModSetting() {Activated = true, ClassName = modClass});
            }
            else
            {
                modSettings.First(set => set.ClassName == modClass).Activated = true;
            }


            ModSettings.WriteSettings(modSettings);
        }

        internal static void DeactivateMod(IFarplaneMod mod, bool temporarily = false)
        {
            ModLogger.WriteLine("Calling {0}.Deactivate()", GetClassName(mod));

            try
            {
                mod.Deactivate();
            }
            catch (Exception ex)
            {
                ModLogger.WriteLine("An exception occurred while executing Deactivate()");
                ModLogger.Write(ex.Message);
            }

            if (temporarily) return;

            var modSettings = ModSettings.ReadSettings();

            var modClass = GetClassName(mod);

            var setting = modSettings.FirstOrDefault(s => s.ClassName == modClass);
            if (setting == null)
            {
                modSettings.Add(new ModSetting() {Activated = false, ClassName = modClass});
            }
            else
            {
                modSettings.First(set => set.ClassName == modClass).Activated = false;
            }

            ModSettings.WriteSettings(modSettings);
        }

        private static IFarplaneMod CreateModInterface(Assembly modAssembly)
        {
            IFarplaneMod csObject = null;

            try
            {
                var className = modAssembly.DefinedTypes.First().Name;
                csObject = modAssembly.CreateObject(className).AlignToInterface<IFarplaneMod>();
            }
            catch (Exception ex)
            {
                ModLogger.WriteLine("An exception occurred while creating the mod interface:");
                ModLogger.WriteLine(ex.Message);
            }

            return csObject;
        }

        private static Assembly CreateModAssembly(string scriptFile)
        {
            try
            {
                // Attempt to compile assembly
                var csAssembly = CSScript.Load(scriptFile);
                return csAssembly;
            }
            catch (Exception ex)
            {
                // Error occurred compiling assembly
                ModLogger.WriteLine("An exception occurred while compiling script file:");
                ModLogger.WriteLine(ex.Message);
                return null;
            }
        }
    }
}