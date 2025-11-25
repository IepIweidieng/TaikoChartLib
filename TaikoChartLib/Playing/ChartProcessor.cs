using System;
using System.Collections.Generic;
using System.Text;

namespace TaikoChartLib.Playing
{
    public abstract class ChartProcessor
    {
        public delegate void TickChip(ref int index, ref double time, ref Chip chip, ref TCLVector2 position);

        public EventHandler<ChipAddArgs> AddedChip { get; set; }
        public TickChip TickedChip { get; set; }

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
