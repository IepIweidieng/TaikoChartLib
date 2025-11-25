using System;
using System.Collections.Generic;
using System.Text;

namespace TaikoChartLib.Playing
{
    public class PlayingChipRoll : PlayingChip
    {
        public TCLVector2 RollLength { get; internal set; }

        public PlayingChipRoll(Chip chip, int index) : base(chip, index)
        {
        }
    }
}
