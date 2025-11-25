using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaikoChartLib.PlayableTests.Graphics
{
    internal class TextureRegion
    {
        public Texture2D Texture2D { get; private set; }
        public Rectangle SourceRect { get; private set; }
        public int Width => SourceRect.Width;
        public int Height => SourceRect.Height;

        public TextureRegion(Texture2D texture2D, Rectangle sourceRect)
        {
            Texture2D = texture2D;
            SourceRect = sourceRect;
        }
    }
}
