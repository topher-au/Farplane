using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.FarplaneMod
{
    public interface IFarplaneMod
    {
        string Name { get; }
        string Author { get; }
        string Description { get; }
        GameType GameType { get; }

        bool Activated { get; }

        void Activate();
        void Deactivate();
        void Update();
    }

    public enum GameType
    {
        FFX,
        FFX2,
        FFX2LM
    }
}
