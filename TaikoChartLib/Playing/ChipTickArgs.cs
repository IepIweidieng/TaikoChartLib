using System;
using System.Collections.Generic;
using System.Text;

namespace TaikoChartLib.Playing
{
    public class ChipTickArgs : EventArgs
    {
        public int Index { get; private set; }
        public double Time { get; private set; }
        public Chip Chip { get; private set; }
        public TCLVector2 Position { get; private set; }

        public ChipTickArgs(int index, double time, Chip chip, TCLVector2 position)
        {
            Index = index;
            Time = time;
            Chip = chip;
            Position = position;
        }
    }
}
