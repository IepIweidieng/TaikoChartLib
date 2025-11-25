using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaikoChartLib.PlayableTests.Graphics;

namespace TaikoChartLib.PlayableTests.Play
{
    internal class PlayChip
    {
        public Chip Chip { get; private set; }
#nullable enable
        public Sprite? Sprite { get; private set; }

        public PlayChip(Chip chip, Sprite? sprite)
#nullable restore
        {
            Chip = chip;
            Sprite = sprite;
        }
    }
}
