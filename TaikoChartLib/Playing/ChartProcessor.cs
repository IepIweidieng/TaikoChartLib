using System;
using System.Collections.Generic;
using System.Text;

namespace TaikoChartLib.Playing
{
    public delegate void AddedChip(PlayingChip playingChip);
    public delegate void TickedChip(PlayingChip playingChip);

    public abstract class ChartProcessor
    {
        public AddedChip AddedChip { get; set; }
        public TickedChip TickedChip { get; set; }

        private ChipsData _chipsData;
        public ChipsData ChipsData
        {
            protected get => _chipsData;
            set
            {
                if (_chipsData != value)
                {
                    for (int i = 0; i < value.Chips.Count; i++)
                    {
                        AddChip(i, value.Chips[i]);
                    }
                }
                _chipsData = value;
            }
        }

        public ChartProcessor()
        {

        }

        public abstract void AddChip(int index, Chip chip);
        public abstract void Tick(double time);
    }
}
