using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaikoChartLib.PlayableTests.Graphics
{
    internal class Sprite
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Color Color { get; set; } = Color.White;
        public float Rotation { get; set; } = 0.0f;
        public Vector2 Scaling { get; set; } = Vector2.One;
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
        public float DepthLayer { get; set; } = 0.0f;
        public Vector2 Origin { get; set; } = Vector2.Zero;

#nullable enable
        public TextureRegion? TextureRegion;
#nullable restore

        public Sprite(TextureRegion textureRegion)
        {
            this.TextureRegion = textureRegion;
            Origin = new Vector2(textureRegion.Width, textureRegion.Height) * 0.5f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureRegion, Position, Color, Rotation, Origin, Scaling, Effects, DepthLayer);
        }
    }
}
