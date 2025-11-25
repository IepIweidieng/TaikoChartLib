using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaikoChartLib.PlayableTests.Graphics;
using TaikoChartLib.Playing;

namespace TaikoChartLib.PlayableTests.Play
{
    internal class PlayChip
    {
        public PlayingChip PlayingChip { get; private set; }
#nullable enable
        public NoteSprite? Sprite { get; private set; }

        public PlayChip(PlayingChip playingChip, NoteSprite? sprite)
#nullable restore
        {
            PlayingChip = playingChip;
            Sprite = sprite;
        }
    }
}
