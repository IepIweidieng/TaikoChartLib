using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaikoChartLib.PlayableTests.Graphics;

namespace TaikoChartLib.PlayableTests.Play
{
    internal class NoteSprite
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Color Color { get; set; } = Color.White;
        public float Rotation { get; set; } = 0.0f;
        public Vector2 Scaling { get; set; } = Vector2.One;

        private Sprite faceSprite;

        public NoteSprite(TextureRegion face)
        {
            faceSprite = new Sprite(face);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            faceSprite.Position = Position;
            faceSprite.Color = Color;
            faceSprite.Rotation = Rotation;
            faceSprite.Scaling = Scaling;


            faceSprite.Draw(spriteBatch);
        }
    }
}
