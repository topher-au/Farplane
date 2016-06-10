using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Farplane.FarplaneMod
{
    internal static class ModUpdateThread
    {
        private const int UpdateInterval = 10;

        public static bool IsRunning
            => _modThread != null && _modThread.ThreadState == ThreadState.Running;

        public static bool ProcessMods { get; set; }

        private static Thread _modThread;

        public static bool Start()
        {
            if (_modThread != null) return false;

            _modThread = new Thread(ModThread);
            _modThread.Start();
            
            if (_modThread.ThreadState == ThreadState.Running) return true;

            return false;
        }

        public static void Stop()
        {
            ProcessMods = false;
            _modThread.Abort();
            _modThread = null;
        }
        
        private static void ModThread()
        {
            while (_modThread.ThreadState == ThreadState.Running)
            {
                foreach (var mod in ModLoader.GetLoadedMods())
                {
                    if (!ProcessMods) break;

                    try
                    {
                        // Attempt to execute mod update
                        mod.Update();
                    }
                    catch (Exception ex)
                    {
                        var modName = ModLoader.GetClassName(mod);
                        ModLogger.WriteLine("An exception occured processing {0}.Update():" + Environment.NewLine, modName, ex.Message);
                        ModLogger.NewLine();
                    }
                    
                }

                Thread.Sleep(UpdateInterval);
            }
        }
    }
}
