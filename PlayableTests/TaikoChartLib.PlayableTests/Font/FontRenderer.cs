using Microsoft.Xna.Framework;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaikoChartLib.PlayableTests.Font
{
    internal class FontRenderer : IDisposable
    {
        public static FontRenderer FromFile(string path, float size)
        {
            SKTypeface typeface = SKTypeface.FromFile(path);
            return new FontRenderer(typeface, size);
        }

        public float Size => font.Size;

        private SKTypeface typeface;
        private SKFont font;
        private SKPaint paint;

        public FontRenderer(SKTypeface typeface, float size)
        {
            paint = new SKPaint()
            {
                IsAntialias = true
            };
            this.typeface = typeface;

            font = new SKFont()
            {
                Typeface = typeface,
                Size = size
            };
        }

        public void Dispose()
        {
            paint.Dispose();
            font.Dispose();
            typeface.Dispose();
        }

        public SKBitmap CreateBitmap(string text, Color color)
        {
            paint.Color = new SKColor(color.R, color.G, color.B, color.A);

            SKBitmap bitmap = new SKBitmap((int)Math.Ceiling(font.MeasureText(text)), (int)(font.Metrics.Descent - font.Metrics.Ascent));
            using SKCanvas canvas = new SKCanvas(bitmap);

            canvas.DrawText(text, 0, -font.Metrics.Ascent, font, paint);
            canvas.Flush();

            return bitmap;
        }
    }
}
