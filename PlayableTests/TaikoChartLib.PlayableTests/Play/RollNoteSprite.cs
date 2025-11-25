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
    internal class RollNoteSprite : NoteSprite
    {
        public float Length { get; set; }

        private Sprite bodySprite;
        private Sprite tailSprite;

        public RollNoteSprite(TextureRegion face, TextureRegion body, TextureRegion tail) : base(face)
        {
            bodySprite = new Sprite(body)
            {
                Origin = new Vector2(0, body.Height * 0.5f)
            };
            tailSprite = new Sprite(tail)
            {
                Origin = new Vector2(tail.Width * 0.5f, tail.Height * 0.5f)
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 offset = new Vector2(Length, 0);
            offset.Rotate(Rotation);

            tailSprite.Position = Position + offset;
            tailSprite.Color = Color;
            tailSprite.Rotation = Rotation;
            tailSprite.Scaling = Scaling;

            tailSprite.Draw(spriteBatch);

            bodySprite.Position = Position;
            bodySprite.Color = Color;
            bodySprite.Rotation = Rotation;
            bodySprite.Scaling = Scaling * new Vector2(Length / bodySprite.TextureRegion.Width, 1.0f);

            bodySprite.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}
