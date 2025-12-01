using System;
using System.Collections.Generic;
using System.Text;

namespace TaikoChartLib.TJA.Status
{
    internal class ParseState
    {
        public Course Course = new Course();
        public float BPM = 120.0f;
        public TCLVector2 HeadScroll = new TCLVector2(1.0f, 0.0f);
        public bool Loading;
    }
}
