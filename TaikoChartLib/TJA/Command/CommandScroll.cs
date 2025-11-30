using System;
using System.Collections.Generic;
using System.Text;
using TaikoChartLib.TJA.Status;

namespace TaikoChartLib.TJA.Command
{
    internal class CommandScroll : TJACommand
    {
        public override QueueChip Process(string text, TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState)
        {
            if (!TCLVector2.TryParseComplex(text, out TCLVector2 scroll)) {
                scroll = new TCLVector2(1.0f, 0.0f);
            }

            QueueChip queueChip = new QueueChip()
            {
                ChipType = ChipType.Scroll,
                Param = scroll
            };
            return queueChip;
        }

        public override void PostProcess(QueueChip queueChip, TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState)
        {
            chipsState.CurrentParams.Scroll = (TCLVector2?)queueChip.Param ?? new TCLVector2(1.0f, 0.0f);
        }
    }
}
