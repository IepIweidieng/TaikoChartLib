using System;
using System.Collections.Generic;
using System.Text;

namespace TaikoChartLib.Playing
{
    internal class BigNoteQueue
    {
        public TimeSpan Time { get; set; }
        public PlayingChip PlayingChip { get; private set; }
        public double HitTime { get; private set; }

        public BigNoteQueue(PlayingChip playingChip)
        {
            PlayingChip = playingChip;
            HitTime = playingChip.Time;
        }
    }
}
