using System;
using System.Collections.Generic;
using System.Text;
using TaikoChartLib.Playing;

namespace TaikoChartLib.TJA
{
    public class TJAChartProcessor : ChartProcessor
    {

        public TJAChartProcessor()
        {
        }


        public override void AddChip(int index, Chip chip)
        {
            AddedChip?.Invoke(this, new ChipAddArgs(index, chip));
        }

        public override void Tick(double time)
        {
            for (int i = 0; i < ChipsData.Chips.Count; i++)
            {
                Chip chip = ChipsData.Chips[i];

                double chipTime = chip.Params.Time - time;
                TCLVector2 position = chip.Params.Scroll * (chip.Params.BPM * (float)chipTime);

                if (chip.ChipType != ChipType.None)
                {
                    TickedChip(ref i, ref time, ref chip, ref position);
                }
            }
        }
    }
}
