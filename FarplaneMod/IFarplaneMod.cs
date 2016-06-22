using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.FarplaneMod
{
    public interface IFarplaneMod
    {
        /// <summary>
        /// The name of the mod
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The author of the mod
        /// </summary>
        string Author { get; }
        /// <summary>
        /// A description of the mod's features or function
        /// </summary>
        string Description { get; }
        /// <summary>
        /// The text displayed on the configuration button. Set to null to disable.
        /// </summary>
        string ConfigButton { get; }
        /// <summary>
        /// Whether the mod is allowed to auto-activate. User will still need to activate mod manually the first time.
        /// </summary>
        bool AutoActivate { get; }
        /// <summary>
        /// The game type the mod is for
        /// </summary>
        GameType GameType { get; }

        /// <summary>
        /// Returns the current state of the mod
        /// </summary>
        ModState GetState();
        /// <summary>
        /// This function is called when the mod is activated, and should contain
        /// any code necessary for the mod to start functioning.
        /// </summary>
        void Activate();
        /// <summary>
        /// This function is called when the mod is deactivated, and should attempt
        /// to reverse any changes made by the mod where possible before the mod is
        /// unloaded.
        /// </summary>
        void Deactivate();
        /// <summary>
        /// This function is called every time the mod thread loops. This is approximately
        /// every 10ms, but could be slower depending on the currently active mods.
        /// </summary>
        void Update();
        /// <summary>
        /// This function is called when the user clicks the configuration button.
        /// </summary>
        void Configure(object parent);
    }

    public enum GameType
    {
        FFX,
        FFX2,
        FFX2LM
    }

    public enum ModState
    {
        Deactivated,
        Activated,
        Unknown = 0xFF
    }
}
