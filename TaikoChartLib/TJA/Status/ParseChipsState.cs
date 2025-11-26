using System;
using System.Collections.Generic;
using System.Text;

namespace TaikoChartLib.TJA.Status
{
    internal class ParseChipsState
    {
        public ChipParams CurrentParams = new ChipParams();
        public ChipParams PreviousBranchParams;
        public List<QueueChip> ChipQueues = new List<QueueChip>();
    }
}
