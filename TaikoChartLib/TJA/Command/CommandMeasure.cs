using System;
using System.Collections.Generic;
using System.Text;
using TaikoChartLib.TJA.Status;

namespace TaikoChartLib.TJA.Command
{
    internal class CommandMeasure : TJACommand
    {
        public override QueueChip Process(string text, TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState)
        {
            string[] measureSplited = text.Split('/');
            TCLVector2 measure = new TCLVector2(4, 4);

            if (measureSplited.Length >= 1 && float.TryParse(measureSplited[0], out float x))
            {
                measure.X = x;
            }
            if (measureSplited.Length >= 2 && float.TryParse(measureSplited[1], out float y))
            {
                measure.Y = y;
            }

            QueueChip queueChip = new QueueChip()
            {
                ChipType = ChipType.Measure,
                Param = measure
            };
            return queueChip;
        }

        public override void PostProcess(QueueChip queueChip, TJATaikoChart taikoChart, ref ParseState state, ref ParseCourseState courseState, ref ParseChipsState chipsState)
        {
            chipsState.CurrentParams.Measure = (TCLVector2?)queueChip.Param ?? new TCLVector2(4.0f, 4.0f);
        }
    }
}
