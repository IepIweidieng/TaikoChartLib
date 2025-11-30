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
            TCLVector2 scroll = new TCLVector2(1.0f, 0.0f);
            if (float.TryParse(text, out float scroll_))
            {
                scroll.X = scroll_;
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
