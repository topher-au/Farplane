using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Farplane.FarplaneMod
{
    internal static class ModThread
    {
        private const int UpdateInterval = 10;

        public static bool IsRunning
            => _modThread != null && _modThread.ThreadState == ThreadState.Running;

        public static bool ProcessMods { get; set; }

        private static Thread _modThread;

        public static bool Start()
        {
            if (_modThread != null) return false;

            _modThread = new Thread(ThreadFunction);
            _modThread.Start();
            
            if (_modThread.ThreadState == ThreadState.Running) return true;

            return false;
        }

        public static void Stop()
        {
            ProcessMods = false;
            _modThread?.Abort();
            _modThread = null;
        }
        
        private static void ThreadFunction()
        {
            while (_modThread.ThreadState == ThreadState.Running)
            {
                foreach (var mod in ModLoader.GetLoadedMods())
                {
                    if (!ProcessMods) break;

                    try
                    {
                        mod.Mod.Update();
                    }
                    catch (Exception ex)
                    {
                        var modName = mod.Assembly.DefinedTypes.FirstOrDefault().ToString();
                        ModLogger.WriteLine("An exception occured processing {0}.Update():" + Environment.NewLine + "{1}", modName, ex.Message);
                        if(ex.InnerException != null)
                            ModLogger.WriteLine("Inner Exception:" + Environment.NewLine + "{1}", modName, ex.InnerException.Message);
                        ModLogger.NewLine();
                    }
                    
                }

                Thread.Sleep(UpdateInterval);
            }
        }
    }
}
