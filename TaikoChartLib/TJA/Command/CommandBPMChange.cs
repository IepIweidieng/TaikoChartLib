using System;
using System.Collections.Generic;
using System.Text;
using TaikoChartLib.TJA.Status;

namespace TaikoChartLib.TJA.Command
{
    internal class CommandBPMChange : TJACommand
    {
        public override QueueChip Process(string text, TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState)
        {
            if (!float.TryParse(text, out float bpm))
            {
                bpm = 150.0f;
            }

            QueueChip queueChip = new QueueChip()
            {
                ChipType = ChipType.BPMChange,
                Param = bpm
            };
            return queueChip;
        }

        public override void PostProcess(QueueChip queueChip, TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState)
        {
            chipsState.CurrentParams.BPM = (float?)queueChip.Param ?? 150.0f;
        }
    }
}
