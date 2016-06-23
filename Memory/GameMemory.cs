using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Binarysharp.MemoryManagement;
using Farplane.Properties;
using ThreadState = System.Threading.ThreadState;

namespace Farplane.Memory
{
    public static partial class GameMemory
    {
        internal delegate void ProcessExitDelegate();
        internal static event ProcessExitDelegate ProcessExited;
        internal static MemorySharp MemorySharp { get; private set; }

        private static Thread _processCheck;

        public static bool IsAttached => MemorySharp != null && MemorySharp.IsRunning;
        public static Process Process => MemorySharp.Native;
        public static string[] FFX_Hashes =
        {
            "3D13E5ED53821DF8DB204CD3C27D470C5C220B18E16849FC4440996C15F277E1", // FFX
            "D7F08CDA89DBCD6F429814E9523CE7A63A094417442A025573AFBBEF3251EF20", // FFX-2
        };

        internal static bool Attach(Process process)
        {
            if (MemorySharp != null) Detach();

            MemorySharp = new MemorySharp(process);
            var sigCheck = CheckProcessSignature();
            if (!sigCheck)
            {
                MemorySharp = null;
                return false;
            }
            CheckForUnX();

            _processCheck = new Thread(ProcessCheck);
            _processCheck.Start();
            return true;
        }

        internal static void Detach()
        {
            MemorySharp?.Dispose();
            MemorySharp = null;
            ProcessExited?.Invoke();

            OffsetScanner.Reset();
        }

        internal static bool CheckProcessSignature()
        {
            if (!IsAttached) return false;
            
            // Create new instance of the SHA256 service provider
            var shaCrypt = new System.Security.Cryptography.SHA256CryptoServiceProvider();
            var stringHash = new StringBuilder();

            try
            {
                // Read the executable data for the selected process
                var exePath = MemorySharp.Modules.MainModule.Path;
                var exeBytes = File.ReadAllBytes(exePath);

                // Compute module hash
                var moduleHash = shaCrypt.ComputeHash(exeBytes);

                // Convert hash to string

                foreach (var hashByte in moduleHash)
                    stringHash.Append(hashByte.ToString("X2"));
            }
            catch
            {
                var proceed = MessageBox.Show("An error occurred while validating the selected process:\n{ex.Message}\n\nWould you like to continue anyway?", "Process verification error",
                    MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                return proceed == MessageBoxResult.Yes;
            }
            
            
            // Compare against known hashes
            if (!FFX_Hashes.Contains(stringHash.ToString()))
            {
                // No matching hash
                Console.WriteLine("Hash: " + stringHash);
                var proceed = MessageBox.Show("The selected process does not appear to match any known compatible processes.\n\n" +
                                              "Farplane will attempt to calculate offsets automatically, but proceeding " +
                                              "may cause errors or cause the game to crash. Now is a good time to make a " +
                                              "backup of your saved games!\n\n" +
                                              "Are you sure you wish to continue?", "Unknown process error",
                    MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                return proceed == MessageBoxResult.Yes;
            }

            return true;
        }

        internal static void CheckForUnX()
        {
            if (MemorySharp == null) return;

            var modules = MemorySharp.Modules.RemoteModules.ToList();
            var unxModule =
                modules.FirstOrDefault(
                    module => string.Compare(module.Name, "unx.dll", StringComparison.CurrentCultureIgnoreCase) == 0);

            if (unxModule != null && !Settings.Default.NeverShowUnXWarning)
            {
                // UnX is installed, display warning message
                MessageBox.Show("Farplane has detected that Untitled Project X (unx.dll) is currently loaded.\n\n" +
                                "Some features of UnX, even when disabled, will overwrite changes made by Farplane, " +
                                "leading to unpredictable results.\n\n" +
                                "This warning message can be disabled from the Settings menu.", "UnX Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        internal static void ProcessCheck()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (MemorySharp != null && MemorySharp.IsRunning) continue;

                try
                {
                    Application.Current.Dispatcher.Invoke(Detach);
                } catch { }
                
                break;
            }
        }

        public static T Read<T>(int offset, bool isRelative = true)
        {
            if (!IsAttached) return default(T);
            return MemorySharp.Read<T>((IntPtr)offset, isRelative);;
        }

        public static T[] Read<T>(int offset, int count, bool isRelative = true)
        {
            if (!IsAttached) return default(T[]);
            return MemorySharp.Read<T>((IntPtr)offset, count, isRelative);
        }

        public static void Write<T>(int offset, T value, bool isRelative = true)
        {
            if (!IsAttached) return;
            MemorySharp.Write((IntPtr)offset, value, isRelative);
        }

        public static void Write<T>(int offset, T[] value, bool isRelative = true)
        {
            if (!IsAttached) return;
            MemorySharp.Write((IntPtr)offset, value, isRelative);
        }

        
    }
}
