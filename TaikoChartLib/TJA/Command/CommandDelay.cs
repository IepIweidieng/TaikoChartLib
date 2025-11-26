using System;
using System.Collections.Generic;
using System.Text;
using TaikoChartLib.TJA.Status;

namespace TaikoChartLib.TJA.Command
{
    internal class CommandDelay : TJACommand
    {
        public override QueueChip Process(string text, TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState)
        {
            if (!float.TryParse(text, out float delay))
            {
                delay = 0.0f;
            }

            QueueChip queueChip = new QueueChip()
            {
                ChipType = ChipType.Delay,
                Param = delay
            };
            return queueChip;
        }

        public override void PostProcess(QueueChip queueChip, TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState)
        {
            chipsState.CurrentParams.Time += (float?)queueChip.Param ?? 0;
        }
    }
}
