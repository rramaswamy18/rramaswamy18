using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryDLL.ArchitectureLibraryEnumerations
{
    public enum CountEnum
    {
        [Description("Cone(s)")]
        Cones = 100,
        [Description("Cup(s)")]
        Cups = 200,
        [Description("Piece(s)")]
        Pieces = 300,
        [Description("Set(s)")]
        Sets = 400,
        [Description("Stem(s)")]
        Stems = 500,
        [Description("Stick(s)")]
        Sticks = 600,
    }
}
