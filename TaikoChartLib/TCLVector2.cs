using System;
using System.Collections.Generic;
using System.Text;

namespace TaikoChartLib
{
    [Serializable]
    public struct TCLVector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public TCLVector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static TCLVector2 operator +(TCLVector2 a, float b)
        {
            return new TCLVector2(a.X + b, a.Y + b);
        }
        public static TCLVector2 operator +(TCLVector2 a, TCLVector2 b)
        {
            return new TCLVector2(a.X + b.X, a.Y + b.Y);
        }

        public static TCLVector2 operator -(TCLVector2 a, float b)
        {
            return new TCLVector2(a.X - b, a.Y - b);
        }
        public static TCLVector2 operator -(TCLVector2 a, TCLVector2 b)
        {
            return new TCLVector2(a.X - b.X, a.Y - b.Y);
        }

        public static TCLVector2 operator *(TCLVector2 a, float b)
        {
            return new TCLVector2(a.X * b, a.Y * b);
        }
        public static TCLVector2 operator *(TCLVector2 a, TCLVector2 b)
        {
            return new TCLVector2(a.X * b.X, a.Y * b.Y);
        }

        public static TCLVector2 operator /(TCLVector2 a, float b)
        {
            return new TCLVector2(a.X / b, a.Y / b);
        }
        public static TCLVector2 operator /(TCLVector2 a, TCLVector2 b)
        {
            return new TCLVector2(a.X / b.X, a.Y / b.Y);
        }
    }
}
