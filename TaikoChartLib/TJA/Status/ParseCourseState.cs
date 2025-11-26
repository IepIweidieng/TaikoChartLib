using System;
using System.Collections.Generic;
using System.Text;

namespace TaikoChartLib.TJA.Status
{
    internal class ParseCourseState
    {
        public int ScoreInit;
        public int ScoreDiff;
        public bool HiddenBranch;
        public ChipsData ChipsData = new ChipsData();
    }
}
