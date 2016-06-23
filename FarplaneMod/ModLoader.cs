using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using CSScriptLibrary;
using Farplane.Properties;

namespace Farplane.FarplaneMod
{
    public static class ModLoader
    {
        public const string ModFolder = "mods";
        public const string ModExtension = "cs";

        private static List<FarplaneMod> _mods;
        private static List<Exception> _exceptions;
        private static string[] _scripts;

        private static ModLoadWindow _modLoadWindow;

        public delegate void CompileFinishedDelegate();
        public static event CompileFinishedDelegate CompileFinishedEvent;

        public delegate void CompileStartedDelegate();
        public static event CompileStartedDelegate CompileStartedEvent;

        public delegate void CompileExceptionDelegate();
        public static event CompileExceptionDelegate CompileExceptionEvent;

        public delegate void CompileProgressDelegate(int progress);
        public static event CompileProgressDelegate CompileProgressEvent;

        public static void LoadScripts(GameType gameType)
        {
            if (!Settings.Default.EnableMods) return;
            if (!Directory.Exists(ModFolder)) return;
            _mods = new List<FarplaneMod>();
            _exceptions = new List<Exception>();
            _scripts = Directory.GetFiles(ModFolder, "*." + ModExtension);
            ModLogger.WriteLine("Found {0} mods, starting compile...", _scripts.Length.ToString());

            // Show progress window and do mod compilation
            var compileThread = new Thread(() => { CompileScripts(gameType); });
            var progWindow = new ModLoadWindow(_scripts);
            CompileStartedEvent += progWindow.CompileStarted;
            CompileProgressEvent += progWindow.CompileProgress;
            CompileFinishedEvent += progWindow.CompileFinished;
            CompileExceptionEvent += progWindow.CompileException;
            compileThread.Start();
            progWindow.ShowDialog();
            
            if (_exceptions.Count > 0)
            {
                // Log exceptions to file
                ModLogger.WriteLine("Exceptions during script compilation:");
                foreach (var ex in _exceptions)
                {
                    ModLogger.WriteLine(ex.Message);
                    var innerEx = ex.InnerException;
                    while (innerEx != null)
                    {
                        ModLogger.WriteLine(innerEx.Message);
                        innerEx = innerEx.InnerException;
                    }
                }
            }
            CompileStartedEvent -= progWindow.CompileStarted;
            CompileProgressEvent -= progWindow.CompileProgress;
            CompileFinishedEvent -= progWindow.CompileFinished;
            CompileExceptionEvent -= progWindow.CompileException;
        }

        public static void CompileScripts(GameType gameType)
        {
            CompileStartedEvent?.Invoke();
            for (int i = 0; i < _scripts.Length; i++)
            {
                CompileProgressEvent?.Invoke(i);
                try
                {
                    ModLogger.WriteLine("Compiling {0}...", _scripts[i]);
                    var scriptAssembly = CSScript.Load(_scripts[i]);
                    var scriptObject = scriptAssembly.CreateObject("*").AlignToInterface<IFarplaneMod>();
                    if (scriptObject != null && scriptObject.GameType == gameType) _mods.Add(new FarplaneMod() { Assembly = scriptAssembly, Mod = scriptObject });
                }
                catch (Exception ex)
                {
                    _exceptions.Add(ex);
                    CompileExceptionEvent?.Invoke();
                }
            }
            CompileFinishedEvent?.Invoke();
        }

        public static FarplaneMod[] GetLoadedMods()
        {
            return _mods?.ToArray();
        }

        public class FarplaneMod
        {
            public IFarplaneMod Mod { get; set; }
            public Assembly Assembly { get; set; }
        }
    }
}
