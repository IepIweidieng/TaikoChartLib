using System;
using System.Collections.Generic;
using System.Text;
using TaikoChartLib.TJA.Status;

namespace TaikoChartLib.TJA.Command
{
    internal class CommandGoGoStart : TJACommand
    {
        public override QueueChip Process(string text, TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState)
        {
            QueueChip queueChip = new QueueChip()
            {
                ChipType = ChipType.GoGoStart
            };
            return queueChip;
        }

        public override void PostProcess(QueueChip queueChip, TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState)
        {
        }
    }
}
