using System;
using System.Collections.Generic;
using System.Text;
using TaikoChartLib.Playing;

namespace TaikoChartLib.TJA
{
    public class TJAChartProcessor : ChartProcessor
    {
        private List<PlayingChip> playingChips = new List<PlayingChip>();

        public TJAChartProcessor()
        {
        }


        public override void AddChip(int index, Chip chip)
        {
            PlayingChip playingChip;
            if (Chip.IsRoll(chip.ChipType))
            {
                playingChip = new PlayingChipRoll(chip, index);
            }
            else
            {
                playingChip = new PlayingChip(chip, index);
            }

            playingChips.Add(playingChip);

            AddedChip?.Invoke(playingChip);
        }

        public override void Tick(double time)
        {
            PlayingChipRoll prevRoll = null;

            for (int i = 0; i < playingChips.Count; i++)
            {
                PlayingChip playingChip = playingChips[i];
                Chip chip = playingChip.Chip;

                playingChip.Time = chip.Params.Time - time;
                playingChip.Position = chip.Params.Scroll * (chip.Params.BPM * (float)playingChip.Time);

                if (playingChip is PlayingChipRoll playingChipRoll)
                {
                    prevRoll = playingChipRoll;
                }
                else if (chip.ChipType == ChipType.RollEnd)
                {
                    prevRoll.RollLength = playingChip.Position - prevRoll.Position;
                    prevRoll = null;
                }

                if (chip.ChipType != ChipType.None)
                {
                    TickedChip(playingChip);
                }
            }
        }
    }
}
