using System;
using System.Collections.Generic;
using System.Text;

namespace TaikoChartLib.Playing
{
    public class ChipAddArgs : EventArgs
    {
        public int Index { get; private set; }
        public Chip Chip { get; private set; }

        public ChipAddArgs(int index, Chip chip)
        {
            Index = index;
            Chip = chip;
        }
    }
}
