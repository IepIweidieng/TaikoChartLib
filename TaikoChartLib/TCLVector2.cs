using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TaikoChartLib
{
    [Serializable]
    public struct TCLVector2
    {
        public static readonly Regex ComplexOperatorRegex = new Regex("(?<!^\\s*|[Ee])\\s*(?=[\\s+-])");
        public static readonly Regex ImaginaryUnitRegex = new Regex("^[+-]?i$");

        public float X { get; set; }
        public float Y { get; set; }

        public TCLVector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static bool TryParseComplex(string text, out TCLVector2 result)
        {
            string[] parts = ComplexOperatorRegex.Split(text.Trim(), 2); // a +b / a -b
            if (parts.Length == 1) // a / bi
            {
                parts = (parts[0].EndsWith("i")) ? new string[] { "0", parts[0] } : new string[] { parts[0], "0" };
            }
            if (float.TryParse(parts[0], out float re)) {
                if (ImaginaryUnitRegex.IsMatch(parts[1])) // -i / +i
                {
                    result = new TCLVector2(re, parts[1].StartsWith("-") ? -1 : 1);
                    return true;
                }
                if (float.TryParse(parts[1].TrimEnd('i'), out float im)) // bi
                {
                    result = new TCLVector2(re, im);
                    return true;
                }
            }
            result = new TCLVector2(1.0f, 0.0f);
            return false;
        }

        public static TCLVector2 ParseComplex(string text)
        {
            if (!TryParseComplex(text, out TCLVector2 result))
            {
                throw new FormatException("Input string was not in a correct format.");
            }
            return result;
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
