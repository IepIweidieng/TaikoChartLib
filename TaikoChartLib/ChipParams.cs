using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TaikoChartLib
{
    [Serializable]
    public class ChipParams : ICloneable
    {
        public double Time { get; set; } = 0.0;
        public float BPM { get; set; } = 150.0f;
        public Vector2 Scroll { get; set; } = new Vector2(1, 1);
        public Vector2 Measure { get; set; } = new Vector2(4, 4);
        public BranchType Branch { get; set; } = BranchType.Normal;

        public object Clone()
        {
            return new ChipParams()
            {
                Time = Time,
                BPM = BPM,
                Scroll = Scroll,
                Measure = Measure,
                Branch = Branch
            };
        }

        public ChipParams Copy()
        {
            return (ChipParams)Clone();
        }
    }
}
