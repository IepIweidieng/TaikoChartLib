using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaikoChartLib.PlayableTests.Font;

namespace TaikoChartLib.PlayableTests
{
    internal class TestLabel
    {
        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    CreateTexture(value, Color);
                }
                _text = value;
            }
        }

        private Color _color = Color.White;
        public Color Color
        {
            private get => _color;
            set
            {
                if (_color != value)
                {
                    CreateTexture(Text, value);
                }
                _color = value;
            }
        }

        public TestLabel(GraphicsDevice graphicsDevice, FontRenderer fontRenderer)
        {
            this.graphicsDevice = graphicsDevice;
            this.fontRenderer = fontRenderer;
            this.Texture2D = new Texture2D(graphicsDevice, 1, 1);
        }

        ~TestLabel()
        {
            Texture2D.Dispose();
        }

#nullable enable
        private GraphicsDevice graphicsDevice;
        private FontRenderer fontRenderer;
        public Texture2D Texture2D;
#nullable restore

        private void CreateTexture(string text, Color color)
        {
            Texture2D.Dispose();

            if (graphicsDevice is null || fontRenderer is null) return;

            using SKBitmap bitmap = fontRenderer.CreateBitmap(text, color);

            Texture2D texture = new Texture2D(graphicsDevice, bitmap.Width, bitmap.Height);

            byte[] pixels = bitmap.GetPixelSpan().ToArray();
            texture.SetData(pixels);

            Texture2D = texture;
        }
    }
}
