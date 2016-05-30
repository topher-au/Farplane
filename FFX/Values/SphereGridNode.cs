using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.FFX.Values
{
    public class SphereGridNode
    {
        public int ID { get; set; }
        public NodeType Type { get; set; }
        public byte ActivatedBy { get; set; }
    }

    public enum NodeType
    {
        
    }
}
