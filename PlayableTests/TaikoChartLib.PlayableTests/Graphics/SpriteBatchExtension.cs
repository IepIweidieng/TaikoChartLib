using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaikoChartLib.PlayableTests.Graphics
{
    internal static class SpriteBatchExtension
    {
        public static void Draw(this SpriteBatch spriteBatch, TextureRegion texture, Vector2 position, Color color)
        {
            spriteBatch.Draw(texture.Texture2D, position, texture.SourceRect, color);
        }

        public static void Draw(this SpriteBatch spriteBatch, TextureRegion texture, Rectangle destinationRectangle, Color color)
        {
            spriteBatch.Draw(texture.Texture2D, destinationRectangle, texture.SourceRect, color);
        }

        public static void Draw(this SpriteBatch spriteBatch, TextureRegion texture, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            spriteBatch.Draw(texture.Texture2D, position, texture.SourceRect, color, rotation, origin, scale, effects, layerDepth);
        }
        
        public static void Draw(this SpriteBatch spriteBatch, TextureRegion texture, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            spriteBatch.Draw(texture.Texture2D, position, texture.SourceRect, color, rotation, origin, scale, effects, layerDepth);
        }

        public static void Draw(this SpriteBatch spriteBatch, TextureRegion texture, Rectangle destinationRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            spriteBatch.Draw(texture.Texture2D, destinationRectangle, texture.SourceRect, color, rotation, origin, effects, layerDepth);
        }
    }
}
