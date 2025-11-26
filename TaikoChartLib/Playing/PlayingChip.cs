using System;
using System.Collections.Generic;
using System.Text;

namespace TaikoChartLib.Playing
{
    public class PlayingChip
    {
        public Chip Chip { get; private set; }
        public int Index { get; private set; }

        public double Time { get; internal set; }
        public TCLVector2 Position { get; internal set; }

        public bool IsOver { get; internal set; }
        public bool IsHit { get; internal set; }
        public bool IsMiss { get; internal set; }

        public PlayingChip(Chip chip, int index)
        {
            Chip = chip;
            Index = index;
        }
    }
}
