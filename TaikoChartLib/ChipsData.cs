using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TaikoChartLib
{
    [Serializable]
    public class ChipsData
    {
        public float InitBPM { get; set; } = 150.0f;
        public Vector2 InitScroll { get; set; } = new Vector2(4, 4);

        public List<Chip> Chips { get; set; } = new List<Chip>();
    }
}
